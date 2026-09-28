# 1.26 TODO：敌人死亡时不再查找玩家

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.25 已收尾。
- [x] 读过技术设计：玩家在 `Awake` 缓存，死亡不再查找。

**阶段门槛：** 知道不改 `soulsAwardedDeath`，不改 `isDead`。

## 1. 缓存

- [x] `Awake` 缓存 `PlayerManager`。
- [x] `HandleEnemyDeathEvent` 用这份引用，不再 `FindAnyObjectByType`。
- [x] 引用为空时仍打日志并返回。

**阶段门槛：** 解锁和 `AwardSoulsOnDeath` 的顺序不改。

## 2. 单元测试

- [x] `EnemyDeathPlayerCacheTests` 断言魂只加在缓存指向的那名玩家身上，数额是 `soulsAwardedDeath`。
- [x] `lockOnFlag` 为假时仍为假。
- [x] 测试不调用 `TakeDamage`。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.26 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
