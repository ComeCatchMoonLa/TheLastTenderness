namespace CatchMoon
{
    public static class SpecialEffectQualityTable
    {
        public static string Label(SpecialEffectQuality_Options option)
        {
            switch (option)
            {
                case SpecialEffectQuality_Options.low: return "低";
                default: return "高";
            }
        }

        public static SpecialEffectQuality_Options Next(SpecialEffectQuality_Options option)
        {
            switch (option)
            {
                case SpecialEffectQuality_Options.low: return SpecialEffectQuality_Options.high;
                default: return SpecialEffectQuality_Options.low;
            }
        }
    }
}
