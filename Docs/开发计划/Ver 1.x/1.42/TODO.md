# 1.42 TODO：锁定查询从相机跟随里拆出

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.41 已收尾。
- [x] 读过技术设计：扫描挪走，跟随和撞墙留在原类，场景上的半径 30 要跟着走。

**阶段门槛：** 知道 `curLockOnTarget` 和锁定旗标仍留在 `PlayerCameraManager`。

## 1. 拆出

- [x] `LockOnQuery` 放在 `Player Camera` 上，拿走扫描、换目标和半径。
- [x] `PlayerCameraManager.UpdateLockOnTargets` 只转接。
- [x] `LockOnLayerTests` 改读新文件。

**阶段门槛：** `Game.unity` 里半径仍是 30，相机支架引用还在。

## 2. 单元测试

- [x] `LockOnSplitTests` 断言跟随、撞墙不调用扫描，场景半径仍是 30。
- [x] 测试不创建场景。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。锁定、跟随、撞墙拉近留给人看。
- [x] 大纲里的 1.42 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节里 Edit Mode 能断言的那一条满足。
