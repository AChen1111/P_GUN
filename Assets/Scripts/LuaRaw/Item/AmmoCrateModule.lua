AmmoCrateModule = {}
AmmoCrateModule.__index = AmmoCrateModule
setmetatable(AmmoCrateModule, {__index = ItemBase})

-- 弹药补给箱: 补满当前枪的弹夹与备弹, 无限弹药的枪没有补弹意义.
function AmmoCrateModule:CanUse()
    local gun = self:GetCurrentGun()
    if gun == nil then
        return false
    end

    local clip = gun.GunClip
    local bag = gun.BulletBag
    if clip == nil or bag == nil then
        return false
    end

    if clip.IsInfinite and bag.maxBullet < 0 then
        return false
    end

    return not clip.IsFull or not (bag.maxBullet > 0 and bag.currentBullet >= bag.maxBullet)
end

function AmmoCrateModule:OnPick()
    local gun = self:GetCurrentGun()
    if gun == nil then
        error("AmmoCrateModule: 当前枪不存在.")
    end

    local clip = gun.GunClip
    local bag = gun.BulletBag
    gun:RestoreAmmo(clip.maxAmmo, clip.maxAmmo, bag.maxBullet, bag.maxBullet)
    -- 通知弹药 UI 刷新.
    gun:OnGunUsed()
    self:ShowMessage("弹药已补满", 1.5)
end

-- 取玩家当前枪, 装载未完成或索引越界时返回 nil.
function AmmoCrateModule:GetCurrentGun()
    local player = self:GetPlayer()
    if player == nil then
        return nil
    end

    local guns = player.guns
    if guns == nil or guns.Count == 0 then
        return nil
    end

    local index = player.currentGunIndex
    if index < 0 or index >= guns.Count then
        return nil
    end

    return guns[index]
end

return AmmoCrateModule