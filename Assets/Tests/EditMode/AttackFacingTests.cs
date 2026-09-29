using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class AttackFacingTests
    {
        [Test]
        public void Apply_SkipsWhenItCannotTurn()
        {
            GameObject enemy = new GameObject("attack-facing-enemy");
            GameObject state = new GameObject("attack-facing-state");
            try
            {
                Quaternion start = Quaternion.Euler(0f, 30f, 0f);
                enemy.transform.rotation = start;
                Vector3 dir = Vector3.right;

                AttackFacing.Apply(enemy.transform, state.transform, ref dir, false, true, 1f);
                Assert.IsTrue(enemy.transform.rotation == start);
                Assert.AreEqual(Vector3.right, dir);

                AttackFacing.Apply(enemy.transform, state.transform, ref dir, true, false, 1f);
                Assert.IsTrue(enemy.transform.rotation == start);
                Assert.AreEqual(Vector3.right, dir);
            }
            finally
            {
                Object.DestroyImmediate(enemy);
                Object.DestroyImmediate(state);
            }
        }

        [Test]
        public void Apply_UsesStateForward_AndTurnsTheEnemy()
        {
            GameObject enemy = new GameObject("attack-facing-enemy");
            GameObject state = new GameObject("attack-facing-state");
            try
            {
                enemy.transform.rotation = Quaternion.identity;
                state.transform.rotation = Quaternion.LookRotation(Vector3.right);
                Vector3 dir = Vector3.zero;
                AttackFacing.Apply(enemy.transform, state.transform, ref dir, true, true, 1f);
                Assert.AreEqual(state.transform.forward, dir);
                Assert.IsTrue(enemy.transform.rotation == Quaternion.LookRotation(Vector3.right));
                Assert.IsTrue(state.transform.rotation == Quaternion.LookRotation(Vector3.right));
            }
            finally
            {
                Object.DestroyImmediate(enemy);
                Object.DestroyImmediate(state);
            }
        }

        [Test]
        public void CallSites_KeepTheCurrentSlerpFactor()
        {
            string general = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Character", "NPC", "State", "General A.I", "AttackState.cs"));
            string humanoid = File.ReadAllText(Path.Combine(Application.dataPath, "Scripts", "Character", "NPC", "State", "Humanoid A.I", "AttackStateHumanoid.cs"));
            Assert.IsTrue(general.Contains("AttackFacing.Apply(enemy.transform, transform,"));
            Assert.IsTrue(general.Contains("rotationSpeed / Time.deltaTime"));
            Assert.IsFalse(general.Contains("rotationSpeed * Time.deltaTime"));
            Assert.IsTrue(humanoid.Contains("AttackFacing.Apply(enemy.transform, transform,"));
            Assert.IsTrue(humanoid.Contains("rotationSpeed * Time.deltaTime"));
            Assert.IsFalse(humanoid.Contains("rotationSpeed / Time.deltaTime"));
        }
    }
}
