MysteryPotionModule = {}
MysteryPotionModule.__index = MysteryPotionModule

-- 奇特药水: 具体效果尚未定义, 配置完成前不允许消耗.
function MysteryPotionModule:CanUse()
    CS.UnityEngine.Debug.LogWarning("MysteryPotionModule: 奇特药水的效果尚未配置, 物品暂时不可使用.")
    return false
end

function MysteryPotionModule:OnPick()
end

return MysteryPotionModule