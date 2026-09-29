using System.Collections.Generic;

namespace CatchMoon
{
    public static class DialogueTail
    {
        public static void DropSpoken<T>(List<T> lines)
        {
            if (lines == null || lines.Count <= 1) return;
            lines.RemoveAt(0);
        }
    }
}
