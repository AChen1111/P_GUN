# P_GUN Scripts 代码设计审查报告

**审查范围**: `D:\GameWorkplace\Doing\P_GUN\Assets\Scripts`  
**文件总数**: 133 个 `.cs` 文件  
**审查日期**: 2026-05-22

---

## 一、过度设计 (Over-Engineering)

### 1. Buff 系统抽象层级过多（6~7 层）

```
Buff (数据)
  → BuffRuntimeInfo (运行时状态)
    → BuffScriptRuntime (全局静态注册点)
      → IBuffScriptFactory (接口)
        → IBuffScriptInstance (接口)
          → LuaBuffInstance (唯一实现)
            → Lua Table
```

**涉及文件**:
- `Gameplay/Buffs/Core/BuffScriptRuntime.cs`
- `Gameplay/Buffs/Core/IBuffScriptFactory.cs`
- `Gameplay/Buffs/Core/IBuffScriptInstance.cs`

**问题**: `IBuffScriptFactory` 和 `IBuffScriptInstance` 的目的是"隔离 xLua 依赖"，但整个项目只有 `LuaManager` / `LuaBuffInstance` 这一组实现，不存在也看不出需要第二套脚本后端的场景。`BuffScriptRuntime` 作为全局静态服务定位器，纯粹是中间人。这是典型的 **YAGNI**。

**建议**: 删除 `IBuffScriptFactory`、`IBuffScriptInstance`、`BuffScriptRuntime` 三个文件。`BuffManager` 直接依赖 `LuaManager` 创建 `LuaBuffInstance`。

---

### 2. `BuffTriggerType` 枚举从未被使用

**涉及文件**: `Gameplay/Buffs/Core/BuffTriggerType.cs`

定义了 `Continuous` 和 `Interval`，但 `BuffManager.Update()` 中实际逻辑是：每帧调用 `OnUpdate`，当 `interval > 0` 时额外调用 `OnInterval`。枚举不参与任何分支判断。

**建议**: 删除该文件。

---

### 3. `ScriptableObjectDatabase<TDatabase, TKey, TValue>` 泛型过度抽象

**涉及文件**: `Core/ScriptableObjectDatabase.cs`

CRTP 模式 + 3 个泛型参数 + 自引用约束 + 可覆写的 `KeyComparer` 虚属性。但三个子类的 `TryGetKey` 实现全部是直接返回 `Id` / `itemId` / `weaponId`：

| 子类 | TryGetKey 逻辑 |
|------|---------------|
| `BuffDataBase` | `key = buff.Id` |
| `ItemDatabase` | `key = data.itemId` |
| `WeaponDatabase` | `key = data.weaponId` |

没有子类覆写 `KeyComparer`（仅 `WeaponDatabase` 用了 `StringComparer.OrdinalIgnoreCase`，其余用默认比较器）。

**建议**: 简化为直接内联 `Dictionary<TKey, TValue>`，或使用 Unity 的 `SerializedDictionary`。

---

### 4. `ISaveDataProvider<T>` / `ISaveDataRestorer<T>` 无人实现

**涉及文件**:
- `Gameplay/Save/ISaveDataProvider.cs`
- `Gameplay/Save/ISaveDataRestorer.cs`

标注为"后续模块化接入时使用"，但项目中无任何类实现这两个接口。

**建议**: 删除。等真正需要时再加回来。

---

### 5. 存档系统同步/异步两套重复代码

**涉及文件**: `Gameplay/Save/SaveGameService.cs`（518 行）

`RestoreSaveData` 与 `RestoreSaveDataAsync`、`TryRestorePendingSave` 与 `TryRestorePendingSaveAsync` 逻辑几乎完全相同。当前异步版本中的 await 操作最终都是同步完成的。同样的 `RestoreRooms` / `RestoreCurrentRoom` 方法在文件内出现了**两次**（一次在 `RestoreSaveData` 的嵌套方法中，一次在顶级私有方法中）。

**建议**: 统一为一套实现，用 `async Task` 即可兼容同步调用（调用方 `.Wait()` 或 `await`）。

---

