using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class AllyCallTests
    {
        [Test]
        public void Enters_GroupOnDiscovery_DistantOnlyAfterTheCallFinishes()
        {
            Assert.IsTrue(AllyCall.Enters(true, true, false));
            Assert.IsFalse(AllyCall.Enters(true, false, true));
            Assert.IsTrue(AllyCall.Enters(false, false, true));
            Assert.IsFalse(AllyCall.Enters(false, false, false));

            bool calling = false;
            bool finished = false;
            AllyCall.Begin(ref calling, ref finished);
            AllyCall.Interrupt(ref calling, ref finished);
            Assert.IsFalse(AllyCall.Finish(ref calling, ref finished));
            Assert.IsFalse(finished);

            AllyCall.Begin(ref calling, ref finished);
            Assert.IsTrue(AllyCall.Finish(ref calling, ref finished));
            Assert.IsTrue(finished);
        }
    }
}
