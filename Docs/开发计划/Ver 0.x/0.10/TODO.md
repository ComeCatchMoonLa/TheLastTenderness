# 0.10 TODO：落地、交互锁、敌人双手

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.9 已完成。
- [x] 读过技术设计：上半身按层名判断，落地只在 `wasInAir` 时切动画。

**阶段门槛：** 知道 0.11 的吸收和数量不在本版。

## 1. 落地只切一次

- [x] `HandleFall` 记下 `wasInAir`。只有从空中落到地上且落差够，才切 `Land`。
- [x] 从空中落到地上但落差不够时，切 `NonCombat Whole Body Empty`，`isInteracting` 传 false。

**阶段门槛：** 人已经在地上时，这个方法不再调用 `PlayTargetAnimation("Land")`。

## 2. 交互锁

- [x] `HandleEmptyState` 在层名为 `Upper Body` 时直接返回。
- [x] `PlayTargetAnimation` 在控制器里找不到状态名时返回，不写 `isInteracting`。

**阶段门槛：** 上半身 Empty 不再清全身交互。错误状态名不会把人锁住。

## 3. 敌人双手顺序

- [x] `EnemyInventoryManager.Start` 在 `base.Start()` 之前，按右手槽里的武器类型写 `isTwoHandingWeapon`。

**阶段门槛：** `LoadWeaponsOnBothHands` 执行时，双手标记已经是 true 或保持原值。

## 4. Play 验收

- [x] 游戏设计第 3 节看过。不把普通走路和冲刺单独再测一遍，平地那一步只看会不会连播落地。
- [x] 大纲里的 0.10 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
