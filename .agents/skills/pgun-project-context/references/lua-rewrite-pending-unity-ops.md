# Lua 重写待执行的 Unity 操作

代码侧改造已完成的操作清单. 以下每一条都需要在 Unity 编辑器里执行, 按顺序做完后本分支才算可运行. 完成一项就勾掉一项.

计划二（随机房间生成）的 Unity 操作见 `random-room-generation-acceptance.md` 文末清单，与本文档第 8 节的房间地址注册一起执行.

## 1. Root 场景

- [ ] 移除旧 `LuaManager` 组件. 旧类已删除, 场景上是丢失脚本, 直接删除组件即可.
- [ ] 挂上框架 `LuaManager`（`Assets/Scripts/LuaComponet/LuaManager.cs`）.
- [ ] 挂上 `PgunLuaRuntimeBootstrap`（`Assets/Scripts/Gameplay/Lua/PgunLuaRuntimeBootstrap.cs`）. 它负责注册配置读取和执行 `hotfix/main`.
- [ ] Project Settings → Script Execution Order: `LuaManager` 最先, 之后 `PgunLuaRuntimeBootstrap`, 最后 `LuaComponet`.

## 2. GameScene 场景

- [ ] 挂 `WeaponManager`（`Assets/Scripts/Gameplay/Managers/WeaponManager.cs`）, 和对象池、`WeaponGlobal` 放在一起.
- [ ] 挂 `BuffBehaviorPool`（`Assets/Scripts/Gameplay/Buffs/Core/BuffBehaviorPool.cs`）, 配置激活与未激活父节点.
- [ ] 挂 `ItemEffectPool`（`Assets/Scripts/Items/Inventory/ItemEffectPool.cs`）, 配置激活与未激活父节点.

## 3. 枪械预制体

对 `weapon/pistol`, `weapon/ak`, `weapon/awp`, `weapon/bow`, `weapon/laser`, `weapon/mp5`, `weapon/rocket_gun`, `weapon/shotgun` 逐个执行:

- [ ] 把枪械子类组件（AK/AWP/Bow/Laser/MP5/Pistol/RocketGun/ShotGun）换成基类 `Gun`.
- [ ] 挂 `LuaComponet`, `m_typeName` 填对应模块名: `PistolModule`, `AKModule`, `MP5Module`, `AWPModule`, `ShotGunModule`, `RocketGunModule`, `BowModule`, `LaserModule`.
- [ ] `Bow` 预制体: ObjectReference 加 `m_Arrow`, 指到箭矢 SpriteRenderer.
- [ ] `Laser` 预制体: ObjectReference 加 `m_LineRenderer`, 指到 LineRenderer; DataReference 加 `maxLaserDistance`, Float, 100.
- [ ] `AK` 预制体: ObjectReference 加 `m_ShootEndClip`, 指到原来序列化的 AKShootEnd 音效.
- [ ] 全部验证通过后删除 `Assets/Scripts/Gameplay/Entity/Player/Weapon/Gun/` 下的 AK.cs, AWP.cs, Bow.cs, Laser.cs, MP5.cs, Pistol.cs, RocketGun.cs, ShotGun.cs 及其 `.meta`.

## 4. 子弹预制体

- [ ] 玩家子弹预制体挂 `LuaComponet`, `m_typeName` 填 `PlayerBulletModule`, 并把 `bulletId` 字段填成 `player_bullet`.
- [ ] 敌人子弹预制体挂 `LuaComponet`, `m_typeName` 填 `EnemyBulletModule`, 并把 `bulletId` 字段填成 `enemy_bullet`.
- [ ] 敌人子弹上原来的 `hitBuffId` 序列化字段已删除, 命中 Buff 改从 `BulletData.lua` 读取.

## 5. Buff 行为预制体

- [ ] 新建空预制体, 挂 `LuaComponet`, `m_typeName` 填 `PoisonBehavior`.
- [ ] 放进 `Buff` 分组, Addressables 地址设为 `buff/poison_behavior`, 标签带 `buff;hot_update`.
- [ ] 如需 `HaHaBuff`: 先在 `Assets/csv/BuffData.csv` 加数据行, 再建行为预制体并配地址, 然后重跑 CSV 导入.

契约系与击杀系行为预制体, 同样是空预制体加 `LuaComponet`, 都放进 `Buff` 分组并带 `buff;hot_update`:

- [ ] `buff/blood_pact_behavior`, `m_typeName` = `BloodPactBehavior`.
- [ ] `buff/blood_rage_behavior`, `m_typeName` = `BloodRageBehavior`.
- [ ] `buff/lifesteal_behavior`, `m_typeName` = `LifestealBehavior`.
- [ ] `buff/frenzy_behavior`, `m_typeName` = `FrenzyBehavior`.
- [ ] `buff/bandolier_behavior`, `m_typeName` = `BandolierBehavior`.
- [ ] `buff/war_drum_behavior`, `m_typeName` = `WarDrumBehavior`.
- [ ] 皮糙肉厚、破甲、护盾、缓速、血契是纯属性 Buff, 不需要行为预制体.

