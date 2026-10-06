namespace CatchMoon
{
    public static class BossPhase
    {
        public static bool ShouldShift(float current, float max, float threshold, bool alreadyShifted)
        {
            if (alreadyShifted || max <= 0f || threshold <= 0f) return false;
            return current / max <= threshold;
        }
    }
}
