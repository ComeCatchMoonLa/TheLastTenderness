using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class CatalystMatchTests
    {
        [Test]
        public void Allows_MatchesEachSchool_AndRejectsTheWrongOne()
        {
            Assert.IsTrue(CatalystMatch.Allows(CatalystKind.staff, SpellType.sorcery));
            Assert.IsTrue(CatalystMatch.Allows(CatalystKind.chime, SpellType.miracle));
            Assert.IsTrue(CatalystMatch.Allows(CatalystKind.talisman, SpellType.miracle));
            Assert.IsTrue(CatalystMatch.Allows(CatalystKind.flame, SpellType.pyromancy));
            Assert.IsFalse(CatalystMatch.Allows(CatalystKind.staff, SpellType.miracle));
            Assert.IsFalse(CatalystMatch.Allows(CatalystKind.chime, SpellType.sorcery));
            Assert.IsFalse(CatalystMatch.Allows(CatalystKind.talisman, SpellType.pyromancy));
            Assert.IsFalse(CatalystMatch.Allows(CatalystKind.flame, SpellType.miracle));
        }
    }
}
