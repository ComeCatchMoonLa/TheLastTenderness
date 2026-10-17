using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class AreaRefreshTests
    {
        [Test]
        public void OrdinaryReturns_BossAndStoryDoNot()
        {
            Assert.IsTrue(AreaRefresh.Returns(new AreaRefresh.Actor()));
            Assert.IsTrue(AreaRefresh.Returns(new AreaRefresh.Actor { dead = true }));
            Assert.IsFalse(AreaRefresh.Returns(new AreaRefresh.Actor { boss = true }));
            Assert.IsFalse(AreaRefresh.Returns(new AreaRefresh.Actor { story = true }));
        }

        [Test]
        public void MimicAndLizard_ReturnOnlyWhileAlive_OneShotFollowsTheAsset()
        {
            Assert.IsFalse(AreaRefresh.Returns(new AreaRefresh.Actor { mimic = true, dead = true }));
            Assert.IsTrue(AreaRefresh.Returns(new AreaRefresh.Actor { mimic = true }));
            Assert.IsFalse(AreaRefresh.Returns(new AreaRefresh.Actor { lizard = true, dead = true }));
            Assert.IsTrue(AreaRefresh.Returns(new AreaRefresh.Actor { lizard = true }));
            Assert.IsFalse(AreaRefresh.Returns(new AreaRefresh.Actor { oneShot = true, dead = true }));
            Assert.IsTrue(AreaRefresh.Returns(new AreaRefresh.Actor { oneShot = true, oneShotReturns = true, dead = true }));
        }
    }
}
