# 0.38 TODO：人不会卡在下落里

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。0.x 不写单元测试。

## 0. 开始前

- [ ] 0.37 已收尾。
- [ ] 读过技术设计：只改 `PlayerLocomotionManager`，判地、贴地、转身走刚体。

**阶段门槛：** 知道不改预制体、场景、控制器和 `Layer.cs`，不写 Edit Mode 测试。

## 1. 判地和贴地

- [ ] `HandleFall` 的脚底位置和向前方向读 `rigidBody`。
- [ ] 向下射线从胶囊体底部球心打，长度是起点高度加 `HeightToBeginLand`。
- [ ] 向下射线用 `RaycastNonAlloc`，忽略触发器，跳过自己，取最近的命中。
- [ ] 打中时用竖直速度贴地，不写 `transform.position`。刚离地时去掉向上的速度。
- [ ] `Land` 按脚底到命中的落差判，门槛不变。
- [ ] 射线打不到时用短球探胶囊体底面下方。有命中就算站住，`wasInAir` 时播 `NonCombat Whole Body Empty`，不抬高。

**阶段门槛：** `HandleFall` 里没有对 `transform` 的读写。

## 2. 转身

- [ ] `HandleRotate` 用 `MoveRotation`，从 `rigidBody.rotation` 插值。锁定方向用 `rigidBody.position` 算。
- [ ] `HandleRoll`、`HandleJump` 起手朝向用 `MoveRotation`。

**阶段门槛：** `PlayerLocomotionManager` 的 `FixedUpdate` 路径不再写 `transform`。

## 3. 编辑器验收

- [ ] Console 没有新的编译错误。
- [ ] reviewer 结论是「通过」或「有保留通过」。
- [ ] 用户在编辑器里按游戏设计第 3 节看完。

**阶段门槛：** 用户验收之前不提交代码。

## 4. 收尾

- [ ] 更新日志写 0.38 一条 `fix(移动)`，开头 `fix` 的数字加一。
- [ ] 0.x 大纲把 0.38 标成已完成，开发计划第 1 节、第 3 节的 0.x 号段改到 0.38。
- [ ] 写回 [FR-移动-05](../../../需求文档/功能需求/玩家移动与闪避.md)、[既有切片](../../../需求文档/验收方式/既有切片.md) 第 5 条，以及 [正确性](../../../代码分析/正确性.md) 里 FR-移动-05 的两处。
- [ ] 提交只含 `Assets/Scripts/Character/Player/PlayerLocomotionManager.cs`、本版三件套、`Docs/开发计划/README.md`、`Docs/开发计划/Ver 0.x/README.md`、`Docs/更新日志/更新日志.md`，以及上一条的三份文档。

**阶段门槛：** 游戏设计第 3 节全部满足。没有单元测试要跑。
