# P_GUN Project Architecture

## Current Shape

P_GUN is a Unity 2022.3 2D top-down shooter. The main runtime code lives in `Assets/Scripts` and is split with asmdefs such as `Game.Core`, `Game.Gameplay`, `Game.Items`, `Game.ItemEffects`, `Game.Pooling`, `Game.UI`, `Game.Animation`, and `Game.Presentation`.

Important external packages and conventions:

- QFramework is used for `ViewController`, FSM, UI tooling, and utility extensions.
- DOTween is used for runtime animation feedback.
- Addressables are used for loading hot-update databases, room data, item/enemy spawn tables, item prefabs, enemy prefabs, and weapon prefabs.
- xLua is used by the Buff system.
- `Assets/UnityEasyWorkTools` is the shared editor tooling suite, currently containing visual animation sequences, UI auto binding, and table import tools.
- `Assets/UnityEasyWorkTools/UnityEasyWorkToolsPathSettings.asset` stores editable default paths for UnityEasyWorkTools. Open it through `Tools/UnityEasyWorkTools/Settings/Open Path Settings`.
- UnityEasyWorkTools editor UI uses UI Toolkit. `.uxml` and `.uss` files are kept in each module's `Editor/UI` folder; C# editor scripts bind serialized data and implement commands.

## Core Module

Location: `Assets/Scripts/Core`

`ScriptableObjectDatabase<TDatabase, TKey, TValue>` is the shared base for data tables. Subclasses provide `DataValues` and `TryGetKey`, then query with `TryGetById(TKey id, out TValue data)`. Importers should update data through subclass replace methods so the runtime index rebuilds.

Known subclasses:

- `Game.Items.ItemDatabase`: key `int itemId`, value `ItemData`.
- `Game.Gameplay.BuffDataBase`: key `int Id`, value `Buff`.
- `Game.Gameplay.WeaponDatabase`: key `string WeaponId`, value `WeaponData`.
- `Game.Gameplay.EnemyDatabase`: key `int enemyId`, value `EnemyData`.

`EventCenter` is the simple static event bus. Use `AddListener`, `RemoveListener`, and `Trigger` with `GameEvent`. Payload events are generic, so listener payload types must match trigger payload types.

Current `GameEvent` includes player health/death/game events, player Buff changes, minimap show/hide/toggle, item tip show/hide, item picked, bullet clip/bag changes, door open/close, and room generation completion.

## Database Loading

Location: `Assets/Scripts/Gameplay/Managers/DataBaseManager.cs`

`DataBaseManager` is a persistent singleton that lives in the `Root` scene and loads global databases through Addressables:

- `ItemDatabase`
- `WeaponDatabase`
- `BuffDataBase`
- `EnemyDatabase`

Call `LoadAllAsync()` from `RootHotUpdateController` before entering gameplay. Runtime systems typically prefer explicitly assigned database references, then fall back to `DataBaseManager.Instance`; `Item` can also use `ItemDatabase.RuntimeDatabase` after `DataBaseManager` finishes loading.

## Pooling

Location: `Assets/Scripts/Pooling`

`IPoolable` declares:

```csharp
void OnSpawnFromPool();
void OnRecycleToPool();
```

`PoolBase<T>` manages multiple prefab-specific `ObjectPool<T>` instances for one component type. It maps instances back to their source pool, supports `Get()`, `Get(prefab)`, `Get(prefab, position, rotation)`, `Release(item)`, and `Prewarm(prefab, count)`.

Rules:

- Any pooled component must reset all runtime state in `OnSpawnFromPool` and `OnRecycleToPool`.
- Do not assume `Start()` is called again after reuse.
- If a component has tweens, coroutines, event subscriptions, room ownership, collider state, or animator triggers, clear them during recycle/spawn.

Known pools:

- `PlayerBulletPool`
- `EnemyBulletPool`
- `EnemyPool`
- `ItemPool`
- `VfxPool`

## Items

Locations:

- `Assets/Scripts/Items`
- `Assets/Scripts/ItemEffects`

`ItemData` is pure display data:

- `int itemId`
- `string itemName`
- `string description`
- `Sprite icon`

