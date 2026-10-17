using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class FlaskRestoreTests
    {
        [Test]
        public void Estus_RaisesHealthOnly()
        {
            float hp = 100f;
            float mp = 40f;
            FlaskRestore.Apply(FlaskType.estus, 30, ref hp, ref mp, 150f, 80f);
            Assert.AreEqual(130f, hp, 0.001f);
            Assert.AreEqual(40f, mp, 0.001f);
        }

        [Test]
        public void Ash_RaisesFocusOnly()
        {
            float hp = 100f;
            float mp = 40f;
            FlaskRestore.Apply(FlaskType.ashen, 30, ref hp, ref mp, 150f, 80f);
            Assert.AreEqual(100f, hp, 0.001f);
            Assert.AreEqual(70f, mp, 0.001f);
        }

        [Test]
        public void FullHealth_EstusDoesNotChangeEither()
        {
            float hp = 150f;
            float mp = 40f;
            FlaskRestore.Apply(FlaskType.estus, 30, ref hp, ref mp, 150f, 80f);
            Assert.AreEqual(150f, hp, 0.001f);
            Assert.AreEqual(40f, mp, 0.001f);
        }

        [Test]
        public void FullFocus_AshDoesNotChangeEither()
        {
            float hp = 100f;
            float mp = 80f;
            FlaskRestore.Apply(FlaskType.ashen, 30, ref hp, ref mp, 150f, 80f);
            Assert.AreEqual(100f, hp, 0.001f);
            Assert.AreEqual(80f, mp, 0.001f);
        }
    }
}
