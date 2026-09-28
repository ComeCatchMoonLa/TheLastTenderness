# 1.17 TODO：受伤音和挥刀音共用选段

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.16 已收尾。
- [x] 读过技术设计：选段只留 `PickClipAvoidingPrevious`，播放和音量留在原方法。

**阶段门槛：** 知道两手都空时仍直接返回。

## 1. 一处选段

- [x] 增加 `PickClipAvoidingPrevious`。
- [x] `PlayRandomDamageSoundsFX` 和 `PlayRandomWeaponWhoosh` 在多于一首时都调用它。
- [x] 两处都不再 `new List<AudioClip>`。

**阶段门槛：** 长度为 1 时仍播第 0 首。受伤音多于一首时音量仍是 0.4。

## 2. 单元测试

- [x] `ClipSelectionTests` 覆盖游戏设计第 3 节。
- [x] 测试不调用 `PlayRandomDamageSoundsFX` 和 `PlayRandomWeaponWhoosh`。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.17 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