`ItemDatabase` stores all `ItemData` and queries by `itemId`.

`ItemSpawnTableSO` stores weighted prefab entries. Entries may use `itemId`, Addressables address, or a legacy direct prefab reference. Runtime spawning should resolve the table from `AddressableRuntimeContent` first, then resolve prefabs through `TryResolvePrefab` or `TryGetRandomPrefab`, because these APIs prefer hot-update prefabs before falling back to legacy references. It does not own display data.

`Item` is the runtime pickup component. It:

- Requires `Collider2D`.
- Uses `itemId` plus an optional `ItemDatabase` to resolve display data.
- Shows/hides item tips through `EventCenter.Trigger(GameEvent.ItemTipShown, ItemData)` and `GameEvent.ItemTipHidden`.
- Picks up on `F` while the player is in range.
- Triggers `GameEvent.ItemPicked`.
- Plays Animator trigger `OnPickup` if available.
- Falls back to `GameDOTweenAnimation.Play(callback)` if no pickup trigger exists.
- Adds itself to the player's `PlayerInventory`; item effects are executed later from the inventory UI.
- Plays optional pickup audio through `GlobalAudioPlay`.
- Releases itself through `ItemPool.Instance.Release(this)` when `isDestroy` is true.
- Implements `IPoolable` and resets pickup state, animator state, tweens, and tips during pooling lifecycle.

`ItemEffectBase` is a ScriptableObject effect API:

```csharp
public abstract void OnPick(ItemEffectContext ctx);
```

`ItemEffectBase.CanUse(ItemEffectContext ctx)` defaults to `true` and lets inventory use checks block consumption when an effect cannot currently work, such as full HP healing or cleansing without negative Buffs.

`ItemEffectContext` currently carries:

- `GameObject SourceObject`
- `Vector3 WorldPosition`

Known item effects include healing, applying buffs, cleansing negative buffs, chest random loot, and spawning prefabs at fight-room end.

`PlayerInventory` lives on the player prefab and stores runtime `InventoryItemStack` data by `itemId`. Stacks preserve display data and effect assets, but do not keep references to pooled pickup GameObjects. `AddFromItem(Item item)` stacks pickups and triggers `GameEvent.InventoryChanged`; `Use(int itemId)` executes all currently usable effects and consumes one item only when at least one effect can be used.

## Save System

Locations:

- `Assets/Scripts/Gameplay/Save`
- `Assets/Scripts/UI/Save`
- `Assets/Prefab/UI/Save`

The v1 save system is a JSON safe-point framework with 3 fixed slots:

- Slot files: `Application.persistentDataPath/Saves/slot_1.json` through `slot_3.json`.
- `SaveGameService` is the UI-facing API for `SaveToSlot`, `LoadFromSlot`, `DeleteSlot`, and `GetSlotSummaries`.
- `SaveSlotStorage` owns JSON file path, read, write, delete, and summary extraction only.
- `GameSaveData` stores version, saved time, scene name, LevelGraph address, map seed, current room id, player snapshot, and room snapshot list.
- `LoadFromSlot` reads JSON, stores it as pending data, reloads `GameScene`, injects the saved LevelGraph address and Edgar seed before generation, then restores room progress and player state after generated rooms initialize.

Rules:

- Safe-point saves are only allowed in `GameScene`.
- Saving fails while `FightRoom.currentFightRoom` is not null.
- Ground drop items are not saved in v1; only inventory stacks are saved.
- Loading a slot always reloads `GameScene` to clear unsaved enemies, bullets, and ground drops before restoration.
- The system uses ordinary static services for file IO and must not dynamically create manager GameObjects.
- `SaveSlotPanel` opens in main-menu mode for load/delete and safe-house mode for save/load/delete.
- `GameSceneUIInputController` uses `F5` as the temporary safe-house test shortcut until a real safe-house interaction is defined.

## Buffs

Locations:

- `Assets/Scripts/Gameplay/Buffs`
- `Assets/Scripts/Gameplay/Managers/LuaManager.cs`

