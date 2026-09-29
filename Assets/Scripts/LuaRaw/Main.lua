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

--根据名称初始化一个 table 并返回
function Main.Init(typeName,go)
    local module = m_module[typeName]
    if module == nil then
        print("module not found: " .. tostring(typeName))
        return nil
    end
    local table = {}
    setmetatable(table, module)
    table.gameObject = go
    return table
end

--根据名称调用函数
function Main.CallFunction(table, functionName)
    local func = table[functionName]
    if func ~= nil then
        func(table)
    else
        print("function not found: " .. functionName)
    end
end

--生命周期转发: 模块未定义对应函数时跳过
local function callLifecycle(table, name)
    local func = table[name]
    if func ~= nil then
        func(table)
    end
end

--运行时重新加载
function Main.runtimeReload(typeName)
    --旧表身份以 moduleList 为准; require 失败时 _G 会被清掉, 但 m_module 仍持有引用
    local oldTable = _G[typeName] or m_module[typeName]
    if oldTable == nil then
        print("runtimeReload: module not loaded: " .. tostring(typeName))
        return
    end

    --只清 package.loaded 以强制重执行; 不要先清 _G, 否则 require 失败后模块就丢了
    package.loaded[typeName] = nil
    require(typeName)

    --先清空旧表, 保证源文件里已删除的函数不残留
    local newTable = _G[typeName]
    for k in pairs(oldTable) do
        oldTable[k] = nil
    end
    --把新表内容合并进旧表, 保证所有已持有 oldTable 引用(包括各实例的元表)自动生效
    for k, v in pairs(newTable) do
        oldTable[k] = v
    end
    --合并进来的 __index 指向 newTable, 不改回来的话下次 reload 改的是 oldTable 而实例查的是 newTable
    oldTable.__index = oldTable

    --把 _G 和 package.loaded 都恢复指向旧表, 保证身份一致, 便于下次 reload
    _G[typeName] = oldTable
    package.loaded[typeName] = oldTable
end

--运行时重新加载全部模块
function Main.runtimeReloadAll()
    --先逐个更新已有模块, 单个失败不影响其余模块
    for typeName in pairs(m_module) do
        local ok, err = pcall(Main.runtimeReload, typeName)
        if not ok then
            print("runtimeReloadAll failed: " .. tostring(typeName) .. " , " .. tostring(err))
        end
    end

    --再重跑 Include 与 module, 让新增模块进入 moduleList(已有模块身份不变)
    package.loaded["Include"] = nil
    package.loaded["module"] = nil
    require("Include")
    require("module")
    m_module = moduleList
end

--返回模块名数组, 供 Editor 侧列出
function Main.getModuleNames()
    local names = {}
    for typeName in pairs(m_module) do
        names[#names + 1] = typeName
    end
    return names
end