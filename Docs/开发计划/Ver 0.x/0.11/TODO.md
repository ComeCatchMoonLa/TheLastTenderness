# 0.11 TODO：吸收和数量

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 0.10 已完成。
- [x] 读过技术设计：吸收补进现有三段；数量不写回资产。

**阶段门槛：** 知道 1.3 才把三个部位收成一份描述。

## 1. 五段吸收

- [x] `EquipAllArmorModels` 为头、躯干、下身写入火、魔、雷、暗。空槽写成 0。

**阶段门槛：** 脱下某一部位后，该部位五种吸收都是 0。

## 2. 数量离开资产

- [x] 删掉 `CharacterInventoryManager.Start` 里对 `currentAmmo.cnt` 的赋值。
- [x] `PlayerInventoryManager` 按上限记下药和箭的剩余，使用时只减这份。
- [x] 血瓶、炸弹、玩家射箭改为调用这份剩余。箭打空后 `currentAmmo` 为 null。

**阶段门槛：** `Assets/Scripts` 里不再出现 `currentItemAmount--` 或 `currentAmmo.cnt =` 或 `--currentAmmo.cnt`。

## 3. Play 验收

- [x] 游戏设计第 3 节看过。
- [x] 大纲里的 0.11 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
