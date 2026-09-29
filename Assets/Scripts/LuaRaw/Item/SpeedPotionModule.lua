SpeedPotionModule = {}
SpeedPotionModule.__index = SpeedPotionModule

-- 加速药水: buffId 由预制体的 DataReference 注入, 默认使用移速 Buff.
function SpeedPotionModule:CanUse()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    return player ~= nil and player.buffManager ~= nil
end

function SpeedPotionModule:OnPick()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    if player == nil then return end

    player.buffManager:AddBuffById(self.buffId or 0)
    CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage("加速生效", 1.5)
end

return SpeedPotionModule