namespace CatchMoon
{
    public static class DisplayQualityTable
    {
        public static string Label(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return "非常低";
                case DisplayQuality_Options.low: return "低";
                case DisplayQuality_Options.medium: return "中等";
                case DisplayQuality_Options.high: return "高";
                case DisplayQuality_Options.veryHigh: return "非常高";
                default: return "最高";
            }
        }

        public static int Level(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return 0;
                case DisplayQuality_Options.low: return 1;
                case DisplayQuality_Options.medium: return 2;
                case DisplayQuality_Options.high: return 3;
                case DisplayQuality_Options.veryHigh: return 4;
                default: return 5;
            }
        }

        public static DisplayQuality_Options Next(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return DisplayQuality_Options.low;
                case DisplayQuality_Options.low: return DisplayQuality_Options.medium;
                case DisplayQuality_Options.medium: return DisplayQuality_Options.high;
                case DisplayQuality_Options.high: return DisplayQuality_Options.veryHigh;
                case DisplayQuality_Options.veryHigh: return DisplayQuality_Options.Ultra;
                default: return DisplayQuality_Options.veryLow;
            }
        }

        public static DisplayQuality_Options Previous(DisplayQuality_Options option)
        {
            switch (option)
            {
                case DisplayQuality_Options.veryLow: return DisplayQuality_Options.Ultra;
                case DisplayQuality_Options.low: return DisplayQuality_Options.veryLow;
                case DisplayQuality_Options.medium: return DisplayQuality_Options.low;
                case DisplayQuality_Options.high: return DisplayQuality_Options.medium;
                case DisplayQuality_Options.veryHigh: return DisplayQuality_Options.high;
                default: return DisplayQuality_Options.veryHigh;
            }
        }
    }
}
