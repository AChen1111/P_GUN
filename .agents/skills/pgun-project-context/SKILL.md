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

P_GUN is a Unity 2022.3 2D top-down shooter project. Runtime code is split by asmdef under `Assets/Scripts`, with gameplay, item, UI, pooling, animation, presentation, and core modules. Data-driven content is primarily ScriptableObject-based and can be generated/imported with the embedded `Packages/com.pgun.excel2so` editor package.

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
- For pooled objects, implement and reset state through `IPoolable.OnSpawnFromPool()` and `IPoolable.OnRecycleToPool()`.
- For databases, prefer `ScriptableObjectDatabase<TDatabase, TKey, TValue>` and `TryGetById` patterns instead of ad hoc list scans.
- For editor table import, prefer extending `Excel2SoListAssetImporter<TAsset>` or `ExcelTableImporterBase` in `Assets/Editor`.
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
