using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class IdleSightTests
    {
        [Test]
        public void SightMask_IncludesSolids_ExcludesCharacters()
        {
            int mask = IdleState.SightMask;
            Assert.AreNotEqual(0, mask & LayerMask.defaultLayerMask);
            Assert.AreNotEqual(0, mask & LayerMask.environment);
            Assert.AreNotEqual(0, mask & LayerMask.interactable);
            Assert.AreEqual(0, mask & LayerMask.player);
            Assert.AreEqual(0, mask & LayerMask.npc);
            Assert.AreEqual(0, mask & LayerMask.characterCollisionBlocker);
        }
    }
}
