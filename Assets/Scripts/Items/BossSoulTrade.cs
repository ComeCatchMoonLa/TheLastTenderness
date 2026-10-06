using System.Collections.Generic;

namespace CatchMoon
{
    public static class BossSoulTrade
    {
        public static bool TryExchange(bool present, List<Item> bag, Item soul, Item chosen)
        {
            if (!present || soul == null || chosen == null || bag == null) return false;
            if (!bag.Contains(soul)) return false;
            bag.Remove(soul);
            bag.Add(chosen);
            return true;
        }
    }
}
