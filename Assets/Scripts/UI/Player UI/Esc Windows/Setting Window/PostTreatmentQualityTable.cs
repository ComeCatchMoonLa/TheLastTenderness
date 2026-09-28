namespace CatchMoon
{
    public static class PostTreatmentQualityTable
    {
        public static string Label(PostTreatmentQuality_Options option)
        {
            switch (option)
            {
                case PostTreatmentQuality_Options.low: return "低";
                default: return "高";
            }
        }

        public static PostTreatmentQuality_Options Next(PostTreatmentQuality_Options option)
        {
            switch (option)
            {
                case PostTreatmentQuality_Options.low: return PostTreatmentQuality_Options.high;
                default: return PostTreatmentQuality_Options.low;
            }
        }
    }
}
