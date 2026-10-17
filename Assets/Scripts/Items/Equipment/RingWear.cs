using UnityEngine;

namespace CatchMoon
{
    public static class RingWear
    {
        public static bool TryPut(Item[] slots, Item ring)
        {
            if (slots == null || ring == null || !ring.isRing) return false;
            if (string.IsNullOrEmpty(ring.ringFamily))
            {
                Debug.LogError($"{ring.name}: ringFamily 未填");
                return false;
            }

            int empty = -1;
            int n = slots.Length < EquipmentLayout.RingSlots ? slots.Length : EquipmentLayout.RingSlots;
            for (int i = 0; i < n; i++)
            {
                if (slots[i] == null)
                {
                    if (empty < 0) empty = i;
                    continue;
                }
                if (slots[i].ringFamily == ring.ringFamily) return false;
            }
            if (empty < 0) return false;
            slots[empty] = ring;
            return true;
        }

        public static bool TryRemove(Item[] slots, Item ring)
        {
            if (slots == null || ring == null) return false;
            int n = slots.Length < EquipmentLayout.RingSlots ? slots.Length : EquipmentLayout.RingSlots;
            for (int i = 0; i < n; i++)
            {
                if (slots[i] != ring) continue;
                slots[i] = null;
                return true;
            }
            return false;
        }
    }
}
