# 0.2 TODO：唯一运行入口

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 0.1 已完成，或至少 Unity 版本控制已经卸掉、工作区干净。
- [ ] 读过技术设计：只追加场景，保留 Input System 的 `m_configObjects`。

**阶段门槛：** 知道本版不改场景物体。

## 1. Build Settings 只登记 Game

- [ ] `EditorBuildSettings.asset` 的场景列表只追加 `Assets/Scenes/Game.unity`。
- [ ] `m_configObjects` 里的 Input System 配置还在。
- [ ] 不改 `Game.unity`。

**阶段门槛：** Build Settings 里只有 `Game`。

## 2. 写明入口并验收

- [ ] 更新 `Docs/现状.md` 和根目录 `README.md`。
- [ ] Play：激活的玩家是 Player SPP，开着的近战敌人来自 `Humanoid A.I - Melee`，移动和攻击与本版开始前一致。
- [ ] 大纲里的 0.2 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
