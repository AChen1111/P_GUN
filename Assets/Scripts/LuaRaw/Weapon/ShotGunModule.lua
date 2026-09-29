ShotGunModule = {}
ShotGunModule.__index = ShotGunModule

function ShotGunModule:Awake()
    self.gun = self.gameObject:GetComponent(typeof(CS.Game.Gameplay.Gun))
end

-- 霰弹枪: 一次 5 发, 中心一发不偏移, 其余左右交替按 2 度展开.
function ShotGunModule:ShootDown(dir)
    if not self.gun:TryConsumeShot() then return end

    local baseAngle = math.deg(math.atan(dir.y, dir.x))
    for i = 0, 4 do
        local angle = baseAngle
        if i > 0 then
            -- 与原 C# 规则一致: 奇数向左, 偶数向右.
            local sign = 1
            if i % 2 ~= 0 then sign = -1 end
            angle = baseAngle + sign * i * 2
        end

        local rad = math.rad(angle)
        local pelletDir = CS.UnityEngine.Vector2(math.cos(rad), math.sin(rad))
        self.gun:FireBullet(pelletDir)
        if i == 0 then
            self.gun:PlayFireSound(false)
            self.gun:PlayFireVfx(dir)
        end
    end
end

return ShotGunModule