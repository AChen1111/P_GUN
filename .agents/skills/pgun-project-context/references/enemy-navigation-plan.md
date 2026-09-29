# 敌人行为重写方案

这是三份计划里的第三份，排在 [Lua 玩法重写计划](lua-gameplay-rewrite-plan.md) 和 [随机房间生成方案](random-room-generation-plan.md) 之后。数值读第一份计划的 `EnemyData`。可行走格来自第二份计划摆好的房间实例，其上的 `Floor` 和 `Walls` 不能被生成器改掉。发射走第一份计划的 `WeaponManager.SpawnEnemyBullet`，不直接调用 `EnemyBulletPool`。

抛弃现有追击和攻击状态里的寻敌逻辑，换成一套新的行为。新行为只负责「看没看见、往哪走、会不会挤、何时出手」。受击、死亡、掉落、对象池和房间清波计数仍留在 `EnemyBase`。

不使用整张地牢导航，也不使用 RVO。战斗中门是关的，导航图只覆盖当前房间。

## 删掉的旧逻辑

这些方法不再保留，也不要在里面改成流场或分离。

- `EnemyBase.FollowPlayerWithBodySpace`。它沿玩家坐标直线设速度。
- `EnemyState` 里仅有的 `Follow` 和 `Attack`。四个敌人子类用这两个状态各自写追击。
- `EnemyA` 的 `DoFollow`、计时后切攻击、朝玩家坐标射击。
- `EnemyBat` 的 `UpdateFollow`、`TryEnterAttackWhenSafe`、`DoFollow`。
- `EnemyMelee` 的 `DoFollow`。检测器仍用于近战命中，但不再由这段追击代码驱动走位。
- `EnemyBig` 的 `MoveTowardPlayerUntilSafe`，以及用 `Follow` / `Attack` 两个名字表示环形弹幕和追踪的状态机。

子类不再实现自己的移动。`RegisterFSM` 不再由 `EnemyA`、`EnemyBat`、`EnemyMelee`、`EnemyBig` 分别注册追击。

## 留下的部分

- `Hurt`、`Dead`、受击闪白、死亡回收、`FightRoom.NotifyEnemyDefeated`、掉落。
- 生命、移速、伤害、掉落概率，以及视野半径、视野角度、追踪时间、分离半径、分离权重、攻击间隔和射程。这些数从 `EnemyData` 读取。缺字段直接失败，不在代码里再写一套默认值。`EnemyDatabase` 不再作为这些数值的来源。
- `EnemyDeltaTime`、`EnemyTimeScale`。子弹时间只拖慢新行为的计时和位移。
- 对象池的 `OnSpawnFromPool` / `OnRecycleToPool`。新行为的运行时数据在回收时清空。
- 动画参数 `Speed`、攻击触发、死亡触发。新行为只调用现有的设速度和播攻击动画方法。
- 近战的 `MeleeAttackDetector` 预制体。它只回答「这一下有没有打到玩家」。

## 新结构

新代码放在 `Assets/Scripts/Gameplay/Entity/Enemy/Brain`。房间导航放在 `Assets/Scripts/Gameplay/Room`。不新建单例，不在代码里创建管理器。

### 实现偏离说明

落地时相对下表有三处有意偏离，功能不受影响：

- `EnemyNavigator` 没有独立成类，流场与 A* 的读取逻辑并入 `EnemyBrain`，避免一层只做转发的壳。
- `EnemyBrain` 是 `EnemyBase` 持有的普通类，不是挂在敌人预制体上的组件，敌人预制体因此零改动；热修注入名单里照样可用。
- `EnemyAttack` 以 `IEnemyAttack` 接口由敌人子类直接实现，不新增独立组件，同样是为了避免预制体操作。

| 类型 | 职责 |
| --- | --- |
| `RoomWalkGrid` | 挂在战斗房间上。用 `Floor` 去掉 `Walls` 得到可行走格 |
| `RoomFlowField` | 同一组件内。玩家所在格变化时，从该格向外搜索，每个空格记录下一格方向 |
| `EnemyPerception` | 扇形视野、墙体射线、最后已知位置、追踪剩余时间 |
| `EnemyNavigator` | 目标是玩家时读流场；目标是最后已知位置时跑 A*，输出单位方向 |
| `EnemySeparation` | 按同伴距离产生推开向量 |
| `EnemyBrain` | 挂在敌人预制体上，替换旧的追击状态机。每帧按感知、寻路、分离、攻击的顺序执行 |
| `EnemyAttack` | 子类只实现这一抽象。决定攻击距离、冷却、是否停步，以及打出什么样的子弹或近战伤害 |

`EnemyBase.Update` 在未死亡时调用 `EnemyBrain`，不再 `FSM.Update()` 跑旧的 `Follow` / `Attack`。

## 状态

`EnemyBrain` 使用四个状态。

- `Idle`：没有看见玩家，也没有在追踪。速度为 0。
- `Chase`：当前看得见玩家。用流场方向移动。
- `Search`：曾经看见，现在看不见。用 A* 走向 `lastSeenPosition`。
- `Attack`：到达攻击条件。调用 `EnemyAttack`。攻击要求停步时速度为 0，否则仍用 `Chase` 的移动。

