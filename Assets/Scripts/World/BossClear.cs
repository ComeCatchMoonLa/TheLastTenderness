namespace CatchMoon
{
    public static class BossClear
    {
        public static bool CanOpenAgain(bool alreadyDefeated)
        {
            return !alreadyDefeated;
        }

        public static void Apply(ref bool fogUp, ref bool bossStillHere, ref bool fireSealed)
        {
            fogUp = false;
            bossStillHere = false;
            fireSealed = false;
        }
    }
}
