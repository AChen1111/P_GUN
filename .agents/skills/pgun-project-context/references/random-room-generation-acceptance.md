# 随机房间生成验收文档

验收对象是 `RandomRoomGenerator` 替换 Edgar 布局后的房间生成流程。代码已落地，验收在 Unity 编辑器内执行。逐项核对，任何一项不通过即停止验收并记录原因。

## 前置条件

以下操作未完成时，验收无法开始。

1. 计划一的待办清单（`lua-rewrite-pending-unity-ops.md`）完成第 1、2、8、9 节：Root 场景的 `LuaManager` 与 `PgunLuaRuntimeBootstrap`，CSV 导入生成的 Data Lua，XLua 注入。
2. 本计划自身的 Unity 操作（见文末清单）完成：GameScene 挂 `RandomRoomGenerator`，房间预制体补四个门锚点，走廊预制体引用已拖好，房间预制体地址已注册进 Addressables。
3. `Assets/csv/LevelConfig.csv` 与 `Assets/csv/SpawnData.csv` 至少各有一行有效数据，且执行过 `Tools/UnityEasyWorkTools/CSV To Lua Table/Import All`。

## 一、编译与启动

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 1.1 | 打开工程等待编译 | 无编译错误；Console 无 `LuaComponet`、`LuaDataRuntime` 相关红色报错 |
| 1.2 | 从 Root 场景启动进入 GameScene | 生成流程无异常；Console 出现生成完成前无 `生成房间失败` 报错 |
| 1.3 | GameScene 不再依赖 Edgar | `DungeonGeneratorGrid2D` 与 `AddressableDungeonBootstrapper` 可以从场景移除后游戏仍可运行（本项在场景切换完成后勾选） |

## 二、关卡配置与房间分配

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 2.1 | 统计生成后的房间数 | 等于 `LevelData.lua` 中该关卡 `roomCount`，一间不多一间不少 |
| 2.2 | 找到出生房 | `(0,0)` 格位置是 `InitRoom`，玩家出生在该房间 |
| 2.3 | 找到终点房 | 与出生房格子距离最远的一间是 `FinalRoom`，且只有一间 |
| 2.4 | 统计宝箱房与存档房 | 数量分别等于 `chestCount` 与 `saveCount`，优先落在死胡同 |
| 2.5 | 统计普通战斗房 | 数量等于 `normalCount`，预制体从 `normalPrefabs` 列表抽取 |
| 2.6 | 连通性 | 从出生房出发，穿过门与走廊能走到每一间房，没有孤立房间 |

## 三、种子一致性

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 3.1 | 同一关卡连续开新局两次，对比布局 | 两次布局不同（种子随机） |
| 3.2 | 用同一个 seed 调 `RandomRoomGenerator.OverrideLevel` 连续生成两次 | 两次的房间格子、类型、门位置完全一致 |
| 3.3 | 修改 `roomCount` 后重新导入 CSV 再生成 | 房间数随之变化，分配规则仍满足第二节的数量关系 |

## 四、走廊与门

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 4.1 | 检查每对相邻房间 | 之间有且仅有一条走廊；水平边是 `LRCorridor`，垂直边是 `UDCorridor` |
| 4.2 | 检查走廊对齐 | 走廊两端与两侧 `DoorAnchor` 对齐，无悬空、无穿模、无拉伸 |
| 4.3 | 检查非相邻房间 | 共用墙的方向没有走廊也没有门 |
| 4.4 | 统计每间房的门 | 门数等于该房间的邻居数，位置在对应锚点上 |
| 4.5 | 进战斗房 | 关门生效；清完波次后开门生效；波次 UI 正常 |
| 4.6 | 走廊与房间碰撞 | 玩家能穿过走廊，不能穿墙；敌人子弹会被走廊墙挡住 |

## 五、存档与读档

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 5.1 | 在安全房按存档 | 槽位文件包含 `levelId` 与 `mapSeed`；`levelGraphAddress` 字段不再出现 |
| 5.2 | 在战斗中按存档 | 存档失败并提示，不产生槽位文件 |
| 5.3 | 读取 5.1 的存档 | 房间布局、类型、门状态与存档时一致（同 levelId 同 seed 重建） |
| 5.4 | 已清空的战斗房读档 | 不再刷怪，也不重复结算清房奖励；`SaveRoomId` 形如 `NormalRoom_2_-1` |
| 5.5 | 已进入过的宝箱房读档 | 不重复生成宝箱 |
| 5.6 | 存档后走远再读档 | 玩家位置、血量、背包、Buff、武器弹药恢复正确 |
| 5.7 | 用旧版本存档（含 `levelGraphAddress`）读档 | 明确报错并中止，不静默生成错误地图 |

