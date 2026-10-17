namespace CatchMoon
{
    public static class ArmorPoise
    {
        public static float Fold(float current, float piece)
        {
            return current + piece - current * piece / 100f;
        }

        public static float FromPieces(float head, float torso, float hips, float hand)
        {
            float total = Fold(0f, head);
            total = Fold(total, torso);
            total = Fold(total, hips);
            return Fold(total, hand);
        }
    }
}