`Buff` is serializable config stored inside `BuffDataBase`, not a standalone ScriptableObject. It contains id, display name, icon, description, positive/negative tag, Lua file, duration, permanence, interval, and stat modifiers.

Regular stat changes are data-driven through `StatModifier`:

- `StatType`: `MoveSpeed`, `Attack`, `Defense`, `MaxHp`.
- `ModifierType`: `Flat`, `PercentAdd`, `FinalMul`.
- Formula: `Final = (Base + FlatSum) * (1 + PercentAddSum) * FinalMulProduct`.
- `MoveSpeed`, `Attack`, and `MaxHp` are currently wired into `Player`; `Defense` is reserved and does not affect `Player.Hurt()` until a damage reduction rule is defined.
- Buff CSV `modifiers` format is `StatType:ModifierType:Value;StatType:ModifierType:Value`, for example `MoveSpeed:PercentAdd:0.3`.
- Lua Buff scripts should handle special lifecycle behavior only; regular speed, attack, and max HP changes should stay in `Buff.Modifiers`.

`BuffManager` lives on the player and manages runtime buff instances. Important APIs:

- `AddBuffById(int buffId)`
- `AddBuffById(int buffId, UnityEngine.Object source)`
- `AddBuff(Buff buff)`
- `AddBuff(Buff buff, UnityEngine.Object source)`
- `RemoveBuff(Buff buff)`
- `RemoveBuffById(int buffId)`
- `TriggerBuffById(int buffId)`
- `ClearBuffs()`
- `RemoveBuffsByTag(BuffTag tag)`
- `CalculateStat(StatType statType, float baseValue)`
- `ActiveBuffs`

If a non-permanent buff already exists, adding it resets duration and triggers `OnAdd` again. If a permanent buff already exists, adding it increments `BuffRuntimeInfo.StackCount`; stat modifiers scale by stack count, with `FinalMul` repeated once per stack. Add, remove, and clear operations trigger `GameEvent.PlayerBuffsChanged` for UI refresh.

`BuffTag` has `Positive` and `Negative`. Buff CSV uses a `tag` column with those enum names. Purge/cleanse item effects should call `RemoveBuffsByTag(BuffTag.Negative)` instead of scanning UI state.

`LuaBuffInstance` caches optional Lua methods:

- `OnAdd(BuffRuntimeInfo info)`
- `OnRemove(BuffRuntimeInfo info)`
- `OnUpdate(BuffRuntimeInfo info, float deltaTime)`
- `OnInterval(BuffRuntimeInfo info)`
- `OnTrigger(BuffRuntimeInfo info)`

Lua failures are caught and logged, so C# callers should still validate missing or invalid Lua assets early where possible.

## Player And Weapons

Locations:

- `Assets/Scripts/Gameplay/Entity/Player`
- `Assets/Scripts/Gameplay/Entity/Player/Weapon`

`Player` is a QFramework `ViewController`. It owns movement, sleep animation, health, hurt feedback, gun switching, auto aim, and player-global registration via `Global.player`.

Important `Player` APIs:

- `Hurt()` and `Hurt(DamageInfo damageInfo)`
- `Restart()`
- `Heal(int amount)`
- `ShowDisPlayer(string text, float duration)`
- `AutoAim(ref Vector2 dir)`
- `CurrentMoveSpeed`
- `GetSpeed()`
- `AddSpeedByValue(float value)`
- `SetSpeed(float value)`
- `CalculateBulletDamage(int baseDamage)`

`Gun` is the abstract weapon base. It loads `WeaponData` by `WeaponId` from a serialized database or `DataBaseManager.Instance.Weapons`. Empty `weaponId` defaults to the class name.

Important `Gun` concepts:

- `BulletPrefab` is abstract and provided by concrete weapons.
- `ApplyData(WeaponData data)` copies shoot sounds, reload sound, damage, clip, interval, and speed.
- `ShootDown`, `ShootUp`, `Shooting`, `Reload`, and `OnGunUsed` are the main interaction lifecycle.
- `GetBullet(Vector2 dir)` gets bullets through `PlayerBulletPool.Instance`.
- Audio uses static `Gun.PlayerAudioSource`.

