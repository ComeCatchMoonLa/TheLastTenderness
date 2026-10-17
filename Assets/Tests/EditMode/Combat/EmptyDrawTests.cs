using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class EmptyDrawTests
    {
        [Test]
        public void Zero_DrawsWithoutAShot_Positive_DoesNot()
        {
            Assert.IsTrue(EmptyDraw.Plays(0));
            Assert.IsFalse(EmptyDraw.Plays(1));
            Assert.AreEqual("Shoot Arrow", EmptyDraw.Clip);
        }
    }
}
