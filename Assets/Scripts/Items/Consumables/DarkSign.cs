namespace CatchMoon
{
    public class DarkSignState
    {
        public bool bossFight;
        public int souls;
        public string lastFire;
        public string place;
        public bool hadRemnant;
        public int remnant;
    }

    public static class DarkSign
    {
        public static bool TryUse(DarkSignState state)
        {
            if (state.bossFight || state.souls <= 0 || string.IsNullOrEmpty(state.lastFire))
                return false;
            state.place = state.lastFire;
            state.souls = 0;
            return true;
        }
    }
}
