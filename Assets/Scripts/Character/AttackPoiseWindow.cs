namespace CatchMoon
{
    public static class AttackPoiseWindow
    {
        public static int Open(CharacterStatsManager stats, float bonus)
        {
            if (bonus <= 0f) return 0;
            float poise = ArmorPoise.Fold(stats.armorPoiseBonus, bonus);
            if (!stats.attackPoiseActive)
                stats.totalPoiseDefence += poise - stats.armorPoiseBonus;
            stats.attackPoise = poise;
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
