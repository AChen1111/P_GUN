AKModule = {}
AKModule.__index = AKModule

function AKModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 步枪: 按住循环音效并按间隔发射, 抬起播放结束音.
function AKModule:ShootDown(dir)
    local clip = self.gun.GunClip
    if clip.IsOutOfAmmo or not clip.CanShoot then return end
    self.gun:PlayFireSound(true)
end

function AKModule:Shooting(dir, deltaTime)
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

function AKModule:ShootUp(dir)
    if not CS.Game.Gameplay.Gun.PlayerAudioSource.isPlaying then return end
    -- 结束音由预制体的 ObjectReference 注入.
    self.gun:PlaySoundClip(self.m_ShootEndClip, false)
end

return AKModule