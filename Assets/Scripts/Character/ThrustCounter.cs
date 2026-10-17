namespace CatchMoon
{
    public static class ThrustCounter
    {
        public static float Scale(bool targetAttacking, PhysicalKind kind, bool filled, float weaponScale, float ringScale)
        {
            if (!targetAttacking || kind != PhysicalKind.Thrust || !filled || weaponScale <= 0f)
                return 1f;
            float ring = ringScale <= 0f ? 1f : ringScale;
            return weaponScale * ring;
        }
    }
}
