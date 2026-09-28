namespace CatchMoon
{
    public struct DamageSegments
    {
        public float Physical;
        public float Fire;
        public float Magic;
        public float Lightning;
        public float Dark;
    }

    public struct IncomingDamage
    {
        public float Multiplier;
        public DamageSegments Segments;
    }
}
