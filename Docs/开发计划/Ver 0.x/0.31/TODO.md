# 0.31 TODO：治疗扣蓝失败就停住

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [ ] 0.30 已完成。
- [ ] 读过技术设计：只看 `DeductMP` 的返回值，不改门禁。

**阶段门槛：** 知道失败起手仍是 `Shrug`，不在本版。

## 1. 扣蓝失败就返回

- [ ] `HealingSpell.AttemptToCastSpell` 在 `DeductMP` 返回 false 时返回。
- [ ] 返回 true 时仍生成 `spellWarmUpFX` 并播 `spellAnimation`。
- [ ] `isUsingSpell` 的提前返回不改。
- [ ] `MagicSpellAction` 和 `ProjectileSpell` 不改。

**阶段门槛：** `DeductMP` 本身不改。

## 2. 单元测试

- [ ] 不写。0.x 不补用例。

**阶段门槛：** 蓝够和蓝不够靠人看。

## 3. 验收

- [ ] 游戏设计第 3 节看过。
- [ ] 大纲里的 0.31 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
