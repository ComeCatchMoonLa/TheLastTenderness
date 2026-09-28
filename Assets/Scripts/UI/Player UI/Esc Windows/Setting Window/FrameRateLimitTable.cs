namespace CatchMoon
{
    public static class FrameRateLimitTable
    {
        public static string Label(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return "60";
                case FrameRateLimit_Options.max90: return "90";
                case FrameRateLimit_Options.max120: return "120";
                default: return "无限制";
            }
        }

        public static int Value(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return 60;
                case FrameRateLimit_Options.max90: return 90;
                case FrameRateLimit_Options.max120: return 120;
                default: return -1;
            }
        }

        public static FrameRateLimit_Options Next(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return FrameRateLimit_Options.max90;
                case FrameRateLimit_Options.max90: return FrameRateLimit_Options.max120;
                case FrameRateLimit_Options.max120: return FrameRateLimit_Options.unlimited;
                default: return FrameRateLimit_Options.max60;
            }
        }

        public static FrameRateLimit_Options Previous(FrameRateLimit_Options option)
        {
            switch (option)
            {
                case FrameRateLimit_Options.max60: return FrameRateLimit_Options.unlimited;
                case FrameRateLimit_Options.max90: return FrameRateLimit_Options.max60;
                case FrameRateLimit_Options.max120: return FrameRateLimit_Options.max90;
                default: return FrameRateLimit_Options.max120;
            }
        }
    }
}