## 六、小地图

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 6.1 | 检查每间房的 `MinimapRoomData` | `Positions` 非空，格子来自该房 Floor Tilemap |
| 6.2 | 走进不同房间 | 当前房间高亮，上一间的高亮被清除 |

## 七、错误暴露

以下操作应直接报错，报错信息包含缺失项的名字。出现静默跳过即不通过。

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 7.1 | `LevelConfig.csv` 临时删除 `level1` 行后导入并生成 | 报缺少关卡行，不生成任何房间 |
| 7.2 | 某房间预制体不挂 `DoorAnchor_E`，生成带东邻居的布局 | 报缺少 `DoorAnchor_E`，不在此处开门 |
| 7.3 | 生成器不拖走廊预制体 | 报走廊预制体未配置，生成中止 |
| 7.4 | `LevelConfig.csv` 五类数量之和与 `roomCount` 不等后导入 | CSV 导入直接失败并指出行号 |
| 7.5 | 房间预制体没有 BoxCollider2D | 报无法测量外框，生成中止 |

## 八、回归项

计划一的功能不能因本计划回归。

| 项 | 操作 | 通过标准 |
| --- | --- | --- |
| 8.1 | 拾取并使用道具 | 背包消耗条件、效果执行与计划一验收一致 |
| 8.2 | 开火换弹 | 各枪弹道、弹药 UI、换弹音效正常 |
| 8.3 | 战斗流程 | 波次刷怪、敌人掉落、清房奖励与迁移前一致 |
| 8.4 | 子弹时间 | 只拖慢敌人移动、攻击计时、敌人子弹与动画 |

## 已知限制

- 小地图的基础显示层原来由 Edgar 的 `MinimapPostProcess` 生成，随机生成路径不再执行它。`MinimapRoomData` 数据已填充，但小地图底图需要单独补一套渲染方案，验收第六节只核对数据与高亮。
- 房间预制体尺寸不一致时，走廊按最大外框计算步长，个别房间边缘可能与走廊留缝。出现错位先调步长（改走廊长度或外框），不拉伸 Tilemap。
- `AddressableDungeonBootstrapper` 仍在工程中，但没有存档重建能力，只用于临时对照，验收通过后可以从场景与工程中移除。

## 本计划自身的 Unity 操作

- [ ] GameScene 移除 `AddressableDungeonBootstrapper` 与 `DungeonGeneratorGrid2D`，挂 `RandomRoomGenerator`（`Assets/Scripts/Gameplay/Room/Generation/RandomRoomGenerator.cs`），`levelId` 填 `level1`。
- [ ] 生成器 Inspector 拖入 `lrCorridorPrefab` 与 `udCorridorPrefab`（`Assets/Prefab/Room/CorridorTemplate/`）。
- [ ] 生成器的 `minimapLayer` 填 MiniMap 图层（默认 0 会落到 Default 层, 小地图相机可能不渲染高亮）。
- [ ] 五类房间预制体各补四个空子物体：`DoorAnchor_N`、`DoorAnchor_E`、`DoorAnchor_S`、`DoorAnchor_W`，放在对应墙边门口中心。
- [ ] 房间预制体注册进 `Room` 分组，地址与 `LevelConfig.csv` 一致（`room/init`、`room/final`、`room/chest`、`room/save`、`room/normal`），标签带 `room;hot_update`。
- [ ] 房间预制体确认有根 `BoxCollider2D`（外框）与名为 `Floor` 的 Tilemap，以及 `MinimapRoomData` 组件。
- [ ] `NormalRoom` 预制体按计划一挂好 `LuaComponet`（`RoomRewardModule`）并填 `spawnTableId`。

## 验收结论

| 轮次 | 日期 | 执行人 | 结果 | 未通过项与原因 |
| --- | --- | --- | --- | --- |
| 1 |  |  |  |  |