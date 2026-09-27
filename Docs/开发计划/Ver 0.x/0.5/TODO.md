# 0.5 TODO：移动旗标只有一个写入方

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.4 已完成。工作区里没有未提交的玩法改动。
- [x] 读过技术设计：只动冲刺的第二写入方。

**阶段门槛：** 知道翻滚、跳跃、在空中的赋值不用改。

## 1. 冲刺只由 InputManager 写

- [x] `BlockAction.PerformAction` 删除 `character.isSprinting = false`。
- [x] `InputManager.HandleRollInput` 在 `isInteracting` 的 return 之前，若 `cCombat.isBlocking` 则把 `isSprinting` 设为 false。
- [x] 搜索 `Assets/Scripts`：`isRolling`、`isJumping`、`isInAir` 的赋值仍只在技术设计第 1 节那一处；`isSprinting` 的赋值只在 `InputManager`。

**阶段门槛：** 四个字段各一个写入类型。

## 2. Play 验收

- [x] 走、按住 Shift 冲刺、短按 Shift 翻滚、跳跃、从高处落下落地。
- [x] 冲刺中举盾：速度回到跑步，盾的起手动画仍在。按住 Shift 时不再把 `isSprinting` 写回 true。
- [x] 大纲里的 0.5 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
