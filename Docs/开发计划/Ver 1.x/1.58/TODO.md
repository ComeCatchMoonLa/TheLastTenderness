# 1.58 TODO：屏幕界面不挂在角色下

> **人先改层级，代码后改。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。这一版不写 Edit Mode。收尾要等联调看完。

## 0. 开始前

- [ ] 1.57 已收尾。
- [ ] 读过技术设计：只拆屏幕界面，不改相机。

**阶段门槛：** 知道 `World` 保持关闭，知道代码改完之前不点 Play。

## 1. 人改层级

在 `Assets/Scenes/Game.unity` 里做。场景视图左上角的工具用「轴心」，不要用「中心」。

- [ ] 展开场景里的 `Player`，选中子物体 `UI`。它上面有 `Canvas`（屏幕空间）和 `PlayerUIManager`。
- [ ] 在 Hierarchy 里把 `UI` 拖到最外层，父物体留空。不要拖到 `Test Map - Interacte` 或 `World` 下面。
- [ ] 把这个物体改名为 `Global UI`。不要改它下面的 HUD、暂停、分页窗、弹出层的名字。
- [ ] 选中场景里的 `Player` 实例。只对「去掉 UI」这一条覆盖做 Apply，把 `Player.prefab` 里的这块界面也去掉。不要 Apply All。角色现在的位置、已经填过的走路字段，都不要写进预制体。
- [ ] 确认 `Player` 下面已经没有 `UI` 或 `Global UI`。`Global UI` 的父物体是场景根。
- [ ] `World` 保持灰色关闭。不要选中 `World Canvas`，不要把它或 `Boss Health Bar` 拖出来，不要改名，不要激活。

**阶段门槛：** 层级已经这样，并且还没有点 Play。做到这里就停，交给改代码。

## 2. 改两处查找

层级完成之后才做。

- [ ] `PlayerManager` 用序列化字段引用 `Global UI` 上的 `PlayerUIManager`，不再 `GetComponentInChildren`。
- [ ] `PlayerUIManager` 用序列化字段引用场景里的 `Player`，不再 `transform.root`。
- [ ] 两个字段都在 Inspector 里接上。空着时 Play 会报出物体名并停住。

**阶段门槛：** 场景里两处引用都不是空。

## 3. 联调后再收尾

- [ ] 场景工具仍是「轴心」。点中 `Player`，手柄在人身上。
- [ ] Play：血条在，暂停能打开背包和状态，靠近可交互物体时提示还在。
- [ ] 大纲里的 1.58 标成已完成。提交说明用中文。相机不在这次提交里。

**阶段门槛：** 上面两条都看过。没看过不收尾。
