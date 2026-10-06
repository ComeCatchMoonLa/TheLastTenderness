using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public static class ShopTrade
    {
        public static bool Buy(string assetName, int souls, int price, List<Item> bag, Item item, out int newSouls)
        {
            newSouls = souls;
            if (price <= 0)
            {
                Debug.LogError($"{assetName}: price 未填");
                return false;
            }
            if (souls < price || item == null || bag == null) return false;
            bag.Add(item);
            newSouls = souls - price;
            return true;
        }

        public static bool Sell(string assetName, int souls, int price, List<Item> bag, Item item, out int newSouls)
        {
            newSouls = souls;
            if (price <= 0)
            {
                Debug.LogError($"{assetName}: sellPrice 未填");
                return false;
            }
            if (item == null || bag == null || !bag.Contains(item)) return false;
            bag.Remove(item);
            newSouls = souls + price;
            return true;
        }
    }
}
