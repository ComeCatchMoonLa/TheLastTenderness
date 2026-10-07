namespace CatchMoon
{
    public enum StanceDerive
    {
        None,
        Light,
        Heavy
    }

    public static class WeaponStance
    {
        public static bool Enter(bool stanceArt, bool interacting, bool holding)
        {
            return stanceArt && !interacting && !holding;
        }

        public static StanceDerive TakeSwing(bool holding, bool light)
        {
            if (!holding) return StanceDerive.None;
            return light ? StanceDerive.Light : StanceDerive.Heavy;
        }

        public static bool Breaks(bool holding, bool poiseHolds)
        {
            return holding && !poiseHolds;
        }
    }
}
