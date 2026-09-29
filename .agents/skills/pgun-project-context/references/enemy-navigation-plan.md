# 敌人寻路与分离方案

敌人在当前战斗房间里绕开墙走向玩家，并用分离避免叠在一起。不使用整张地牢的导航，也不使用 RVO。

现在的追击在 `Assets/Scripts/Gameplay/Entity/Enemy/Enemys/EnemyBase.cs` 的 `FollowPlayerWithBodySpace`。它读取 `PlayerRegistry.Current` 的坐标，沿直线设置刚体速度，距离小于 `playerStopDistance` 时停住。`EnemyBig.MoveTowardPlayerUntilSafe` 自己写速度，不经过这个方法。门在战斗中关闭，敌人不会离开当前房间。

## 算法分工

| 问题 | 算法 |
| --- | --- |
| 绕开墙走向玩家 | 流场 |
| 走向该敌人自己的最后已知位置 | A* |
| 不要叠在一起 | Boids 的分离 |
| 决定目标点是玩家还是最后已知位置 | 扇形视野加墙体射线 |

流场从目标格子向外搜索一次，每个可行走格记录下一步方向。同一波敌人都追玩家时只算这一次。A* 只在某个敌人的目标格和别人不同时单独计算。分离只修正已经选出的前进方向，不负责绕墙。

聚集力不要加。RVO 先不接。寻路已经绕开墙，RVO 也不能代替流场。

## 每帧顺序

速度最后乘 `EnemyTimeScale`。计时用 `EnemyDeltaTime`。

1. 玩家在扇形内，并且到玩家的射线没有打到 `Wall`，才算看见。看见时目标格是玩家所在格，并写下 `lastSeenPosition`。看不见时目标格改为最后已知位置。走到该格，或追踪时间结束仍看不见，就停住。
2. 目标是玩家时，读取流场在敌人脚下那一格的方向，记为 \(\vec{d}\)。目标是最后已知位置时，用 A* 路径的下一格作为 \(\vec{d}\)。
3. 在分离半径 \(r\) 内累加

\[
\vec{s} = \sum_i \left(1 - \frac{d_i}{r}\right)\frac{\vec{p}-\vec{p}_i}{d_i}
\]

再合成速度

\[
\vec{v} = \operatorname{normalize}(\vec{d} + w_s\vec{s}) \cdot \mathrm{MoveSpeed} \cdot \mathrm{EnemyTimeScale}
\]

\(w_s\) 取 `1.5`。\(r\) 取 `1.5` 到 `2`。距离小于 `playerStopDistance` 时停住，分离不能把敌人再推进玩家。

远程敌人在 `EnemyA`、`EnemyBat`、`EnemyBig` 发射前再做一次到玩家的墙体射线。没有视线就不开枪。近战仍由 `MeleeAttackDetector` 判定命中。

## 格子与重算

可行走格来自当前房间的 `Floor` Tilemap，去掉 `Walls` 上有墙的格子。流场和 A* 都只使用这张图。

玩家跨过一格时重算流场，不每帧重算。A* 在该敌人的目标格变化，或下一格变得不可走时重算。

流场数据放在房间预制体上的组件里，不新建单例，也不在代码里创建管理器物体。战斗结束或房间回收时清掉这张图。

## 代码落点

新代码放在 `Assets/Scripts/Gameplay/Entity/Enemy` 下的导航目录。

- 房间导航组件：从 `Floor` 和 `Walls` 建格子，维护流场。
- `EnemyBase.FollowPlayerWithBodySpace`：改为「视野、流场或 A*、分离」这条顺序，并继续输出朝向。
- `EnemyA`、`EnemyBat`、`EnemyMelee` 继续调用这个方法。
- `EnemyBig.MoveTowardPlayerUntilSafe` 改为调用它，不再单独写 `velocity`。

`playerStopDistance`、`EnemyDeltaTime` 和对象池重置保持不变。敌人死亡或回收时清掉最后已知位置和未走完的 A* 路径。
