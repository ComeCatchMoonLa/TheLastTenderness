# 0.12 TODO：设置和暂停会生效

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 1.3 已完成。工作区里没有未提交的玩法改动。
- [ ] 读过技术设计：垂直同步、相机速度、死亡后的暂停是三处接线，不改职责。

**阶段门槛：** 知道 0.13 的镜头缓动、0.14 的炸弹、0.15 的受伤音不在本版。

## 1. 垂直同步

- [ ] `VSYNC_SwitchOption` 改完枚举后调用 `VSYNC_ApplyCurrentOption`。
- [ ] Apply 按启用 / 禁用写 `QualitySettings.vSyncCount` 为 1 或 0。

**阶段门槛：** 当次切换和开局套用都写到 `vSyncCount`。帧率档的代码不动。

## 2. 相机速度

- [ ] `SettingsWindowManager` 取到玩家。
- [ ] 两个 Save 在写入设置数据之后调用 `ApplyCameraSpeedSetting`。

**阶段门槛：** 当次保存后，`leftAndRightSpeed` 与 `upAndDownSpeed` 等于刚写入的数据。

## 3. 死亡后关掉暂停

- [ ] `isDead` 且暂停菜单已打开时，仍处理返回和选择器。
- [ ] 没开菜单时，死亡仍不处理界面，也不新开暂停。

**阶段门槛：** 关掉根窗口仍只走 `EscWindowsManager.Close`，由它把 `timeScale` 写回 1。

## 4. Play 验收

- [ ] 游戏设计第 3 节看过。走路不重测。
- [ ] 大纲里的 0.12 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
