namespace CatchMoon
{
    public static class PauseMenuEntries
    {
        public static readonly string[] Names = { "背包", "状态", "姿态" };

        public static bool Contains(string name)
        {
            if (name == null) return false;
            for (int i = 0; i < Names.Length; i++)
            {
                if (Names[i] == name) return true;
            }
            return false;
        }
    }
}
