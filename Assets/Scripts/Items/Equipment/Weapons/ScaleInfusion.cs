namespace CatchMoon
{
    public enum UpgradePath
    {
        normal,
        scale,
        twinkling
    }

    public static class ScaleInfusion
    {
        public static int List(UpgradePath path, string[] names, string[] into)
        {
            if (path == UpgradePath.scale || path == UpgradePath.twinkling)
                return 0;
            if (names == null || into == null) return 0;
            int written = 0;
            for (int i = 0; i < names.Length; i++)
            {
                if (written >= into.Length) break;
                into[written] = names[i];
                written++;
            }
            return written;
        }
    }
}
