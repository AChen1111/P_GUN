# 代码精简与单一职责优化清单

本文基于全项目 `Assets/Scripts/` 静态阅读, 并对照最近三次对象池相关提交 (`056e127`, `eabdd87`, `6fca29b`) 中你在 `PlayerBullet` / `PoolBase` 上体现的删改偏好整理.  
**每一条仅描述可优化点, 不附带 patch; 采纳前请在分支上逐条评审.**

标点统一为英文标点, 与本仓库近期注释风格一致.

---

## 一, 你从近三次提交中体现的六种删除模式

| 标签 | 含义 |
| --- | --- |
| **Pattern A** | `GetComponent` / `rb == null` 等运行时组件回填 |
| **Pattern B** | 参数 null, `gameObject != null`, `layer != -1` 等与 Unity 语义重叠的兜底 |
| **Pattern C** | `OnDisable`, `Reset` 里"以防万一"的清理或与池回调重复的副作用 |
| **Pattern D** | 调试日志, 注释掉的 `Debug.Log`, 生产路径上的 `Debug.Log` |
| **Pattern E** | 运行时自动 `new GameObject` 做单例兜底, `Resources.Load`/自动创建 UI 等绕过配置的路径 |
| **Pattern F** | 中间聚合方法或池回调与调用方重复的同一职责 (重复 `StopMove`, 双层 `Recycle` 等) |

---

## 二, 对象池子类 (`Pattern E`, `Pattern F`)

### 2.1 四个池的 `Instance` 自动创建

以下均在 `instance == null` 时 `new GameObject(...)` + `AddComponent`:

- `Assets/Scripts/Gameplay/Entity/Pool/PlayerBulletPool.cs` L8-17 — **Pattern E**
- `Assets/Scripts/Gameplay/Entity/Pool/EnemyPool.cs` L8-16 — **Pattern E**
- `Assets/Scripts/Gameplay/Entity/Pool/EnemyBulletPool.cs` L8-16 — **Pattern E**
- `Assets/Scripts/Presentation/VfxPool.cs` L4-13 — **Pattern E**
- `Assets/Scripts/Items/ItemPool.cs` L8-20 — **Pattern E**

**建议:** 改为仅转发基类静态实例, 缺场景配置时及早失败暴露问题, 例如:

```text
public new static XxxPool Instance => PoolBase<T>.Instance as XxxPool;
```

你已遇到过 `prefabInfos` 未配时 `[0]` 越界, 自动生成空池会放大这类静默错误.

### 2.2 `GameObject` → 组件兼容重载 (`Pattern F` + **Pattern B**)

- `Assets/Scripts/Gameplay/Entity/Pool/EnemyPool.cs` L19-34 — **Pattern F**, L23-31 亦为 **Pattern B** (null 与 `GetComponent` 分支)
- `Assets/Scripts/Gameplay/Entity/Pool/EnemyBulletPool.cs` L19-34 — **Pattern F**, **Pattern B**
- `Assets/Scripts/Items/ItemPool.cs` L23-38 — **Pattern F**, **Pattern B**

**建议:** 调用方一律传入强类型 prefab (`EnemyBase`, `EnemyBullet`, `Item`), 删除上述重载, 错误在编译期或单一入口报错.

---

## 三, `EnemyBullet` (与精简后 `PlayerBullet` 对标)

路径: `Assets/Scripts/Gameplay/Entity/Enemy/EnemyBullet.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L35-37, L43-45 | `rb == null` 回填 | **A** |
| L63-65 | `Reset()` 里 `AddComponent<CircleCollider2D>` | **C** |
| L58-60 | `LogHit` 调试调用 | **D** |
| L76 | `gameObject != null` (协程尾) | **B** |
| L82 | `target == null` | **B** |
| L98 | `wallLayer != -1 &&` | **B** |
| L68 | `rb == null` 早退 | **A** |
| L111-116 | `Recycle()` 与 `OnRecycleToPool` 重复收尾 | **F** |
| L121-125 | `StopMove()` public + `rb != null` | **A** (可内化) |
| L127-130 | `OnDisable` 协程 / 速度清理 | **C** |

**建议:** 对齐当前 `PlayerBullet` 的结构: `Init` 只设方向; 协程与 `Release` 在池侧或单一私有方法收口; 去掉调试与重复清理.

---

## 四, `EnemyBase`

路径: `Assets/Scripts/Gameplay/Entity/Enemy/Enemys/EnemyBase.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L49-59 | `Reset()` 挂载四件套组件 | **C** → 更合适用 `[RequireComponent(...)]` 由 Unity 管线保证 |
| L65-67 | `sr == null` 再 `GetComponentInChildren` | **A** |
| L112 | `damageInfo == null` 新建 | **B** |
| L114, L152 | 注释调试 | **D** |
| L189-191 | `col != null` | **B** vs 若强制 Serialize 可删掉 |
| L194-199, L201-213 | `hasDefaultSpriteColor` 与多级 `sr == null` | **B** |
| L216-220 | `rb != null` | **B** |
| L222-231 | `GetBloodVfxPosition` 三段 fallback | **B** |

