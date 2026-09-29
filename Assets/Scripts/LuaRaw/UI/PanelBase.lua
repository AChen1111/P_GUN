PanelBase = {}
PanelBase.__index = PanelBase

-- 栈流程生命周期: 由 UIPanelBase 在 C# 逻辑之后转发.
function PanelBase:OnOpen()
end

function PanelBase:OnClose()
end

function PanelBase:OnPause()
end

function PanelBase:OnResume()
end

-- 绑定按钮并登记, 面板禁用时统一解绑.
function PanelBase:BindButton(button, handler)
    if button == nil then
        error("PanelBase: 按钮未注入, 检查 LuaComponet 的 ObjectReference.")
    end

    if self.boundButtons == nil then
        self.boundButtons = {}
    end

    button.onClick:AddListener(handler)
    self.boundButtons[#self.boundButtons + 1] = { button = button, action = handler }
end

-- 解绑全部已登记的按钮监听.
function PanelBase:UnbindButtons()
    if self.boundButtons == nil then
        return
    end

    for i = 1, #self.boundButtons do
        local entry = self.boundButtons[i]
        if entry.button ~= nil then
            entry.button.onClick:RemoveListener(entry.action)
        end
    end

    self.boundButtons = nil
end