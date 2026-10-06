using UnityEngine;

namespace CatchMoon
{
    public static class ChestLoot
    {
        public static bool Give(bool opened, Item item, PlayerInventoryManager inventory)
        {
            if (opened || item == null || inventory == null) return false;
            inventory.AddItem(item);
            return true;
        }

        public static bool AfterRest(bool opened)
        {
            return opened;
        }
    }
}
