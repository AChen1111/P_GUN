MP5Module = {}
MP5Module.__index = MP5Module

function MP5Module:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 冲锋枪: 按住循环音效并按间隔发射, 抬起停止音效.
function MP5Module:ShootDown(dir)
    local clip = self.gun.GunClip
    if clip.IsOutOfAmmo or not clip.CanShoot then return end
    self.gun:PlayFireSound(true)
end

function MP5Module:Shooting(dir, deltaTime)
    if self.gun:TryConsumeShot() then
        if not CS.Game.Gameplay.Gun.PlayerAudioSource.isPlaying then
            self.gun:PlayFireSound(true)
        end
        self.gun:FireBullet(dir)
        self.gun:PlayFireVfx(dir)
    else
        local clip = self.gun.GunClip
        if clip.IsOutOfAmmo or clip.IsReloading then
            self.gun:StopFireSound()
        end
    end
end

function MP5Module:ShootUp(dir)
    self.gun:StopFireSound()
end

return MP5Module