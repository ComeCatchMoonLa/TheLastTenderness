# Ver 0.x：先把旧工程的债清掉

> **这一大版本不开发玩法。** 计划里的小版本做到 0.8。0.x 结束即阶段性冻结，1.x 现在不写。
>
> 前排仍是 AI 改文件、人工验收。要在 Unity 里改场景、或必须先点名路径才能删磁盘的，靠后。耦合按现有入口拆开，不另起一套框架。
>
> **不在 0.x 里的**：新玩法、改名（含 `Perfabs`）、把场景还在用的整包资源一次删掉、给历史脚本补测试、卸掉还没点名的包。

版本号约定见 [`../README.md`](../README.md)。0.1～0.4 已完成。0.4 补丁已交付：只导入了 `Assets/Art`，并改掉因此出现的 Unity 6 编译问题。

## 完成定义

0.1～0.8 已完成。

1. Git 是唯一版本控制（0.1，已完成）。
2. 工程编辑器是 Unity `6000.5.9f1`（0.2）。
3. Build Settings 只进 `Game`，文档写明当前玩家和近战敌人（0.3）。
4. 从 `TheLastTenderness0.9.1.unitypackage` 补回场景还在引用、工程里没有的资源（0.4）。不整包导入。
5. 移动旗标各只有一个写入方（0.5）。
6. `Assets/Scripts` 的中文注释是 UTF-8（0.6）。
7. 未参与当前玩法的关闭副本不再留在 `Game` 里，激活的玩家是名为 Player 的物体（0.7）。
8. 激活的玩家是仍挂现有脚本的预制体实例（0.8）。

## 前排：AI 实施，人工验收

### 0.1 只留 Git（已完成）

文档：[`0.1/游戏设计.md`](0.1/游戏设计.md) / [`0.1/技术设计.md`](0.1/技术设计.md) / [`0.1/TODO.md`](0.1/TODO.md)。

卸掉 `com.unity.collab-proxy`，关掉两处 Collaborate 残留开关，`ignore.conf` 移出版本库。不改场景，不改玩法脚本。

**证明的边界**：没有第二套版本控制。验收只查包清单、开关、`ignore.conf` 和商店资源是否仍被忽略。

### 0.2 编辑器改为 Unity 6.5（已完成）

文档：[`0.2/游戏设计.md`](0.2/游戏设计.md) / [`0.2/技术设计.md`](0.2/技术设计.md) / [`0.2/TODO.md`](0.2/TODO.md)。

本机已安装 `6000.5.9f1`。人先用它打开工程。打得开，再把 `ProjectVersion.txt` 对齐到这个版本。打不开就停，不改场景和玩法。

**证明的边界**：以后打开这个仓库用的编辑器是 Unity 6.5，不是 2022.3。

### 0.3 唯一运行入口（已完成，Play 由人看一眼）

文档：[`0.3/游戏设计.md`](0.3/游戏设计.md) / [`0.3/技术设计.md`](0.3/技术设计.md) / [`0.3/TODO.md`](0.3/TODO.md)。

Build Settings 只登记 `Game`。要激活的玩家是名为 Player 的物体；场景文件里目前开着的仍是 Player SPP，0.7 再换。近战敌人是 `Humanoid A.I - Melee` 的实例。不改场景里的物体。

**证明的边界**：运行入口只有一个，而且写在文档里。控制台里的缺预制体和空图标不是本版引入的。

### 0.4 补回缺失资源（已完成）

文档：[`0.4/游戏设计.md`](0.4/游戏设计.md) / [`0.4/技术设计.md`](0.4/技术设计.md) / [`0.4/TODO.md`](0.4/TODO.md)。

从 `TheLastTenderness0.9.1.unitypackage` 导入了雾、`Fire_02_FX`、营火，补丁再只导入 `Assets/Art`。导入带进的 `TreeView`、`EndNameEditAction`、`OpenGLES2` 已按 Unity 6 改掉。`Construct/Fire/Cylinder` 缺的那份材质用人指定一份已有材质，不另导入。

**证明的边界**：脚本和 `Game` 场景没被旧文件覆盖。这三处编译问题不再出现。WebGL 缺 `node.exe` 仍不在本版。

### 0.5 移动旗标只有一个写入方（已完成）

文档：[`0.5/游戏设计.md`](0.5/游戏设计.md) / [`0.5/技术设计.md`](0.5/技术设计.md) / [`0.5/TODO.md`](0.5/TODO.md)。

`isRolling` 只由 `HandleRollState` 写，`isJumping` 只由 `HandleJumpState` 写，`isInAir` 只由 `PlayerLocomotionManager` 写。`isSprinting` 只由 `InputManager` 写，`BlockAction` 不再改它。

**证明的边界**：这四个移动状态不再是谁都能改的公共袋子。

### 0.6 脚本注释改成 UTF-8（已完成）

文档：[`0.6/游戏设计.md`](0.6/游戏设计.md) / [`0.6/技术设计.md`](0.6/技术设计.md) / [`0.6/TODO.md`](0.6/TODO.md)。

`Assets/Scripts` 里仍是 GB2312 的 `.cs` 转成 UTF-8。注释已损坏的从基线 `6e7e791` 贴回。不改语句。

### 0.7 关掉的副本移出场景（已完成）

文档：[`0.7/游戏设计.md`](0.7/游戏设计.md) / [`0.7/技术设计.md`](0.7/技术设计.md) / [`0.7/TODO.md`](0.7/TODO.md)。

激活的内容是 **Player**、Humanoid A.I - Melee (0)(1)(2)、Map 0。灰色物体不重新打开。快捷栏 `itemIcon` 为空时不再 `LogError`。改场景前的备份是 `Assets/Scenes/Game_Before_0.7.unity`。

### 0.8 玩家收成预制体实例（已完成）

文档：[`0.8/游戏设计.md`](0.8/游戏设计.md) / [`0.8/技术设计.md`](0.8/技术设计.md) / [`0.8/TODO.md`](0.8/TODO.md)。

把场景里名为 Player 的物体收成 `Assets/Perfabs/Player/Player.prefab` 的实例。脚本保持现状。不用 `Perfabs/1 Old/Player`。
