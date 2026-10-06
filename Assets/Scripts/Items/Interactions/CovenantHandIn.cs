using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class HandInState
    {
        public int handed;
        public int firstAt;
        public int secondAt;
        public bool gotFirst;
        public bool gotSecond;
        public bool giveFirst;
        public bool giveSecond;
    }

    public static class CovenantHandIn
    {
        public static bool TryOffer(IList<Item> bag, Item wanted, Item offered, HandInState state, string assetName)
        {
            state.giveFirst = false;
            state.giveSecond = false;
            if (state.firstAt <= 0)
            {
                Debug.LogError($"{assetName}: firstAt 未填");
                return false;
            }
            if (state.secondAt <= 0)
            {
                Debug.LogError($"{assetName}: secondAt 未填");
                return false;
            }
            if (!GiftExchange.CanTake(bag, wanted, offered, false))
                return false;
            bag.Remove(offered);
            state.handed++;
            state.giveFirst = !state.gotFirst && state.handed >= state.firstAt;
            state.giveSecond = !state.gotSecond && state.handed >= state.secondAt;
            if (state.giveFirst) state.gotFirst = true;
            if (state.giveSecond) state.gotSecond = true;
            return true;
        }
    }
}
