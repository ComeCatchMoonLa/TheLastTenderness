using UnityEngine;

namespace CatchMoon
{
    public struct EquipPreview
    {
        public float attackDelta;
        public float absorptionDelta;
    }

    public static class EquipCompare
    {
        public static Item armed;

        public static EquipPreview Preview(int currentAttack, int nextAttack, float currentAbsorption, float nextAbsorption)
        {
            return new EquipPreview
            {
                attackDelta = nextAttack - currentAttack,
                absorptionDelta = nextAbsorption - currentAbsorption
            };
        }

        public static bool Arm(Item item)
        {
            if (item == null) return false;
            if (armed != item)
            {
                armed = item;
                return false;
            }
            armed = null;
            return true;
        }

        public static bool CommitAbsorption(bool confirm, ref float absorption, float nextAbsorption)
        {
            if (!confirm) return false;
            absorption = nextAbsorption;
            return true;
        }
    }
}
