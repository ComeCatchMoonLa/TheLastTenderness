using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class DisplayQualityTests
    {
        [Test]
        public void SixTiers_MatchApplyLabelsAndLevels()
        {
            AssertTier(DisplayQuality_Options.veryLow, "非常低", 0);
            AssertTier(DisplayQuality_Options.low, "低", 1);
            AssertTier(DisplayQuality_Options.medium, "中等", 2);
            AssertTier(DisplayQuality_Options.high, "高", 3);
            AssertTier(DisplayQuality_Options.veryHigh, "非常高", 4);
            AssertTier(DisplayQuality_Options.Ultra, "最高", 5);
        }

        [Test]
        public void NextSixTimes_ReturnsToStart()
        {
            DisplayQuality_Options option = DisplayQuality_Options.veryLow;
            for (int i = 0; i < 6; i++)
                option = SettingsWindowManager.NextDisplayQuality(option);
            Assert.AreEqual(DisplayQuality_Options.veryLow, option);
        }

        [Test]
        public void PreviousSixTimes_ReturnsToStart()
        {
            DisplayQuality_Options option = DisplayQuality_Options.veryLow;
            for (int i = 0; i < 6; i++)
                option = SettingsWindowManager.PreviousDisplayQuality(option);
            Assert.AreEqual(DisplayQuality_Options.veryLow, option);
        }

        static void AssertTier(DisplayQuality_Options option, string label, int level)
        {
            Assert.AreEqual(label, SettingsWindowManager.DisplayQualityLabel(option));
            Assert.AreEqual(level, SettingsWindowManager.DisplayQualityLevel(option));
        }
    }
}
