# 1.13 TODO：写台词和播空动画拆开

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.12 已完成。
- [ ] 读过技术设计：`SetTalkerStatus` 不再 `Play`，调用点另外播空动画。

**阶段门槛：** 知道结束对话的 `HandleTalkEnd` 不在本版。

## 1. 名字和动画分开

- [ ] `SetTalkerStatus` 只改 `talkWithSB` 和说话人名字。
- [ ] `PlayTalkerEmptyPose` 按现在的分支只 `Play` 一次 `NonCombat Whole Body Empty`。
- [ ] 逐句的三处入口和两段逐字协程在换说话人时调用它。
- [ ] `UpdateDialogueSBS_Helper` 不调用 `Play`。`HandleTalkEnd` 不改。

**阶段门槛：** 四种开关下的逐字、逐句、间隔和跳过与改之前相同。

## 2. 单元测试

- [ ] 调用 `UpdateDialogueSBS_Helper`：全文、列表少一句、名字是 NPC 的 `cName`。
- [ ] 测试不创建 Animator，不使用 `LogAssert`。

**阶段门槛：** 测试在 Edit Mode。

## 3. 验收

- [ ] 游戏设计第 3 节看过，含其中的单元测试。
- [ ] 大纲里的 1.13 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
