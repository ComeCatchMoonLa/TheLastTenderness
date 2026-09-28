using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class LockOnSplitTests
    {
        [Test]
        public void FollowAndCollisionDoNotScan_SceneKeepsRadius()
        {
            string camera = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Camera", "PlayerCameraManager.cs"));
            string follow = Between(camera, "public void FollowPlayer()", "public void SetCameraPos_LockedMode()");
            string collision = Between(camera, "void HandleCameraCollisions()", "void HandleCameraRotation()");
            Assert.IsFalse(follow.Contains("CollectOverlaps"));
            Assert.IsFalse(follow.Contains("UpdateLockOnTargets"));
            Assert.IsFalse(collision.Contains("CollectOverlaps"));
            Assert.IsFalse(collision.Contains("UpdateLockOnTargets"));
            Assert.IsFalse(camera.Contains("float maxLockOnDist"));

            string query = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Camera", "LockOnQuery.cs"));
            Assert.IsTrue(query.Contains("CollectOverlaps(camera.Player.transform.position, maxLockOnDist, LayerMask.npc, ref lockOnOverlapResults)"));
            Assert.IsTrue(query.Contains("float maxLockOnDist"));

            string scene = File.ReadAllText(Path.Combine(Application.dataPath, "Scenes", "Game.unity"));
            Assert.IsTrue(scene.Contains("guid: e7c3a9b24d1f4e6a8b0c5d7f19284631"));
            Assert.IsTrue(scene.Contains("maxLockOnDist: 30"));
            Assert.IsTrue(scene.Contains("cameraPivotTransform: {fileID: 887288630}"));
        }

        static string Between(string text, string start, string end)
        {
            int from = text.IndexOf(start);
            int to = text.IndexOf(end, from + start.Length);
            Assert.GreaterOrEqual(from, 0, start);
            Assert.Greater(to, from, end);
            return text.Substring(from, to - from);
        }
    }
}
