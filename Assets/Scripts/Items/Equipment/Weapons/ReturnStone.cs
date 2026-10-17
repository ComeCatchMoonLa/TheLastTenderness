using UnityEngine;

namespace CatchMoon
{
    public static class ReturnStone
    {
        public static bool TryRevert(string assetName, string stoneInfusion, WeaponItem weapon)
        {
            if (weapon == null) return false;
            if (string.IsNullOrEmpty(stoneInfusion))
            {
                Debug.LogError($"{assetName}: infusion 未填");
                return false;
            }
            if (weapon.infusion != stoneInfusion) return false;
            weapon.infusion = string.Empty;
            return true;
        }
    }
}
