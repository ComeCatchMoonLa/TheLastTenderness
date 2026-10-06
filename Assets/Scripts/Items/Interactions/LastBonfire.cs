namespace CatchMoon
{
    public static class LastBonfire
    {
        public static string recorded;

        public static void Remember(string fire, bool sat)
        {
            if (!sat || string.IsNullOrEmpty(fire)) return;
            recorded = fire;
        }
    }
}
