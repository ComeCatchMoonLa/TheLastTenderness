namespace CatchMoon
{
    public static class InfusionGate
    {
        public static bool Allows(WeaponType type, bool torch)
        {
            if (torch) return false;
            if (type == WeaponType.bow || type == WeaponType.crossbow || type == WeaponType.greatbow) return false;
            if (type == WeaponType.melee_OH_Shield) return false;
            if (type == WeaponType.pyromancyCaster || type == WeaponType.faithCaster || type == WeaponType.spellCaster) return false;
            return true;
        }
    }
}
