ShieldGeneratorModule = {}
ShieldGeneratorModule.__index = ShieldGeneratorModule
setmetatable(ShieldGeneratorModule, {__index = ItemBase})

-- 护盾发生器: 短时间内显著减伤.
function ShieldGeneratorModule:OnPick()
    self:AddBuffById(12)
    self:ShowMessage("护盾生效: 受到的伤害降低", 1.5)
end

return ShieldGeneratorModule