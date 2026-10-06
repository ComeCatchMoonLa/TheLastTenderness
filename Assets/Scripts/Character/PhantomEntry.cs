namespace CatchMoon
{
    public enum PhantomSign
    {
        White,
        Yellow,
        Red
    }

    public struct PhantomLoadout
    {
        public int estus;
        public int ash;
        public float maxHp;
        public bool saved;
    }

    public static class PhantomEntry
    {
        public static int HalfDown(int cap)
        {
            return cap / 2;
        }

        public static bool Enter(
            PhantomSign sign,
            int estus,
            int estusCap,
            int ash,
            int ashCap,
            float maxHp,
            float unemberedMax,
            out PhantomLoadout saved,
            out int estusNow,
            out int ashNow,
            out float maxNow)
        {
            saved = new PhantomLoadout { estus = estus, ash = ash, maxHp = maxHp };
            estusNow = estus;
            ashNow = ash;
            maxNow = maxHp;
            if (sign != PhantomSign.White && sign != PhantomSign.Yellow) return false;
            estusNow = HalfDown(estusCap);
            ashNow = HalfDown(ashCap);
            maxNow = unemberedMax;
            saved.saved = true;
            return true;
        }

        public static bool Return(PhantomLoadout saved, out int estus, out int ash, out float maxHp)
        {
            estus = saved.estus;
            ash = saved.ash;
            maxHp = saved.maxHp;
            return saved.saved;
        }
    }
}
