# 0.4 TODO：补回缺失资源

> **由人在 Unity 里导入。** 需求以 [`游戏设计.md`](游戏设计.md) 为准。

## 0. 开始前

- [x] 0.3 已收尾。不要 Import All。

## 1. 只导入三份预制体和它们的依赖

- [x] 打开 `D:\Project\Unity_Projects\TheLastTenderness 其他文件\TheLastTenderness0.9.1.unitypackage`。
- [x] 只导入雾、`Fire_02_FX`、营火，以及 Unity 标出的依赖。
- [x] 确认 `Assets/Scripts` 没有被旧文件覆盖。

**阶段门槛：** 打开 `Game` 不再报这三份 Missing Prefab。

## 2. 收尾

- [x] 大纲里的 0.4 标成已完成。这些资源默认仍不进 git。

**阶段门槛：** 游戏设计第 2 节全部满足。

## 3. 补丁：只导入 `Assets/Art`（已交付）

- [x] 打开同一个 `.unitypackage`。只勾 `Assets/Art`。不勾 `Assets/Scripts`，不勾 `Assets/Scenes`，不点 Import All。
- [x] 改掉导入后挡住编译的三处：`MuscleInspectorEditor` 的 `TreeView<int>`，`PostProcessingFactory` 的 `AssetCreationEndAction`，`MotionBlurComponent` 去掉 `OpenGLES2`。
- [x] `Construct/Fire/Cylinder` 的缺失材质不另导入。人在 Inspector 指定一份已有材质。
- [x] 大纲里写明这一节已交付。血条、体力条、快捷栏空图标仍留在 0.8。WebGL 的 `node.exe` 仍不进 0.x。

**阶段门槛：** 游戏设计第 3 节全部满足。
