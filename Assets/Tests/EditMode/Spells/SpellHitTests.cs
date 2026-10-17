using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class SpellHitTests
    {
        [Test]
        public void EachSchool_FillsOnlyItsSegment()
        {
            DamageSegments fire = SpellHit.Of(SpellType.pyromancy, 25);
            Assert.AreEqual(25f, fire.Fire, 0.001f);
            Assert.AreEqual(0f, fire.Magic + fire.Lightning + fire.Dark + fire.Physical, 0.001f);

            DamageSegments magic = SpellHit.Of(SpellType.sorcery, 25);
            Assert.AreEqual(25f, magic.Magic, 0.001f);
            Assert.AreEqual(0f, magic.Fire + magic.Lightning + magic.Dark + magic.Physical, 0.001f);

            DamageSegments lightning = SpellHit.Of(SpellType.miracle, 25);
            Assert.AreEqual(25f, lightning.Lightning, 0.001f);
            Assert.AreEqual(0f, lightning.Fire + lightning.Magic + lightning.Dark + lightning.Physical, 0.001f);

            DamageSegments dark = SpellHit.Of(SpellType.dark, 25);
            Assert.AreEqual(25f, dark.Dark, 0.001f);
            Assert.AreEqual(0f, dark.Fire + dark.Magic + dark.Lightning + dark.Physical, 0.001f);
        }
    }
}
