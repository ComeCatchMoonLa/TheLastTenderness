namespace CatchMoon
{
    public static class CoilFragment
    {
        public static bool TryUse(HomewardState state)
        {
            int count = state.bones;
            if (!HomewardBone.TryUse(state)) return false;
            state.bones = count;
            return true;
        }

        public static int Pickup(int already)
        {
            if (already > 0) return already;
            return 1;
        }
    }
}
