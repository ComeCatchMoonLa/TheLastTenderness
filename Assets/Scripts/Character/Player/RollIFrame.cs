namespace CatchMoon
{
    public static class RollIFrame
    {
        public const float FramesPerSecond = 30f;

        public static int Frame(float elapsed)
        {
            if (elapsed < 0f) elapsed = 0f;
            return 1 + (int)(elapsed * FramesPerSecond);
        }

        public static bool Open(int frame, bool heavy)
        {
            int end = heavy ? 13 : 14;
            return frame >= 2 && frame <= end;
        }
    }
}
