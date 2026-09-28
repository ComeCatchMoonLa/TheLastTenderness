using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 休眠状态
    /// </summary>
    public class AmbushState : State
    {
        public bool isSleeping;             // 沉睡中
        public float detectionRadius = 2;   // 察觉半径
        public string sleepAnimation;       // 沉睡动画
        public string wakeAnimation;        // 苏醒动画

        PursueTargetState pursueTargetState;
        Collider[] detectionOverlapResults = new Collider[16];

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (isSleeping && !enemy.isInteracting)
                enemy.eAnimator.PlayTargetAnimation(sleepAnimation, true);

            int detectionCount = OverlapQuery.CollectOverlaps(enemy.transform.position, detectionRadius, LayerMask.player, ref detectionOverlapResults);

            for (int i = 0; i < detectionCount; ++i)
            {
                CharacterManager targetCharacter = detectionOverlapResults[i].gameObject.GetComponent<CharacterManager>();

                if (targetCharacter != null)
                {
                    Vector3 targetsDirection = targetCharacter.transform.position - enemy.transform.position;
                    float viewableAngle = Vector3.SignedAngle(targetsDirection, enemy.transform.forward, Vector3.up);

                    if (viewableAngle > enemy.aiSettings.minViewAngel && viewableAngle < enemy.aiSettings.maxViewAngel)
                    {
                        enemy.currentTarget = targetCharacter;
                        isSleeping = false;
                        enemy.eAnimator.PlayTargetAnimation(wakeAnimation, true);
                    }
                }
            }

            if (enemy.currentTarget != null)
                return pursueTargetState;
            else
                return this;
        }
    }
}

