namespace CatchMoon
{
    public static class AilmentRest
    {
        public static void Clear(CharacterStatsManager stats)
        {
            stats.poisonMeter = 0f;
            stats.poisonTimeLeft = 0f;
            stats.toxicMeter = 0f;
            stats.toxicTimeLeft = 0f;
            stats.bleedMeter = 0f;
            stats.frostMeter = 0f;
            stats.frostTimeLeft = 0f;
            stats.curseMeter = 0f;
        }
    }
}
