using System.Collections.Generic;

namespace CatchMoon
{
    public static class MerchantAsh
    {
        public static bool TryDrop(Item ash, List<Item> bag)
        {
            if (ash == null || bag == null || bag.Contains(ash)) return false;
            bag.Add(ash);
            return true;
        }

        public static bool TryHandOver(bool accepts, List<Item> bag, Item ash, List<Item> catalog, Item[] goods)
        {
            if (!accepts || ash == null || bag == null || catalog == null || goods == null) return false;
            if (!bag.Contains(ash)) return false;
            bag.Remove(ash);
            for (int i = 0; i < goods.Length; i++)
            {
                if (goods[i] == null || catalog.Contains(goods[i])) continue;
                catalog.Add(goods[i]);
            }
            return true;
        }
    }
}