**建议:** `Hurt` 要求非 null `DamageInfo`; 视觉效果单一数据源 (仅 `col` 或仅 `sr`).

---

## 五, `Player`

路径: `Assets/Scripts/Gameplay/Entity/Player/Player.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L91-95 | `Reset()` AddComponent + tag | **C** |
| L64-70 | `Awake` 里先 `GetComponentInChildren<Animator>` 再 `ResolveAnimator` | **A** (与 L97-110 双层解析) |
| L97-110 | `ResolveAnimator` 三层 fallback + `LogWarning` | **A**, **D** |
| L222 | `damageInfo == null` | **B** |
| L337-356 | `guns == null`/元素 null skip | **B** |
| L366-377 | `GetBloodVfxPosition` 三段 fallback | **B** |

**建议:** Animator 仅用 `[SerializeField]` 必填; Q/E 切枪两段重复代码可抽 `SwitchGun(int delta)` — **单一职责 / 简写** (Plan 第七节).

---

## 六, `Item`

路径: `Assets/Scripts/Items/Item.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L45-53 | `_dotweenAnimation` / `_animator` Awake 回填 | **A** |
| L56-62 | `Reset()` 编辑器占位 | **C** |
| L74-77 | `OnDisable` → `HideTip`; `OnRecycleToPool` 已 `HideTip` | **C** (与池回收重叠时需确认是否只保留一端) |
| L235-238 | `IsPlayer` 内 `other != null` | **B** |

---

## 七, `Room` / `FightRoom`

### `Assets/Scripts/Gameplay/Room/RoomBase/Room.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L45-52 | `if (itemSpawner == null)` 二元赋值 | **B** → 可简写为 `canGenerateItems = itemSpawner != null` |
| L56, L118, L134 | 注释调试 | **D** |
| L169-173 | `roomCenterPoint == null` | **B** |
| L176-181 | `Reset()` `GetOrAddComponent` | **C** |

### `Assets/Scripts/Gameplay/Room/RoomBase/FightRoom.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L140-141 | 注释调试 + todo | **D** |
| L167 | `Debug.Log("enemyList.Count: "...)` — **仍在生产路径** | **D** |
| L166 | `playerTransform == null` | **B** |
| L170-196 | `invalidEnemies` 延迟清理列表 | **B** 复杂版 — 可用 `RemoveWhere` 或单独维护集合缩短逻辑 |
| L203-206 | `OnDisable` 清 `currentFightRoom` | **建议保留** — 避免跨房间陈旧引用 (见下文「建议保留」) |

---

## 八, UI / 全局音频

### `Assets/Scripts/UI/GameUI.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L41-44 | `OnDestroy` 再次 `RemoveButtonListeners` / `RemoveEventListeners` | **C** (通常 `OnDisable` 已执行) |
| L144-154 | `ResolveItemTipPanel` 找不到则 `ItemTipPanel.CreateDefault` | **E** |

### `Assets/Scripts/Presentation/GlobalAudioPlay.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L37-45 | Load 后字典项为 null 时 `LogError` + `return` (当前无无限递归, 但二次调用自身仍属隐式重试) | **E** / **D** — 可改为显式单次加载失败分支 |
| L59-63 | `clip == null` LogError | **B**, **D** |
| L76 | `clip == null \|\| SelfAudioSource == null` | **B** |

---

## 九, `PoolBase` 与池基础设施

路径: `Assets/Scripts/Pooling/PoolBase.cs`

