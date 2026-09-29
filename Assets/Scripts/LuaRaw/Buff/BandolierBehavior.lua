BandolierBehavior = {}
BandolierBehavior.__index = BandolierBehavior
setmetatable(BandolierBehavior, {__index = BuffBase})

-- 弹夹大师: 添加时扩容所有枪的弹夹, 移除或复用时还原.
function BandolierBehavior:OnAdd()
    if self.originalClipMax ~= nil then
        return
    end

    local owner = self:GetOwner()
    if owner == nil then
        error("BandolierBehavior: owner is nil.")
    end

    self.originalClipMax = {}
    for i = 0, owner.guns.Count - 1 do
        local gun = owner.guns[i]
        if gun ~= nil then
            local clip = gun.GunClip
            local bag = gun.BulletBag
            if clip ~= nil and not clip.IsInfinite and bag ~= nil then
                self.originalClipMax[gun.WeaponId] = clip.maxAmmo
                gun:RestoreAmmo(clip.currentAmmo, math.ceil(clip.maxAmmo * 1.5), bag.currentBullet, bag.maxBullet)
            end
        end
    end
end

function BandolierBehavior:OnRemove()
    self:RestoreOriginalClips()
end

function BandolierBehavior:OnRecycle()
    BuffBase.OnRecycle(self)
    self.originalClipMax = nil
end

-- 按记录的原容量还原弹夹, 当前弹药不超过还原后的上限.
function BandolierBehavior:RestoreOriginalClips()
    if self.originalClipMax == nil then
        return
    end

    local owner = self:GetOwner()
    if owner ~= nil then
        for i = 0, owner.guns.Count - 1 do
            local gun = owner.guns[i]
            if gun ~= nil then
                local originalMax = self.originalClipMax[gun.WeaponId]
                local clip = gun.GunClip
                local bag = gun.BulletBag
                if originalMax ~= nil and clip ~= nil and bag ~= nil then
                    gun:RestoreAmmo(math.min(clip.currentAmmo, originalMax), originalMax, bag.currentBullet, bag.maxBullet)
                end
            end
        end
    end

    self.originalClipMax = nil
end

return BandolierBehavior