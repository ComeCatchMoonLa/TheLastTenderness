namespace CatchMoon
{
    public static class ArenaDevice
    {
        public static bool IsOn(bool fightActive, bool onDuringFight, bool staysAfter)
        {
            if (fightActive) return onDuringFight;
            return staysAfter;
        }
    }
}
