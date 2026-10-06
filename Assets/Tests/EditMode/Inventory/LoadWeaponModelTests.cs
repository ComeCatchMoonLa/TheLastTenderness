using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class LoadWeaponModelTests
    {
        [Test]
        public void MissingPrefab_KeepsPreviousModel()
        {
            var go = new GameObject("slot");
            var parent = new GameObject("holding").transform;
            var existing = new GameObject("previous");
            var slot = go.AddComponent<WeaponHolderSlot>();
            slot.overrideParentWhileHolding = parent;
            slot.currentWeaponModel = existing;

            var weapon = ScriptableObject.CreateInstance<WeaponItem>();

            LogAssert.Expect(LogType.Error, "model is null.");
            Assert.IsFalse(slot.LoadWeaponModel(null));
            Assert.AreSame(existing, slot.currentWeaponModel);
            LogAssert.Expect(LogType.Error, "model is null.");
            Assert.IsFalse(slot.LoadWeaponModel(weapon));
            Assert.AreSame(existing, slot.currentWeaponModel);

            Object.DestroyImmediate(weapon);
            Object.DestroyImmediate(existing);
            Object.DestroyImmediate(parent.gameObject);
            Object.DestroyImmediate(go);
        }
    }
}
