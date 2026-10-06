using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class CampFireRestTests
    {
        [Test]
        public void Allow_StopsWhenRadiusIsEmptyOrAPursuerIsInside()
        {
            Assert.IsFalse(CampFireRest.Allow(0f, false));
            Assert.IsFalse(CampFireRest.Allow(2f, true));
            Assert.IsTrue(CampFireRest.Allow(2f, false));
        }
    }
}
