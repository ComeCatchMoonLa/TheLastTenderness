# 0.37 TODO：打开工程就在 Game

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。0.x 不写单元测试。

## 0. 开始前

- [x] 2.94 已收尾。
- [x] 读过技术设计：只改 `templateDefaultScene`。

**阶段门槛：** 知道不改 Build Settings，不改场景里的物体。

## 1. 默认场景

- [x] `templateDefaultScene` 改为 `Assets/Scenes/Game.unity`。

**阶段门槛：** 这一行不再指向 `SampleScene.unity`。

## 2. 验收

- [x] 游戏设计第 3 节对照过。Build Settings 仍只有 `Game`。
- [x] 大纲里的 0.37 标成已完成。提交说明用中文。这一次提交不含 `ProjectSettings.asset` 里这一行以外的字段。

**阶段门槛：** 游戏设计第 3 节全部满足。没有单元测试要跑。
