DamagePotionModule = {}
DamagePotionModule.__index = DamagePotionModule

-- 增伤药水: buffId 由预制体的 DataReference 注入, 默认使用子弹伤害 Buff.
function DamagePotionModule:CanUse()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    return player ~= nil and player.buffManager ~= nil
end

function DamagePotionModule:OnPick()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    if player == nil then return end

    player.buffManager:AddBuffById(self.buffId or 1)
    CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage("伤害提升", 1.5)
end

return DamagePotionModule