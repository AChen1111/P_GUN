LifestealBehavior = {}
LifestealBehavior.__index = LifestealBehavior
setmetatable(LifestealBehavior, {__index = BuffBase})

-- 吸血: 击杀敌人时按层数回复生命.
function LifestealBehavior:OnKill(enemyGameObject)
    self:HealOwner(self:GetStackCount())
end

return LifestealBehavior