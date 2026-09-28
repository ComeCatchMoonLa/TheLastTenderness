using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class MeleeStateUnifyTests
    {
        [Test]
        public void SharedTicks_PursueReturnsHumanoidCombatInsideAggro()
        {
            Assert.AreEqual(typeof(IdleState), DeclaringTick(typeof(IdleStateHumanoid)));
            Assert.AreEqual(typeof(PursueTargetState), DeclaringTick(typeof(PursueTargetStateHumanoid)));
            Assert.AreEqual(typeof(RotateTowardsTargetState), DeclaringTick(typeof(RotateTowardsTargetStateHumanoid)));
            Assert.AreEqual(typeof(CombatStanceStateHumanoid), DeclaringTick(typeof(CombatStanceStateHumanoid)));
            Assert.IsNotNull(typeof(AmbushState));
            Assert.IsNotNull(typeof(BossCombatStanceState).GetField("sencondPhaseAttacks"));

            GameObject enemyObject = new GameObject("enemy");
            GameObject targetObject = new GameObject("target");
            EnemyAISettings settings = ScriptableObject.CreateInstance<EnemyAISettings>();
            try
            {
                enemyObject.AddComponent<Rigidbody>();
                enemyObject.AddComponent<Animator>();
                enemyObject.AddComponent<NavMeshAgent>();
                EnemyManager enemy = enemyObject.AddComponent<EnemyManager>();
                enemy.rigidBody = enemyObject.GetComponent<Rigidbody>();
                enemy.animator = enemyObject.GetComponent<Animator>();
                enemy.navmeshAgent = enemyObject.GetComponent<NavMeshAgent>();
                settings.aggroRadius = 2f;
                enemy.aiSettings = settings;
                enemy.enableAI = true;
                enemy.eStats = enemyObject.AddComponent<EnemyStatsManager>();
                enemy.eStats.isDead = false;
                CombatStanceStateHumanoid combat = enemyObject.AddComponent<CombatStanceStateHumanoid>();
                PursueTargetStateHumanoid pursue = enemyObject.AddComponent<PursueTargetStateHumanoid>();
                typeof(PursueTargetStateHumanoid).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(pursue, null);
                enemy.currentTarget = targetObject.AddComponent<EnemyManager>();
                enemy.distFromTarget = 1f;
                enemy.isInteracting = false;
                enemy.isPreformingAction = false;
                LogAssert.Expect(LogType.Error, new Regex("SetDestination"));

                Assert.AreSame(combat, pursue.Tick(enemy));
            }
            finally
            {
                Object.DestroyImmediate(targetObject);
                Object.DestroyImmediate(enemyObject);
                Object.DestroyImmediate(settings);
            }
        }

        static System.Type DeclaringTick(System.Type type)
        {
            MethodInfo tick = type.GetMethod("Tick");
            Assert.IsNotNull(tick);
            return tick.DeclaringType;
        }
    }
}
