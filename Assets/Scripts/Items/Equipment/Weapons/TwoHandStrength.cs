namespace CatchMoon
{
    public static class TwoHandStrength
    {
        public static int ForNeed(int strength, bool twoHanding)
        {
            if (!twoHanding) return strength;
            int raised = strength * 3 / 2;
            return raised > 99 ? 99 : raised;
        }
    }
}
