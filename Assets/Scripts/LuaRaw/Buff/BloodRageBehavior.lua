BloodRageBehavior = {}
BloodRageBehavior.__index = BloodRageBehavior
setmetatable(BloodRageBehavior, {__index = BuffBase})

-- 血怒: 添加瞬间扣当前生命的 30%, 保底剩 1 点.
function BloodRageBehavior:OnAdd()
    local owner = self:GetOwner()
    if owner == nil then
        error("BloodRageBehavior: owner is nil.")
    end

    local damage = math.ceil(owner.HP * 0.3)
    damage = math.min(damage, owner.HP - 1)
    if damage > 0 then
        self:HurtOwner(damage)
    end
end

return BloodRageBehavior