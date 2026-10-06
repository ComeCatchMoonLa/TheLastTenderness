using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class InputActionsRenameTests
    {
        const string AssetGuid = "5c2ea1392b940f04c8066d98d81b66e3";

        [Test]
        public void ClassAndAssetRenamed_GuidAndWrapperPathStay()
        {
            Assert.IsNotNull(typeof(InputActions));
            FieldInfo field = typeof(InputManager).GetField("inputActions", BindingFlags.Instance | BindingFlags.Public);
            Assert.IsNotNull(field);
            Assert.AreEqual("InputActions", field.FieldType.Name);

            string asset = Path.Combine(Application.dataPath, "InputActions.inputactions");
            string oldAsset = Path.Combine(Application.dataPath, "InputAcitons.inputactions");
            Assert.IsTrue(File.Exists(asset));
            Assert.IsFalse(File.Exists(oldAsset));
            string assetText = File.ReadAllText(asset);
            Assert.IsTrue(assetText.Contains("\"name\": \"InputActions\""));
            Assert.IsFalse(assetText.Contains("InputAcitons"));

            string meta = File.ReadAllText(asset + ".meta");
            Assert.IsTrue(meta.Contains("guid: " + AssetGuid));
            Assert.IsTrue(meta.Contains("Assets/Scripts/Input/InputActions.cs"));

            string generated = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Input", "InputActions.cs"));
            Assert.IsFalse(generated.Contains("InputAcitons"));
        }
    }
}
