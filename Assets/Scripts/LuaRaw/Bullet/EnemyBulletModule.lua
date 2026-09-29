EnemyBulletModule = {}
EnemyBulletModule.__index = EnemyBulletModule
setmetatable(EnemyBulletModule, {__index = EnemyBulletBase})

-- 默认敌人子弹: 命中与存活规则全部继承基类, 命中 Buff 由 BulletData 的 hitBuffId 决定.
return EnemyBulletModule