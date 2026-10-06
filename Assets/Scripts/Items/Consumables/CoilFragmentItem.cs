using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Consumable/Coil Fragment")]
    public class CoilFragmentItem : HomewardBoneItem
    {
        void OnEnable()
        {
            itemType = ItemType.consumable;
            if (maxItemAmount <= 0) maxItemAmount = 1;
        }

        public override void SuccessfullyUsedConsumable(PlayerManager player)
        {
            if (!TryCapture(player, out _, out HomewardState state)) return;
            if (!CoilFragment.TryUse(state)) return;
            ApplyReturn(player, state);
        }
    }
}