### 6. 武器子类继承层次可能过重

**涉及文件**: `Gameplay/Entity/Player/Weapon/Gun/` 下 9 个文件

`Gun`(abstract) → `AK`, `AWP`, `Bow`, `Laser`, `MP5`, `Pistol`, `RocketGun`, `ShotGun`。

客观地说，AK(全自动)、Pistol(半自动)、ShotGun(散射)、Laser(光束) 确实有不同开火逻辑，继承有一定合理性。但如果 `AWP`/`MP5`/`RocketGun`/`Bow` 的开火模式与已有类型高度相似，可考虑改为"开火模式枚举 + 单一 Gun 类"的数据驱动方案。

---

## 二、设计不当 (Design Issues)

### 1. EventCenter 存在类型安全和性能隐患

**涉及文件**: `Core/EventCenter.cs`

| 问题 | 说明 |
|------|------|
| **类型安全缺失** | 泛型 `Trigger<T>` 使用 `HashSet<Delegate>` + 运行时 `is Action<T>` 检查，编译期无法发现类型不匹配。不匹配时只打 `Warning` 然后静默跳过。 |
| **顺序不确定** | `HashSet` 不保证迭代顺序，同一事件的多个监听器执行顺序不可预测。 |
| **每次触发分配内存** | `CopyListeners` 每次调用创建新 `List`，高频事件（如 `BulletClipChanged`）产生不必要的 GC 压力。 |

**建议**: 改用 `List` 替代 `HashSet`（保证注册顺序），考虑用接口 + 泛型约束替代 `Delegate` 的动态转换。

---

### 2. `Buff.cs` 数据与调度职责混杂

**涉及文件**: `Gameplay/Buffs/Buff/Buff.cs`

`Buff` 既是可序列化的纯数据类（id, icon, duration, modifiers），又承担向 Lua 分发生命周期事件的职责：

```csharp
// 数据类不应该知道 Lua 实例的存在
public void OnAdd(BuffRuntimeInfo info) { info?.LuaInstance?.OnAdd(info); }
public void OnRemove(BuffRuntimeInfo info) { info?.LuaInstance?.OnRemove(info); }
```

这违反了单一职责原则。

**建议**: 将 `OnAdd/OnRemove/OnUpdate/OnInterval/OnTrigger` 五个调度方法移到 `BuffManager` 或 `BuffRuntimeInfo` 中。

---

### 3. `Global.cs` — 贫血全局状态

**涉及文件**: `Gameplay/Managers/Global.cs`

只有一个字段 `public static Player player`。类名过于泛化，通过静态可变状态共享，场景重载时容易产生悬空引用。

**建议**: 合并到 `WeaponGlobal` 或创建更明确的 `PlayerTracker` / `GameContext` 类。

---

### 4. 普遍存在的无用 using 导入

几乎每个文件都包含这组导入，包括明显不需要它们的文件（`GunClip.cs`、`ShootDuration.cs`、`StatModifier.cs`）：

```csharp
using Game.Core;
using Game.Pooling;
using Game.Animation;
using Game.Presentation;
using Game.Items;
```

这是典型的模板复制粘贴残留，增加代码噪音。

---

### 5. Core 层的两个纯透传包装

| 文件 | 内容 | 问题 |
|------|------|------|
| `Core/Config.cs` | 两个 `static string` 字段 | 可以用常量替代 |
| `Core/InputCheck.cs` | 两个方法，直接返回 `Input.anyKey` / `Input.anyKeyDown` | 零附加价值，增加调用链长度 |

---

### 6. `GameplayCursorState` 五个复制粘贴的方法

**涉及文件**: `Core/GameplayCursorState.cs`

`SetControlKeyHeld`、`SetSettingsPanelOpen`、`SetDebugPanelOpen`、`SetInventoryPanelOpen`、`SetSaveSlotPanelOpen` 五个方法结构完全一致：

```csharp
public static void SetXxx(bool value)
{
    if (xxxField == value) return;
    xxxField = value;
    ApplyCursorState();
}
```

**建议**: 合并为 `SetState(ref bool field, bool value)` 或使用枚举标识面板类型。

