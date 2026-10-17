using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class WeaponCatalystTests
    {
        [Test]
        public void MatchingSchool_Casts()
        {
            Assert.AreEqual(WeaponArtSpell.Cast, WeaponCatalyst.Decide(CatalystKind.staff, SpellType.sorcery, true));
            Assert.AreEqual(WeaponArtSpell.Cast, WeaponCatalyst.Decide(CatalystKind.chime, SpellType.miracle, true));
            Assert.AreEqual(WeaponArtSpell.Cast, WeaponCatalyst.Decide(CatalystKind.talisman, SpellType.miracle, true));
            Assert.AreEqual(WeaponArtSpell.Cast, WeaponCatalyst.Decide(CatalystKind.flame, SpellType.pyromancy, true));
        }

        [Test]
        public void WrongSchoolOrNoSpell_DoesNotCast()
        {
            Assert.AreEqual(WeaponArtSpell.NoSpell, WeaponCatalyst.Decide(CatalystKind.staff, SpellType.miracle, true));
            Assert.AreEqual(WeaponArtSpell.NoSpell, WeaponCatalyst.Decide(CatalystKind.flame, SpellType.sorcery, false));
        }

        [Test]
        public void UnmarkedWeapon_KeepsWeaponArt()
        {
            Assert.AreEqual(WeaponArtSpell.WeaponArt, WeaponCatalyst.Decide(CatalystKind.none, SpellType.sorcery, true));
        }
    }
}
