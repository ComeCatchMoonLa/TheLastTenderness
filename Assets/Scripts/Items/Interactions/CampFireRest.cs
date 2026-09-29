using UnityEngine;

namespace CatchMoon
{
    public static class CampFireRest
    {
        public static bool Allow(float radius, bool pursuingInside)
        {
            if (radius <= 0f) return false;
            return !pursuingInside;
        }
    }
}
