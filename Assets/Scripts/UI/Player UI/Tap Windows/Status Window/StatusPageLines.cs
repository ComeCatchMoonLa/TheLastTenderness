namespace CatchMoon
{
    public static class StatusPageLines
    {
        public static string Format(int health, int stamina, int focus, int poise, int strength, int dexterity, int intelligence, int faith, int souls)
        {
            return "生命 " + health + "\n"
                + "精力 " + stamina + "\n"
                + "专注 " + focus + "\n"
                + "韧性 " + poise + "\n"
                + "力量 " + strength + "\n"
                + "敏捷 " + dexterity + "\n"
                + "智力 " + intelligence + "\n"
                + "信仰 " + faith + "\n"
                + "灵魂 " + souls;
        }
    }
}
