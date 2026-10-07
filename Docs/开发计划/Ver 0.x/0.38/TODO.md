# 0.38 TODO：人不会卡在下落里

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。0.x 不写单元测试。

## 0. 开始前

- [x] 0.37 已收尾。
- [x] 读过技术设计：Blocker 不再顶人，判地的层遮罩改成常量。

**阶段门槛：** 知道不改判地、贴地、转身的写法，不改场景、控制器和敌人预制体，不写 Edit Mode 测试。

## 1. 玩家的 Blocker

- [x] `Player.prefab` 的 Blocker 刚体改成运动学，碰撞检测改成 Continuous，和敌人一致。
- [x] `PlayerManager.Awake` 让身体胶囊体忽略自己第 10 层的碰撞体。

**阶段门槛：** 预制体 diff 只有这两行。敌人的 Blocker 仍和玩家身体胶囊体相撞。

## 2. 地面层遮罩

- [x] `GroundCheckLayer` 序列化字段换成常量，三层不变，`Start` 里不再赋值。

**阶段门槛：** `PlayerLocomotionManager` 只有这一处改动。运行中判地的层不会变成 0。

## 3. 编辑器验收

- [x] Console 没有新的编译错误。
- [x] 用户在编辑器里按游戏设计第 3 节看完。
- [x] 不算高风险，用户确认不调用 reviewer。

**阶段门槛：** 用户验收之前不提交代码。

## 4. 收尾

- [x] 更新日志写 0.38 一条 `fix(移动)`，开头 `fix` 的数字加一。
- [x] 0.x 大纲把 0.38 标成已完成，开发计划第 1 节、第 3 节的 0.x 号段改到 0.38。
- [x] 写回 [FR-移动-05](../../../需求文档/功能需求/玩家移动与闪避.md)、[既有切片](../../../需求文档/验收方式/既有切片.md) 第 5 条，以及 [正确性](../../../代码分析/正确性.md) 里 FR-移动-05 的两处。
- [x] 提交只含 `Assets/Scripts/Character/Player/PlayerLocomotionManager.cs`、`Assets/Scripts/Character/Player/PlayerManager.cs`、`Assets/Prefabs/Characters/Player.prefab`、本版三件套、`Docs/开发计划/README.md`、`Docs/开发计划/Ver 0.x/README.md`、`Docs/更新日志/更新日志.md`，以及上一条的三份文档。

**阶段门槛：** 游戏设计第 3 节全部满足。没有单元测试要跑。
