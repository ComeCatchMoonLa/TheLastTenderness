# 实现偏离需求

> **这一轮只记已经在跑的代码和已写需求对不上的大出入。** 需求自己标待补、当前仓库未找到对应类型的整套系统，不算偏离。需求文档和调研文档还没写完。

## 大出入

下表只收四份分节里认定的大出入，从大到小。分节没写进去的，不另补。

| 出入 | 需求编号 | 代码位置 | 详见 |
| --- | --- | --- | --- |
| 付不清的一记格挡不破防。剩余精力不够付这一下时不扣精力、不放下盾、不播 `Guard_Break`，生命仍按挡住减免 | FR-伤害-04、FR-伤害-07 | `CharacterCombatManager.AttemptBlock` 调 `CharacterStatsManager.DeductStamina`；不够则原样返回。扣完仍大于 0 就继续挡 | [战斗](战斗.md) |
| 装备窗选中护甲槽后再点背包里的武器：武器离开背包，手上没有新模型 | FR-装备-02 | `InventorySlotUI.EquipWeapon`。`selectedSlotType` 是护甲槽时四个手槽分支都不进，仍执行 `RemoveItem` | [物品与法术](物品与法术.md) |
| 侧面在距离稍远时也算挡住。点积没归一化，同一个侧面角度站远会过线、站近不会 | FR-伤害-04 | `CharacterCombatManager.ResolveIncomingHit`。位置差与 `forward` 点积大于 `0.3` 就当挡住 | [战斗](战斗.md) |
| 投射法术打中后不按学派进对应伤害段，统一送进火段 | FR-法术-05 | `ProjectileSpell.SuccessfullyCastSpellWithPlayer` 只写 `fd`。`SpellDamageCollider.OnCollisionEnter` 只把 `fd` 送进 `TakeDamage`。`SpellType` 结算不读 | [物品与法术](物品与法术.md) |

## 查过、没有大出入

- [移动与镜头](移动与镜头.md)：移动、镜头、精力、负重里标成当前已有的条，玩家能看到的结果和需求正文一致。
- [世界与界面](世界与界面.md)：敌人、燃火休息与死亡惩罚、战斗 HUD 里标成当前已有的可观察结果，和正在跑的路径一致。

架势、踢击、背刺整篇待补，以及未激活父节点下的篝火、箱子、雾墙、剧情，没有记成偏离。
