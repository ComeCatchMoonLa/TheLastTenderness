using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 伤害触发器
    /// </summary>
    public class DamageCollider : MonoBehaviour
    {
        [HideInInspector] public CharacterManager character;
        protected Collider damageCollider;
        public bool enabledDamageColliderOnStartUp = false;

        [Header("队伍ID")]
        public int teamID = 0;

        [Header("Poise")]
        public float poiseBreak;
        public float offensivePoiseBonus;

        [Header("伤害")]
        [HideInInspector] public float pd;
        [HideInInspector] public float fd;
        [HideInInspector] public float md;
        [HideInInspector] public float ld;
        [HideInInspector] public float dd;

        [Header("伤害系数")]
        public float guardBreakModifider = 1;
        protected string currentDamageAnimation;

        protected virtual void Awake()
        {
            damageCollider = GetComponent<Collider>();

            #region 检错
            if (damageCollider == null)
            {
                Debug.LogError("damageCollider is null.");
                return;
            }
            #endregion
        }
        protected virtual void Start()
        {
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = enabledDamageColliderOnStartUp;
        }

        /// <summary>
        /// 启用伤害触发器
        /// </summary>
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        /// <summary>
        /// 禁用伤害触发器
        /// </summary>
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        /// <summary>
        /// 进入触发器
        /// </summary>
        /// <param name="collision">触发器捕获的对象</param>
        protected virtual void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player" || collision.tag == "Enemy") // 攻击角色
            {
                CharacterManager damageTarget = collision.GetComponent<CharacterManager>();
                if (damageTarget == null)
                {
                    Debug.LogError("PlayerManager is null.");
                    return;
                }

                if (character == damageTarget || damageTarget.cStats.teamID == teamID) return;

                // 正在弹反
                if (damageTarget.cCombat.isParrying)
                {
                    character.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Parried", true);
                    character.canBeRiposted = true;
                }
                // 防御成功
                else if (damageTarget.cCombat.isBlocking) 
                {
                    DealDamage(damageTarget, "Block - Hit", pd, fd, md, ld, dd);
                }
                // 没有弹反也没有防御成功(例如, 头部受到攻击, 以及受到来自侧面或后面的攻击)
                else
                {
                    if (damageTarget.cStats.isInvulnerable) return;

                    // 获取我们的武器与该collider第一次接触的地方, 在该处播放血溅特效
                    Vector3 contactPoint = collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    character.cEffects.PlayBloodSplatter(contactPoint);

                    // 根据伤害的方向, 决定播放哪个受伤动画
                    float directionHitFrom = Vector3.SignedAngle(character.transform.forward, damageTarget.transform.forward, Vector3.up);
                    ChooseWhichDirectionDamageCameFrom(directionHitFrom);
                    
                    DealDamage(damageTarget, currentDamageAnimation, pd, fd, md, ld, dd);
                }
            }
        }

        protected virtual void DealDamage(CharacterManager damageTarget, string damageAnimation, float pd, float fd, float md, float ld, float dd)
        {
            // 处理伤害系数(根据攻击的类型，如重攻击与轻攻击伤害系数不同)
            if (character.isUsingRightHand)
            {
                WeaponItem rightWeapon = character.cInventory.rightWeapon;
                if (character.cCombat.attackType == AttackType.light_1)
                    DealDamageWithM(rightWeapon.laFirstPhaseDM, ref pd, ref fd, ref md, ref ld, ref dd);
                else if (character.cCombat.attackType == AttackType.light_2)
                    DealDamageWithM(rightWeapon.laSecondPhaseDM, ref pd, ref fd, ref md, ref ld, ref dd);
                else if (character.cCombat.attackType == AttackType.heavy_1)
                    DealDamageWithM(rightWeapon.haFirstPhaseDM, ref pd, ref fd, ref md, ref ld, ref dd);
                else if (character.cCombat.attackType == AttackType.heavy_2)
                    DealDamageWithM(rightWeapon.haSecondPhasDM, ref pd, ref fd, ref md, ref ld, ref dd);
            }
            else if (character.isUsingLeftHand)
            {
                WeaponItem leftWeapon = character.cInventory.leftWeapon;
                if (character.cCombat.attackType == AttackType.light_1)
                    DealDamageWithM(leftWeapon.laFirstPhaseDM, ref pd, ref fd, ref md, ref ld, ref dd);
                else if (character.cCombat.attackType == AttackType.light_2)
                    DealDamageWithM(leftWeapon.laSecondPhaseDM, ref pd, ref fd, ref md, ref ld, ref dd);
                else if (character.cCombat.attackType == AttackType.heavy_1)
                    DealDamageWithM(leftWeapon.haFirstPhaseDM, ref pd, ref fd, ref md, ref ld, ref dd);
                else if (character.cCombat.attackType == AttackType.heavy_2)
                    DealDamageWithM(leftWeapon.haSecondPhasDM, ref pd, ref fd, ref md, ref ld, ref dd);
            }
            // 处理格挡(若格挡成功，则伤害会被吸收一部分)
            CharacterStatsManager enemyShield = damageTarget.cStats;
            Vector3 directinoFromPlayerToEnemy = character.transform.position - damageTarget.transform.position;
            float dotValueFromPlayerToEnemy = Vector3.Dot(directinoFromPlayerToEnemy, damageTarget.transform.forward);
            bool successfulBlocked = damageTarget.cCombat.isBlocking && dotValueFromPlayerToEnemy > 0.3f;
            if (successfulBlocked)
            {
                damageTarget.cCombat.AttemptBlock(this, damageAnimation, pd, fd, md, ld, dd);

                pd *= (1 - enemyShield.blockingPDA);
                fd *= (1 - enemyShield.blockingFDA);
                md *= (1 - enemyShield.blockingMDA);
                ld *= (1 - enemyShield.blockingLDA);
                dd *= (1 - enemyShield.blockingDDA);

                damageTarget.cStats.TakeDamage(null, pd, fd, md, ld, dd);
            }
            else
            {
                // 判断 机体稳定性 是否被打破(若被打破则播放受伤动画，否则不播放受伤动画)
                damageTarget.cStats.poiseResetTimer = damageTarget.cStats.totalPoiseResetTime;
                damageTarget.cStats.totalPoiseDefence -= poiseBreak;
                if (damageTarget.cStats.totalPoiseDefence > poiseBreak)
                    damageTarget.cStats.TakeDamage(null, pd, fd, md, ld, dd);
                else
                    damageTarget.cStats.TakeDamage(damageAnimation, pd, fd, md, ld, dd);
            }
        }

        void DealDamageWithM(float damageMuiltiplier, ref float pd, ref float fd, ref float md, ref float ld, ref float dd)
        {
            pd *= damageMuiltiplier;
            fd *= damageMuiltiplier;
            md *= damageMuiltiplier;
            ld *= damageMuiltiplier;
            dd *= damageMuiltiplier;
        }

        protected virtual void ChooseWhichDirectionDamageCameFrom(float direction)
        {
            if (direction <= 0)
            {
                // BackwardLeft: [-90f, 0f] 
                if (-90f <= direction)
                    currentDamageAnimation = "Damage_BackwardLeft_01";
                // ForwardLeft: [-180f, -90f)
                else
                    currentDamageAnimation = "Damage_ForwardLeft_01";
            }
            else
            {
                // BackwardRight: (0f, 90f)
                if (direction < 90f)
                    currentDamageAnimation = "Damage_BackwardRight_01";
                // ForwardRight: [90f, 180f] 
                else
                    currentDamageAnimation = "Damage_ForwardRight_01";
            }
        }
    }
}