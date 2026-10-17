using System;
using UnityEngine;

namespace CatchMoon
{
    [Serializable]
    public class DropLine
    {
        public Item item;
        public bool guaranteed;
        public bool rateFilled;
        public float baseRate;
    }

    public static class DropRoll
    {
        public static float Rate(float discovery, float baseRate)
        {
            return discovery / 100f * baseRate;
        }

        public static bool Chance(float discovery, float baseRate, float roll)
        {
            return roll < Rate(discovery, baseRate);
        }

        public static int Collect(string assetName, DropLine[] lines, float discovery, float[] rolls, Item[] into)
        {
            if (lines == null || into == null) return 0;
            int written = 0;
            int rollIndex = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                DropLine line = lines[i];
                if (line == null) continue;
                if (line.item == null)
                {
                    Debug.LogError($"{assetName}: item 未填");
                    continue;
                }
                if (line.guaranteed)
                {
                    if (written < into.Length) into[written++] = line.item;
                    continue;
                }
                if (!line.rateFilled)
                {
                    Debug.LogError($"{assetName}: baseRate 未填");
                    continue;
                }
                float roll = rolls != null && rollIndex < rolls.Length ? rolls[rollIndex] : 1f;
                rollIndex++;
                if (!Chance(discovery, line.baseRate, roll)) continue;
                if (written < into.Length) into[written++] = line.item;
            }
            return written;
        }
    }
}
