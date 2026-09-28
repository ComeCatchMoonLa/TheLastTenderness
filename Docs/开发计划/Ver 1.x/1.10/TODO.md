# 1.10 TODO：镜头偏移一次缓动

> **一节做完再做下一节。** 需求以 [`游戏设计.md`](游戏设计.md) 为准，改哪些文件以 [`技术设计.md`](技术设计.md) 为准。

## 0. 开始前

- [x] 1.9 已完成。
- [x] 读过技术设计：目标位置是静态方法，缓动只写一次。

**阶段门槛：** 知道跟随、旋转、碰撞和锁定扫描不在本版。

## 1. 三个目标交给一次缓动

- [x] `AimingCameraOffsetTarget`、`LockedCameraOffsetTarget`、`DefaultCameraOffsetTarget` 返回现在的三个向量。
- [x] `SmoothDamp` 只出现在一个私有方法里，仍用 `pivotOffsetVelocity` 和 `changeModeFadeTime`。
- [x] 三个公开方法和 `SetCameraPosOffset` 把目标交给这个私有方法。
- [x] `CameraPose` 保持私有。跟随、旋转、碰撞的 `switch` 不改。

**阶段门槛：** 距离和高度的默认值不改。

## 2. 单元测试

- [x] 传入左右距离和两个高度，三个目标与现在的公式相同。
- [x] 测试不调用 `SmoothDamp`，不创建场景相机。

**阶段门槛：** 测试在 Edit Mode。

## 3. 验收

- [x] 游戏设计第 3 节看过，含其中的单元测试。
- [x] 大纲里的 1.10 标成已完成。提交说明用中文。

**阶段门槛：** 游戏设计第 3 节全部满足。
