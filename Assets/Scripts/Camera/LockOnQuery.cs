using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class LockOnQuery : MonoBehaviour
    {
        PlayerCameraManager camera;
        Collider[] lockOnOverlapResults = new Collider[32];

        [SerializeField] float maxLookOnAngle_Half = 50f;
        [SerializeField] float maxLookOnAngle_Half_AutoChangeLockOnTargetMode = 30f;
        [SerializeField] float autoChangeLockOnTargetTime = 2f;
        [SerializeField] float autoChangeLockOnTargetTimer = 1f;
        [SerializeField] float maxLockOnDist = 30.0f;
        [SerializeField] List<CharacterManager> lockableTargets = new List<CharacterManager>();

        void Awake()
        {
            camera = GetComponent<PlayerCameraManager>();
        }

        public void UpdateLockOnTargets()
        {
            if (!camera.lockOnMode) return;

            lockableTargets.Clear();
            camera.nearestLockableTarget = null;
            camera.leftLockableTarget = null;
            camera.rightLockableTarget = null;

            float minDist = Mathf.Infinity;
            float minDistOfLeftTarget = -Mathf.Infinity;
            float minDistOfRightTarget = Mathf.Infinity;

            int lockOnCount = OverlapQuery.CollectOverlaps(camera.Player.transform.position, maxLockOnDist, LayerMask.npc, ref lockOnOverlapResults);
            for (int i = 0; i < lockOnCount; ++i)
            {
                CharacterManager lockableTarget = lockOnOverlapResults[i].GetComponent<CharacterManager>();
                if (lockableTarget == null || lockableTarget == camera.Player) continue;
                Vector3 dir = lockableTarget.transform.position - camera.Player.transform.position;
                float fov = Vector3.SignedAngle(dir, camera.cameraTransform.forward, Vector3.up);
                float dist = Vector3.Distance(lockableTarget.transform.position, camera.Player.transform.position);
                if (camera.Player.ui.escWin.GetSettingWin().gameSettingsData.autoChangeLockOnTarget)
                {
                    if (fov < -maxLookOnAngle_Half_AutoChangeLockOnTargetMode
                        || fov > maxLookOnAngle_Half_AutoChangeLockOnTargetMode || dist > maxLockOnDist) continue;
                }
                else
                {
                    if (fov < -maxLookOnAngle_Half || fov > maxLookOnAngle_Half || dist > maxLockOnDist) continue;
                }

                RaycastHit hit;
                if (Physics.Linecast(camera.Player.lockOnTransform.position, lockableTarget.lockOnTransform.position, out hit, camera.cameraCanDetectLayers)
                    && hit.transform.gameObject.layer == Layer.environment)
                    continue;
                if (lockableTarget.cStats.isDead) continue;
                lockableTargets.Add(lockableTarget);
                if (dist < minDist)
                {
                    minDist = dist;
                    camera.nearestLockableTarget = lockableTarget;
                }
            }
            foreach (CharacterManager lockableTarget in lockableTargets)
            {
                if (camera.curLockOnTarget != null)
                {
                    if (lockableTarget == camera.curLockOnTarget) continue;
                }
                else
                {
                    if (lockableTarget == camera.nearestLockableTarget) continue;
                }
                float relativePlayerPosX = camera.Player.transform.InverseTransformPoint(lockableTarget.transform.position).x;
                if (relativePlayerPosX <= 0f)
                {
                    if (relativePlayerPosX > minDistOfLeftTarget)
                    {
                        minDistOfLeftTarget = relativePlayerPosX;
                        camera.leftLockableTarget = lockableTarget;
                    }
                }
                else
                {
                    if (relativePlayerPosX < minDistOfRightTarget)
                    {
                        minDistOfRightTarget = relativePlayerPosX;
                        camera.rightLockableTarget = lockableTarget;
                    }
                }
            }
            HandleChangeLockOnTarget();
        }

        void HandleChangeLockOnTarget()
        {
            if (camera.Player.ui.escWin.GetSettingWin().gameSettingsData.autoChangeLockOnTarget)
            {
                if (camera.curLockOnTarget == null)
                {
                    if (camera.nearestLockableTarget)
                    {
                        camera.curLockOnTarget = camera.nearestLockableTarget;
                        camera.lockOnFlag = true;
                    }
                }
                else
                {
                    autoChangeLockOnTargetTimer += Time.deltaTime;

                    if (autoChangeLockOnTargetTimer > autoChangeLockOnTargetTime)
                    {
                        autoChangeLockOnTargetTimer = 0f;

                        if (camera.nearestLockableTarget)
                        {
                            camera.curLockOnTarget = camera.nearestLockableTarget;
                            camera.lockOnFlag = true;
                        }
                        else
                        {
                            camera.Unlock();
                        }
                    }
                }
            }
            else
            {
                bool needChangeLockTarget = false;
                if (camera.curLockOnTarget != null)
                {
                    float curTargetDist = Vector3.Distance(camera.curLockOnTarget.transform.position, camera.Player.transform.position);
                    if (curTargetDist > maxLockOnDist)
                        needChangeLockTarget = true;
                    RaycastHit hit;
                    if (Physics.Linecast(camera.Player.lockOnTransform.position, camera.curLockOnTarget.lockOnTransform.position, out hit, camera.cameraCanDetectLayers)
                        && hit.transform.gameObject.layer == Layer.environment)
                        needChangeLockTarget = true;
                }
                else
                {
                    needChangeLockTarget = true;
                }

                if (needChangeLockTarget)
                {
                    if (camera.nearestLockableTarget != null)
                    {
                        if (camera.changeLockOnTargetMode == ChangeLockOnTargetMode.nearest)
                            camera.curLockOnTarget = camera.nearestLockableTarget;
                        camera.lockOnFlag = true;
                    }
                    else
                    {
                        camera.Unlock();
                    }
                }
            }
        }
    }
}
