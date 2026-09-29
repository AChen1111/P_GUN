LaserModule = {}
LaserModule.__index = LaserModule

function LaserModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
    -- 激光只需要墙体和敌人两层.
    self.hitMask = CS.UnityEngine.LayerMask.GetMask("Wall", "EnemyLayer")
end

-- 激光: 按住持续射线, 按射击间隔对命中的敌人结算伤害.
function LaserModule:ShootDown(dir)
    self.m_LineRenderer.enabled = true
    self.lastDamageTime = CS.UnityEngine.Time.time - self.gun:GetShootInterval()
    self.gun:PlayFireSound(true)
end

function LaserModule:Shooting(dir, deltaTime)
    local maxDistance = self.maxLaserDistance or 100
    local origin = self.gun:GetFirePointPosition()
    local hit = CS.UnityEngine.Physics2D.Raycast(origin, dir, maxDistance, self.hitMask)

    self.m_LineRenderer:SetPosition(0, CS.UnityEngine.Vector3(origin.x, origin.y, 0))
    if hit.collider ~= nil then
        self.m_LineRenderer:SetPosition(1, CS.UnityEngine.Vector3(hit.point.x, hit.point.y, 0))

        -- 激光可能先命中敌人的攻击检测子物体, 向父级查找敌人本体.
        local enemy = hit.collider:GetComponentInParent(typeof(CS.Game.Gameplay.EnemyBase))
        if enemy ~= nil and CS.UnityEngine.Time.time - self.lastDamageTime >= self.gun:GetShootInterval() then
            self.lastDamageTime = CS.UnityEngine.Time.time
            local damageInfo = CS.Game.Gameplay.DamageInfo(self.gun.Damage, dir)
            enemy:Hurt(damageInfo)
        end
        return
    end

    local endPos = origin + dir * maxDistance
    self.m_LineRenderer:SetPosition(1, CS.UnityEngine.Vector3(endPos.x, endPos.y, 0))
end

function LaserModule:ShootUp(dir)
    self.m_LineRenderer.enabled = false
    self.gun:StopFireSound()
end

return LaserModule