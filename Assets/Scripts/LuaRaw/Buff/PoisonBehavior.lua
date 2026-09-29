PoisonBehavior = {}
PoisonBehavior.__index = PoisonBehavior

-- 中毒行为: 每次间隔回调按当前层数对玩家结算伤害.
function PoisonBehavior:OnAdd()
    CS.UnityEngine.Debug.Log("PoisonBehavior OnAdd")
end

function PoisonBehavior:OnRemove()
    CS.UnityEngine.Debug.Log("PoisonBehavior OnRemove")
end

function PoisonBehavior:OnInterval()
    local owner = self.owner
    if owner == nil then
        error("PoisonBehavior owner is nil.")
    end

    -- 每层中毒独立贡献伤害, 例如两层每次间隔扣 2 点.
    local damage = math.max(1, self.stackCount or 1)
    -- 使用玩家统一受伤流程, 保持受击反馈, 无敌帧和死亡事件一致.
    local damageInfo = CS.Game.Gameplay.DamageInfo(damage, CS.UnityEngine.Vector2(0, 0))
    owner:Hurt(damageInfo)
end

-- 对象池复用前清掉上一次的运行时状态.
function PoisonBehavior:OnRecycle()
    self.owner = nil
    self.buffId = nil
    self.buffName = nil
    self.stackCount = nil
end

return PoisonBehavior