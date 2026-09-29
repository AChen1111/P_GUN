require("ItemBase")

HealEffectModule = {}
HealEffectModule.__index = HealEffectModule
setmetatable(HealEffectModule, {__index = ItemBase})

-- 治疗药水: healAmount 由预制体的 DataReference 注入.
function HealEffectModule:CanUse()
    local player = self:GetPlayer()
    return player ~= nil and not player.IsHPFull
end

function HealEffectModule:OnPick()
    local healed = self:HealPlayer(self.healAmount or 1)
    self:ShowMessage("恢复 " .. tostring(healed) .. " 点生命", 1.5)
end

return HealEffectModule