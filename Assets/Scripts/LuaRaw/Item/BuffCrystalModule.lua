BuffCrystalModule = {}
BuffCrystalModule.__index = BuffCrystalModule
setmetatable(BuffCrystalModule, {__index = ItemBase})

-- Buff 结晶的 itemId 从 10 开始, 对应 buffId 从 0 开始.
function BuffCrystalModule:CanUse()
    return self:GetBuffManager() ~= nil and self.itemId >= 10 and self.itemId <= 24
end

function BuffCrystalModule:OnPick()
    local buffId = self.itemId - 10
    self:AddBuffById(buffId)
    self:ShowMessage("获得 Buff: " .. tostring(buffId), 1.5)
end

return BuffCrystalModule