---

### 7. `SaveGameService` — 上帝类

**涉及文件**: `Gameplay/Save/SaveGameService.cs`（518 行）

一个静态类承担：存档、读档（同步/异步）、截图、数据构建（玩家/背包/Buff/武器/房间）、pending load 管理、Edgar 地图种子恢复。

**建议**: 至少将截图逻辑（`CaptureSlotSnapshot`）和数据构建逻辑（`BuildSaveData` / `CapturePlayer` 等）拆分为独立类。

---

### 8. 9 个空目录

```
Scripts/Config/
Scripts/Gameplay/Entity/Player/Weapon/Global/
Scripts/Gameplay/Lua/Hotfix/
Scripts/Gameplay/Room/RoomBase/Event/
Scripts/Global/
Scripts/ItemEffects/Lua/
Scripts/Tools/
Scripts/VFX/
```

这些是计划但未实现的功能残留。

---

### 9. `BuffRuntimeInfo.Params` — 类型不安全的黑板

**涉及文件**: `Gameplay/Buffs/Core/BuffRuntimeInfo.cs:32`

`Dictionary<string, object>` 给 Lua 读写临时数据，完全绕过 C# 类型系统。如果 Lua 侧的数据需求是明确的，应该用强类型字段替代。

---

### 10. `GunFire` — 单方法类强耦合单例

**涉及文件**: `Gameplay/Entity/Player/Weapon/Feature/GunFire.cs`

整个类只有一个 `Show(Vector2, Vector2)` 方法，且直接硬编码访问 `WeaponGlobal.Instance`。可以直接合并到 `WeaponGlobal` 或 `Gun` 中。

---

### 11. 注释风格问题 — 无信息量的转发注释

大量方法注释格式为 `/// <summary>执行 XXX 逻辑.</summary>`，仅用中文重述方法名：

```csharp
/// <summary>
/// 执行 AddListener 逻辑.
/// </summary>
public static void AddListener(...)
```

这类注释不提供任何方法名以外的信息，属于噪音。好的注释应解释 **Why**（为什么这样做），而非 **What**（方法名已经说明了做什么）。

---

## 三、设计上做得好的地方

- **`PoolBase<T>`** (`Pooling/PoolBase.cs`) — 基于 `UnityEngine.Pool.ObjectPool` 封装合理，prefab→pool 映射清晰，API 设计直观。
- **`SaveSlotStorage`** — 职责单一，只做文件 IO，不碰场景对象。
- **`PlayerInventory`** — 背包逻辑干净，通过 EventCenter 通知 UI 而非直接引用。
- **`ItemEffectBase`** (ScriptableObject) — 道具效果多态设计简洁，`CanUse` + `OnPick` 抽象得当。
- **房间继承体系** — `Room` → `FightRoom` / `NormalRoom` / `InitRoom` / `SaveRoom` / `FinalRoom` 各有明确差异化行为，合理。

---

## 四、改进优先级

| 优先级 | 问题 | 影响 | 改动量 |
|--------|------|------|--------|
| **高** | Buff 系统 6 层抽象 | 新人理解成本高 | 删除 3 文件 + 简化 Buff.cs |
| **高** | EventCenter 类型安全问题 | 运行时隐患 | 重构 EventCenter |
| **中** | Buff.cs 数据/调度混杂 | 可维护性 | 移动方法 |
| **中** | SaveGameService 上帝类 + 同步/异步重复 | 修改易遗漏 | 拆分文件 |
| **中** | `ISaveDataProvider`/`ISaveDataRestorer` 死代码 | 误导 | 删除 2 文件 |
| **低** | 无用 using / 空目录 / Config / InputCheck | 噪音 | 批量清理 |
| **低** | `BuffTriggerType` 死代码 | 误导 | 删除 1 文件 |
| **低** | `GameplayCursorState` 复制粘贴 | 维护不便 | 合并方法 |

**核心建议**: 削减 Buff 系统的抽象层级是投入产出比最高的改进——删除 3 个文件 + 简化 `Buff.cs`，可一次性解决多个问题。
