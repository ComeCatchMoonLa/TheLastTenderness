using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SettingsApplySplitTests
    {
        [Test]
        public void LeftAndRight_VisitTheCurrentNeighbor()
        {
            Assert.AreEqual(SettingsWinType.control, SettingsSubWindowNav.Left(SettingsWinType.gameSettings));
            Assert.AreEqual(SettingsWinType.gameSettings, SettingsSubWindowNav.Left(SettingsWinType.display));
            Assert.AreEqual(SettingsWinType.display, SettingsSubWindowNav.Left(SettingsWinType.sound));
            Assert.AreEqual(SettingsWinType.sound, SettingsSubWindowNav.Left(SettingsWinType.control));

            Assert.AreEqual(SettingsWinType.display, SettingsSubWindowNav.Right(SettingsWinType.gameSettings));
            Assert.AreEqual(SettingsWinType.sound, SettingsSubWindowNav.Right(SettingsWinType.display));
            Assert.AreEqual(SettingsWinType.control, SettingsSubWindowNav.Right(SettingsWinType.sound));
            Assert.AreEqual(SettingsWinType.gameSettings, SettingsSubWindowNav.Right(SettingsWinType.control));
        }

        [Test]
        public void DisplayApply_KeepsCurrentTiers()
        {
            SettingsDisplayApply.Vsync(VSYNC_Options.enable, out string enableLabel, out int enableCount);
            SettingsDisplayApply.Vsync(VSYNC_Options.disable, out string disableLabel, out int disableCount);
            Assert.AreEqual("启用", enableLabel);
            Assert.AreEqual(1, enableCount);
            Assert.AreEqual("禁用", disableLabel);
            Assert.AreEqual(0, disableCount);

            SettingsDisplayApply.FrameRate(FrameRateLimit_Options.unlimited, out string frameLabel, out int frameRate);
            Assert.AreEqual("无限制", frameLabel);
            Assert.AreEqual(-1, frameRate);

            SettingsDisplayApply.DisplayQuality(DisplayQuality_Options.veryLow, out string qualityLabel, out int level);
            Assert.AreEqual("非常低", qualityLabel);
            Assert.AreEqual(0, level);
        }
    }
}
