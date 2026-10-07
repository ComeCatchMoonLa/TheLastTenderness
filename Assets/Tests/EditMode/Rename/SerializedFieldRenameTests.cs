using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class SerializedFieldRenameTests
    {
        [Test]
        public void NewFieldsExist_OldFieldsDoNot_PrefabIdsStay()
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            Assert.IsNotNull(typeof(CharacterManager).GetField("isPerformingBackStabbed", flags));
            Assert.IsNull(typeof(CharacterManager).GetField("isPerformingBackSttbbed", flags));
            Assert.IsNotNull(typeof(CharacterInventoryManager).GetField("unarmed", flags));
            Assert.IsNull(typeof(CharacterInventoryManager).GetField("unaremd", flags));
            Assert.IsNotNull(typeof(WeaponFX).GetField("normalWeaponTrail", flags));
            Assert.IsNull(typeof(WeaponFX).GetField("noramalWeaponTrial", flags));
            Assert.IsFalse(Enum.IsDefined(typeof(WeaponType), "unaremd"));

            AssertPrefab("Prefabs/Characters/Humanoid A.I - Melee.prefab", "isPerformingBackStabbed: 0", "unarmed: {fileID: 0}");
            AssertPrefab("Prefabs/Characters/Player.prefab", "isPerformingBackStabbed: 0", "unarmed: {fileID: 11400000, guid: 2fca6e6dda10e70439d4ed7559a71089, type: 2}");
            AssertPrefab("Prefabs/Characters/Default Story NPC.prefab", "isPerformingBackStabbed: 0", "unarmed: {fileID: 0}");
            AssertPrefab("Prefabs/Items/Weapons/Axe2H_Epic.prefab", "normalWeaponTrail: {fileID: 1042571222865278056}");
            AssertPrefab("Prefabs/Items/Weapons/Sword/Sword15_Lave.prefab", "normalWeaponTrail: {fileID: 347222509994406306}");
            AssertPrefab("Prefabs/Items/Weapons/Sword/Sword15_Frost.prefab", "normalWeaponTrail: {fileID: 8791912116022625030}");
            AssertPrefab("Prefabs/Items/Weapons/Sword/Greatsword.prefab", "normalWeaponTrail: {fileID: 6731106343894556902}");
        }

        static void AssertPrefab(string relativePath, params string[] snippets)
        {
            string text = File.ReadAllText(Path.Combine(Application.dataPath, relativePath));
            Assert.IsFalse(text.Contains("isPerformingBackSttbbed"));
            Assert.IsFalse(text.Contains("noramalWeaponTrial"));
            Assert.IsFalse(text.Contains("unaremd:"));
            for (int i = 0; i < snippets.Length; i++)
                Assert.IsTrue(text.Contains(snippets[i]), relativePath + " missing " + snippets[i]);
        }
    }
}
