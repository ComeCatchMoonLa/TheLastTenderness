# 2.10 TODO：装备窗能改选这只手的每一把

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 2.9 已收尾。
- [x] 读过技术设计：新槽在枚举末尾，改选走 `SelectHandWeapon`。

**阶段门槛：** 知道已有 `EquipmentSlotType` 成员的序号不改。

## 1. 下标 2 和 3

- [x] 枚举末尾追加四个武器槽。
- [x] `TryHandIndex` 和 `SetAllSlotIcon` 按数组长度画。
- [x] `SelectHandWeapon` 写当前下标和当前武器。
- [x] 选中格子和从背包放入都走这个下标。
- [x] `UI.prefab` 的 `weaponSlotsUI` 加上四格。不改已有四格的槽类型。

**阶段门槛：** 下标超出数组长度时不写。

## 2. 单元测试

- [x] `HandWeaponSelectTests` 覆盖左右手下标 2、3，以及下标 4 选不中。
- [x] 测试不创建 Animator，不调用 `LoadWeaponOnSlot`。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 2.10 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
