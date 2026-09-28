# 1.22 TODO：正面点积收成一次

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.21 已收尾。
- [x] 读过技术设计：正面只在 `ComesFromFront` 里算。

**阶段门槛：** 知道阈值仍是 0.3，背刺的 ±0.8 不动。

## 1. 正面

- [x] `ComesFromFront` 按技术设计返回。
- [x] `HitComesFromFront` 调用它。
- [x] `ResolveIncomingHit` 里的点积改为调用它。`isBlocking` 为假或攻击者为空时仍不调用。

**阶段门槛：** 挡住之后的吸收和 `TakeDamage` 不改。

## 2. 单元测试

- [x] `ComesFromFrontTests` 断言正前方为真，正后方为假，重合为假。
- [x] 测试不创建 `DamageCollider`，不设置 `isBlocking`。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.22 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
