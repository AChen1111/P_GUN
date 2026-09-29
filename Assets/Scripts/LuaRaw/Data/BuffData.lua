-- 本文件由 CSVToLuaTable 生成, 请勿手改.
-- 源表: Assets/csv/BuffData.csv

return {
    ["0"] = {
        buffName = "SpeedUpBuff",
        description = "移动速度提升40%.",
        iconAddress = "Assets/Malicious_statusiconset1/escape icon.png",
        tag = "Positive",
        duration = 5,
        isPermanent = false,
        interval = 1,
        modifiers = {
            { stat = "MoveSpeed", type = "PercentAdd", value = 0.4 },
        },
        behaviorPrefabAddress = "",
    },
    ["1"] = {
        buffName = "PowerUpBuff",
        description = "子弹伤害提升50%.",
        iconAddress = "Assets/Malicious_statusiconset1/attack icon.png",
        tag = "Positive",
        duration = 5,
        isPermanent = false,
        interval = 1,
        modifiers = {
            { stat = "Attack", type = "PercentAdd", value = 0.5 },
        },
        behaviorPrefabAddress = "",
    },
    ["2"] = {
        buffName = "MaxHpUpBuff",
        description = "最大生命值提升2点",
        iconAddress = "Assets/Malicious_statusiconset1/buff icon.png",
        tag = "Positive",
        duration = 0,
        isPermanent = true,
        interval = 1,
        modifiers = {
            { stat = "MaxHp", type = "Flat", value = 2 },
        },
        behaviorPrefabAddress = "",
    },
    ["3"] = {
        buffName = "PoisonBuff",
        description = "永久中毒: 每隔10秒每层受到1点伤害",
        iconAddress = "Assets/Malicious_statusiconset1/confusion icon.png",
        tag = "Negative",
        duration = 0,
        isPermanent = true,
        interval = 10,
        modifiers = {},
        behaviorPrefabAddress = "buff/poison_behavior",
    },
}