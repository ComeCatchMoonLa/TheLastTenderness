# 1.15 TODO：灵魂数字跟 AddSouls 走

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 2.2 已收尾。
- [ ] 读过技术设计：HUD 写入放在 `AddSouls`，击杀不再另写。

**阶段门槛：** 知道不改击杀奖励数量。

## 1. 一处写入

- [ ] `AddSouls` 改完 `soulCount` 后调用 `SetSoulCountText`。
- [ ] `soulCountUI` 没接上时不调用。
- [ ] `AwardSoulsOnDeath` 不再调用 `SetSoulCountText`。

**阶段门槛：** `player` 的查找方式不改。

## 2. 单元测试

- [ ] `AddSoulsHudTests` 调用 `AddSouls(5)` 后，`soulCount` 为 5，文本为 `5`。
- [ ] 测试不调用 `AwardSoulsOnDeath`，不创建 Animator。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 大纲里的 1.15 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
