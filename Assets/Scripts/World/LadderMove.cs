namespace CatchMoon
{
    public struct LadderState
    {
        public bool onLadder;
        public float along;
        public float bottom;
        public float top;
    }

    public static class LadderMove
    {
        public static LadderState Climb(LadderState state, float input)
        {
            if (!state.onLadder) return state;
            state.along += input;
            if (state.along < state.bottom) state.along = state.bottom;
            if (state.along > state.top) state.along = state.top;
            return state;
        }

        public static LadderState Slide(LadderState state, bool dodgeHeld, bool stickDown)
        {
            if (!state.onLadder || !dodgeHeld || !stickDown) return state;
            state.along = state.bottom;
            return state;
        }

        public static bool JumpOff(LadderState state, bool dodge, bool stickCentered)
        {
            if (!state.onLadder || !dodge || !stickCentered) return false;
            return true;
        }

        public static LadderState Step(LadderState state, float input, bool dodgeHeld, bool stickDown, bool stickCentered, out bool jumped)
        {
            jumped = false;
            if (!state.onLadder) return state;
            if (JumpOff(state, dodgeHeld, stickCentered))
            {
                state.onLadder = false;
                jumped = true;
                return state;
            }
            if (dodgeHeld && stickDown)
                return Slide(state, true, true);
            return Climb(state, input);
        }
    }
}
