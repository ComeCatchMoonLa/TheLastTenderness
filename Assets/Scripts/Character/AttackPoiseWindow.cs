namespace CatchMoon
{
    public static class AttackPoiseWindow
    {
        public static int Open(CharacterStatsManager stats, float bonus)
        {
            if (bonus <= 0f) return 0;
            if (!stats.attackPoiseActive)
                stats.totalPoiseDefence += bonus;
            stats.attackPoise = bonus;
            stats.attackPoiseActive = true;
            stats.attackPoiseToken += 1;
            return stats.attackPoiseToken;
        }

        public static void Close(CharacterStatsManager stats, int token)
        {
            if (token == 0 || stats.attackPoiseToken != token) return;
            stats.totalPoiseDefence = stats.armorPoiseBonus;
            stats.attackPoise = 0f;
            stats.attackPoiseActive = false;
        }
    }
}
