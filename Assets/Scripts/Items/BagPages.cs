using System.Collections.Generic;

namespace CatchMoon
{
    public enum BagPage
    {
        weapon,
        armor,
        ring,
        item,
        spell
    }

    public static class BagPages
    {
        public static BagPage PageOf(Item item)
        {
            if (item is SpellItem) return BagPage.spell;
            if (item is ArmorItem) return BagPage.armor;
            if (item is WeaponItem) return BagPage.weapon;
            if (item != null && item.isRing) return BagPage.ring;
            return BagPage.item;
        }

        public static int Collect(IList<Item> bag, BagPage page, Item[] into)
        {
            if (bag == null || into == null) return 0;
            int written = 0;
            for (int i = 0; i < bag.Count; i++)
            {
                Item item = bag[i];
                if (item == null || PageOf(item) != page) continue;
                if (written >= into.Length) break;
                into[written] = item;
                written++;
            }
            return written;
        }
    }
}
