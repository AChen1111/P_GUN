BowModule = {}
BowModule.__index = BowModule

function BowModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 弓: 按住蓄力超过 0.5 秒后抬起才发射.
function BowModule:ShootDown(dir)
    self.pressing = true
    self.pressingTime = 0
    self.m_Arrow.enabled = false
end

function BowModule:Shooting(dir, deltaTime)
    if self.pressing then
        self.pressingTime = (self.pressingTime or 0) + deltaTime
    end

    if self.pressingTime >= 0.5 then
        self.m_Arrow.enabled = true
    else
        self.m_Arrow.enabled = false
    end
end

function BowModule:ShootUp(dir)
    if self.pressing and self.pressingTime > 0.5 then
        local bullet = self.gun:FireBullet(dir)
        if bullet ~= nil then
            bullet.transform.right = CS.UnityEngine.Vector3(dir.x, dir.y, 0)
        end
        self.gun:PlayFireSound(false)
    end

    self.pressingTime = 0
    self.pressing = false
    self.m_Arrow.enabled = false
end

return BowModule