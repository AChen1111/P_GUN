-- 本文件由 CSVToLuaTable 生成, 请勿手改.
-- 源表: Assets/csv/EnemyData.csv

return {
    ["1"] = {
        displayName = "Bat",
        prefabAddress = "enemy/bat",
        maxHp = 10,
        moveSpeed = 2.5,
        damage = 1,
        itemDropChance = 1,
        attackAngle = 120,
        separationRadius = 1.8,
        separationWeight = 1.5,
        attackInterval = 1,
        attackRange = 6,
    },
    ["2"] = {
        displayName = "Slime",
        prefabAddress = "enemy/slime",
        maxHp = 3,
        moveSpeed = 2.5,
        damage = 1,
        itemDropChance = 0.25,
        attackAngle = 120,
        separationRadius = 1.8,
        separationWeight = 1.5,
        attackInterval = 1,
        attackRange = 1,
    },
    ["3"] = {
        displayName = "Big_enemy",
        prefabAddress = "enemy/big_enemy",
        maxHp = 35,
        moveSpeed = 1.6,
        damage = 2,
        itemDropChance = 1,
        attackAngle = 120,
        separationRadius = 2.2,
        separationWeight = 1.5,
        attackInterval = 1.5,
        attackRange = 8,
    },
}
