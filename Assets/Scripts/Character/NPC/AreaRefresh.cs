namespace CatchMoon
{
    public static class AreaRefresh
    {
        public struct Actor
        {
            public bool story;
            public bool boss;
            public bool mimic;
            public bool lizard;
            public bool dead;
            public bool oneShot;
            public bool oneShotReturns;
        }

        public static bool Returns(Actor actor)
        {
            if (actor.story || actor.boss) return false;
            if (actor.mimic || actor.lizard) return !actor.dead;
            if (actor.oneShot) return actor.oneShotReturns;
            return true;
        }
    }
}
