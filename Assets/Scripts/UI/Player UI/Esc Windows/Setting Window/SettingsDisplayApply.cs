namespace CatchMoon
{
    public static class SettingsDisplayApply
    {
        public static void Vsync(VSYNC_Options option, out string label, out int vSyncCount)
        {
            if (option == VSYNC_Options.enable)
            {
                label = "启用";
                vSyncCount = 1;
                return;
            }

            label = "禁用";
            vSyncCount = 0;
        }

        public static void FrameRate(FrameRateLimit_Options option, out string label, out int targetFrameRate)
        {
            label = FrameRateLimitTable.Label(option);
            targetFrameRate = FrameRateLimitTable.Value(option);
        }

        public static void DisplayQuality(DisplayQuality_Options option, out string label, out int level)
        {
            label = DisplayQualityTable.Label(option);
            level = DisplayQualityTable.Level(option);
        }

        public static string PostTreatment(PostTreatmentQuality_Options option)
        {
            return PostTreatmentQualityTable.Label(option);
        }

        public static string SpecialEffect(SpecialEffectQuality_Options option)
        {
            return SpecialEffectQualityTable.Label(option);
        }
    }
}
