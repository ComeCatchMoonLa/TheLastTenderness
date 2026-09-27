# 0.16 TODO：付不清的格挡会破防

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 0.15 已完成。工作区里没有未提交的玩法改动。
- [ ] 读过技术设计：付不清只在 `AttemptBlock` 里把精力写成 0。

**阶段门槛：** 知道侧面点积、翻滚不够精力、武器的 `guardBreakM` 不在本版。

## 1. 精力写成 0 再破防

- [ ] `CharacterStatsManager.EmptyStamina` 把 `currentStamina` 写成 0。玩家覆写并刷新体力条。
- [ ] `AttemptBlock` 在 `DeductStamina` 返回 false 时调用 `EmptyStamina`。返回 true 时不调用。
- [ ] `DeductStamina` 的比较和返回值不改。

**阶段门槛：** `ResolveIncomingHit` 里减伤和 `playHurtSound` 的代码不动。

## 2. Play 验收

- [ ] 游戏设计第 3 节看过。侧面角度不重测。
- [ ] 大纲里的 0.16 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
