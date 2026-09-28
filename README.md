# TheLastTenderness

Unity **6000.5.9f1** 的第三人称动作游戏。现有玩法代码是基线，之后按切片做。

**打开。** 用 Unity Hub 打开本目录，版本与 `ProjectSettings/ProjectVersion.txt` 一致。Build Settings 里只有 `Assets/Scenes/Game.unity`。激活的玩家是 `Assets/Perfabs/Player/Player.prefab` 的实例，不是 `Perfabs/1 Old/Player`。开着的近战敌人是 `Assets/Perfabs/Humanoid A.I - Melee.prefab` 的实例。播放器 `bundleVersion` 的 `0.1` 不是开发计划版本。

**工作流。** 只维护 `main`，一次只做一个可验收切片。入口是 [`Docs/开发计划/README.md`](Docs/开发计划/README.md)。更新日志见 [`Docs/更新日志/更新日志.md`](Docs/更新日志/更新日志.md)。当前代码怎么拼的见 [`Docs/代码分析/README.md`](Docs/代码分析/README.md)。

**资源。** 商店能再下的模型、角色、音效、动画片段、字体和贴图不进 git，留在本机。缺了从商店再导入。版本库里是脚本、配置、场景和项目自己搭的预制体。
