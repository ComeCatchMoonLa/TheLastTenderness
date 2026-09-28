# 2.8 TODO：瓶碎片加总次数，骨片按表加一口

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 2.7 已收尾。
- [x] 读过技术设计：总次数在玩家身上，一口回复只读 `FlaskRecoveryTable`。

**阶段门槛：** 知道这一版不写 `consumableLeft`。

## 1. 总次数

- [x] `flaskTotal`、`estusShare`、`ashShare` 的初值是 3、3、0。
- [x] `ObtainAshFlask` 一次变成 4、3、1，再调用不变。
- [x] `AddFlaskShard` 加到 15 为止，不改两边口数。

**阶段门槛：** 三个方法都不写 `consumableLeft`。

## 2. 骨片和表

- [x] `FlaskRecoveryTable` 和 `Assets/Data/Items/Consumables/Flask Recovery Table.asset` 按技术设计的十一行填好。
- [x] `AddBoneShard` 加到 10 为止。
- [x] `TryGetFlaskSip` 按当前档读表。表不合法时打出资产名和字段并返回 false。
- [x] `SucessfullyUsedConsumable` 用这个返回值，不再读瓶子资产上的两段回复。

**阶段门槛：** 代码里没有 250、600 这些字面量。

## 3. 单元测试

- [x] `FlaskChargeTests` 覆盖开局、拿到灰瓶、十五口、十档，以及 0 档和 10 档的读数。
- [x] 瓶子资产上的回复写成 1 时，读出的仍是表。
- [x] 表没接上时返回 false，错误被 `LogAssert.Expect` 接住。
- [x] 测试不调用 `ConsumableRemaining`，不调用 `RefillConsumablesToMax`。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 4. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 2.8 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
