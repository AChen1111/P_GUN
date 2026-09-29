BloodTomeModule = {}
BloodTomeModule.__index = BloodTomeModule
setmetatable(BloodTomeModule, {__index = ItemBase})

-- 血契宝典: 永久提升攻击, 代价是生命上限; 叠到保底生命时不可再使用.
function BloodTomeModule:CanUse()
    local player = self:GetPlayer()
    return player ~= nil and player.MaxHP > 1
end

function BloodTomeModule:OnPick()
    self:AddBuffById(11)
    self:ShowMessage("血契生效: 攻击提升, 生命上限降低", 1.5)
end

return BloodTomeModule