# 0.7 TODO：关掉的副本移出场景

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.6 已完成。读过游戏设计第 2 节的留下和移出。

**阶段门槛：** 知道本版不从场景移出地图，不删脚本，不删预制体资源。

## 1. 备份，再从场景移出关闭的副本

- [x] Unity 里保存 `Game`。把 `Assets/Scenes/Game.unity` 复制为 `Assets/Scenes/Game_before_0.7.unity`，不复制 `.meta`。
- [x] 在 Hierarchy 把 Player SP、Player SPP 移出场景。
- [x] 把已关闭的 Archer、Boss (Simple A.I)、Enemy (Simple A.I )，以及已关闭的 Enemy (Humanoid A.I - Melee) 实例移出场景。
- [x] 确认激活的是 Player、Humanoid A.I - Melee (0)(1)(2)、Map 0。灰色物体不重新打开。预制体仍在 Project 里。

**阶段门槛：** 游戏设计第 2 节的移出项不在 Hierarchy 里。

## 2. 空图标不报错

- [x] `QuickSlotsUI` 三处 `itemIcon == null` 不再 `LogError`，直接返回。
- [x] Image 槽位本身为空的 `LogError` 不删。

**阶段门槛：** 进 Play 不再出现那三条 `itemIcon == null`。

## 3. 收尾

- [x] 只看本版：激活的是 Player、三只近战、Map 0；控制台没有那三条 `itemIcon == null`。WebGL 的 `node.exe` 不算。
- [x] 大纲里的 0.7 标成已完成。`Game.unity` 与备份 `Game_Before_0.7.unity` 一起提交。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
