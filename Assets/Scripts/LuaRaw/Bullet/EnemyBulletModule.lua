EnemyBulletModule = {}
EnemyBulletModule.__index = EnemyBulletModule

function EnemyBulletModule:OnSpawn()
    -- 默认弹道参数已由 C# 写入, 特殊弹道在这里改写方向或速度.
end

function EnemyBulletModule:OnSpawnFromPool()
    self.lifeTimer = 0
end

function EnemyBulletModule:OnRecycle()
    self.lifeTimer = 0
end

-- 存活计时使用敌人局部时间, 子弹时间中射程不会被真实时间提前截断.
function EnemyBulletModule:OnMove(deltaTime)
    self.lifeTimer = (self.lifeTimer or 0) + deltaTime
    if self.lifeTimer >= (self.lifeTime or 3) then
        self.bullet:Recycle()
    end
end

function EnemyBulletModule:OnHit()
    local target = self.hitTarget
    self.hitTarget = nil
    if target == nil then return end

    if target.tag == "Player" then
        self.bullet:ApplyPlayerHit(target)
        return
    end

    local wallLayer = CS.UnityEngine.LayerMask.NameToLayer("Wall")
    if target.tag == "Wall" or target.layer == wallLayer then
        self.bullet:ApplyWallHit(target)
    end
end

return EnemyBulletModule