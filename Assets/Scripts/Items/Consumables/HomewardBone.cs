namespace CatchMoon
{
    public class HomewardState
    {
        public bool bossBarVisible;
        public int bones;
        public string lastFire;
        public string place;
        public int souls;
        public bool emberLit;
        public float hp;
        public float maxHp;
        public float mp;
        public float maxMp;
        public int estus;
        public int estusShare;
        public int ash;
        public int ashShare;
        public bool resetEnemies;
    }

    public static class HomewardBone
    {
        public static bool Allowed(bool bossBarVisible, int bones, string lastFire)
        {
            return !bossBarVisible && bones > 0 && !string.IsNullOrEmpty(lastFire);
        }

        public static bool TryUse(HomewardState state)
        {
            if (!Allowed(state.bossBarVisible, state.bones, state.lastFire))
                return false;
            state.place = state.lastFire;
            state.bones -= 1;
            state.hp = state.maxHp;
            state.mp = state.maxMp;
            state.estus = state.estusShare;
            state.ash = state.ashShare;
            state.resetEnemies = true;
            return true;
        }
    }
}
