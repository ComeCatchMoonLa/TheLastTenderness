# 2.95 TODO：下一周收回箱子、幻影墙和近战

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 2.94 已收尾。2.80 的下一周已经收火、门、钥匙和头目。
- [x] 读过技术设计：`Apply` 再调箱子、幻影墙和 `RestoreAfterRest`。

**阶段门槛：** 知道不刷新拾取物，不关雾门。

## 1. 收回

- [ ] `ChestLoot.ForNewCycle` 返回假。`AfterRest` 仍保持开过。
- [ ] `OpenChest.CloseForNewCycle` 写上 `opened`、标签和关上的姿势。
- [ ] `IllusionWall.CloseForNewCycle` 关上并打开碰撞和渲染。`AfterRest` 仍空。
- [ ] `NewCycle.Apply` 找出这三样并调用。近战走 `RestoreAfterRest`。
- [ ] `NewCyclePropTests` 按技术设计。[`可测试性.md`](../../../代码分析/可测试性.md) 补上 2.95。

**阶段门槛：** 储物箱和背包里的非钥匙不在这次调用里。

## 2. 验收

- [ ] 游戏设计第 3 节在编辑器里看过。Edit Mode 里 `NewCyclePropTests` 通过。
- [ ] 大纲里的 2.95 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
