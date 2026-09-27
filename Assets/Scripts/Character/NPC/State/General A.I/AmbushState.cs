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

        private void Awake()
        {
            pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemy)
        {
            if (isSleeping && !enemy.isInteracting)
                enemy.eAnimator.PlayTargetAnimation(sleepAnimation, true);

            Collider[] colliders = Physics.OverlapSphere(enemy.transform.position, detectionRadius, LayerMask.player);

            foreach(Collider collider in colliders)
            {
                CharacterManager targetCharacter = collider.gameObject.GetComponent<CharacterManager>();

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

