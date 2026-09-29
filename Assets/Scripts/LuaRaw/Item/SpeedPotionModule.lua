require("ItemBase")

SpeedPotionModule = {}
SpeedPotionModule.__index = SpeedPotionModule
setmetatable(SpeedPotionModule, {__index = ItemBase})

-- 加速药水: buffId 由预制体的 DataReference 注入, 默认使用移速 Buff.
function SpeedPotionModule:CanUse()
    return self:GetBuffManager() ~= nil
end

function SpeedPotionModule:OnPick()
    self:AddBuffById(self.buffId or 0)
    self:ShowMessage("加速生效", 1.5)
end

return SpeedPotionModule