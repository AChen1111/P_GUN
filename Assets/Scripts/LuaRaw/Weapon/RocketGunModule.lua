RocketGunModule = {}
RocketGunModule.__index = RocketGunModule

function RocketGunModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 火箭筒: 单发节流, 子弹朝向瞄准方向.
function RocketGunModule:ShootDown(dir)
    self:TryFire(dir)
end

function RocketGunModule:Shooting(dir, deltaTime)
    self:TryFire(dir)
end

function RocketGunModule:TryFire(dir)
    if not self.gun:TryConsumeShot() then return end
    local bullet = self.gun:FireBullet(dir)
    if bullet ~= nil then
        bullet.transform.right = CS.UnityEngine.Vector3(dir.x, dir.y, 0)
    end
    self.gun:PlayFireVfx(dir)
    self.gun:PlayFireSound(false)
end

return RocketGunModule