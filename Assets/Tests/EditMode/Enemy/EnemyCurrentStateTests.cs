using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class EnemyCurrentStateTests
    {
        [Test]
        public void SwitchToNextState_CurrentStateIsThatObject()
        {
            var root = new GameObject("enemy");
            var enemy = root.AddComponent<EnemyManager>();
            var stateObject = new GameObject("state");
            var state = stateObject.AddComponent<ReadableState>();

            enemy.SwitchToNextState(state);

            Assert.AreSame(state, enemy.CurrentState);
        }

        public class ReadableState : State
        {
            public override State Tick(EnemyManager enemy)
            {
                return null;
            }
        }
    }
}
