using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class AmmoMatchTests
    {
        [Test]
        public void MatchingPairs_Allow_CrossedPairs_DoNot()
        {
            Assert.IsTrue(AmmoMatch.Allows(WeaponType.bow, AmmoType.arrow));
            Assert.IsTrue(AmmoMatch.Allows(WeaponType.crossbow, AmmoType.bolt));
            Assert.IsTrue(AmmoMatch.Allows(WeaponType.greatbow, AmmoType.greatArrow));

            Assert.IsFalse(AmmoMatch.Allows(WeaponType.bow, AmmoType.greatArrow));
            Assert.IsFalse(AmmoMatch.Allows(WeaponType.greatbow, AmmoType.arrow));
            Assert.IsFalse(AmmoMatch.Allows(WeaponType.crossbow, AmmoType.arrow));
            Assert.IsFalse(AmmoMatch.Allows(WeaponType.bow, AmmoType.bolt));
        }
    }
}
