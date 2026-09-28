# 1.7 TODO：帧率上限收成一处

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.6 已完成。
- [ ] 读过技术设计：文案和上限数字只写在静态方法里。

**阶段门槛：** 知道垂直同步、画质、后处理、特效不在本版。

## 1. 前进和后退交给套用

- [ ] `FrameRateLimitLabel` 与 `FrameRateLimitValue` 按现在的四档。无限制的数字是 -1。
- [ ] `NextFrameRateLimit` 与 `PreviousFrameRateLimit` 保持现在的环绕。
- [ ] 前进、后退只改枚举，再调用 `FrameRateLimit_ApplyCurrentOption`。
- [ ] 套用用上述方法写文本和 `Application.targetFrameRate`。

**阶段门槛：** 其它画面开关不改。

## 2. 单元测试

- [ ] 四档的文案和数字与套用现在的结果相同。
- [ ] 前进四次、后退四次都回到起点。
- [ ] 测试不调用 `Application.targetFrameRate`。

**阶段门槛：** 测试在 Edit Mode。

## 3. 验收

- [ ] 游戏设计第 3 节看过，含其中的单元测试。
- [ ] 大纲里的 1.7 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
