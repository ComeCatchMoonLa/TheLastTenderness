# 0.1 TODO：只留 Git

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 工作区在 `main`，没有未提交的玩法改动。
- [ ] 读过技术设计：两处开关都要关，`ignore.conf` 不并进 `.gitignore`。

**阶段门槛：** 知道本版不改场景、不移出资源。

## 1. 卸包并关掉开关

- [ ] 从 `Packages/manifest.json` 删除 `com.unity.collab-proxy`。
- [ ] `VersionControlSettings.asset` 与 `EditorSettings.asset` 的 `m_CollabEditorSettings.inProgressEnabled` 改为 `0`。模式仍是 Visible Meta Files。

**阶段门槛：** 包列表里没有 Collaborate。

## 2. `ignore.conf` 退出版本库

- [ ] 从版本库删除 `ignore.conf`，并在 `.gitignore` 忽略它。
- [ ] 确认商店资源目录仍然被现有 `.gitignore` 规则忽略。

**阶段门槛：** `git check-ignore ignore.conf` 能命中。

## 3. 验收

- [ ] 用 Unity 打开工程，确认能打开，且没有因此改到 `Game.unity`。
- [ ] `git status` 里没有商店资源。
- [ ] 大纲里的 0.1 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 2 节全部满足。
