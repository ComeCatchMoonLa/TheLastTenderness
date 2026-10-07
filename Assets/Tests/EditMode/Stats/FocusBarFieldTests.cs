using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class FocusBarFieldTests
    {
        [Test]
        public void FieldRenamed_PrefabReferenceStaysEmpty()
        {
            FieldInfo field = typeof(HUDWindowsManager).GetField("focusPointsBar");
            Assert.IsNotNull(field);
            Assert.AreEqual(typeof(FocusPointsBar), field.FieldType);
            Assert.IsNull(typeof(HUDWindowsManager).GetField("manaBar"));

            string prefab = File.ReadAllText(Path.Combine(Application.dataPath, "Prefabs", "UI", "Global UI", "Global UI.prefab"));
            Assert.IsTrue(prefab.Contains("focusPointsBar: {fileID: 0}"));
            Assert.IsFalse(prefab.Contains("manaBar"));
        }
    }
}
