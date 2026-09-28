# 2.7 TODO：快捷栏能看见回复剩余次数

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.29 已收尾。
- [x] 读过技术设计：文本只由 `QuickSlotsUI.SetConsumableCount` 写入，数字来自 `ConsumableRemaining`。

**阶段门槛：** 知道不改 `RefillConsumablesToMax` 里写成 `maxItemAmount` 的那一行。

## 1. 次数文本

- [x] `QuickSlotsUI` 增加 `consumableCountText` 和 `SetConsumableCount`。
- [x] 引用为空时打出 `consumableCountText == null`，不写假数字。
- [x] 换图标、扣到当前这件、写回上限这三处刷新文本。没接上快捷栏时不调用。
- [x] `Assets/Perfabs/#/UI.prefab` 的消耗品格接上这个文本。不改另外三个图标。

**阶段门槛：** 写回上限的赋值仍是 `maxItemAmount`。

## 2. 单元测试

- [x] `QuickSlotConsumableCountTests`：扣一次文本是 `2`，扣到 0 文本是 `0`，`RefillConsumablesToMax` 后文本是 `3`。
- [x] 测试不创建篝火，不创建 Animator。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 2.7 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