Known concrete weapons include AK, AWP, Bow, Laser, MP5, Pistol, RocketGun, and ShotGun.

At runtime, the `Player` first checks `AddressableRuntimeContent` for the fixed weapon address order `weapon/pistol`, `weapon/ak`, `weapon/awp`, `weapon/bow`, `weapon/laser`, `weapon/mp5`, `weapon/rocket_gun`, `weapon/shotgun`. When the Root preload is ready, those prefabs are instantiated under the player `Weapon` node and replace the player prefab's serialized guns list. Direct GameScene play still uses the serialized fallback guns.

## Enemies And Rooms

Locations:

- `Assets/Scripts/Gameplay/Entity/Enemy`
- `Assets/Scripts/Gameplay/Room`

`EnemyBase` is the common enemy superclass and implements `IPoolable`. Subclasses must implement:

```csharp
protected abstract void OnInit();
protected abstract WeaponType WeaponType { get; }
protected abstract void RegisterFSM(FSM<EnemyState> fsm);
```

Important behavior:

- `Init()` runs once per spawn and registers the FSM.
- `OnSpawnFromPool()` resets runtime state and calls `Init()`.
- `OnRecycleToPool()` clears movement, room reference, visual state, and death coroutine.
- `ApplyConfig(EnemyData enemyData)` applies database values for HP, speed, damage, and item drop chance.
- `Hurt(DamageInfo damageInfo)` applies damage, blood VFX, hurt animation, and death check.
- `Dead()` stops movement, disables collider, plays death animation, triggers `OnDead`, then recycles after delay.
- `OnDead()` notifies the owning fight room and attempts item drop through `ItemSpawner`.

Rooms track fight progression. `FightRoom.NotifyEnemyDefeated(this)` is part of enemy death cleanup. When editing enemies, preserve room ownership cleanup to avoid stale fight-room state after pooling.

`GameScene` uses `AddressableDungeonBootstrapper` on the Edgar `DungeonGeneratorGrid2D` owner. The generator should stay `GenerateOn = Manually`; the bootstrapper loads `room/level1` from `AddressableRuntimeContent`, assigns `FixedLevelGraphConfig.LevelGraph`, then calls `Generate()`. Direct GameScene play can fall back to the inspector-assigned level graph.

`NormalRoom` resolves `EnemySpawnTableSO` from `AddressableRuntimeContent` by Addressables address first. Direct GameScene play without Root content may use the inspector-assigned table for local testing.

## Animation And Presentation

Locations:

- `Assets/Scripts/DOTween`
- `Assets/Scripts/Presentation`

`AnimEffectSO` is the ScriptableObject base for DOTween-driven effects. Current concrete assets/scripts include blink, jump, shake, hurted, and scale 0 to 1.

`DOTweenAnimMgr` maps effect assets and supports playing by key/string or direct SO. Prefer this for reusable feedback instead of duplicating tween sequences in many classes.

`GameDOTweenAnimation` is a component wrapper for object-specific DOTween animation playback and callback flow.

Presentation classes include:

- `BloodVfx`
- `VfxPool`
- `GlobalAudioPlay`
- `DamageText`
- `DamageTextPool`

`DamageText` is the world-space damage-number presentation component. It requires `TextMeshPro`, implements `IPoolable`, randomizes local offset and font size per play, then uses DOTween for rise, scale, and fade animation. It invokes `OnComplete` when playback ends.

`DamageTextPool` is a `PoolBase<DamageText>` wrapper. Add it to the gameplay scene through Unity scene setup, assign the `DamageText` prefab in `prefabInfos`, and configure active/inactive roots like other pools. Gameplay callers should pass final damage and world position to `DamageTextPool.Play`; they should not instantiate damage text directly.

Visual animation sequences live in `Assets/UnityEasyWorkTools/AnimationSequence` under `Game.Animation`:

- Runtime scripts and `Game.AnimationSequence.asmdef`: `Assets/UnityEasyWorkTools/AnimationSequence/Scripts`.
- Editor scripts and `Game.AnimationSequence.Editor.asmdef`: `Assets/UnityEasyWorkTools/AnimationSequence/Editor`.
- Editor UI resources: `Assets/UnityEasyWorkTools/AnimationSequence/Editor/UI`.

