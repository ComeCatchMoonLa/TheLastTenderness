# 旧代码分析

> **`Assets/Scripts` 共 142 个脚本，按文件夹分成角色、动画、物品、伤害、界面、世界、相机、音效、数据和根目录公共脚本。** 下面四篇按这些模块写调用链。11 条纠错和 1.x 重构都写在 [`重构方案.md`](重构方案.md)。版本入口是 [`../开发计划/Ver 0.x/README.md`](../开发计划/Ver%200.x/README.md) 与 [`../开发计划/Ver 1.x/README.md`](../开发计划/Ver%201.x/README.md)。

当前 `Game` 里还在跑的角色是 [`../../Assets/Perfabs/Player/Player.prefab`](../../Assets/Perfabs/Player/Player.prefab) 的实例，以及三只 [`../../Assets/Perfabs/Humanoid A.I - Melee.prefab`](../../Assets/Perfabs/Humanoid%20A.I%20-%20Melee.prefab)。这两份预制体用 `Humanoid.controller`。General / Boss 状态只出现在 [`../../Assets/Scenes/Game_Before_0.7.unity`](../../Assets/Scenes/Game_Before_0.7.unity)。Boss、雾墙、宝箱、篝火和剧情对话的物体还在 `Game.unity` 里，挂在未激活的父节点下。

| 文档 | 内容 |
| --- | --- |
| [`架构.md`](架构.md) | 每个文件夹谁调用谁 |
| [`接口.md`](接口.md) | 旗标、动画名、伤害签名、物品入口、枚举 |
| [`可维护性.md`](可维护性.md) | 可读性、一处行为要同时看的地方、加一种东西要改哪里 |
| [`性能.md`](性能.md) | 每帧和每次命中实际做的查询 |
| [`重构方案.md`](重构方案.md) | 1.x 候选各版的调用链和边界 |

模块和文档的对应：

| 文件夹 | 主要类型 | 写在哪 |
| --- | --- | --- |
| `Character/` | `CharacterManager` 与七个 Manager；玩家移动、输入、战斗；敌人 `State.Tick` | 架构「角色」；接口「旗标」；性能「玩家」「近战」 |
| `Animator/` | `StateMachineBehaviour`、双手 IK | 架构「动画」；接口「播动画」「动画事件」 |
| `Items/0 Item Actions/` | `WeaponItemAction`、轻重击、格挡、弓、三系法术门禁 | 架构「武器动作」「法术」；接口「物品三种入口」 |
| `Items/Equipment/` | 武器槽、护甲、换模 | 架构「武器槽」「护甲」 |
| `Items/Spells/`、`Items/Consumables/` | 投射物、治疗、血瓶、炸弹 | 架构「法术」「消耗品」 |
| `Items/Interactions/` | 拾取、对话、箱子、篝火、雾墙、查看 | 架构「交互物」；接口「设置与文案」 |
| `Damage/` | 近战、弹药、法术、炸弹、环境伤害 | 架构「伤害」；接口「伤害入口」；性能「命中时」 |
| `UI/` | HUD、背包、装备窗、暂停菜单、弹窗 | 架构「界面」；性能「界面」 |
| `World/`、`Camera/` | Boss 战、雾墙、跟随与锁定 | 架构「世界与相机」；性能「相机」 |
| `Sounds/`、`SomeData/`、脚本根目录 | 一个 `AudioSource`、三份数据、枚举与层号 | 架构「数据与公共脚本」；接口「枚举」 |
