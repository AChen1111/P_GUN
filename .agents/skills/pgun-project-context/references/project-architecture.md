# P_GUN Project Architecture

## Current Shape

P_GUN is a Unity 2022.3 2D top-down shooter. The main runtime code lives in `Assets/Scripts` and is split with asmdefs such as `Game.Core`, `Game.Gameplay`, `Game.Items`, `Game.ItemEffects`, `Game.Pooling`, `Game.UI`, `Game.Animation`, and `Game.Presentation`.

Important external packages and conventions:

- QFramework is used for `ViewController`, FSM, UI tooling, and utility extensions.
- DOTween is used for runtime animation feedback.
- Addressables are used for loading global databases.
- xLua is used by the Buff system.
- `Packages/com.pgun.excel2so` is an embedded Unity Editor package for importing `.xlsx` and `.csv` into ScriptableObject assets.

## Core Module

Location: `Assets/Scripts/Core`

`ScriptableObjectDatabase<TDatabase, TKey, TValue>` is the shared base for data tables. Subclasses provide `DataValues` and `TryGetKey`, then query with `TryGetById(TKey id, out TValue data)`. Importers should update data through subclass replace methods so the runtime index rebuilds.

Known subclasses:

- `Game.Items.ItemDatabase`: key `int itemId`, value `ItemData`.
- `Game.Gameplay.BuffDataBase`: key `int Id`, value `Buff`.
- `Game.Gameplay.WeaponDatabase`: key `string WeaponId`, value `WeaponData`.
- `Game.Gameplay.EnemyDatabase`: key `int enemyId`, value `EnemyData`.

`EventCenter` is the simple static event bus. Use `AddListener`, `RemoveListener`, and `Trigger` with `GameEvent`. Payload events are generic, so listener payload types must match trigger payload types.

Current `GameEvent` includes player health/death/game events, minimap show/hide/toggle, item tip show/hide, item picked, bullet clip/bag changes, door open/close, and room generation completion.

## Database Loading

Location: `Assets/Scripts/Gameplay/Managers/DataBaseManager.cs`

`DataBaseManager` is a persistent singleton that loads global databases through Addressables:

- `ItemDatabase`
- `WeaponDatabase`
- `BuffDataBase`
- `EnemyDatabase`

Call `LoadAllAsync()` before gameplay that depends on data. Runtime systems typically prefer explicitly assigned database references, then fall back to `DataBaseManager.Instance`.

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

`ItemSpawnTableSO` stores weighted prefab entries. It returns a prefab through `TryGetRandomPrefab(out GameObject prefab)`. It does not own display data.

`Item` is the runtime pickup component. It:

- Requires `Collider2D`.
- Uses `itemId` plus an optional `ItemDatabase` to resolve display data.
- Shows/hides item tips through `EventCenter.Trigger(GameEvent.ItemTipShown, ItemData)` and `GameEvent.ItemTipHidden`.
- Picks up on `F` while the player is in range.
- Triggers `GameEvent.ItemPicked`.
- Plays Animator trigger `OnPickup` if available.
- Falls back to `GameDOTweenAnimation.Play(callback)` if no pickup trigger exists.
- Executes each `ItemEffectBase.OnPick(ItemEffectContext ctx)`.
- Plays optional pickup audio through `GlobalAudioPlay`.
- Releases itself through `ItemPool.Instance.Release(this)` when `isDestroy` is true.
- Implements `IPoolable` and resets pickup state, animator state, tweens, and tips during pooling lifecycle.

`ItemEffectBase` is a ScriptableObject effect API:

```csharp
public abstract void OnPick(ItemEffectContext ctx);
```

`ItemEffectContext` currently carries:

- `GameObject SourceObject`
- `Vector3 WorldPosition`

Known item effects include healing, applying buffs, chest random loot, and spawning prefabs at fight-room end.

## Buffs

Locations:

- `Assets/Scripts/Gameplay/Buffs`
- `Assets/Scripts/Gameplay/Managers/LuaManager.cs`

`Buff` is serializable config stored inside `BuffDataBase`, not a standalone ScriptableObject. It contains id, display name, Lua file, duration, permanence, and interval.

`BuffManager` lives on the player and manages runtime buff instances. Important APIs:

- `AddBuffById(int buffId)`
- `AddBuffById(int buffId, UnityEngine.Object source)`
- `AddBuff(Buff buff)`
- `AddBuff(Buff buff, UnityEngine.Object source)`
- `RemoveBuff(Buff buff)`
- `RemoveBuffById(int buffId)`
- `TriggerBuffById(int buffId)`
- `ClearBuffs()`

If a buff already exists, adding it resets duration and triggers `OnAdd` again.

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

## UI

Locations:

- `Assets/Scripts/UI`

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

When adding UI, use existing event flow before adding direct scene references.

## Excel2SO

Locations:

- Package: `Packages/com.pgun.excel2so`
- Project importers: `Assets/Editor/Excel2SOImporters`

Excel2SO converts `.xlsx` and `.csv` table data into ScriptableObject assets in the Unity Editor. Open via `Tools/Excel2SO/Importer Window`.

Typical importer flow:

1. Create a project importer under `Assets/Editor`.
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

`Assets/Editor/AddressablesLocalGroupSetup.cs` and `Assets/Editor/AddressablesLabelTableImporter.cs` support editor-side Addressables setup/import. Preserve labels and database keys when moving assets.

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
