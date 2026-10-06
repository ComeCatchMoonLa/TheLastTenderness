namespace CatchMoon
{
    public static class CampfireMenu
    {
        public static readonly string[] Names = { "传送", "整理法术", "储物箱", "离开" };

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
