namespace CatchMoon
{
    public static class StatusBuildup
    {
        public static float Add(bool invulnerable, string assetName, float meter, float gain, float resist, float capacity)
        {
            if (invulnerable) return meter;
            return BleedMeter.Add(assetName, meter, gain, resist, capacity);
        }
    }
}
