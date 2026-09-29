Main = {}
Main.__index = Main

--加载公共模块
require("Include")
--基类先于具体模块加载, 各行为模块只在注册时引用基类全局, 不在模块文件里 require.
require("BuffBase")
require("ItemBase")
require("EnemyBulletBase")
--模块列表
require("module")
local m_module = moduleList