# 1.21 TODO：左右手段位系数收成一处

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 2.6 已收尾。
- [ ] 读过技术设计：四档只在 `PhaseDamageMultiplier` 里读。

**阶段门槛：** 知道两手都不用时仍是 1，`critical` 不进四档。

## 1. 系数

- [ ] `PhaseDamageMultiplier` 按技术设计返回。
- [ ] `DealDamage` 左右手都调用它。
- [ ] `ResolveIncomingHit` 的参数不改。

**阶段门槛：** 四档字段名仍是原来的四个。

## 2. 单元测试

- [ ] `PhaseDamageMultiplierTests` 断言左右手四档相同，两手都不用时是 1，`critical` 是 1。
- [ ] 测试不创建 `DamageCollider`，不创建 Animator。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 大纲里的 1.21 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
