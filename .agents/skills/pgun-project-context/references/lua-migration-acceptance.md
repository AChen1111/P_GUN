# Lua 迁移总验收文档

验收范围是一次提交合入的全部迁移工作：Lua 框架与数据表（计划一）、随机房间生成（计划二）、敌人行为重写（计划三）、Buff 与道具内容扩展。代码已全部落地，验收在 Unity 编辑器内执行，按阶段顺序推进，任何一项不通过即停止并记录原因。

各阶段的完整逐项清单见三份详细文档，本文只保留每阶段的通过关键项：

- 计划一详细操作: `lua-rewrite-pending-unity-ops.md`
- 计划二详细验收: `random-room-generation-acceptance.md`
- 计划三详细验收: `enemy-behavior-acceptance.md`

## 零、前置条件

以下编辑器操作全部完成前，任何阶段都无法开始。逐项做完后勾掉。

### Root 场景

- [ ] 移除旧 `LuaManager` 组件（旧类已删除, 场景上是丢失脚本）。
- [ ] 挂框架 `LuaManager`（`Assets/Scripts/LuaComponet/LuaManager.cs`）。
- [ ] 挂 `PgunLuaRuntimeBootstrap`（`Assets/Scripts/Gameplay/Lua/PgunLuaRuntimeBootstrap.cs`）。
- [ ] Script Execution Order: `LuaManager` 最先, 之后 `PgunLuaRuntimeBootstrap`, 最后 `LuaComponet`。

### GameScene 场景

- [ ] 挂 `WeaponManager`。
- [ ] 挂 `BuffBehaviorPool` 与 `ItemEffectPool`, 各配激活与未激活父节点。
- [ ] 移除 `AddressableDungeonBootstrapper` 与 `DungeonGeneratorGrid2D`, 挂 `RandomRoomGenerator`, `levelId` 填 `level1`。
- [ ] 生成器拖入 `lrCorridorPrefab` 与 `udCorridorPrefab`, `minimapLayer` 填 MiniMap 图层。

### 预制体

- [ ] 八把枪换成 `Gun` 基类加 `LuaComponet`, `m_typeName` 按枪填对应模块名; Bow 配 `m_Arrow`, Laser 配 `m_LineRenderer` 与 `maxLaserDistance`, AK 配 `m_ShootEndClip`。换绑验证后删除 AK.cs 等八个枪械子类。
- [ ] 玩家与敌人子弹预制体挂 `LuaComponet`, `m_typeName` 填 `PlayerBulletModule` / `EnemyBulletModule`, `bulletId` 填 `player_bullet` / `enemy_bullet`。
- [ ] 七个 Buff 行为预制体（poison, blood_pact, blood_rage, lifesteal, frenzy, bandolier, war_drum）挂 `LuaComponet` 配对应模块名, 进 `Buff` 分组。
- [ ] 九个道具效果预制体（heal, mystery, speed, damage, cleanse, blood_tome, shield_generator, war_drum, ammo_crate）挂 `LuaComponet` 配模块名, 进 `Item` 分组; heal 配 `healAmount`, speed 与 damage 配 `buffId`。
- [ ] 冰晶弹与诅咒弹复制现有敌人子弹预制体, `bulletId` 填 `frost_bullet` / `curse_bullet`, 进 `Enemy` 分组。
- [ ] 五类房间预制体各补 `DoorAnchor_N/E/S/W` 四个空子物体; `NormalRoom` 挂 `RoomRewardModule` 并填 `spawnTableId`; `ChestRoom` 挂 `ChestRoomModule`。

### 数据与注入

- [ ] Addressables 注册音效、Buff 图标、道具图标、敌人预制体与房间预制体地址, 与 CSV 内地址一致。
- [ ] 执行 `Tools/UnityEasyWorkTools/CSV To Lua Table/Import All`。
- [ ] 执行 `XLua/Generate Code`, 等编译完成, 再执行 `XLua/Hotfix Inject In Editor`（名单为 `Player`、`EnemyBase`、`EnemyBrain`）。
- [ ] 真机构建前执行 `Tools/Lua/Build LuaBundle`。

## 一、Lua 框架与数据表

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 1.1 | 打开工程等待编译 | 无编译错误, Console 无 Lua 相关红色报错 |
| 1.2 | Root 场景启动 | 启动日志出现启动热修执行完成, 进入主菜单无异常 |
| 1.3 | 改任意 CSV 后重新导入 | `Assets/Scripts/LuaRaw/Data` 下对应 Lua 更新, 缺列或坏数值直接导入失败 |
| 1.4 | 临时删掉 `LevelConfig.csv` 的 level1 行后导入 | 报缺少关卡行, 生成中止 |
| 1.5 | 玩家血量与移速 | 与 `PlayerData.csv` 一致, 预制体上的旧字段不再生效 |
| 1.6 | 各枪伤害、弹夹、射速 | 与 `WeaponData.csv` 一致, 音效按地址加载播放 |
| 1.7 | 右键使用满血治疗药水 | 不消耗; 正常使用时消耗并生效 |

