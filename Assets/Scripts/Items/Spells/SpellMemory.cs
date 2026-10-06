using System.Collections.Generic;

namespace CatchMoon
{
    public static class SpellMemory
    {
        public static SpellItem Next(IList<SpellItem> memorized, ref int index)
        {
            if (memorized == null || memorized.Count == 0) return null;
            index = (index + 1) % memorized.Count;
            return memorized[index];
        }
    }
}
