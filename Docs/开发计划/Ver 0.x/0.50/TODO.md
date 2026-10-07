# 0.50 TODO：活动范围围着放置点

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。0.x 不新写单元测试。

## 0. 开始前

- [x] 0.49 已收尾。
- [x] 读过技术设计：`LeftActivity` 量离放置点，周旋里也交给追击回家。

**阶段门槛：** 知道不改察觉距离，不改头目。

## 1. 放置点

- [x] `EnemyManager.LeftActivity` 用到 `placement` 的距离。
- [x] `PursueTargetState` 的活动范围改调它。`Leash.TooFar` 仍用 `distFromTarget`。
- [x] General 周旋和人形周旋在 `LeftActivity` 时设回家并回到追击。

**阶段门槛：** `Territory.PulledOut` 和 `TerritoryTests` 不改。

## 2. 验收

- [x] 游戏设计第 3 节在编辑器里看过。
- [x] 大纲里的 0.50 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。没有新的单元测试要跑。
