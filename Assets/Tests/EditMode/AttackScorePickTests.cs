using System.IO;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AttackScorePickTests
    {
        const float Distance = 1f;
        const float Angle = 0f;

        [Test]
        public void StopAfterAssign_ReturnsFirstScorePastRandom()
        {
            FixedWindows windows = Covering(1, 1, 1);
            Assert.AreEqual(0, Pick(windows, 0, hasCurrent: false, abortIfHasCurrent: false, stopAfterAssign: true));
            Assert.AreEqual(1, Pick(windows, 1, hasCurrent: false, abortIfHasCurrent: false, stopAfterAssign: true));
        }

        [Test]
        public void GeneralMode_StopsAtFirstScorePastRandom()
        {
            FixedWindows windows = Covering(1, 1, 1);
            Assert.AreEqual(0, Pick(windows, 0, hasCurrent: false, abortIfHasCurrent: true, stopAfterAssign: true));
        }

        [Test]
        public void AbortIfHasCurrent_ReturnsNegative()
        {
            FixedWindows windows = Covering(1, 1, 1);
            Assert.AreEqual(-1, Pick(windows, 0, hasCurrent: true, abortIfHasCurrent: true, stopAfterAssign: false));
        }

        [Test]
        public void SkipsWindowOutsideRange_ThenStops()
        {
            FixedWindows windows = new FixedWindows
            {
                windows = new[]
                {
                    Window(0f, 0.5f, 5),
                    Window(0f, 2f, 1)
                }
            };
            Assert.AreEqual(1, Pick(windows, 0, hasCurrent: false, abortIfHasCurrent: false, stopAfterAssign: true));
        }

        [Test]
        public void NoWindowInRange_ReturnsNegative()
        {
            FixedWindows windows = new FixedWindows
            {
                windows = new[] { Window(0f, 0.1f, 1) }
            };
            Assert.AreEqual(-1, Pick(windows, 0, hasCurrent: false, abortIfHasCurrent: false, stopAfterAssign: true));
        }

        [Test]
        public void AttackFieldsStaySplit_AndCallSitesUsePickIndex()
        {
            Assert.AreEqual(typeof(State), typeof(AttackStateHumanoid).BaseType);
            Assert.AreEqual(typeof(ItemBasedAttackAction), FieldType(typeof(AttackStateHumanoid), "currentAttack"));
            Assert.AreEqual(typeof(EnemyAttackAction), FieldType(typeof(AttackState), "currentAttack"));
            Assert.AreEqual(typeof(State), typeof(CombatStanceStateHumanoid).BaseType);
            Assert.IsNotNull(typeof(BossCombatStanceState).GetField("sencondPhaseAttacks"));
            Assert.IsNotNull(typeof(AmbushState));

            Assert.IsTrue(Source("General A.I", "CombatStanceState.cs").Contains("AttackScore.PickIndex"));
            Assert.IsTrue(Source("Humanoid A.I", "CombatStanceStateHumanoid.cs").Contains("AttackScore.PickIndex"));
            Assert.IsTrue(Source("Boss A.I", "BossCombatStanceState.cs").Contains("AttackScore.PickIndex"));
        }

        static int Pick(FixedWindows windows, int randomValue, bool hasCurrent, bool abortIfHasCurrent, bool stopAfterAssign)
        {
            return AttackScore.PickIndex(windows.windows.Length, Distance, Angle, randomValue, hasCurrent, abortIfHasCurrent, stopAfterAssign, windows);
        }

        static FixedWindows Covering(params int[] scores)
        {
            AttackWindow[] windows = new AttackWindow[scores.Length];
            for (int i = 0; i < scores.Length; ++i)
                windows[i] = Window(0f, 2f, scores[i]);
            return new FixedWindows { windows = windows };
        }

        static AttackWindow Window(float minDist, float maxDist, int score)
        {
            return new AttackWindow
            {
                MinDist = minDist,
                MaxDist = maxDist,
                MinAngle = -35f,
                MaxAngle = 35f,
                Score = score
            };
        }

        static System.Type FieldType(System.Type type, string fieldName)
        {
            FieldInfo field = type.GetField(fieldName);
            Assert.IsNotNull(field);
            return field.FieldType;
        }

        static string Source(string folder, string fileName)
        {
            string path = Path.Combine(Application.dataPath, "Scripts", "Character", "NPC", "State", folder, fileName);
            return File.ReadAllText(path);
        }

        struct FixedWindows : IAttackWindows
        {
            public AttackWindow[] windows;

            public AttackWindow Window(int index)
            {
                return windows[index];
            }
        }
    }
}
