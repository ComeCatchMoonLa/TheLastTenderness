# 战斗 HUD

> **生命、精力、专注、灵魂数、快捷栏图标和回复剩余次数、敌人头上的短时血条、交互提示，当前战斗画面里已经有。** 灵魂增减走 `AddSouls` 时，HUD 数字跟着变。

锁定标记见 [FR-镜头-07](镜头与锁定.md)，不在本文件重复编号。

## 已有入口

| 内容 | 类型 | 路径 |
| --- | --- | --- |
| HUD 根 | `HUDWindowsManager` | `Assets/Scripts/UI/Player UI/HUD Windows/HUDWindowsManager.cs` |
| 生命 | `HealthBar` | `Assets/Scripts/UI/Player UI/HUD Windows/Bar/HealthBar.cs` |
| 精力 | `StaminaBar` | `Assets/Scripts/UI/Player UI/HUD Windows/Bar/StaminaBar.cs` |
| 专注 | `FocusPointsBar` | `Assets/Scripts/UI/Player UI/HUD Windows/Bar/FocusPointsBar.cs` |
| 灵魂数字 | `SoulCountUI` | `Assets/Scripts/UI/Player UI/HUD Windows/SoulCountUI.cs` |
| 快捷栏 | `QuickSlotsUI` | `Assets/Scripts/UI/Player UI/HUD Windows/QuickSlotsUI.cs` |
| 交互提示 | `InteractUI` | `Assets/Scripts/UI/Player UI/HUD Windows/InteractUI.cs` |
| 小怪血条 | `UIEnemyHealthBar` | `Assets/Scripts/UI/UIEnemyHealthBar.cs` |

三条玩家资源条由 `PlayerStatsManager` 在数值变化时写入，条自己不每帧去读属性。灵魂数字跟 `AddSouls` 走（1.15）。回复剩余次数在快捷栏上（2.7）。

`WorldUIManager` 上有 Boss 血条。当前 `Game` 的世界根物体未激活，本需求不把它列为必达 HUD。

## 需求

### FR-界面-01 三条资源条

**当前已有**

战斗中能同时看见生命、精力、专注。受伤后生命变短，喝血成功后变长，冲刺时精力变短，停手回复时精力变长。三条都到上限时显示为满，到 0 时显示为空。

### FR-界面-02 快捷栏图标

**当前已有**

换右手武器、换当前法术或换当前消耗品后，快捷栏上对应格子的图标换成新的那一件。没有图标的格子保持空白，不挡住另外两格。

从背包点选法术和消耗品会改变当前项，见 [FR-装备-07](装备与道具.md)。方向键或现有快捷切换能改到的那些，图标要跟着变。

### FR-界面-03 敌人血条

**当前已有**

打中一名近战敌人。它头上出现血条，长度对应当前生命。停止再打后血条隐藏，隐藏延迟写在配置上。再次打中，血条重新出现并反映新的生命。隐藏延迟是表现层细化，调研未写精确条件。

### FR-界面-04 交互提示

**当前已有**

玩家走进可交互物的检测范围。屏幕上出现该物体配置的提示文字。离开范围后提示消失。对燃火来说，未点燃与已点燃的提示文字不同，见 [FR-燃火-01](燃火休息与死亡惩罚.md)。

### FR-界面-05 灵魂数字始终同步

**当前已有**

灵魂增加或减少的同一次结果里，HUD 数字等于身上的 `soulCount`。包括击杀（已有）、升级扣除（[FR-属性-04](属性与升级.md)）、死亡清零（[FR-燃火-05](燃火休息与死亡惩罚.md)）、捡回残留（[FR-燃火-06](燃火休息与死亡惩罚.md)）。

落点：`PlayerStatsManager.AddSouls` 改数字时写 `SoulCountUI`（1.15）。

### FR-界面-06 回复次数显示

**当前已有**

快捷栏的回复格子上能看到剩余次数。喝成功一次，次数显示减一。次数为 0 时显示为 0，再按使用出现 [FR-回复-02](回复类消耗品.md) 的耸肩。燃火补满后，显示回到上限。

落点：`QuickSlotsUI` 读取 [FR-回复-04](回复类消耗品.md) 记在玩家身上的次数（2.7）。血瓶和灰瓶不跟快捷道具挤掉，见 [FR-菜单-07](菜单.md)。

### FR-界面-07 状态图标、横幅与界面自动隐藏

**待补**

- 角色身上正在生效的异常与增益在 HUD 上有小图标；异常积蓄时还能看见对应的槽往上走，结算后只留下持续状态的图标。被动效果不占位。
- 生命低于一档时，画面边缘发红；余烬形态下血条变长带火、角色身上有火星（余烬外形做不做见 [FR-周目-01](周目与结局.md) 的待裁定项）。
- 战斗中不弹出伤害数字。死亡、头目战败、有人入侵、获得道具、留言被评好回血、篝火短句等事件各出对应横幅；获得道具的横幅可以再打开看说明。
- 无操作一段时间后战斗界面自动隐藏，操作后唤回；精力低位时的绿条在隐藏后仍单独给出。召唤进来的幻影与入侵者在画面一侧有头像和身份颜色，旁边带其血条（联机规则见 [联机](联机.md)）。

落点：当前仓库未找到对应类型。
