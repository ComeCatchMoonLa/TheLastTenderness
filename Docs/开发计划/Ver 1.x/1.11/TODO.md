# 1.11 TODO：查询助手收成一处

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.10 已完成。
- [ ] 读过技术设计：查询步骤在 `OverlapQuery`，半径和层留在调用点。

**阶段门槛：** 知道伏击、锁定扇形和追击切换不在本版。

## 1. 三处调用同一段查询

- [ ] `Func.cs` 里的 `OverlapQuery.CollectOverlaps` 与现在的装满才加倍相同。
- [ ] `PlayerCameraManager`、`IdleState`、`IdleStateHumanoid` 删掉私有副本并调用它。
- [ ] 锁定仍是 `maxLockOnDist`、`~0`、从 32 起。待机仍是 `detectionRadius`、`LayerMask.player`、从 16 起。
- [ ] `AmbushState` 仍每次 `OverlapSphere`。

**阶段门槛：** 不新增查询结果的容器类型。

## 2. 单元测试

- [ ] 不写 Edit Mode 测试。不测命中了哪些碰撞体。不把查询改成 `virtual`。

**阶段门槛：** 半径、层、初始容量和装满才加倍靠人看。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 大纲里的 1.11 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
