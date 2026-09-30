-- 世界道具的拾取规则模块: pickInBackBag=false 的道具在拾取瞬间触发 OnPickedUp.
-- 配置在道具预制体的 LuaComponet 上, itemId 与 sourceObject 由 Item 组件注入或模块自行读取.

WorldItemPickupModule = {}
WorldItemPickupModule.__index = WorldItemPickupModule

function WorldItemPickupModule:OnPickedUp()
    -- 具体道具的拾取效果在此实现, 例如直接治疗或直接施加 Buff.
    local player = CS.Game.Gameplay.PlayerRegistry.Current
    if player == nil then
        error("WorldItemPickupModule: 玩家不存在.")
    end

    local itemId = self.itemId
    if itemId == nil then
        error("WorldItemPickupModule: itemId 未注入.")
    end

    -- 拾取直发效果与背包使用效果共用同一套效果预制体.
    CS.Game.Items.LuaItemEffectHelper.ShowHeadMessage("拾取了物品 " .. tostring(itemId), 1.5)
end

return WorldItemPickupModule