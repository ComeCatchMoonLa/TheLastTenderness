namespace CatchMoon
{
    public enum LevelStat
    {
        Health,
        Focus,
        Stamina
    }

    public struct LevelPoints
    {
        public int health;
        public int focus;
        public int stamina;
    }

    public struct LevelCaps
    {
        public float health;
        public float focus;
        public float stamina;
    }

    public static class LevelPreview
    {
        public static LevelCaps Preview(LevelPoints points, LevelStat chosen)
        {
            LevelPoints next = points;
            Raise(ref next, chosen);
            return CapsOf(next);
        }

        public static bool TryRaise(
            string assetName,
            int souls,
            int cost,
            int level,
            LevelPoints points,
            LevelStat chosen,
            out int newSouls,
            out int newLevel,
            out LevelPoints raised,
            out LevelCaps caps)
        {
            raised = points;
            caps = CapsOf(points);
            if (!LevelPurchase.TryConfirm(assetName, souls, cost, level, out newSouls, out newLevel)) return false;
            Raise(ref raised, chosen);
            caps = CapsOf(raised);
            return true;
        }

        static void Raise(ref LevelPoints points, LevelStat chosen)
        {
            if (chosen == LevelStat.Health) points.health += 1;
            else if (chosen == LevelStat.Focus) points.focus += 1;
            else points.stamina += 1;
        }

        static LevelCaps CapsOf(LevelPoints points)
        {
            return new LevelCaps
            {
                health = CharacterStatsManager.LevelTimesTen(points.health),
                focus = CharacterStatsManager.LevelTimesTen(points.focus),
                stamina = CharacterStatsManager.LevelTimesTen(points.stamina)
            };
        }
    }
}
