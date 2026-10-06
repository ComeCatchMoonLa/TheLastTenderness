using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BonfireTravelTests
    {
        [Test]
        public void List_KeepsOnlyLitFires()
        {
            string[] names = { "甲", "乙" };
            bool[] lit = { true, false };
            string[] into = new string[2];
            int count = BonfireTravel.List(names, lit, into);
            Assert.AreEqual(1, count);
            Assert.AreEqual("甲", into[0]);
        }

        [Test]
        public void TryGo_RejectsUnlit_AndAcceptsAnotherLitFire()
        {
            Assert.IsFalse(BonfireTravel.TryGo(true, false, false));
            Assert.IsFalse(BonfireTravel.TryGo(false, true, false));
            Assert.IsFalse(BonfireTravel.TryGo(true, true, true));
            Assert.IsTrue(BonfireTravel.TryGo(true, true, false));
        }
    }
}
