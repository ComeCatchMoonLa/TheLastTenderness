# 1.32 TODO：特效模型字段拼写

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.31 已收尾。
- [x] 读过技术设计：只改 `instantialtedFXModel`，fileID 不动。

**阶段门槛：** 知道不改目录、类名和动画方法名。

## 1. 改名

- [x] 字段和 `FlaskItem`、`BombItem` 的写入改成 `instantiatedFXModel`。
- [x] `Player.prefab` 与 `Game_Before_0.7.unity` 只改键名，`{fileID: 0}` 保持。

**阶段门槛：** 这两份 YAML 里该引用仍是空。

## 2. 单元测试

- [x] `InstantiatedFxFieldTests` 断言字段名和两份文件的键。
- [x] 测试不创建角色。

**阶段门槛：** Test Runner 里这个测试通过后才收尾。

## 3. 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 1.32 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
