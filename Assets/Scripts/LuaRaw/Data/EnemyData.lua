-- 本文件由 CSVToLuaTable 生成, 请勿手改.
-- 源表: Assets/csv/EnemyData.csv

return {
    ["1"] = {
        displayName = "Bat",
        prefabAddress = "Assets/Prefab/Enemy/Bat.prefab",
        maxHp = 10,
        moveSpeed = 2.5,
        damage = 1,
        itemDropChance = 1,
        visionRadius = 7,
        visionAngle = 120,
        searchTime = 2,
        separationRadius = 1.8,
        separationWeight = 1.5,
        attackInterval = 1,
        attackRange = 6,
    },
    ["2"] = {
        displayName = "Slime",
        prefabAddress = "Assets/Prefab/Enemy/Slime.prefab",
        maxHp = 3,
        moveSpeed = 2.5,
        damage = 1,
        itemDropChance = 0.25,
        visionRadius = 7,
        visionAngle = 120,
        searchTime = 2,
        separationRadius = 1.8,
        separationWeight = 1.5,
        attackInterval = 1,
        attackRange = 1,
    },
}