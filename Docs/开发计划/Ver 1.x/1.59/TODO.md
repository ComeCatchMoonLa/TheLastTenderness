# 1.59 TODO：血迹和爆炸不再用 DestroyAuto

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 2.96 已收尾。1.58 仍只有施工文档。
- [x] 读过技术设计：根粒子 Stop Action 用 Destroy，删掉 `DestroyAuto`。

**阶段门槛：** 知道不改拾取。

## 1. 粒子

- [x] 血迹根粒子发射仍是 0.1、寿命上限 2.9、不循环、`stopAction` 为 2，去掉 `DestroyAuto`。
- [x] 爆炸根粒子发射仍是 0.5、寿命上限 4.5、不循环、`stopAction` 为 2，去掉 `DestroyAuto`。
- [x] 删 `DestroyAuto.cs`、它的 `.meta`，以及 `DestroyAutoRenameTests` 和它的 `.meta`。
- [x] `FxStopActionTests` 按技术设计。[`可测试性.md`](../../../代码分析/可测试性.md) 改 1.33 那一行并补上 1.59。

**阶段门槛：** 子物体上的循环粒子不单独设 Stop Action。

## 2. 验收

- [x] 游戏设计第 3 节在编辑器里看过。Edit Mode 里 `FxStopActionTests` 通过。
- [x] 大纲里的 1.59 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
