using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class BossClearTests
    {
        [Test]
        public void Apply_DropsTheFog_AndUnsealsTheNewFire()
        {
            bool fogUp = true;
            bool bossStillHere = true;
            bool fireSealed = true;
            BossClear.Apply(ref fogUp, ref bossStillHere, ref fireSealed);
            Assert.IsFalse(fogUp);
            Assert.IsFalse(bossStillHere);
            Assert.IsFalse(fireSealed);
            Assert.IsFalse(BossClear.CanOpenAgain(true));
            Assert.IsTrue(BossClear.CanOpenAgain(false));
        }
    }
}
