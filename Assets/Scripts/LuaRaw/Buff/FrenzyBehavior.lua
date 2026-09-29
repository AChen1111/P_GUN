FrenzyBehavior = {}
FrenzyBehavior.__index = FrenzyBehavior
setmetatable(FrenzyBehavior, {__index = BuffBase})

-- 屠戮者: 击杀敌人时重新添加自己, 复用非永久 Buff 重置时长的机制刷新 4 秒增伤.
function FrenzyBehavior:OnKill(enemyGameObject)
    self:AddBuffById(self.buffId)
end

return FrenzyBehavior