WarDrumModule = {}
WarDrumModule.__index = WarDrumModule
setmetatable(WarDrumModule, {__index = ItemBase})

-- 战鼓: 一段时间内击杀敌人回复生命.
function WarDrumModule:OnPick()
    self:AddBuffById(13)
    self:ShowMessage("战鼓生效: 击杀回复生命", 1.5)
end

return WarDrumModule