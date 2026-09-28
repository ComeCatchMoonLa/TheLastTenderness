# 1.27 TODO：待结算处决伤害不再留第二段入口

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.26 已收尾。
- [x] 读过技术设计：删掉无写入的二次扣血入口。

**阶段门槛：** 知道处决仍只走 `GetBackStabbed` / `GetRiposte`。

## 1. 收掉

- [x] 删掉 `ApplyPendingDamage`。
- [x] 删掉 `pendingCriticalDamage`。
- [x] `GetBackStabbed` 和 `GetRiposte` 不改。

**阶段门槛：** 不给处决再加一次扣血。

## 2. 单元测试

- [x] `PendingCriticalDamageRemovalTests` 反射断言这两个成员已经不在。
- [x] 测试不调用处决，不创建 Animator。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.27 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
