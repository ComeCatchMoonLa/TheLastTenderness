using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class PostTreatmentQualityTests
    {
        [Test]
        public void TwoTiers_MatchApplyLabels()
        {
            Assert.AreEqual("低", PostTreatmentQualityTable.Label(PostTreatmentQuality_Options.low));
            Assert.AreEqual("高", PostTreatmentQualityTable.Label(PostTreatmentQuality_Options.high));
        }

        [Test]
        public void SwitchTwice_ReturnsToStart()
        {
            PostTreatmentQuality_Options option = PostTreatmentQuality_Options.low;
            option = PostTreatmentQualityTable.Next(option);
            option = PostTreatmentQualityTable.Next(option);
            Assert.AreEqual(PostTreatmentQuality_Options.low, option);
        }
    }
}
