# 2.5 TODO：休息把回复次数写回上限

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.20 已收尾。
- [ ] 读过技术设计：次数只在 `RefillConsumablesToMax` 里写回，休息成功路径调用它。

**阶段门槛：** 知道不改 `TrySpendConsumable`，不改篝火预制体。

## 1. 写回

- [ ] `RefillConsumablesToMax` 把字典里已有的每件写成 `maxItemAmount`。
- [ ] `RestoreVitalsToMax` 在刷新 HUD 之前调用它。
- [ ] `player.pInventory` 没接上时不调用。

**阶段门槛：** 篝火的 `Interact` 仍只调用 `RestoreVitalsToMax`。

## 2. 单元测试

- [ ] `RestRefillConsumableTests` 先扣到 1，调用 `RestoreVitalsToMax` 后为 3。
- [ ] 测试不创建篝火，不创建 Animator。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 大纲里的 2.5 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
