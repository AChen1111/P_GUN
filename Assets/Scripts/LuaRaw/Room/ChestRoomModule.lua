ChestRoomModule = {}
ChestRoomModule.__index = ChestRoomModule

-- 宝箱房: 玩家进入时在配置的点位生成物品.
function ChestRoomModule:OnPlayerEnteredRoom()
    local spawner = self.gameObject:GetComponent(typeof(CS.Game.Items.ItemSpawner))
    if spawner == nil then
        error("ChestRoomModule: 房间缺少 ItemSpawner.")
    end

    local points = self.spawnPoints
    if points == nil then
        error("ChestRoomModule: 未收到生成点位.")
    end

    for i = 0, points.Count - 1 do
        spawner:SpawnItem(points[i])
    end
end

return ChestRoomModule