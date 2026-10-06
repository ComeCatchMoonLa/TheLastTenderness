namespace CatchMoon
{
    public static class PhantomTeam
    {
        public static int Team(bool invader, int hostTeam, int foeTeam)
        {
            return invader ? foeTeam : hostTeam;
        }

        public static bool SameTeamSkips(int attackerTeam, int targetTeam)
        {
            return attackerTeam == targetTeam;
        }
    }
}
