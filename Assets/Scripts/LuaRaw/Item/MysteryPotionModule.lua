MysteryPotionModule = {}
MysteryPotionModule.__index = MysteryPotionModule
setmetatable(MysteryPotionModule, {__index = ItemBase})

-- 奇特药水: 随机触发一种已有道具效果.
local RandomEffects = {
    function(self)
        self:HealPlayer(2)
        self:ShowMessage("奇特的效果: 恢复 2 点生命", 1.5)
    end,
    function(self)
        self:AddBuffById(0)
        self:ShowMessage("奇特的效果: 加速生效", 1.5)
    end,
    function(self)
        self:AddBuffById(1)
        self:ShowMessage("奇特的效果: 伤害提升", 1.5)
    end,
    function(self)
        self:AddBuffById(2)
        self:ShowMessage("奇特的效果: 生命上限提升", 1.5)
    end,
    function(self)
        local removedCount = self:RemoveNegativeBuffs()
        if removedCount > 0 then
            self:ShowMessage("奇特的效果: 净化了 " .. tostring(removedCount) .. " 个负面状态", 1.5)
        else
            self:ShowMessage("奇特的效果: 没有可净化的负面状态", 1.5)
        end
    end,
}

function MysteryPotionModule:OnPick()
    local index = math.random(#RandomEffects)
    RandomEffects[index](self)
end

return MysteryPotionModule