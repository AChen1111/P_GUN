BuffBase = {}
BuffBase.__index = BuffBase

-- 生命周期: 由 BuffManager 转发调用, 子类按需重写.
function BuffBase:OnAdd()
end

function BuffBase:OnRemove()
end

function BuffBase:OnInterval()
end

-- 击杀回调: 击杀事件由 BuffManager 转发, 参数是被杀敌人的 GameObject.
function BuffBase:OnKill(enemyGameObject)
end

-- 对象池复用前清掉注入字段, 子类有额外状态时重写并先调 BuffBase.OnRecycle(self).
function BuffBase:OnRecycle()
    self.owner = nil
    self.buffId = nil
    self.buffName = nil
    self.stackCount = nil
end

-- 获取归属玩家.
function BuffBase:GetOwner()
    return self.owner
end

-- 获取当前层数, 至少为 1.
function BuffBase:GetStackCount()
    return math.max(1, self.stackCount or 1)
end

-- 自伤走 Player.SelfDamage, 不触发受击反馈和无敌帧.
function BuffBase:HurtOwner(damage)
    local owner = self:GetOwner()
    if owner == nil then
        error("BuffBase: owner is nil.")
    end

    if damage <= 0 then
        return
    end

    owner:SelfDamage(damage)
end

-- 治疗归属玩家, 返回实际治疗量.
function BuffBase:HealOwner(amount)
    local owner = self:GetOwner()
    if owner == nil or amount <= 0 then
        return 0
    end

    return owner:Heal(amount)
end

-- 给归属玩家添加另一个 Buff.
function BuffBase:AddBuffById(buffId)
    local owner = self:GetOwner()
    local manager = owner ~= nil and owner.buffManager or nil
    if manager == nil then
        error("BuffBase: owner has no buffManager.")
    end

    manager:AddBuffById(buffId)
end

-- 在玩家头顶显示提示文本.
function BuffBase:ShowMessage(text, seconds)
    CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage(text, seconds or 1.5)
end