require("BuffBase")

PoisonBehavior = {}
PoisonBehavior.__index = PoisonBehavior
setmetatable(PoisonBehavior, {__index = BuffBase})

function PoisonBehavior:OnAdd()
    CS.UnityEngine.Debug.Log("PoisonBehavior OnAdd")
end

function PoisonBehavior:OnRemove()
    CS.UnityEngine.Debug.Log("PoisonBehavior OnRemove")
end

-- 每次间隔按当前层数对玩家结算伤害.
function PoisonBehavior:OnInterval()
    self:HurtOwner(self:GetStackCount())
end

return PoisonBehavior