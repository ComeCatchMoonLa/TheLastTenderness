using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class EnemyDeathPlayerCacheTests
    {
        [Test]
        public void Death_AwardsCachedPlayer_LeavesLockOff()
        {
            PlayerManager active = NewPlayer("active", true);
            PlayerManager cached = NewPlayer("cached", false);
            cached.pCamera.lockOnFlag = false;

            var enemyObject = new GameObject("enemy");
            var enemy = enemyObject.AddComponent<EnemyManager>();
            var settings = ScriptableObject.CreateInstance<EnemyAISettings>();
            settings.isBoss = false;
            enemy.aiSettings = settings;
            var stats = enemyObject.AddComponent<EnemyStatsManager>();
            typeof(EnemyStatsManager).GetField("player", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(stats, cached);

            LogAssert.Expect(LogType.Error, new Regex("Destroy may not be called from edit mode"));
            stats.HandleEnemyDeathEvent();

            Assert.AreEqual(0, active.pStats.soulCount);
            Assert.AreEqual(stats.soulsAwardedDeath, cached.pStats.soulCount);
            Assert.IsFalse(cached.pCamera.lockOnFlag);

            Object.DestroyImmediate(settings);
            Object.DestroyImmediate(active.gameObject);
            Object.DestroyImmediate(cached.pCamera.gameObject);
            Object.DestroyImmediate(cached.gameObject);
            Object.DestroyImmediate(enemyObject);
        }

        static PlayerManager NewPlayer(string name, bool active)
        {
            var root = new GameObject(name);
            var player = root.AddComponent<PlayerManager>();
            var stats = root.AddComponent<PlayerStatsManager>();
            var camera = new GameObject(name + "-camera").AddComponent<PlayerCameraManager>();
            player.pStats = stats;
            player.pCamera = camera;
            root.SetActive(active);
            return player;
        }
    }
}
