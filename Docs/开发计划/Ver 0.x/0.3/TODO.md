# 0.3 TODO：唯一运行入口

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.2 已完成：工程编辑器版本已是 `6000.5.9f1`，工作区干净。
- [x] 读过技术设计：只追加场景，保留 Input System 的 `m_configObjects`。

**阶段门槛：** 知道本版不改场景物体。

## 1. Build Settings 只登记 Game

- [x] `EditorBuildSettings.asset` 的场景列表只追加 `Assets/Scenes/Game.unity`。
- [x] `m_configObjects` 里的 Input System 配置还在。
- [x] 不改 `Game.unity`。

**阶段门槛：** Build Settings 里只有 `Game`。

## 2. 写明入口并验收

- [x] 更新 `Docs/更新日志.md` 和根目录 `README.md`。
- [x] Play：控制台里的 WebGL、缺预制体、空图标不是本版引入的，留到 0.4 和 0.7。Build Settings 只登记了 `Game`。
- [x] 大纲里的 0.3 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
