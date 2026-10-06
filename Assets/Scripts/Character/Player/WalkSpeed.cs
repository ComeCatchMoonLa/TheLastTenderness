using UnityEngine;

namespace CatchMoon
{
    public static class WalkSpeed
    {
        public static float Choose(string assetName, float moveAmount, bool sprinting, float walkSpeed, float runSpeed, float sprintSpeed, float walkUntil)
        {
            if (sprinting) return sprintSpeed;
            if (walkUntil <= 0f || walkUntil >= 1f) return runSpeed;
            if (moveAmount <= 0f || moveAmount > walkUntil) return runSpeed;
            if (walkSpeed <= 0f || walkSpeed >= runSpeed)
            {
                Debug.LogError($"{assetName}: walkSpeed 未填");
                return runSpeed;
            }
            return walkSpeed;
        }
    }
}
