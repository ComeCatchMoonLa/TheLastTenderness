using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class FrameRateLimitTests
    {
        [Test]
        public void FourTiers_MatchApplyLabelsAndValues()
        {
            AssertTier(FrameRateLimit_Options.max60, "60", 60);
            AssertTier(FrameRateLimit_Options.max90, "90", 90);
            AssertTier(FrameRateLimit_Options.max120, "120", 120);
            AssertTier(FrameRateLimit_Options.unlimited, "无限制", -1);
        }

        [Test]
        public void NextFourTimes_ReturnsToStart()
        {
            FrameRateLimit_Options option = FrameRateLimit_Options.max60;
            for (int i = 0; i < 4; i++)
                option = SettingsWindowManager.NextFrameRateLimit(option);
            Assert.AreEqual(FrameRateLimit_Options.max60, option);
        }

        [Test]
        public void PreviousFourTimes_ReturnsToStart()
        {
            FrameRateLimit_Options option = FrameRateLimit_Options.max60;
            for (int i = 0; i < 4; i++)
                option = SettingsWindowManager.PreviousFrameRateLimit(option);
            Assert.AreEqual(FrameRateLimit_Options.max60, option);
        }

        static void AssertTier(FrameRateLimit_Options option, string label, int value)
        {
            Assert.AreEqual(label, SettingsWindowManager.FrameRateLimitLabel(option));
            Assert.AreEqual(value, SettingsWindowManager.FrameRateLimitValue(option));
        }
    }
}