- `AnimationSequenceAsset`: ScriptableObject containing ordered `AnimationStepData` entries.
- `AnimationStepData`: target reference/path, startup active state, `AnimationEffectType`, duration, delay, Ease, and effect parameters.
- `AnimationPlayer`: scene MonoBehaviour that references a sequence asset, appends each step into one DOTween `Sequence`, and exposes an Inspector `UnityEvent` after all steps complete.
- `AnimationTweenFactory`: the only runtime class that builds concrete Tween logic for Fade, Slide, Shake, Scale, Move, and Rotate effects.
- `AnimationSequenceEditorWindow`: editor-only data window opened from `Tools/UnityEasyWorkTools/Animation Sequence Editor`.
- `UnityEasyWorkToolsPathSettings`: shared editor SO that controls animation asset output folder, UI auto-bind setting path, table importer UI paths, code-generation folders, and project table default asset paths.

Rules:

- Keep concrete animation behavior out of EditorWindow code.
- Prefer assigning `AnimationPlayer.bindingRoot` when sequence steps target scene objects, because project assets cannot reliably persist scene object references.
- `AnimationStartupActiveState` is applied once by `AnimationPlayer` during startup or first enable; each step sets its resolved target `activeSelf=true` when that step starts.
- Fade effects require `CanvasGroup`; steps may auto-add it when configured.
- RectTransform position effects use `anchoredPosition3D` first, normal Transform targets use `localPosition`.
- `restoreOnComplete` restores captured position, scale, rotation, and existing CanvasGroup alpha after playback.

## UI

Locations:

- `Assets/Scripts/UI`
- `Assets/UnityEasyWorkTools/UIAutoBind`

UI uses `Game.UI` namespace. The current stack includes:

- `GameUI`: main game UI controller.
- `UIStackManager` and `UIStackInitializer`: stack-based panel flow.
- `UIPanelBase`: panel base behavior.
- `ItemTipPanel`: item hover/pickup tip.
- `HpSlider`: HP display element.
- `MainPanel` and generated `MainPanel.BindComponent`.

The item UI flow is event-driven:

- `Item` triggers `GameEvent.ItemTipShown` with `ItemData`.
- `Item` triggers `GameEvent.ItemTipHidden`.
- UI listens and updates tip visibility/content.

Buff status UI lives in `Assets/Scripts/UI/Buffs`:

- `BuffStatusPanel` listens for `GameEvent.PlayerBuffsChanged`, reads `Global.player.buffManager.ActiveBuffs`, and creates one status icon per active Buff.
- `BuffStatusIcon` displays `Buff.Icon` and either remaining seconds for non-permanent Buffs or `StackCount` for permanent Buffs.
- `BuffTooltipPanel` shows `Buff.BuffName` and `Buff.Description` on hover.

Buff debug UI lives in `Assets/Scripts/UI/GameSceneUIInputController.cs`:

- `BuffDebugWindow` is an IMGUI runtime debug helper for adding, removing, and clearing player Buffs.
- `GameSceneUIInputController` toggles the window with `Alt+Up` and reports the debug panel state to `GameplayCursorState`.
- The debug window reads a serialized `BuffDataBase` when provided, otherwise uses `DataBaseManager.Instance.Buffs`, and applies Buffs through `Global.player`'s `BuffManager`.

Game scene UI stack rules:

- `GameScene` has an explicit scene `UIStackManager` and `UIStackInitializer`.
- `HudPanel` is the stack bottom and contains existing HP, bullet, minimap, item tip, and Buff status UI.
- Win, over, and game settings panels are `UIPanelBase` panels opened with `UIStackManager.Push()`.
- `GameSceneUIInputController` belongs to the scene `GameUI` object, opens the settings panel with Esc, opens the inventory panel with CapsLock, pauses by preserving/restoring `Time.timeScale`, and updates `GameplayCursorState`.
- `GameplayCursorState` is in `Game.Core`; Player must check `BlocksMouseCombat` before mouse aiming, auto-aim display, and mouse shooting.
- `InventoryPanel` and `InventorySlotView` live in `Assets/Scripts/UI/Inventory`; their prefabs live in `Assets/Prefab/UI/Inventory`. The panel listens to `GameEvent.InventoryChanged`, reads `Global.player.GetComponent<PlayerInventory>()`, displays one slot per item stack, shows the selected item description on the right, and pops a top hint when a right-click use is blocked.

