# 1.58 TODO：屏幕界面不挂在角色下

> **人先改层级，代码后改。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。这一版不写 Edit Mode。收尾要等联调看完。

## 0. 开始前

- [ ] 1.57 已收尾。
- [ ] 读过技术设计：只拆屏幕界面，不改相机。

**阶段门槛：** 知道 `World` 保持关闭，知道代码改完之前不点 Play。

## 1. 人改层级

先打开 `Assets/Scenes/Game.unity`。场景视图左上角的工具用「轴心」，不要用「中心」。不要点 Play。

场景里的 `Player` 是预制体实例。直接把子物体 `UI` 拖到外面，会弹出「无法重构预制件实例」。这时点 **Open Prefab**，不要点 Cancel 之后继续在场景里拖，也不要 Unpack 整个 `Player`。

- [ ] 进入 `Assets/Prefabs/Player/Player.prefab` 的预制体模式。层级里有一块嵌套的界面，预制体是 `Assets/Prefabs/UI/Global UI/Global UI.prefab`。
- [ ] 在预制体层级里选中这块界面，按 Delete。这只把它从 `Player` 上拿掉，不删除 `Global UI.prefab` 这份资产。
- [ ] Ctrl+S 保存预制体，点层级顶上的箭头回到 `Game` 场景。场景里的 `Player` 下面不再有这块界面。
- [ ] 把 `Assets/Prefabs/UI/Global UI/Global UI.prefab` 拖到 Hierarchy 最外层，父物体留空。场景里的名字用 `Global UI`。不要拖到 `Test Map - Interacte` 或 `World` 下面。
- [ ] 不要对场景里的 `Player` 做 Apply All。角色现在的位置不要写进预制体。
- [ ] `World` 保持灰色关闭。不要选中 `World Canvas`，不要把它或 `Boss Health Bar` 拖出来，不要改名，不要激活。

**阶段门槛：** `Player` 下面没有界面。`Global UI` 的父物体是场景根。还没有点 Play。做到这里就停，交给改代码。

## 2. 改两处查找

层级完成之后才做。

- [x] `PlayerManager` 用序列化字段引用 `Global UI` 上的 `PlayerUIManager`，不再 `GetComponentInChildren`。
- [x] `PlayerUIManager` 用序列化字段引用场景里的 `Player`，不再 `transform.root`。界面内部原来用 `transform.root` 找玩家的，改从这块界面上取。
- [x] 两个字段都在场景里接上。空着时 Play 会报出物体名。

**阶段门槛：** 场景里两处引用都不是空。

## 3. 联调后再收尾

- [ ] 场景工具仍是「轴心」。点中 `Player`，手柄在人身上。
- [ ] Play：血条在，暂停能打开背包和状态，靠近可交互物体时提示还在。
- [ ] 大纲里的 1.58 标成已完成。提交说明用中文。相机不在这次提交里。

**阶段门槛：** 上面两条都看过。没看过不收尾。
