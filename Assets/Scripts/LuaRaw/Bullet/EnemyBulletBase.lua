EnemyBulletBase = {}
EnemyBulletBase.__index = EnemyBulletBase

-- 生命周期: 子弹参数已由 C# 写入实例表, 特殊弹道按需重写.
function EnemyBulletBase:OnSpawn()
end

function EnemyBulletBase:OnSpawnFromPool()
    self.lifeTimer = 0
end

function EnemyBulletBase:OnRecycle()
    self.lifeTimer = 0
end

-- 存活计时使用敌人局部时间, 超时回池.
function EnemyBulletBase:OnMove(deltaTime)
    self.lifeTimer = (self.lifeTimer or 0) + deltaTime
    if self.lifeTimer >= (self.lifeTime or 3) then
        self.bullet:Recycle()
    end
end

-- 命中分发: 玩家与墙壁交给对应回调, 子类按需重写.
function EnemyBulletBase:OnHit()
    local target = self.hitTarget
    self.hitTarget = nil
    if target == nil then return end

    if self:IsPlayer(target) then
        self:OnHitPlayer(target)
        return
    end

    if self:IsWall(target) then
        self:OnHitWall(target)
    end
end

-- 默认命中玩家: 伤害, 命中 Buff, 音效与回池由 C# 统一处理.
function EnemyBulletBase:OnHitPlayer(target)
    self.bullet:ApplyPlayerHit(target)
end

-- 默认命中墙壁: 表现与回池由 C# 统一处理.
function EnemyBulletBase:OnHitWall(target)
    self.bullet:ApplyWallHit(target)
end

function EnemyBulletBase:IsPlayer(target)
    return target ~= nil and target.tag == "Player"
end

function EnemyBulletBase:IsWall(target)
    if target == nil then
        return false
    end

    if target.tag == "Wall" then
        return true
    end

    return target.layer == CS.UnityEngine.LayerMask.NameToLayer("Wall")
end