using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public enum BodyBuffLayer
    {
        Consumable,
        Art,
        Spell
    }

    public struct BodyBuff
    {
        public BodyBuffLayer layer;
        public string group;
        public string name;
    }

    public static class BodyBuffs
    {
        public static bool TryPut(string assetName, List<BodyBuff> slots, BodyBuffLayer layer, string group, string buffName)
        {
            if (string.IsNullOrEmpty(group))
            {
                Debug.LogError($"{assetName}: group 未填");
                return false;
            }

            if (slots == null) return false;
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                if (slots[i].layer == layer && slots[i].group == group)
                    slots.RemoveAt(i);
            }

            slots.Add(new BodyBuff { layer = layer, group = group, name = buffName });
            return true;
        }

        public static void Clear(List<BodyBuff> slots)
        {
            if (slots == null) return;
            slots.Clear();
        }
    }
}
