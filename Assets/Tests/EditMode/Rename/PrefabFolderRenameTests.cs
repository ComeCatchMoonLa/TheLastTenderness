using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class PrefabFolderRenameTests
    {
        [Test]
        public void FolderRenamed_GuidStays_PlayerPrefabStillReadable()
        {
            string assets = Application.dataPath;
            Assert.IsTrue(Directory.Exists(Path.Combine(assets, "Prefabs")));
            Assert.IsFalse(Directory.Exists(Path.Combine(assets, "Perfabs")));
            string meta = File.ReadAllText(Path.Combine(assets, "Prefabs.meta"));
            Assert.IsTrue(meta.Contains("guid: 0e093786082eec441b09cc5cf58ff373"));
            string player = File.ReadAllText(Path.Combine(assets, "Prefabs", "Characters", "Player.prefab"));
            Assert.IsTrue(player.Contains("instantiatedFXModel: {fileID: 0}"));
        }
    }
}
