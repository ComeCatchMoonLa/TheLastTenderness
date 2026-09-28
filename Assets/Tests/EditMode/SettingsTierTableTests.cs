using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SettingsTierTableTests
    {
        [Test]
        public void FourTiers_KeepLabelsNeighborsAndValues()
        {
            Assert.AreEqual("60", FrameRateLimitTable.Label(FrameRateLimit_Options.max60));
            Assert.AreEqual("90", FrameRateLimitTable.Label(FrameRateLimit_Options.max90));
            Assert.AreEqual("120", FrameRateLimitTable.Label(FrameRateLimit_Options.max120));
            Assert.AreEqual("无限制", FrameRateLimitTable.Label(FrameRateLimit_Options.unlimited));
            Assert.AreEqual(60, FrameRateLimitTable.Value(FrameRateLimit_Options.max60));
            Assert.AreEqual(90, FrameRateLimitTable.Value(FrameRateLimit_Options.max90));
            Assert.AreEqual(120, FrameRateLimitTable.Value(FrameRateLimit_Options.max120));
            Assert.AreEqual(-1, FrameRateLimitTable.Value(FrameRateLimit_Options.unlimited));
            Assert.AreEqual(FrameRateLimit_Options.unlimited, FrameRateLimitTable.Next(FrameRateLimit_Options.max120));
            Assert.AreEqual(FrameRateLimit_Options.max120, FrameRateLimitTable.Previous(FrameRateLimit_Options.unlimited));

            Assert.AreEqual("非常低", DisplayQualityTable.Label(DisplayQuality_Options.veryLow));
            Assert.AreEqual(0, DisplayQualityTable.Level(DisplayQuality_Options.veryLow));
            Assert.AreEqual("最高", DisplayQualityTable.Label(DisplayQuality_Options.Ultra));
            Assert.AreEqual(5, DisplayQualityTable.Level(DisplayQuality_Options.Ultra));

            Assert.AreEqual("低", PostTreatmentQualityTable.Label(PostTreatmentQuality_Options.low));
            Assert.AreEqual(PostTreatmentQuality_Options.high, PostTreatmentQualityTable.Next(PostTreatmentQuality_Options.low));
            Assert.AreEqual("高", PostTreatmentQualityTable.Label(PostTreatmentQuality_Options.high));
            Assert.AreEqual(PostTreatmentQuality_Options.low, PostTreatmentQualityTable.Next(PostTreatmentQuality_Options.high));

            Assert.AreEqual("低", SpecialEffectQualityTable.Label(SpecialEffectQuality_Options.low));
            Assert.AreEqual(SpecialEffectQuality_Options.high, SpecialEffectQualityTable.Next(SpecialEffectQuality_Options.low));
            Assert.AreEqual("高", SpecialEffectQualityTable.Label(SpecialEffectQuality_Options.high));
            Assert.AreEqual(SpecialEffectQuality_Options.low, SpecialEffectQualityTable.Next(SpecialEffectQuality_Options.high));
        }
    }
}
