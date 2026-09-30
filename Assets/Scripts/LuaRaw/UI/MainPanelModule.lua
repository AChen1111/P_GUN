MainPanelModule = {}
MainPanelModule.__index = MainPanelModule
setmetatable(MainPanelModule, {__index = PanelBase})

-- 主菜单面板: 四个按钮分别进入游戏、读档、设置和退出.
-- 按钮与面板引用通过 LuaComponet 的 ObjectReference 注入.

function MainPanelModule:OnEnable()
    self:BindButton(self.m_Btn_Start, function() self:StartGame() end)
    self:BindButton(self.m_Btn_Load, function() self:OpenLoadPanel() end)
    self:BindButton(self.m_Btn_Setting, function() self:OpenSettingPanel() end)
    self:BindButton(self.m_Btn_Quit, function() self:ExitGame() end)
end

function MainPanelModule:OnDisable()
    self:UnbindButtons()
end

function MainPanelModule:StartGame()
    if self.isStartingGame then
        return
    end

    self.isStartingGame = true
    CS.UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene")
end

function MainPanelModule:OpenSettingPanel()
    if self.m_SettingsPanel == nil then
        error("MainPanelModule: SettingsPanel 未注入.")
    end

    local stackManager = CS.Game.UI.UIStackManager.Instance
    if stackManager == nil then
        return
    end

    -- 设置界面作为主菜单上的栈式弹窗打开.
    stackManager:Push(self.m_SettingsPanel)
end

function MainPanelModule:OpenLoadPanel()
    if self.m_SaveSlotPanel == nil then
        error("MainPanelModule: SaveSlotPanel 未注入.")
    end

    -- 主菜单只开放读档和删除, 不允许保存空进度.
    self.m_SaveSlotPanel:OpenForMainMenu()
end

function MainPanelModule:ExitGame()
    CS.UnityEngine.Application.Quit()
end

return MainPanelModule