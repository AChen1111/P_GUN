CleansePotionModule = {}
CleansePotionModule.__index = CleansePotionModule

-- 净化药水: 没有负面 Buff 时不允许消耗.
function CleansePotionModule:CanUse()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    if player == nil or player.buffManager == nil then
        return false
    end

    local activeBuffs = player.buffManager.ActiveBuffs
    for i = 0, activeBuffs.Count - 1 do
        local info = activeBuffs[i]
        if info ~= nil and info.ParsedTag == CS.Game.Gameplay.BuffTag.Negative then
            return true
        end
    end

    return false
end

function CleansePotionModule:OnPick()
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    if player == nil or player.buffManager == nil then return end

    local removedCount = player.buffManager:RemoveBuffsByTag(CS.Game.Gameplay.BuffTag.Negative)
    if removedCount > 0 then
        CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage("净化了 " .. tostring(removedCount) .. " 个负面状态", 1.5)
    else
        CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage("没有可净化的负面状态", 1.5)
    end
end

return CleansePotionModule