namespace CatchMoon
{
    public static class SoulDrop
    {
        public static bool TryLeave(int souls, out int remnant)
        {
            if (souls <= 0)
            {
                remnant = 0;
                return false;
            }
            remnant = souls;
            return true;
        }
    }
}
