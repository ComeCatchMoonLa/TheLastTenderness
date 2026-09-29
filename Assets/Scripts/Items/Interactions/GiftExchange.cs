using System.Collections.Generic;

namespace CatchMoon
{
    public static class GiftExchange
    {
        public static bool CanTake(IList<Item> items, Item wanted, Item offered, bool alreadyAdvanced)
        {
            if (alreadyAdvanced || wanted == null || offered != wanted) return false;
            return items != null && items.Contains(offered);
        }
    }
}
