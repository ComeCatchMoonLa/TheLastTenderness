using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class EquipCompareTests
    {
        [Test]
        public void FirstPoint_PreviewsOnly_SecondPoint_Commits_OtherItemStaysPreview()
        {
            EquipCompare.armed = null;
            WeaponItem current = ScriptableObject.CreateInstance<WeaponItem>();
            current.pd = 10;
            current.physicalDA = 0.2f;
            WeaponItem next = ScriptableObject.CreateInstance<WeaponItem>();
            next.pd = 15;
            next.physicalDA = 0.5f;
            WeaponItem other = ScriptableObject.CreateInstance<WeaponItem>();
            other.pd = 3;

            Assert.IsFalse(EquipCompare.Arm(next));
            EquipPreview preview = EquipCompare.Preview(current.pd, next.pd, current.physicalDA, next.physicalDA);
            Assert.AreEqual(5f, preview.attackDelta, 0.001f);
            Assert.AreEqual(0.3f, preview.absorptionDelta, 0.001f);
            float absorption = current.physicalDA;
            Assert.IsFalse(EquipCompare.CommitAbsorption(false, ref absorption, next.physicalDA));
            Assert.AreEqual(0.2f, absorption, 0.001f);
            Assert.AreEqual(10, current.pd);

            Assert.IsTrue(EquipCompare.Arm(next));
            Assert.IsTrue(EquipCompare.CommitAbsorption(true, ref absorption, next.physicalDA));
            Assert.AreEqual(0.5f, absorption, 0.001f);

            Assert.IsFalse(EquipCompare.Arm(other));
            Assert.IsFalse(EquipCompare.CommitAbsorption(false, ref absorption, 0.1f));
            Assert.AreEqual(0.5f, absorption, 0.001f);
        }
    }
}
