using UnityEngine;

namespace CatchMoon
{
    public static class CameraRigMotion
    {
        public static Vector3 AimPosition(Vector3 aimingAnchor)
        {
            return aimingAnchor;
        }

        public static Vector3 LockPosition(Vector3 playerPosition)
        {
            return playerPosition;
        }

        public static Vector3 DefaultPosition(Vector3 current, Vector3 playerPosition, ref Vector3 velocity, float fadeTime)
        {
            return Vector3.SmoothDamp(current, playerPosition, ref velocity, fadeTime);
        }

        public static float ForwardDistance(bool aiming, float aimingDistance, float defaultDistance, bool blocked, float hitDistance, float collisionOffset, float minOffset)
        {
            if (aiming)
                return aimingDistance;

            float target = defaultDistance;
            if (blocked)
                target = hitDistance - collisionOffset;
            if (Mathf.Abs(target) < minOffset)
                target = minOffset;
            return target;
        }

        public static Vector3 LocalBack(float forwardDistance)
        {
            return -Vector3.forward * forwardDistance;
        }
    }
}
