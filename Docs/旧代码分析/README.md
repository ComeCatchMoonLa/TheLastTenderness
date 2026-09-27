# 旧代码分析

> **看现在 `Game` 里还在跑的角色、物品动作和伤害。** 不写重构方案，不排新版本。

看这些：

- [`../../Assets/Perfabs/Player/Player.prefab`](../../Assets/Perfabs/Player/Player.prefab)
- [`../../Assets/Perfabs/Humanoid A.I - Melee.prefab`](../../Assets/Perfabs/Humanoid%20A.I%20-%20Melee.prefab)
- `Assets/Scripts` 里的物品动作和伤害碰撞体

General / Boss 状态脚本还在工程里，当前场景基本不用。这里只说明它们为什么留着，不逐个拆。

| 文档 | 内容 |
| --- | --- |
| [`架构.md`](架构.md) | 角色怎么拼起来，动画和攻击从哪进 |
| [`接口.md`](接口.md) | 类型之间靠什么说话 |
| [`可维护性.md`](可维护性.md) | 可读、好不好改、加一种行为要动哪里 |
| [`性能.md`](性能.md) | 这条路径每帧在做什么 |
