# 0.9 TODO：战斗结算纠错

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.8 已完成。工作区里没有未提交的玩法改动。
- [x] 读过技术设计：六处都改现有方法，不加结算类型。

**阶段门槛：** 知道 0.10 的下落和交互锁、0.11 的吸收和数量不在本版。

## 1. 破防名字和左手盾

- [x] `AttemptBlock` 的破防动画改为 `Guard_Break`。`BreakGuard` 同样改为 `Guard_Break`。
- [x] 左手是盾时，`Handle_Hold_Q_Input` 把使用手标成左手。
- [x] 左手格挡的 `blockingStabilityRating` 改读 `leftWeapon`。

**阶段门槛：** 破防状态名和控制器一致。盾的稳定率不再读右手。

## 2. 按住 E、点按 E、重击段号

- [x] `hold_e_Input` 在进入处理时清掉，按住期间不再每个 `Update` 调用 `PerformAction`。
- [x] 点按 E 先判断 `leftWeapon` 存在。技术设计第 4 节列出的双手弓提前 return 同样先判空。
- [x] `HandleHeavyAttackCombo` 按实际播放的那一段写 `heavy_1` 或 `heavy_2`。

**阶段门槛：** 按住 E 只开始一次。重播重击第一段时段号不是 `heavy_2`。

## 3. 背刺一次、炸弹不打两次

- [x] `GetBackStabbed` 和 `GetRiposte` 改用攻击者的右手武器。攻击者没有右手武器时不扣血。
- [x] `CriticalAttackAction` 不再写入 `pendingCriticalDamage`。`ApplyPendingDamage` 在该值为 0 时不调用 `TakeDamage`。
- [x] `Explode` 跳过本次直击已经打过的那个 `CharacterStatsManager`。

**阶段门槛：** 背刺只有一次 `TakeDamage`。炸弹直击和溅射不会打在同一个人身上。

## 4. Play 验收

- [x] 游戏设计第 3 节六条都看过。走路和冲刺不重测。
- [x] 大纲里的 0.9 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
