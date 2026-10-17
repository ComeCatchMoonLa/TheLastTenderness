using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class DropRollTests
    {
        [Test]
        public void Discovery400_IsFourTimesDiscovery100()
        {
            Assert.AreEqual(0.1f, DropRoll.Rate(100f, 0.1f), 0.0001f);
            Assert.AreEqual(0.4f, DropRoll.Rate(400f, 0.1f), 0.0001f);
            Assert.IsTrue(DropRoll.Chance(100f, 0.1f, 0.09f));
            Assert.IsFalse(DropRoll.Chance(100f, 0.1f, 0.1f));
            Assert.IsTrue(DropRoll.Chance(400f, 0.1f, 0.39f));
            Assert.IsFalse(DropRoll.Chance(400f, 0.1f, 0.4f));
        }

        [Test]
        public void GuaranteedAlways_UnfilledRateNever_RollsStayIndependent()
        {
            Item sure = ScriptableObject.CreateInstance<Item>();
            sure.name = "必";
            Item rare = ScriptableObject.CreateInstance<Item>();
            rare.name = "稀";
            Item blank = ScriptableObject.CreateInstance<Item>();
            blank.name = "空率";
            DropLine[] lines =
            {
                new DropLine { item = sure, guaranteed = true },
                new DropLine { item = rare, rateFilled = true, baseRate = 0.1f },
                new DropLine { item = blank, rateFilled = false }
            };
            Item[] into = new Item[3];
            LogAssert.Expect(LogType.Error, "敌: baseRate 未填");
            int count = DropRoll.Collect("敌", lines, 100f, new float[] { 0.5f }, into);
            Assert.AreEqual(1, count);
            Assert.AreEqual(sure, into[0]);

            LogAssert.Expect(LogType.Error, "敌: baseRate 未填");
            count = DropRoll.Collect("敌", lines, 400f, new float[] { 0.2f }, into);
            Assert.AreEqual(2, count);
            Assert.AreEqual(sure, into[0]);
            Assert.AreEqual(rare, into[1]);
        }
    }
}
