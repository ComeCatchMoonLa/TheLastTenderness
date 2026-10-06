namespace CatchMoon
{
    public static class BonfireTravel
    {
        public static int List(string[] names, bool[] lit, string[] into)
        {
            int n = names.Length < lit.Length ? names.Length : lit.Length;
            int written = 0;
            for (int i = 0; i < n; i++)
            {
                if (!lit[i]) continue;
                if (written >= into.Length) break;
                into[written] = names[i];
                written++;
            }
            return written;
        }

        public static bool TryGo(bool originLit, bool destinationLit, bool sameFire)
        {
            if (!originLit || !destinationLit || sameFire) return false;
            return true;
        }
    }
}
