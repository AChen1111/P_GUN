HealEffectModule = {}
HealEffectModule.__index = HealEffectModule

-- 治疗药水: healAmount 由预制体的 DataReference 注入.
function HealEffectModule:CanUse()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    return player ~= nil and not player.IsHPFull
end

function HealEffectModule:OnPick()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    if player == nil then return end

    local healed = player:Heal(self.healAmount or 1)
    CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage("恢复 " .. tostring(healed) .. " 点生命", 1.5)
end

return HealEffectModule