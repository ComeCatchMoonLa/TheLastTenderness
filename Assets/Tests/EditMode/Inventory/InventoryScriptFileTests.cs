using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class InventoryScriptFileTests
    {
        [Test]
        public void FileNameMatchesClass_GuidStays()
        {
            Assert.IsNotNull(typeof(CharacterInventoryManager));
            string folder = Path.Combine(Application.dataPath, "Scripts", "Character");
            Assert.IsTrue(File.Exists(Path.Combine(folder, "CharacterInventoryManager.cs")));
            Assert.IsFalse(File.Exists(Path.Combine(folder, "CharacterInvontoryManager.cs")));
            string meta = File.ReadAllText(Path.Combine(folder, "CharacterInventoryManager.cs.meta"));
            Assert.IsTrue(meta.Contains("guid: 24a3efacc9eb3224f9ecee6a2e1981e5"));
        }
    }
}
