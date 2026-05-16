# AGENTS.md instructions for D:\GameWorkplace\Doing\P_GUN

<INSTRUCTIONS>
书写代码时,必须添加必要的注释,注释为中文描述 + 英文标点.

代码规范:

1. 不要设计过多兜底机制,大多时候允许直接报错或明确暴露配置问题.
2. 写单例时,不要在代码中动态创建 GameObject 或 Manager,而是通过 MCP 或相关 Unity skills 在场景中添加并绑定.
3. 每次新建代码或资源,都要放进正确分类的文件夹和模块中.
4. 有不清楚的实现、玩法规则、架构归属或场景配置,先问用户,不要大范围猜测.
5. 如果设计或新增一个系统,要把该系统的规范、职责、目录和核心接口同步补充进项目 skills.

每次开启新对话并处理本项目时,先读取项目技能:

- `D:\GameWorkplace\Doing\P_GUN\.agents\skills\pgun-project-context\SKILL.md`

当任务涉及代码修改、模块分析、接口查询、Unity 场景/预制体/动画、数据导入、物品/Buff/武器/敌人/UI/对象池/数据库时,继续读取:

- `D:\GameWorkplace\Doing\P_GUN\.agents\skills\pgun-project-context\references\project-architecture.md`
</INSTRUCTIONS>