## 6. 道具效果预制体

五个预制体, 全部挂 `LuaComponet`, 放进 `Item` 分组, 标签带 `item;hot_update`:

- [ ] `item/effect/heal`, `m_typeName` = `HealEffectModule`, DataReference 加 `healAmount`, Int, 1.
- [ ] `item/effect/speed`, `m_typeName` = `SpeedPotionModule`, DataReference 加 `buffId`, Int, 0.
- [ ] `item/effect/damage`, `m_typeName` = `DamagePotionModule`, DataReference 加 `buffId`, Int, 1.
- [ ] `item/effect/cleanse`, `m_typeName` = `CleansePotionModule`.
- [ ] `item/effect/mystery`, `m_typeName` = `MysteryPotionModule`. 效果已定义为随机触发一种已有效果.
- [ ] `item/effect/blood_tome`, `m_typeName` = `BloodTomeModule`.
- [ ] `item/effect/shield_generator`, `m_typeName` = `ShieldGeneratorModule`.
- [ ] `item/effect/war_drum`, `m_typeName` = `WarDrumModule`.
- [ ] `item/effect/ammo_crate`, `m_typeName` = `AmmoCrateModule`.

## 6b. 元素弹预制体

两种新子弹是现有敌人子弹的复制体, 挂 `LuaComponet` 并填 `bulletId`:

- [ ] 冰晶弹预制体: `m_typeName` = `EnemyBulletModule`, `bulletId` = `frost_bullet`, 放进 `Enemy` 分组.
- [ ] 诅咒弹预制体: `m_typeName` = `EnemyBulletModule`, `bulletId` = `curse_bullet`, 放进 `Enemy` 分组.
- [ ] 敌人换弹种时替换敌人预制体上的子弹引用即可, 命中 Buff 由 `BulletData.lua` 的 `hitBuffId` 决定.

## 7. 房间预制体

- [ ] `NormalRoom` 预制体挂 `LuaComponet`, `m_typeName` 填 `RoomRewardModule`; `spawnTableId` 字段填 `normal_default` 或新的生成表 id.
- [ ] `ChestRoom` 预制体挂 `LuaComponet`, `m_typeName` 填 `ChestRoomModule`.

## 8. Addressables 地址注册

CSV 里的资源列目前是资源文件路径, 需要注册成真实地址后把 CSV 改成地址, 再重跑导入:

- [ ] 武器音效: `Assets/Audio/SFX/Player/Gun/` 下的换弹与射击音效进入 `Weapon` 或 `Shared` 分组并配地址.
- [ ] Buff 图标: `Assets/Malicious_statusiconset1/` 的四个图标进入 `Buff` 分组.
- [ ] 道具图标: `Assets/Assets/Items/Potion Asset.png` 进入 `Item` 分组.
- [ ] 敌人预制体: Bat 与 Slime 的地址（CSV 中是 `Assets/Prefab/Enemy/Bat.prefab` 这类路径）.
- [ ] 房间预制体: `LevelConfig.csv` 中的 `room/init` 等地址需要在换用随机生成（计划二）前注册.
- [ ] 每次改 CSV 后执行 `Tools/UnityEasyWorkTools/CSV To Lua Table/Import All`.

## 9. XLua 注入

- [ ] `Assets/XLua/Editor/PgunHotfixConfig.cs` 已收紧到 `Player` 与 `EnemyBase`. 执行 `XLua/Generate Code`, 等编译完成, 再执行 `XLua/Hotfix Inject In Editor`.

## 10. 真机构建

- [ ] 执行 `Tools/Lua/Build LuaBundle`, 生成 `Assets/Resources/LuaBundle.bytes`.
- [ ] 待定: 真机热更通道. 框架真机从 `Resources/LuaBundle.bytes` 读取; 如果要让内容脚本走 Addressables 热更, 需要决定 LuaBundle 是否作为 Addressable 条目更新, 或扩展 LuaEnvironment 从 Addressables 读取.

## 11. 旧资产清理（放在最后）

- [ ] 道具预制体上的 `ItemEffectBase` 效果列表已不再被背包使用, 确认后从预制体上移除引用.
- [ ] `HealItemEffect`, `ApplyBuffItemEffect`, `CleanseNegativeBuffItemEffect`, `ChestRandomLootEffect` 类与其资产在效果预制体验证后删除.
- [ ] `SpawnPrefabFightRoomEndEffectSO` 与 `FightRoomEndEffectSO` 在 `RoomRewardModule` 验证后删除.
- [ ] `ItemDatabase`, `WeaponDatabase`, `BuffDataBase`, `EnemyDatabase` 运行时不再读取; `BuffDataBase` 仍被 Buff 调试窗口用来列名字, 其余可评估删除.
- [ ] `AddressableItemAddressCatalog` 已不被背包恢复使用, 评估删除.
- [ ] UI 面板的 Lua 化是单独批次: 面板根物体挂 `LuaComponet` 后用 ObjectReference 绑控件, 再按面板逐个迁移.