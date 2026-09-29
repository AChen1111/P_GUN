require("ItemBase")

CleansePotionModule = {}
CleansePotionModule.__index = CleansePotionModule
setmetatable(CleansePotionModule, {__index = ItemBase})

-- 净化药水: 没有负面 Buff 时不允许消耗.
function CleansePotionModule:CanUse()
    return self:HasNegativeBuff()
end

function CleansePotionModule:OnPick()
    local removedCount = self:RemoveNegativeBuffs()
    if removedCount > 0 then
        self:ShowMessage("净化了 " .. tostring(removedCount) .. " 个负面状态", 1.5)
    else
        self:ShowMessage("没有可净化的负面状态", 1.5)
    end
end

return CleansePotionModule