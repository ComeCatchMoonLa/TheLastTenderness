# 1.51 TODO：下身换模类名对上 Hips

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.50 已收尾。
- [ ] 读过技术设计：只改类名和文件名。guid 不改，预制体 YAML 不改。

**阶段门槛：** 知道字段名仍是 `hipModelChanger`。

## 1. 改名

- [ ] 类和文件改成 `HipsModelChanger`。`.meta` 的 guid 仍是 `9df84263d0eaadb4c90ae009162a0efc`。
- [ ] `PlayerArmorManager`、`ArmorEquipTests` 和技术设计点名的代码分析跟着改。已收尾的版本文档不改。

**阶段门槛：** 预制体和场景文件不进这次 diff。

## 2. 单元测试

- [ ] `HipsModelChangerRenameTests` 断言类型名和 `armor_Hips_Slot`。
- [ ] 测试不创建新模型。

**阶段门槛：** Test Runner 里换模测试和这个改名测试通过后才收尾。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 收尾前 reviewer 的结论是「通过」或「有保留通过」。
- [ ] 大纲里的 1.51 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节里 Edit Mode 能断言的那一条满足。
