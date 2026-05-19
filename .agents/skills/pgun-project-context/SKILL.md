---
name: pgun-project-context
description: Read P_GUN project rules, architecture, module responsibilities, and core APIs before coding or analysis. Use at the start of every new conversation in D:\GameWorkplace\Doing\P_GUN, and whenever working on Unity C# code, gameplay systems, item/buff/weapon/database/UI/pooling modules, Excel2SO importers, or project documentation.
---

# P_GUN Project Context

## Startup

Always read this skill first when working in `D:\GameWorkplace\Doing\P_GUN`.

Use this order:

1. Read `AGENTS.md` in the project root if present.
2. Read this `SKILL.md`.
3. Read `references/project-architecture.md` when the task touches gameplay, UI, data import, object pooling, item, buff, weapon, enemy, or Addressables/database behavior.
4. Inspect the current files before changing code, because the architecture reference is a snapshot and may lag behind the code.

## Project Summary

P_GUN is a Unity 2022.3 2D top-down shooter project. Runtime code is split by asmdef under `Assets/Scripts`, with gameplay, item, UI, pooling, animation, presentation, and core modules. Data-driven content is primarily ScriptableObject-based and can be generated/imported with the `Assets/UnityEasyWorkTools/TableImporter` editor module.

## Coding Rules

