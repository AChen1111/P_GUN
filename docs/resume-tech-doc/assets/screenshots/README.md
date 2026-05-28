# P_GUN 技术文档截图清单

把截图放在本目录后, `docs/resume-tech-doc/index.html` 会自动显示真实图片. 文件名请保持一致.

| 功能 | 文件名 | 建议截图内容 |
| --- | --- | --- |
| 首屏/战斗 | `01-gameplay-combat.png` | 实机战斗图, 包含玩家, 敌人, 子弹, 伤害数字或掉落物. |
| 总体架构 | `02-project-folders.png` | IDE 或 Unity Project 中的 `Assets/Scripts` 模块目录和 asmdef 分层. |
| 数据驱动 | `03-table-importer.png` | TableImporter 窗口或 Excel/CSV 导入到 ScriptableObject Database 的界面. |
| 热更新 | `04-addressables-groups.png` | Addressables Groups, 展示 `Room`, `Buff`, `Item`, `Enemy`, `Weapon`, `Hotfix`, `Shared`. |
| Root 启动 | `05-root-scene-managers.png` | Root 场景 Hierarchy/Inspector, 显示 `DataBaseManager`, `LuaManager`, `AddressableLoader`, `RootHotUpdateController`. |
| 物品背包 | `06-inventory-buff-ui.png` | 背包格子, 物品描述, Buff 图标或使用反馈. |
| 存档读档 | `07-save-slot-panel.png` | 3 个存档槽 UI 或安全屋存档面板. |
| 编辑器工具 | `08-animation-sequence-editor.png` | AnimationSequence 编辑器或 UIAutoBind Inspector. |
| 随机房间 | `09-room-levelgraph.png` | LevelGraph 和房间模板连接关系, 说明地图不是硬编码生成. |
| 随机房间 | `10-generated-dungeon.png` | GameScene 运行时生成出的房间布局和玩家所在房间. |
| 敌人/对象池 | `12-enemy-pool-inspector.png` | EnemyPool 的 prefabInfos, activeRoot 和 inactiveRoot 配置. |
| 敌人 AI | `13-enemy-ai-combat.png` | 敌人追击, 攻击, 死亡掉落或房间战斗状态. |
| 物品效果 | `14-item-effect-assets.png` | 物品 prefab 或 ItemEffectBase 资产列表, 说明效果组合方式. |
| Buff | `15-buff-database.png` | BuffDataBase 中 duration, interval, tag, modifiers 和 LuaFile 配置. |
| Buff UI | `16-buff-status-ui.png` | Buff 图标, 剩余时间, 层数或 Tooltip 说明. |
| 存档数据 | `17-save-json-slot.png` | JSON 存档中的地图 seed, 房间进度, 玩家, 背包和 Buff 数据. |
| SO 数据库 | `18-so-database.png` | Item/Weapon/Enemy/Buff 数据库资产, 说明运行时查询数据来源. |
| xLua Hotfix | `19-hotfix-lua-assets.png` | hotfix/main 和具体 Lua 补丁脚本的 Addressables 地址和标签. |
| xLua Hotfix | `20-xlua-hotfix-code.png` | LuaManager 预加载, AddLoader 或执行 hotfix/main 的关键代码. |
| UI 栈 | `21-ui-stack-hierarchy.png` | GameScene 中 UIStackManager, HUD, Inventory, Settings 等面板层级. |
| UI 自动绑定 | `22-uiautobind-inspector.png` | ComponentAutoBindTool 或生成的 BindComponent 字段绑定结果. |

建议使用 16:9 或 4:3 截图, 宽度至少 1280px. 如果截图包含代码, 只截关键区域, 避免小字号不可读.