| 行号 | 说明 | Pattern |
| --- | --- | --- |
| L137-141 | `prefab == null` Log + return | **B**, **D** — 与你的「缺配置就让错误暴露」策略可二选一 |
| L162-163 | `item == null \|\| !activeSelf` 静默 return | **B** — 去掉 inactive 静默返回可使 `collectionChecks` 抓到重复 Release |
| L170-171 | 映射缺失时 `Destroy` | **E** — 若约定「只允许本池创建的对象」, 可改为 assert / throw |
| L237-238 | `Mathf.Max` 校正 capacity / size | **B** — Inspector 必填正确值则由构造参数直接沿用 |
| L114 | `foreach (prefabInfos)` — `prefabInfos` 若为 null 会 NRE | 配置约束提醒 (非兜底, 属数据契约) |
| L57 | `prefabInfos[0]` — 列表为空仍会越界 | 与上文 **E** 自动生成池组合时高发; 需在场景或运行时契约中保证非空 |

`Awake` 中若 `inactiveRoot` / `activeRoot` 未在 Inspector 赋值, `CreateItem` / `OnTakeFromPool` 会在 `Instantiate`/ `SetParent` 处出错 — **已删除自动 `EnsureRoots` 后更显式**, 清单不视为「可删兜底」, 而是**必须配置**.

---

## 十, 微优化 (单一职责 / 写法缩短)

| 路径 | 行号 | 说明 |
| --- | --- | --- |
| `Assets/Scripts/Gameplay/Entity/Player/Weapon/Gun/Gun.cs` | L189-224 | 两个 `TryPlaySound` 重复 Stop / Play 流程, 可抽私有 `PlayClip(AudioClip clip, bool loop)` |
| `Assets/Scripts/Gameplay/Entity/Player/Player.cs` | L191-207 | Q/E 切枪对称逻辑 → `SwitchGun(int delta)` |
| `Assets/Scripts/Gameplay/Entity/Player/Weapon/Feature/GunFire.cs` | L8-15 | `WeaponGlobal.Instance.GunFire` 多次访问 → 缓存局部变量 |
| `Assets/Scripts/Items/ItemSpawner.cs` | L16-19, L29 | `LogWarning` 缺表 — 可考虑与 **Pattern E** 一致改为强失败 (抛异常或 assert) |

---

## 十一, `PlayerBullet` 当前对齐情况 (对照你的提交偏好)

路径: `Assets/Scripts/Gameplay/Entity/Player/PlayerBullet.cs`

- L28-31 协程起点在 `OnSpawnFromPool`, L20-23 `Init` 只管数据 — **与「池取出再启协程」方向一致**.
- L72-73, L93 直接 `Pool.Instance.Release` — **仍可讨论**是否与「子弹只标记结束, 池订阅」的更严 SRP 冲突; 你已选择单池 + 直调, 本条仅作取舍记录.

---

## 十二, 建议保留的兜底 (不宜机械删除)

以下内容删除后容易引发跨系统 bug, **不建议照搬「删掉所有 if」**:

1. **`EventCenter`**: `listener == null` 忽略, `Trigger` 里 `listener?.Invoke()` — 防止订阅方写法错误拖垮派发 (`Assets/Scripts/Core/EventCenter.cs` L50, L92 等).

2. **`FightRoom.NotifyEnemyDefeated`**: `enemy` / `OwnerFightRoom` 空时使用 `currentFightRoom` — **路由语义** (`FightRoom.cs` L108-112).

3. **`FightRoom.GetNearestEnemy`**: Unity 对已 Destroy 对象的 **fake null** 过滤 — **必要** (`FightRoom.cs` L174-180); 可删掉的是 L167 **调试 Log**, 不是过滤本身.

4. **`FightRoom.OnDisable`**: 清静态 `currentFightRoom` (`L203-206`) — **建议保留**.

5. **对象池映射字典**: `_instance2Pool` / `_instance2Prefab` 在 `DestroyItem` 中 Remove — **资源一致性**, 非冗余兜底.

---

## 十三, 文档与维护范围说明

- 本清单**未扫描** `Assets/Extern/` 等第三方目录.
- 未修改任何 `.cs` / `.unity` / `.prefab`; 与你的 Plan「仅产出文档」一致.
- `Assets/Scripts/Pooling/PoolBase.md` 若仍描述自动创建 `Active`/`Inactive` 节点, 与当前删除了 `EnsureRoots` 的实现可能不一致 — 后续改文档时注意同步.

---

**下一步:** 在专用分支按章节勾选条目, 每改一类跑一遍关卡内「开枪 → 命中敌人 → VFX」「物品生成」「音频」Smoke 路径.
