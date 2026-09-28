# 1.20 TODO：敌人当前状态可以读

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.19 已收尾。
- [x] 读过技术设计：写入仍只在 `SwitchToNextState`，外面读 `CurrentState`。

**阶段门槛：** 知道不改 `Tick` 的选态。

## 1. 读取

- [x] `SwitchToNextState` 公开，方法体仍只赋值。
- [x] `CurrentState` 返回 `currentState`。
- [x] `HandleStateMachine` 仍调用 `SwitchToNextState`。

**阶段门槛：** 不新增第二种写入。

## 2. 单元测试

- [x] `EnemyCurrentStateTests` 断言换上的状态与读到的是同一个对象。
- [x] 测试不调用 `Update`。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.20 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
