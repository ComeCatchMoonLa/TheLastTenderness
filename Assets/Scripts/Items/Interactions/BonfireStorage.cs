using System.Collections.Generic;

namespace CatchMoon
{
    public static class BonfireStorage
    {
        public static bool TryDeposit(List<Item> box, List<Item> bag, Item item, bool equipped)
        {
            if (item == null || box == null || bag == null || equipped) return false;
            if (!bag.Contains(item)) return false;
            bag.Remove(item);
            box.Add(item);
            return true;
        }

        public static bool TryWithdraw(List<Item> box, List<Item> bag, Item item)
        {
            if (item == null || box == null || bag == null) return false;
            if (!box.Contains(item)) return false;
            box.Remove(item);
            bag.Add(item);
            return true;
        }
    }
}
