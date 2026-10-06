using System.Collections.Generic;

namespace CatchMoon
{
    public static class SpellBookStock
    {
        public static bool TryHandIn(List<Item> bag, Item book, bool accepts, List<SpellItem> catalog, SpellItem[] added)
        {
            if (!accepts || book == null || bag == null || catalog == null || added == null) return false;
            if (!bag.Contains(book)) return false;
            bag.Remove(book);
            for (int i = 0; i < added.Length; i++)
            {
                if (catalog.Contains(added[i])) continue;
                catalog.Add(added[i]);
            }
            return true;
        }

        public static bool CanOpen(bool merchantAlive)
        {
            return merchantAlive;
        }
    }
}
