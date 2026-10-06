using UnityEngine;

namespace CatchMoon
{
    public struct WeaponBuffState
    {
        public bool active;
        public float timeLeft;
        public DamageSegments extra;
        public Object weapon;
    }

    public static class WeaponBuff
    {
        public static bool CanApply(string infusion, bool cannotBuff)
        {
            if (cannotBuff) return false;
            if (string.IsNullOrEmpty(infusion)) return true;
            return infusion == "重" || infusion == "锐" || infusion == "精" || infusion == "原质" || infusion == "空虚";
        }

        public static bool TryApply(string assetName, string infusion, bool cannotBuff, float duration, DamageSegments extra, Object weapon, out WeaponBuffState state)
        {
            state = default;
            if (duration <= 0f)
            {
                Debug.LogError($"{assetName}: duration 未填");
                return false;
            }
            if (!CanApply(infusion, cannotBuff)) return false;
            state.active = true;
            state.timeLeft = duration;
            state.extra = extra;
            state.weapon = weapon;
            return true;
        }

        public static WeaponBuffState Tick(WeaponBuffState state, float delta)
        {
            if (!state.active || delta <= 0f) return state;
            state.timeLeft -= delta;
            if (state.timeLeft <= 0f) state.active = false;
            return state;
        }

        public static WeaponBuffState ClearIfUnequipped(WeaponBuffState state, Object right, Object left)
        {
            if (!state.active) return state;
            if (state.weapon != right && state.weapon != left) state.active = false;
            return state;
        }

        public static WeaponBuffState ClearIfStored(WeaponBuffState state, Object stored, bool deposited)
        {
            if (deposited && state.active && state.weapon == stored) state.active = false;
            return state;
        }

        public static DamageSegments Add(DamageSegments hit, WeaponBuffState state)
        {
            if (!state.active) return hit;
            hit.Physical += state.extra.Physical;
            hit.Fire += state.extra.Fire;
            hit.Magic += state.extra.Magic;
            hit.Lightning += state.extra.Lightning;
            hit.Dark += state.extra.Dark;
            return hit;
        }

        public static GameObject FindFire(Transform root)
        {
            if (root == null) return null;
            Transform[] all = root.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].name == "FX_Sword_Fire") return all[i].gameObject;
            }
            return null;
        }

        public static void ShowFire(GameObject fx, bool active)
        {
            if (fx == null) return;
            fx.SetActive(active);
        }
    }
}
