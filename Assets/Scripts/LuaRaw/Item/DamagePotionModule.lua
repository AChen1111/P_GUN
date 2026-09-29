require("ItemBase")

DamagePotionModule = {}
DamagePotionModule.__index = DamagePotionModule
setmetatable(DamagePotionModule, {__index = ItemBase})

-- 增伤药水: buffId 由预制体的 DataReference 注入, 默认使用子弹伤害 Buff.
function DamagePotionModule:CanUse()
    return self:GetBuffManager() ~= nil
end

function DamagePotionModule:OnPick()
    self:AddBuffById(self.buffId or 1)
    self:ShowMessage("伤害提升", 1.5)
end

return DamagePotionModule