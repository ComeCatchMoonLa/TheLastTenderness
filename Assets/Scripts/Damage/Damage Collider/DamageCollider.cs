using System.Collections.Generic;
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
        readonly List<CharacterManager> hitThisWindow = new List<CharacterManager>();

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
            hitThisWindow.Clear();
            damageCollider.enabled = true;
        }

        /// <summary>
        /// 禁用伤害触发器
        /// </summary>
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            hitThisWindow.Clear();
        }

        /// <summary>
        /// 进入触发器
        /// </summary>
        /// <param name="collision">触发器捕获的对象</param>
        protected virtual void OnTriggerEnter(Collider collision)
        {
            IllusionWall wall = collision.GetComponent<IllusionWall>();
            if (wall != null)
            {
                wall.Strike();
                return;
            }

            if (collision.tag == "Player" || collision.tag == "Enemy") // 攻击角色
            {
                CharacterManager damageTarget = collision.GetComponent<CharacterManager>();
                if (damageTarget == null)
                {
                    Debug.LogError("PlayerManager is null.");
                    return;
                }

                if (character == damageTarget || damageTarget.cStats.teamID == teamID) return;
                if (hitThisWindow.Contains(damageTarget)) return;
                hitThisWindow.Add(damageTarget);

                int parryElapsed = Time.frameCount - damageTarget.cCombat.parryOpenedFrame;
                bool parryOpen = damageTarget.cCombat.isParrying
                    && ParryWindow.Open(parryElapsed, damageTarget.cCombat.parryWindowFrames, damageTarget.name);
                if (parryOpen)
                {
                    character.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Parried", true);
                    character.canBeRiposted = true;
                }
                // 举着盾。正面才传盾击；侧面和背后传方向受击，破韧时才播。
                else if (damageTarget.cCombat.isBlocking)
                {
                    if (HitComesFromFront(damageTarget))
                        DealDamage(damageTarget, "Block - Hit", pd, fd, md, ld, dd);
                    else
                    {
                        float directionHitFrom = Vector3.SignedAngle(character.transform.forward, damageTarget.transform.forward, Vector3.up);
                        ChooseWhichDirectionDamageCameFrom(directionHitFrom);
                        DealDamage(damageTarget, currentDamageAnimation, pd, fd, md, ld, dd);
                    }
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
            float multiplier = PhaseDamageMultiplier(character.isUsingRightHand, character.isUsingLeftHand,
                character.cInventory.rightWeapon, character.cInventory.leftWeapon, character.cCombat.attackType);

            DamageSegments segments = WeaponBuff.Add(new DamageSegments
            {
                Physical = pd,
                Fire = fd,
                Magic = md,
                Lightning = ld,
                Dark = dd
            }, character.cCombat.weaponBuff);
            damageTarget.cCombat.ResolveIncomingHit(character, damageAnimation, new IncomingDamage
            {
                Multiplier = multiplier,
                Segments = segments
            }, true, poiseBreak, guardBreakModifider);
        }

        public static float PhaseDamageMultiplier(bool usingRightHand, bool usingLeftHand, WeaponItem rightWeapon, WeaponItem leftWeapon, AttackType attackType)
        {
            if (!usingRightHand && !usingLeftHand)
                return 1f;

            WeaponItem weapon = usingRightHand ? rightWeapon : leftWeapon;
            if (attackType == AttackType.light_1)
                return weapon.laFirstPhaseDM;
            if (attackType == AttackType.light_2)
                return weapon.laSecondPhaseDM;
            if (attackType == AttackType.heavy_1)
                return weapon.haFirstPhaseDM;
            if (attackType == AttackType.heavy_2)
                return weapon.haSecondPhasDM;
            return 1f;
        }

        bool HitComesFromFront(CharacterManager damageTarget)
        {
            return CharacterCombatManager.ComesFromFront(character.transform.position, damageTarget.transform.position, damageTarget.transform.forward);
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