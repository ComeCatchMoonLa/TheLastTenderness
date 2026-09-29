using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class FlaskHudCountTests
    {
        [Test]
        public void Line_StaysReadableWhenAnotherConsumableIsCurrent()
        {
            Assert.AreEqual("血瓶 2 灰瓶 1", FlaskHudCounts.Line(2, 1));

            GameObject root = new GameObject("flask-hud");
            QuickSlotsUI slots = root.AddComponent<QuickSlotsUI>();
            GameObject textObject = new GameObject("flask-text");
            var text = textObject.AddComponent<TMPro.TextMeshProUGUI>();
            typeof(QuickSlotsUI).GetField("flaskCountText", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(slots, text);

            slots.SetFlaskCounts(2, 1);
            Assert.AreEqual("血瓶 2 灰瓶 1", text.text);
            Object.DestroyImmediate(textObject);
            Object.DestroyImmediate(root);
        }
    }
}
