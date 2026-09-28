using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SpecialEffectQualityTests
    {
        [Test]
        public void TwoTiers_MatchApplyLabels()
        {
            Assert.AreEqual("低", SettingsWindowManager.SpecialEffectQualityLabel(SpecialEffectQuality_Options.low));
            Assert.AreEqual("高", SettingsWindowManager.SpecialEffectQualityLabel(SpecialEffectQuality_Options.high));
        }

        [Test]
        public void SwitchTwice_ReturnsToStart()
        {
            SpecialEffectQuality_Options option = SpecialEffectQuality_Options.low;
            option = SettingsWindowManager.NextSpecialEffectQuality(option);
            option = SettingsWindowManager.NextSpecialEffectQuality(option);
            Assert.AreEqual(SpecialEffectQuality_Options.low, option);
        }
    }
}
