using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class EquipLoadTests
    {
        [Test]
        public void BackupAndRing_RaiseLoad_RemovingThem_LowersIt()
        {
            WeaponItem held = ScriptableObject.CreateInstance<WeaponItem>();
            held.weight = 1f;
            WeaponItem backup = ScriptableObject.CreateInstance<WeaponItem>();
            backup.weight = 8f;
            WeaponItem[] right = { held, backup, null };
            Item ring = ScriptableObject.CreateInstance<Item>();
            ring.weight = 2f;
            Item[] rings = { ring, null, null, null };

            Assert.AreEqual(11f, EquipLoad.Total(null, right, rings));

            right[1] = null;
            Assert.AreEqual(3f, EquipLoad.Total(null, right, rings));

            rings[0] = null;
            Assert.AreEqual(1f, EquipLoad.Total(null, right, rings));

            Object.DestroyImmediate(held);
            Object.DestroyImmediate(backup);
            Object.DestroyImmediate(ring);
        }

        [Test]
        public void AmmoWeight_DoesNotChangeLoad()
        {
            WeaponItem held = ScriptableObject.CreateInstance<WeaponItem>();
            held.weight = 1f;
            WeaponItem[] right = { held };
            AmmoItem ammo = ScriptableObject.CreateInstance<AmmoItem>();
            ammo.weight = 0f;
            float before = EquipLoad.Total(null, right, null);
            ammo.weight = 20f;
            Assert.AreEqual(before, EquipLoad.Total(null, right, null));
            Object.DestroyImmediate(held);
            Object.DestroyImmediate(ammo);
        }
    }
}
