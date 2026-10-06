using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class RecordedFireTests
    {
        [Test]
        public void TryMatch_MovesToThatPlace_AndLeavesTheBodyWhenMissing()
        {
            GameObject bodyObject = new GameObject("body");
            Transform body = bodyObject.transform;
            body.position = new Vector3(1f, 2f, 3f);
            string[] names = { "甲", "乙" };
            Vector3[] at = { new Vector3(9f, 0f, 0f), new Vector3(0f, 8f, 0f) };

            Assert.IsTrue(RecordedFire.TryMatch("甲", names, at, 2, out Vector3 hit));
            body.position = hit;
            Assert.AreEqual(new Vector3(9f, 0f, 0f), body.position);

            Vector3 stayed = body.position;
            Assert.IsFalse(RecordedFire.TryMatch("丙", names, at, 2, out _));
            Assert.AreEqual(stayed, body.position);

            Object.DestroyImmediate(bodyObject);
        }
    }
}
