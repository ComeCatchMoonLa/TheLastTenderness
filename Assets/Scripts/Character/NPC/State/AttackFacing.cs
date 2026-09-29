using UnityEngine;

namespace CatchMoon
{
    public static class AttackFacing
    {
        public static void Apply(Transform facingBody, Transform forwardSource, ref Vector3 targetDir, bool canRotate, bool isInteracting, float slerpFactor)
        {
            if (!canRotate || !isInteracting)
                return;

            if (targetDir == Vector3.zero)
                targetDir = forwardSource.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            facingBody.rotation = Quaternion.Slerp(facingBody.rotation, targetRotation, slerpFactor);
        }
    }
}
