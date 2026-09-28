# 1.19 TODO：入伤结果留在可读字段上

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.18 已收尾。
- [ ] 读过技术设计：只在调用 `TakeDamage` 之前写下三个字段。

**阶段门槛：** 知道不改分支条件。

## 1. 留下结果

- [ ] 增加 `lastHitWasBlocked`、`lastHitBrokePoise`、`lastHitDamageTotal`。
- [ ] 三条结算路径在 `TakeDamage` 之前写入。
- [ ] `totalPoiseDefence > poiseDamage` 这个比较不改。

**阶段门槛：** `AttemptBlock` 和 `TakeDamage` 的参数与现在相同。

## 2. 单元测试

- [ ] `IncomingHitResultTests` 覆盖游戏设计第 3 节。
- [ ] 测试不创建 Animator。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 大纲里的 1.19 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
