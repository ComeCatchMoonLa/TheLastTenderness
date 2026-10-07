using NUnit.Framework;

namespace CatchMoon.Tests
{
    public class LadderMoveTests
    {
        [Test]
        public void Climb_Clamps_SlideHitsBottom_CenteredDodgeLeaves()
        {
            LadderState state = new LadderState { onLadder = true, along = 0f, bottom = 0f, top = 4f };
            state = LadderMove.Step(state, 1f, false, false, false, out bool jumped);
            Assert.IsFalse(jumped);
            Assert.AreEqual(1f, state.along, 0.001f);
            Assert.IsTrue(state.onLadder);

            state = LadderMove.Step(state, 0f, false, true, false, out jumped);
            Assert.AreEqual(1f, state.along, 0.001f);

            state = LadderMove.Step(state, 10f, false, false, false, out jumped);
            Assert.AreEqual(4f, state.along, 0.001f);

            state = LadderMove.Step(state, -0.5f, false, true, false, out jumped);
            Assert.AreEqual(3.5f, state.along, 0.001f);

            state = LadderMove.Step(state, -1f, false, false, false, out jumped);
            Assert.IsFalse(jumped);
            Assert.IsTrue(state.onLadder);
            Assert.AreEqual(2.5f, state.along, 0.001f);

            state.onLadder = true;
            state.along = 2f;
            state = LadderMove.Step(state, 0f, true, false, true, out jumped);
            Assert.IsTrue(jumped);
            Assert.IsFalse(state.onLadder);

            state = new LadderState { onLadder = true, along = -1f, bottom = 0f, top = 4f };
            state = LadderMove.Step(state, 0.5f, false, false, false, out jumped);
            Assert.IsFalse(jumped);
            Assert.IsTrue(state.onLadder);
            Assert.AreEqual(-0.5f, state.along, 0.001f);

            state = LadderMove.Step(state, -0.2f, false, false, false, out jumped);
            Assert.IsFalse(jumped);
            Assert.IsTrue(state.onLadder);
            Assert.AreEqual(-0.5f, state.along, 0.001f);
        }
    }
}
