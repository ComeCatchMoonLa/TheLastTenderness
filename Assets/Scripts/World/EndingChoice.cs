namespace CatchMoon
{
    public static class EndingChoice
    {
        public static string Choose(bool stolenFlame, bool eyesGiven, bool keeperSignUsed)
        {
            if (stolenFlame) return "窃火";
            if (eyesGiven && keeperSignUsed) return "灭火";
            return "传火";
        }
    }
}
