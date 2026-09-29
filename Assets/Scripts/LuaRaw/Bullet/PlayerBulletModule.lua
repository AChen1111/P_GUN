PlayerBulletModule = {}
PlayerBulletModule.__index = PlayerBulletModule

function PlayerBulletModule:OnSpawn()
    -- 默认弹道参数已由 C# 写入, 特殊弹道在这里改写方向或速度.
end

function PlayerBulletModule:OnSpawnFromPool()
    self.lifeTimer = 0
end

function PlayerBulletModule:OnRecycle()
    self.lifeTimer = 0
end

-- 存活规则由模块累计, 超时回池.
function PlayerBulletModule:OnMove(deltaTime)
    self.lifeTimer = (self.lifeTimer or 0) + deltaTime
    if self.lifeTimer >= (self.lifeTime or 3) then
        self.bullet:Recycle()
    end
end

function PlayerBulletModule:OnHit()
    local target = self.hitTarget
    self.hitTarget = nil
    if target == nil then return end

    if target.tag == "Enemy" then
        self.bullet:ApplyEnemyDamage(target)
        return
    end

    local wallLayer = CS.UnityEngine.LayerMask.NameToLayer("Wall")
    if target.tag == "Wall" or target.layer == wallLayer then
        self.bullet:ApplyWallHit(target)
    end
end

return PlayerBulletModule