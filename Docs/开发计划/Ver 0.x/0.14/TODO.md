# 0.14 TODO：炸弹认按下时的那一件

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.13 已完成。
- [x] 读过技术设计：只有炸弹记下按下的那一件。血瓶仍走当前快捷栏。

**阶段门槛：** 知道喝药锁移动和合成两个消耗品类不在本版。

## 1. 结算认按下的炸弹

- [x] `PlayerInventoryManager.consumableBeingUsed` 只由炸弹在扣数量成功后写成 `this`。
- [x] 动画事件优先调用这份，调用后写成 null。
- [x] `BombItem.SucessfullyUsedConsumable` 用 `this` 生成炸弹。
- [x] 消耗品状态离开时，玩家的这份字段写成 null。

**阶段门槛：** `BombItem` 里不再出现 `currentConsumable as BombItem`。

## 2. Play 验收

- [x] 游戏设计第 3 节看过。喝药的移动不重测成锁住。
- [x] 大纲里的 0.14 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
