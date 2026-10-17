namespace CatchMoon
{
    public static class SoulLevel
    {
        public static int From(int health, int focus, int stamina, int vitality, int strength, int dexterity, int intelligence, int faith, int luck)
        {
            return health + focus + stamina + vitality + strength + dexterity + intelligence + faith + luck - 89;
        }
    }
}
