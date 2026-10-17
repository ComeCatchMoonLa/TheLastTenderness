namespace CatchMoon
{
    public enum ListedConsumable
    {
        redMoss,
        purpleMoss,
        bloomingMoss,
        blueMoss,
        blessing,
        greenBlossom,
        buddingBlossom,
        rustedCoin,
        rustedGoldCoin
    }

    public static class ConsumableEffect
    {
        public const float Seconds = 60f;

        public static bool Apply(ListedConsumable kind, CharacterStatsManager stats, float now)
        {
            if (stats == null) return false;
            if (kind == ListedConsumable.redMoss)
            {
                stats.bleedMeter = 0f;
                return true;
            }
            if (kind == ListedConsumable.purpleMoss)
            {
                stats.poisonTimeLeft = 0f;
                return true;
            }
            if (kind == ListedConsumable.bloomingMoss)
            {
                stats.poisonTimeLeft = 0f;
                stats.toxicTimeLeft = 0f;
                return true;
            }
            if (kind == ListedConsumable.blueMoss)
            {
                stats.frostMeter = 0f;
                stats.frostTimeLeft = 0f;
                return true;
            }
            if (kind == ListedConsumable.blessing)
            {
                if (stats.isDead) return false;
                if (!CurseMeter.TryBless(stats.curseMeter, stats.curseCapacity, out float cleared)) return false;
                stats.curseMeter = cleared;
                return true;
            }
            if (kind == ListedConsumable.greenBlossom || kind == ListedConsumable.buddingBlossom)
            {
                stats.blossomUntil = now + Seconds;
                stats.blossomPerSecond = kind == ListedConsumable.buddingBlossom ? 9f : 6f;
                return true;
            }
            stats.coinUntil = now + Seconds;
            stats.coinBonus = kind == ListedConsumable.rustedGoldCoin ? 100 : 50;
            return true;
        }

        public static float BlossomGain(float now, float until, float perSecond, float delta)
        {
            if (now >= until || perSecond <= 0f || delta <= 0f) return 0f;
            return perSecond * delta;
        }

        public static int CoinBonus(float now, float until, int bonus)
        {
            if (now >= until) return 0;
            return bonus;
        }
    }
}
