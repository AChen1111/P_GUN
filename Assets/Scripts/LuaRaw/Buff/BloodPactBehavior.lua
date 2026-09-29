require("BuffBase")

BloodPactBehavior = {}
BloodPactBehavior.__index = BloodPactBehavior
setmetatable(BloodPactBehavior, {__index = BuffBase})

-- 血偿: 攻击提升, 代价是每隔一段时间按层数掉血.
function BloodPactBehavior:OnAdd()
    self:ShowMessage("血偿生效: 攻击提升50%", 1.5)
end

function BloodPactBehavior:OnRemove()
end

function BloodPactBehavior:OnInterval()
    self:HurtOwner(self:GetStackCount())
end

return BloodPactBehavior