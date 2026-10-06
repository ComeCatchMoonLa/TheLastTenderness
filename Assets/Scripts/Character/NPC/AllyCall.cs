namespace CatchMoon
{
    public static class AllyCall
    {
        public static bool Enters(bool sameGroup, bool discovered, bool callFinished)
        {
            if (sameGroup) return discovered;
            return callFinished;
        }

        public static void Begin(ref bool isCalling, ref bool callFinished)
        {
            isCalling = true;
            callFinished = false;
        }

        public static void Interrupt(ref bool isCalling, ref bool callFinished)
        {
            if (!isCalling) return;
            isCalling = false;
            callFinished = false;
        }

        public static bool Finish(ref bool isCalling, ref bool callFinished)
        {
            if (!isCalling) return false;
            isCalling = false;
            callFinished = true;
            return true;
        }

        public static void WakeGroup(EnemyManager caller, CharacterManager target)
        {
            if (caller == null || target == null || caller.instantGroup == null) return;
            for (int i = 0; i < caller.instantGroup.Length; i++)
            {
                EnemyManager ally = caller.instantGroup[i];
                if (ally == null || ally.currentTarget != null) continue;
                if (!Enters(true, true, false)) continue;
                ally.currentTarget = target;
            }
        }

        public static void WakeDistant(EnemyManager caller)
        {
            if (caller == null || caller.currentTarget == null || caller.distantAllies == null) return;
            for (int i = 0; i < caller.distantAllies.Length; i++)
            {
                EnemyManager ally = caller.distantAllies[i];
                if (ally == null || ally.currentTarget != null) continue;
                if (!Enters(false, false, true)) continue;
                ally.currentTarget = caller.currentTarget;
            }
        }
    }
}