- Write new or modified C# code with necessary comments.
- Comments must use Chinese descriptions with English punctuation.
- Do not add excessive fallback mechanisms. In most project code, failing loudly or allowing an error is acceptable when required dependencies or configuration are missing.
- Do not dynamically create singleton GameObjects or managers in code. Add singleton objects/components to scenes through Unity MCP tools or relevant Unity skills, then bind references in the scene/prefab.
- Put every newly created script or asset in the correct classified folder/module. Do not place new code in a generic folder when a module folder already exists.
- Ask the user when an implementation detail is unclear instead of guessing a large behavior or architecture decision.
- When designing or adding a new system, update this skill and its references with the new system rules, responsibilities, folders, and core APIs.
- Keep comments purposeful: explain intent, lifecycle coupling, pooling reset requirements, editor import assumptions, or non-obvious gameplay rules.
- Prefer the existing namespace and asmdef layout: `Game.Core`, `Game.Gameplay`, `Game.Items`, `Game.ItemEffects`, `Game.Pooling`, `Game.UI`, `Game.Animation`, `Game.Presentation`.
- Preserve existing serialized field names where possible, because Unity scene and prefab references depend on them.
- Do not rename or move Unity assets, `.meta` files, asmdefs, scenes, or prefabs unless the task requires it.
- For runtime cross-system notifications, prefer `Game.Core.EventCenter` and existing `GameEvent` values before adding new singleton coupling.
- Buff 状态栏 UI lives in `Assets/Scripts/UI/Buffs` and `Assets/Prefab/UI/Buff`; it reads `BuffManager.ActiveBuffs` after `GameEvent.PlayerBuffsChanged` and must not maintain separate Buff display data.
- Buff 调试窗口 lives in `Assets/Scripts/UI/GameSceneUIInputController.cs`; it is toggled with `Alt+Up`, and it adds/removes Buff through the player's `BuffManager` without creating scene managers.
- `GameScene` UI uses an explicit scene `UIStackManager` plus `UIStackInitializer`; HUD is the stack bottom and modal panels such as settings, win, and over panels are pushed through the stack.
- Inventory runtime lives in `Assets/Scripts/Items/Inventory`; `PlayerInventory` must be attached to the player prefab, stacks picked items by `itemId`, and triggers `GameEvent.InventoryChanged` after add/use/clear.
- Inventory UI lives in `Assets/Scripts/UI/Inventory` and prefabs live in `Assets/Prefab/UI/Inventory`; `GameSceneUIInputController` toggles it with `CapsLock`, pauses through the UI stack flow, and right-clicking a slot consumes one item only when at least one effect `CanUse` returns true.
- Save system lives in `Assets/Scripts/Gameplay/Save`; it stores 3 JSON slots under `Application.persistentDataPath/Saves`, uses `SaveGameService` as the UI-facing API, and does not save ground drop items in v1.
- Save slot UI lives in `Assets/Scripts/UI/Save` and `Assets/Prefab/UI/Save`; `SaveSlotPanel` opens in main-menu load/delete mode or safe-house save/load/delete mode, with `F5` as the temporary GameScene safe-house test shortcut.
- Safe-point saves must fail clearly while `FightRoom.currentFightRoom` is not null; do not silently save mid-combat wave/enemy state in the v1 framework.
- `UIPanelBase` roots should carry `ComponentAutoBindTool`; bindable child objects use UIAutoBind prefixes such as `Btn_`, `Txt_`, `Img_`, `Trans_`, and `Rect_`.
- For pooled objects, implement and reset state through `IPoolable.OnSpawnFromPool()` and `IPoolable.OnRecycleToPool()`.
- Damage number presentation belongs to `Game.Presentation`: enemies pass final damage and world position to `DamageTextPool`, while the `DamageText` prefab script owns random font size, offset, tween playback, and recycle callback.
- UnityEasyWorkTools is the shared editor tooling suite under `Assets/UnityEasyWorkTools`, currently containing `AnimationSequence`, `UIAutoBind`, and `TableImporter`.
- UnityEasyWorkTools path defaults are configured by `Assets/UnityEasyWorkTools/UnityEasyWorkToolsPathSettings.asset`; open it from `Tools/UnityEasyWorkTools/Settings/Open Path Settings`.
- UnityEasyWorkTools editor UI must use UI Toolkit with `.uxml` and `.uss` files stored in each module's `Editor/UI` folder; editor C# should only bind data and handle commands.
- Visual animation sequences belong to `Game.Animation`: runtime scripts live in `Assets/UnityEasyWorkTools/AnimationSequence/Scripts`, editor code lives in `Assets/UnityEasyWorkTools/AnimationSequence/Editor`, sequence assets are `AnimationSequenceAsset`, and playback is handled by scene `AnimationPlayer`.
- UI auto binding tooling lives in `Assets/UnityEasyWorkTools/UIAutoBind`: runtime binding component and rules are in `Scripts`, editor inspector/code generation is in `Editor`, and the global setting asset stays beside them at the feature root.
- For databases, prefer `ScriptableObjectDatabase<TDatabase, TKey, TValue>` and `TryGetById` patterns instead of ad hoc list scans.
- Addressables hot-update groups are limited to `Room`, `Buff`, `Item`, `Enemy`, and `Weapon`; these groups are Local-first with `Prevent Updates` enabled, and later resource updates must use `Update a Previous Build` plus the original `addressables_content_state.bin`.
- `Root` must stay first in Build Settings and explicitly owns `DataBaseManager`, `LuaManager`, `AddressableRuntimeContent`, and `RootHotUpdateController`; do not create these singleton GameObjects from code.
- Buff 配置包含 `BuffTag` 正负面标签; 净化类效果应通过 `BuffManager.RemoveBuffsByTag(BuffTag.Negative)` 批量移除负面 Buff.
- For regular Buff stat changes, configure `StatModifier` data on `Buff` and calculate through `BuffManager`; do not let Lua directly mutate player speed, attack, or max HP fields.
- For editor table import, prefer extending `Excel2SoListAssetImporter<TAsset>` or `ExcelTableImporterBase` in `Assets/UnityEasyWorkTools/TableImporter/Importers`.
- Keep generated/bulk assets and Unity cache folders out of manual edits unless explicitly requested.

## Validation

When code changes are made:

- Run targeted compile/test checks when available.
- If Unity Editor/MCP tools are available and the task touches scenes, prefabs, animation, or serialized assets, prefer Unity-aware tools over raw YAML edits.
- If tests cannot be run, state that clearly and mention the likely Unity validation path.

## Detailed Reference

Read `references/project-architecture.md` for:

- Module responsibilities.
- Key classes and public APIs.
- Current item, buff, weapon, enemy, UI, pooling, animation, and database implementation notes.
- Excel2SO workflow.
