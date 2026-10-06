using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class PhantomTeamTests
    {
        [Test]
        public void Team_AllyMatchesTheHost_InvaderUsesTheOtherTeam()
        {
            int ally = PhantomTeam.Team(false, 2, 1);
            int invader = PhantomTeam.Team(true, 2, 1);
            Assert.AreEqual(2, ally);
            Assert.AreEqual(1, invader);
            Assert.IsTrue(PhantomTeam.SameTeamSkips(ally, 2));
            Assert.IsTrue(PhantomTeam.SameTeamSkips(ally, ally));
            Assert.IsFalse(PhantomTeam.SameTeamSkips(invader, 2));
        }
    }
}
