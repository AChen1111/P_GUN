RoomRewardModule = {}
RoomRewardModule.__index = RoomRewardModule

-- 战斗房清空后的奖励生成.
function RoomRewardModule:OnFightAllWavesEnd()
    local spawner = self.gameObject:GetComponent(typeof(CS.Game.Items.ItemSpawner))
    if spawner == nil then
        error("RoomRewardModule: 房间缺少 ItemSpawner.")
    end

    spawner:SpawnItem(self.gameObject.transform.position)
end

return RoomRewardModule