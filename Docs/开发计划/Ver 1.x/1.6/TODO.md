# 1.6 TODO：画质档收成一处

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.5 已完成。
- [x] 读过技术设计：文案和等级只写在静态方法里。

**阶段门槛：** 知道垂直同步、帧率、后处理、特效不在本版。

## 1. 前进和后退交给套用

- [x] `DisplayQualityLabel` 与 `DisplayQualityLevel` 按套用里的六档。`medium` 的文案是「中等」。
- [x] `NextDisplayQuality` 与 `PreviousDisplayQuality` 保持现在的环绕。
- [x] 前进、后退只改枚举，再调用 `DisplayQuality_ApplyCurrentOption`。
- [x] 套用用上述方法写文本和 `SetQualityLevel`。

**阶段门槛：** 其它画质开关不改。

## 2. 单元测试

- [x] 六档的文案和等级与套用现在的结果相同。
- [x] 前进六次、后退六次都回到起点。
- [x] 测试不调用 `QualitySettings`。

**阶段门槛：** 测试在 Edit Mode。

## 3. 验收

- [x] 游戏设计第 3 节看过，含其中的单元测试。
- [x] 大纲里的 1.6 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
