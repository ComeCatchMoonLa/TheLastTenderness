using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class PostTreatmentQualityTests
    {
        [Test]
        public void TwoTiers_MatchApplyLabels()
        {
            Assert.AreEqual("低", SettingsWindowManager.PostTreatmentQualityLabel(PostTreatmentQuality_Options.low));
            Assert.AreEqual("高", SettingsWindowManager.PostTreatmentQualityLabel(PostTreatmentQuality_Options.high));
        }

        [Test]
        public void SwitchTwice_ReturnsToStart()
        {
            PostTreatmentQuality_Options option = PostTreatmentQuality_Options.low;
            option = SettingsWindowManager.NextPostTreatmentQuality(option);
            option = SettingsWindowManager.NextPostTreatmentQuality(option);
            Assert.AreEqual(PostTreatmentQuality_Options.low, option);
        }
    }
}