When adding UI, use existing event flow before adding direct scene references.

UI auto binding tooling lives in `Assets/UnityEasyWorkTools/UIAutoBind`:

- Runtime scripts and `ComponentAutoBindTool.Runtime.asmdef`: `Assets/UnityEasyWorkTools/UIAutoBind/Scripts`.
- Editor scripts and `ComponentAutoBindTool.Editor.asmdef`: `Assets/UnityEasyWorkTools/UIAutoBind/Editor`.
- Editor UI resources: `Assets/UnityEasyWorkTools/UIAutoBind/Editor/UI`.
- `AutoBindGlobalSetting.asset`: `Assets/UnityEasyWorkTools/UIAutoBind/AutoBindGlobalSetting.asset`.

Rules:

- `UIPanelBase` roots should include `ComponentAutoBindTool`, even for simple stack panels such as HUD, win, and over panels.
- Bindable UGUI children should follow UIAutoBind prefixes, for example `Btn_Reset`, `Txt_Title`, `Img_DetailIcon`, `Trans_SlotRoot`, and `Rect_DisplayPage`.
- Keep the asmdef names `ComponentAutoBindTool.Runtime` and `ComponentAutoBindTool.Editor`, because generated UI code and `Game.UI.asmdef` reference them by assembly name.
- Preserve script `.meta` files when moving or reorganizing this tooling, because scene/prefab `ComponentAutoBindTool` component references depend on script GUIDs.
- Generated `*.BindComponent.cs` files may stay in the owning UI panel folder, such as `Assets/Scripts/UI/Panel`.

## Excel2SO

Locations:

- Core editor tooling: `Assets/UnityEasyWorkTools/TableImporter/Editor`
- Project importers: `Assets/UnityEasyWorkTools/TableImporter/Importers`
- Editor UI resources: `Assets/UnityEasyWorkTools/TableImporter/Editor/UI`

TableImporter converts `.xlsx` and `.csv` table data into ScriptableObject assets in the Unity Editor. Open via `Tools/UnityEasyWorkTools/Table Importer/Importer Window`.

Typical importer flow:

1. Create a project importer under `Assets/UnityEasyWorkTools/TableImporter/Importers`.
2. Inherit `Excel2SoListAssetImporter<TAsset>` for database/list assets, or `ExcelTableImporterBase` for custom table behavior.
3. Override `Configure(Excel2SoMapping map)`.
4. Map columns to serialized fields.
5. Import table to target asset.

Existing importers cover item, item spawn table, weapon, enemy, buff, and Addressables labels.

When adding a new data-driven system, prefer generating or importing strongly typed SO data rather than parsing CSV at runtime.

## Addressables And Assets

Addressable database keys are currently string fields in `DataBaseManager`:

- `"ItemDatabase"`
- `"WeaponDatabase"`
- `"BuffDataBase"`
- `"EnemyDatabase"`
- `"item/spawn_table/normal_room"`
- `"enemy/spawn_table/normal_room"`

`Assets/Editor/AddressablesLocalGroupSetup.cs` and `Assets/UnityEasyWorkTools/TableImporter/Importers/AddressablesLabelTableImporter.cs` support editor-side Addressables setup/import. Preserve labels and database keys when moving assets.

`Assets/Editor/AddressablesRemoteUploader.cs` adds `PG/Addressables/一键保存上传`. It saves open scenes/assets, configures generated `Content Update*` groups for remote build/load paths, builds content update bundles from `Assets/AddressableAssetsData/Windows/addressables_content_state.bin`, refreshes catalog hash files, validates the catalog, and uploads every file under `ServerData/P_GUN/StandaloneWindows64` to `/www/wwwroot/39.97.56.180/AB/P_GUN/StandaloneWindows64` through system `ssh/scp`, without deleting old server bundles. The menu expects local SSH key or ssh-agent authentication; do not hardcode server passwords into Unity editor scripts.

