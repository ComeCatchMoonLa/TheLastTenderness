using UnityEngine;

namespace CatchMoon
{
    abstract public class ConsumableItem : Item
    {
        [Header("Item Amount")]
        public int maxItemAmount;
        public int currentItemAmount;

        [Header("Item Model")]
        public GameObject itemModel;

        [Header("Animations")]
        public string consumeAnimation;
        public bool isInteracting;

        abstract public void AttemptToConsumableItem(PlayerManager player);
        abstract public void SuccessfullyUsedConsumable(PlayerManager player);
    }
}