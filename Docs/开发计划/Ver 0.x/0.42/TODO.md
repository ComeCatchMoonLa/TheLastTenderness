# 0.42 TODO：消耗品开局次数用 Current Item Amount

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。0.x 不新写单元测试。

## 0. 开始前

- [ ] 0.41 已收尾。
- [ ] 读过技术设计：初始剩余用 `currentItemAmount`，`maxItemAmount` 只封顶。

**阶段门槛：** 知道不写回资产，休息仍写回上限，血瓶和灰瓶那一行不改。

## 1. 初始剩余

- [ ] `RememberConsumable` 按 `currentItemAmount` 记剩余，超过上限时用上限，小于 0 时用 0。
- [ ] `TrySpendConsumable` 仍是大于 0 才减 1。
- [ ] `RefillConsumablesToMax` 仍写成 `maxItemAmount`。
- [ ] 三份已有测试把 `currentItemAmount` 设成它们断言的开局数。

**阶段门槛：** 已经记下的剩余不因后来改资产而重算。

## 2. 验收

- [ ] 游戏设计第 3 节在编辑器里看过。
- [ ] 大纲里的 0.42 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。没有新的单元测试要跑。
