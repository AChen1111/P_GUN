ItemBase = {}
ItemBase.__index = ItemBase

-- 使用条件: 默认只要玩家存在即可使用, 子类有额外条件时重写.
function ItemBase:CanUse()
    return self:GetPlayer() ~= nil
end

function ItemBase:OnPick()
end

-- 获取当前玩家, 未注册时返回 nil.
function ItemBase:GetPlayer()
    return CS.Game.Gameplay.PlayerRegistry.Current
end

-- 获取玩家身上的 BuffManager, 缺少时返回 nil.
function ItemBase:GetBuffManager()
    local player = self:GetPlayer()
    return player ~= nil and player.buffManager or nil
end

-- 给玩家添加 Buff.
function ItemBase:AddBuffById(buffId)
    local manager = self:GetBuffManager()
    if manager == nil then
        error("ItemBase: player has no buffManager.")
    end

    manager:AddBuffById(buffId)
end

-- 治疗玩家, 返回实际治疗量.
function ItemBase:HealPlayer(amount)
    local player = self:GetPlayer()
    if player == nil or amount <= 0 then
        return 0
    end

    return player:Heal(amount)
end

-- 是否存在负面 Buff.
function ItemBase:HasNegativeBuff()
    local manager = self:GetBuffManager()
    if manager == nil then
        return false
    end

    local activeBuffs = manager.ActiveBuffs
    for i = 0, activeBuffs.Count - 1 do
        local info = activeBuffs[i]
        if info ~= nil and info.ParsedTag == CS.Game.Gameplay.BuffTag.Negative then
            return true
        end
    end

    return false
end

-- 移除全部负面 Buff, 返回移除数量.
function ItemBase:RemoveNegativeBuffs()
    local manager = self:GetBuffManager()
    if manager == nil then
        return 0
    end

    return manager:RemoveBuffsByTag(CS.Game.Gameplay.BuffTag.Negative)
end

-- 在玩家头顶显示提示文本.
function ItemBase:ShowMessage(text, seconds)
    CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage(text, seconds or 1.5)
end