转换：

1. `Idle` 中本帧看见玩家，进入 `Chase`。
2. `Chase` 中丢失视线，记下位置，进入 `Search`。
3. `Search` 走到目标格，或追踪时间耗尽仍看不见，进入 `Idle`。追踪时间用 `EnemyDeltaTime` 累计，时长来自 `EnemyData`。
4. `Search` 中重新看见玩家，进入 `Chase`，并刷新最后已知位置。
5. `Chase` 或 `Search` 中满足该敌人的 `EnemyAttack.CanAttack`，进入 `Attack`。
6. `Attack` 的锁定时间结束，若仍看得见就回 `Chase`，否则回 `Search`。

死亡和回池时强制离开所有状态，清掉最后已知位置、A* 路径和攻击计时。`Start` 不会在对象池复用时再次执行，这些数据放在 `OnSpawnFromPool` 重置。

## 感知

看见必须同时满足：

- `PlayerRegistry.Current` 存在。
- 玩家在 `EnemyData` 的视野半径内。
- 玩家位于面朝方向的扇形内。角度来自 `EnemyData`。面朝由精灵 `flipX` 决定，朝右为 `0` 度，朝左为 `180` 度。
- 从敌人位置到玩家的射线没有打到 `Wall`。

看不见时，不允许把速度直接设成指向玩家当前坐标，也不允许朝该坐标开火。

## 寻路

`RoomWalkGrid` 在房间初始化时建格。第二份计划生成的房间实例上必须仍有 `Floor` 和 `Walls`。`Floor` 上的格子默认可走，`Walls` 上有瓦片的格子不可走。建格失败或房间没有这两张 Tilemap 时直接报错。流场不跨过走廊，不把相邻房间连进同一张图。

流场只服务「当前看得见、目标是玩家」的敌人。玩家跨过一格时重算一次。每个格子保存指向更近一格的方向。敌人读自己脚下格子的方向，得到 \(\vec{d}\)。脚下格子不可走时，改读最近的可走格；仍然没有可走格就停住并报错一次。

`Search` 的目标格是 `lastSeenPosition` 所在格。从敌人脚下到该格做 A*，四方向，只走可走格。\(\vec{d}\) 取路径下一格的方向。目标格变化，或下一格变得不可走时，重新搜索。没有路径就停住，本段追踪结束，回到 `Idle`。

不把流场存在静态全局里。它在房间组件上，房间禁用时清空。

## 分离

只在 `Chase` 和需要边走边打的 `Attack` 里叠加。`Search` 同样叠加，避免追踪时挤成一线。

邻居是分离半径内、未死亡的其他敌人。半径和权重 \(w_s\) 来自 `EnemyData`。

\[
\vec{s} = \sum_i \left(1 - \frac{d_i}{r}\right)\frac{\vec{p}-\vec{p}_i}{d_i}
\]

\[
\vec{v} = \operatorname{normalize}(\vec{d} + w_s\vec{s}) \cdot \mathrm{MoveSpeed} \cdot \mathrm{EnemyTimeScale}
\]

不加对齐，不加聚集。与玩家距离小于 `playerStopDistance` 时速度为 0，分离不能把敌人推进玩家。

刚体速度只由 `EnemyBrain` 写入。子类攻击代码禁止再写 `velocity`。

## 四种攻击

移动规则四种敌人相同。差别只在 `EnemyAttack`。

- `EnemyA`：射程内且有视线时，按 `shootInterval` 向玩家方向发射一发。攻击时不强制停步。
- `EnemyBat`：进入偏好距离或攻击锁定后停步，延迟后发射扇形弹。弹数和夹角沿用现在预制体上的数值。墙挡住视线时不发射。
- `EnemyMelee`：`MeleeAttackDetector` 碰到玩家且冷却结束时停步，播放攻击动画，并在原来的出伤时机造成伤害。检测盒仍跟面朝方向翻转。
- `EnemyBig`：攻击模块自己分两段。环形弹幕段停步并按间隔向四周发射；点射段允许移动，并按间隔朝有视线的玩家发射一发。两段时长沿用现在的 `radialStateDuration` 和 `chaseStateDuration`。走位仍由 `EnemyBrain` 决定，点射段没有视线就只移动、不发射。

发射调用 `WeaponManager.SpawnEnemyBullet`，把预制体、位置、方向和伤害传进去。攻击模块不调用 `EnemyBulletPool`。子弹飞行仍由第一份计划里的子弹 C# 速度写入负责。

## 完成标准

- 四个敌人预制体上的追击都来自 `EnemyBrain`。工程里不再调用 `FollowPlayerWithBodySpace`。
- 玩家在墙后时，敌人不直接穿墙锁定坐标；有路径时沿格子绕行，没有路径时停住。
- 同一波敌人不会重叠在同一个点上。
- 丢失视线后只走到最后已知位置，超时后停止，不会继续跟踪玩家新坐标。
- 子弹时间只拖慢移动和攻击计时。
- 死亡后房间计数、掉落和对象池复用与现在一致。复用出来的敌人没有上一次的路径和视野记忆。
