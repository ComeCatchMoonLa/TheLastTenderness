using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class RingMemorySlotTests
    {
        [Test]
        public void Rings_AddSlots_AndTheFifthNumberIsIgnored()
        {
            Assert.AreEqual(1, MemorySlotBonus.Total(9, new[] { 1 }));
            Assert.AreEqual(2, MemorySlotBonus.Total(9, new[] { 1, 1 }));
            Assert.AreEqual(2, MemorySlotBonus.Total(9, new[] { 2 }));
            Assert.AreEqual(0, MemorySlotBonus.Total(9, new[] { 0, 0, 0, 0 }));
            Assert.AreEqual(4, MemorySlotBonus.Total(9, new[] { 1, 1, 2, 0, 9 }));
        }
    }
}