## 二、随机房间生成

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 2.1 | 开新局两次对比 | 布局不同（种子随机） |
| 2.2 | 存档后读档 | 同 `levelId` 同种子重建同一张地图, 房间进度与门状态一致 |
| 2.3 | 统计房间 | 数量等于 `roomCount`; 出生房在原点, 终点最远, 宝箱与存档房优先死胡同 |
| 2.4 | 走廊与门 | 相邻房间间一条走廊对齐锚点; 门数等于邻居数; 非相邻边无门 |
| 2.5 | 战斗中存档 | 失败并提示, 不产生槽位文件 |
| 2.6 | 旧格式存档读档 | 明确报错, 不静默生成错误地图 |
| 2.7 | 小地图 | 各房 `MinimapRoomData.Positions` 非空, 当前房间高亮切换正确 |

## 三、敌人行为

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 3.1 | 玩家在敌人视野半径外或侧后方 | 敌人待机, 不移动不开火 |
| 3.2 | 玩家躲到同房间墙后 | 敌人不穿墙锁定, 沿可行走格绕行 |
| 3.3 | 丢失视线 | 敌人走向最后已知位置, 到达或超时后回待机, 不跟随新坐标 |
| 3.4 | 同波多个敌人追击 | 互相散开不重叠, 距玩家小于停距时停住 |
| 3.5 | 四类敌人出手 | 点射不停步; 蝙蝠停步扇形且墙后不发射; 近战检测盒翻转加动画事件出伤; 大型敌人环形与点射两段, 点射段边走边打 |
| 3.6 | 子弹时间 | 敌人移动、攻击计时、面朝与敌人子弹整体变慢, 玩家正常 |
| 3.7 | 击杀与复用 | 清波计数与掉落正常; 复用出的敌人无上一次路径与视野记忆 |
| 3.8 | 房间缺 Walls Tilemap | 建格报错; 缺格敌人报错一次后停住 |

## 四、Buff 与道具内容

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 4.1 | 血偿叠层 | 每层攻击 +50%, 每 5 秒按层数掉血; 净化药水可将其移除 |
| 4.2 | 血怒 | 添加瞬间扣 30% 当前生命保底剩 1, 20 秒内攻击 +40%; 重复获得会再次扣血（已知行为） |
| 4.3 | 吸血与战鼓 | 击杀敌人按层数回血; 战鼓期间每次击杀回 1 血 |
| 4.4 | 屠戮者 | 击杀后 4 秒攻击 +30%, 再击杀刷新时长 |
| 4.5 | 弹夹大师 | 添加时全枪弹夹 +50%, 移除或超时还原, 弹药 UI 同步 |
| 4.6 | Defense 系 | 皮糙肉厚减伤 1、护盾减伤 3、破甲增伤 2, 结算后至少 1 点伤害 |
| 4.7 | 血契宝典 | 每层攻击 +2 且生命上限 -1; 生命上限到 1 时不可再用 |
| 4.8 | 弹药补给箱 | 补满当前枪弹夹与备弹并刷新 UI; 无限弹药枪不可使用 |
| 4.9 | 奇特药水 | 随机触发一种已有效果并显示对应提示 |
| 4.10 | 元素弹 | 冰晶弹命中缓速 30% 三秒; 诅咒弹命中附加中毒 |
| 4.11 | 自伤路径 | 中毒与血偿扣血不触发受击闪烁、无敌帧与击退 |
| 4.12 | 击杀事件 | 击杀任意敌人时 Console 无 OnKill 转发报错 |
| 4.13 | 掉落权重 | 击杀掉落与清房奖励按 `SpawnData` 的 `itemDrops` 权重出道具, 多次击杀后比例大致符合权重; 掉落表空时报错且不掉落 |
| 4.14 | 世界道具拾取 | `pickInBackBag=false` 的道具按 F 后由其 Lua 模块 `OnPickedUp` 生效; 未挂 LuaComponet 时报错且不消耗 |
| 4.15 | 主菜单 Lua | 主菜单四个按钮行为与迁移前一致（进游戏、读档、设置、退出）, 连点开始不重复加载 |

## 五、全局回归

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 5.1 | 完整一局 | 从出生房打到终点房, 波次 UI、清房奖励、宝箱房生成正常 |
| 5.2 | 存档循环 | 存档、读档、删除三个槽位全流程正常, 背包、Buff、弹药恢复正确 |
| 5.3 | 热修链路 | `hotfix/main` 默认无补丁; 临时加一个 require 补丁可生效 |
| 5.4 | 旧系统清理 | 运行时不读任何 `ItemDatabase` / `WeaponDatabase` / `BuffDataBase` / `EnemyDatabase`; 调试窗口仅列名不触发运行时读取 |

## 六、已知限制汇总

- 小地图底图原由 Edgar 后处理生成, 现只保留高亮层, 底图渲染方案待补。
- `EnemyData` 的视野、分离与攻击参数是种子值, 按手感调整 CSV 后重新导入。
- 血怒重复获得会再次扣血; 若要改为刷新不扣血, 在 `BloodRageBehavior.OnAdd` 里先判断自身已激活。
- 中毒与持续伤害改走 `SelfDamage` 后不再附带无敌帧, 相比旧版手感变硬。
- `FrameworkLuaDataProvider` 缓存数据模块, CSV 重新导入后需重启 Play 模式。
- 枪械子类（AK.cs 等）与旧效果 ScriptableObject 保留到换绑与验证完成后删除。
- 真机热更通道待定: 框架真机读 `Resources/LuaBundle.bytes`, 与 Addressables 热更的统一方式需要拍板。

## 七、验收结论

| 轮次 | 日期 | 执行人 | 结果 | 未通过项与原因 |
| --- | --- | --- | --- | --- |
| 1 |  |  |  |  |