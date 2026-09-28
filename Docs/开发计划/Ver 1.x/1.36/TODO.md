# 1.36 TODO：消耗品成功回调拼写

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.35 已收尾。
- [x] 读过技术设计：只改这个方法名，不改动画事件。

**阶段门槛：** 知道不改 `SucessfullyGetArrow`，不改目录。

## 1. 改名

- [x] 抽象方法、两个覆盖和 `PlayerAnimatorManager` 的调用改成 `SuccessfullyUsedConsumable`。
- [x] 技术设计点名的现用文档跟着改。已收尾的版本文档不改。

**阶段门槛：** 动画事件名仍是 `SuccessfullyCastConsumable`。

## 2. 单元测试

- [x] `ConsumableCallbackRenameTests` 断言方法名和调用处。
- [x] 测试不创建角色。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.36 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
