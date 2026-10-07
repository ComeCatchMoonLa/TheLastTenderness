namespace CatchMoon
{
    public struct LadderState
    {
        public bool onLadder;
        public float along;
        public float bottom;
        public float top;
        public float floor;
    }

    public static class LadderMove
    {
        public static LadderState Climb(LadderState state, float input)
        {
            if (!state.onLadder) return state;
            float next = state.along + input;
            if (next < state.floor)
            {
                if (state.along > state.floor)
                {
                    state.along = state.floor;
                    state.onLadder = false;
                    return state;
                }
                if (input < 0f)
                    return state;
            }
            if (next > state.top) next = state.top;
            state.along = next;
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
            return Climb(state, input);
        }
    }
}
