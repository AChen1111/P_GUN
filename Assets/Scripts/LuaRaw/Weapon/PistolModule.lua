PistolModule = {}
PistolModule.__index = PistolModule

function PistolModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 手枪: 按下打一发.
function PistolModule:ShootDown(dir)
    if not self.gun:TryConsumeShot() then return end
    self.gun:FireBullet(dir)
    self.gun:PlayFireSound(false)
    self.gun:PlayFireVfx(dir)
end

return PistolModule