Current hot-update Addressables groups are:

- `Room`: `Level1.asset`, room templates, and corridor prefabs.
- `Buff`: `BuffDataBase` and Buff Lua text assets.
- `Item`: `ItemDatabase`, item spawn tables, and item prefabs.
- `Enemy`: `EnemyDatabase`, enemy spawn tables, and enemy prefabs.
- `Weapon`: `WeaponDatabase` and weapon prefabs.

These groups are Local-first hot-update groups. Their `BuildPath` and `LoadPath` use `Local.BuildPath` and `Local.LoadPath`, so the first player build includes the bundles in the package. The project still builds a remote catalog, with `Remote.BuildPath = ServerData/P_GUN/[BuildTarget]` and `Remote.LoadPath = https://achen1o1.xyz/AB/P_GUN/[BuildTarget]`. Each group keeps `ContentUpdateGroupSchema.StaticContent` enabled (`Prevent Updates` in the Inspector), so later updates should be produced with the official Addressables `Update a Previous Build` workflow and the original `addressables_content_state.bin`. Upload generated remote catalog/hash/bundles to `/www/wwwroot/39.97.56.180/AB/P_GUN/[BuildTarget]` on the Nginx server. Do not put first-package-only scene, UI, player, bullet, or VFX assets into Addressables unless they are explicitly intended to hot update.

Generated `Content Update*` groups must use `Remote.BuildPath` and `Remote.LoadPath` before building/uploading update bundles. Use `PG/Addressables/一键保存上传` for routine hot-update publishing; it also validates the catalog and blocks uploads when a `contentupdate__*.bundle` still points to `Addressables.RuntimePath` or `StreamingAssets`.

`Root` is the first Build Settings scene, followed by `StartScene` and `GameScene`. It owns the explicit scene singletons `DataBaseManager`, `LuaManager`, `AddressableRuntimeContent`, and `RootHotUpdateController`. Addressables `DisableCatalogUpdateOnStartup` is enabled so `RootHotUpdateController` owns the update UI timing. The boot flow initializes Addressables, checks and updates catalogs, downloads labels `room`, `buff`, `item`, `enemy`, and `weapon`, loads databases, preloads runtime Addressables content, then loads `StartScene`. Network/catalog/download failures may log and continue with built-in or cached content; missing databases or required runtime content are configuration errors and should fail loudly.

## Current Refactor Notes

The item system has been refactored toward:

- `ItemDatabase` only stores display data.
- Item prefabs own interaction behavior and a list of `ItemEffectBase` effects.
- Spawn tables choose prefabs by weight.
- Effects are ScriptableObject assets and can be combined.
- Runtime pickup flow supports Animator, DOTween fallback, then direct effect execution.

The weapon bug notes in `当前问题.md` describe fixes around reloading, empty clips, looping audio, and updating bullet UI on gun switch. Check current weapon classes before changing reload or shooting audio behavior.

## Editing Checklist

Before changing code:

- Locate the owning module and asmdef.
- Check whether a database, event, pool, or existing lifecycle hook already solves the task.
- Avoid broad fallback logic unless the existing module already uses that pattern. Missing required scene references, config, or database assets may throw/log clearly instead of silently self-healing.
- Do not create singleton GameObjects or managers dynamically from runtime code. Use Unity MCP tools or relevant Unity skills to add required singletons/components to the scene and serialize references.
- Place new scripts/assets into the correct module folder immediately, matching namespace and asmdef boundaries.
- Ask the user before choosing unclear gameplay behavior, data ownership, singleton placement, or cross-module architecture.
- When adding a new system, also update `SKILL.md` and this architecture reference with its rules, responsibilities, folder layout, and core APIs.
- Preserve serialized field names unless the migration is intentional.
- Add Chinese comments with English punctuation for non-obvious new logic.

Before finishing:

- Search for compile-impacting references with `rg`.
- Validate event payload types.
- Validate pooled state reset paths.
- For Unity serialized assets, prefer Unity Editor/MCP operations when available.
