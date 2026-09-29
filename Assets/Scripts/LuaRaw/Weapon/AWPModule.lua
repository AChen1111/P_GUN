AWPModule = {}
AWPModule.__index = AWPModule

function AWPModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 狙击: 按下和按住都走单发节流.
function AWPModule:ShootDown(dir)
    self:TryFire(dir)
end

function AWPModule:Shooting(dir, deltaTime)
    self:TryFire(dir)
end

function AWPModule:TryFire(dir)
    if not self.gun:TryConsumeShot() then return end
    self.gun:FireBullet(dir)
    self.gun:PlayFireVfx(dir)
    self.gun:PlayFireSound(false)
end

return AWPModule