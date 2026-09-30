WarDrumBehavior = {}
WarDrumBehavior.__index = WarDrumBehavior
setmetatable(WarDrumBehavior, {__index = BuffBase})

-- 战鼓: 生效期间击杀敌人回复 1 点生命.
function WarDrumBehavior:OnKill(enemyGameObject)
    self:HealOwner(1)
end

return WarDrumBehavior