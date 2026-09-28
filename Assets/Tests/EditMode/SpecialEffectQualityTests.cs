using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SpecialEffectQualityTests
    {
        [Test]
        public void TwoTiers_MatchApplyLabels()
        {
            Assert.AreEqual("低", SpecialEffectQualityTable.Label(SpecialEffectQuality_Options.low));
            Assert.AreEqual("高", SpecialEffectQualityTable.Label(SpecialEffectQuality_Options.high));
        }

        [Test]
        public void SwitchTwice_ReturnsToStart()
        {
            SpecialEffectQuality_Options option = SpecialEffectQuality_Options.low;
            option = SpecialEffectQualityTable.Next(option);
            option = SpecialEffectQualityTable.Next(option);
            Assert.AreEqual(SpecialEffectQuality_Options.low, option);
        }
    }
}
