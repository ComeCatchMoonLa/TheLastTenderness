using System.Collections;
using UnityEngine;

namespace CatchMoon
{
    public class CharacterCombatManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("当前攻击类型")]
        public AttackType attackType;

        [Header("背刺与弹反的判定范围")]
        public float criticalAttackRange = 0.9f;

        [Header("Transforms")]
        public Transform criticalAttackRayCastStartPoint;

        [Header("Flags")]
        public bool isAttacking;                // 正在攻击
        public bool isParrying;                 // 正在弹反
        public int parryWindowFrames;
        public int parryOpenedFrame;
        public bool isUsingSpell;               // 正在使用法术
        public bool isUsingConsumable;          // 正在使用消耗品
        public bool isBlocking;                 // 正在防御
        public bool isAiming;                   // 正在瞄准
        public bool lastHitWasBlocked;
        public bool lastHitBrokePoise;
        public float lastHitDamageTotal;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void SetBlockingAbsorptionFromBlockingWeapon()
        {
            if (character.isUsingRightHand)
                CopyBlockingAbsorption(character.cStats, character.cInventory.rightWeapon);
            else if (character.isUsingLeftHand)
                CopyBlockingAbsorption(character.cStats, character.cInventory.leftWeapon);
        }
        public virtual void ResetBlockingAbsorption()
        {
            if (character.isUsingRightHand)
                ClearBlockingAbsorption(character.cStats);
            else if (character.isUsingLeftHand)
                ClearBlockingAbsorption(character.cStats);
        }
        public static void CopyBlockingAbsorption(CharacterStatsManager stats, WeaponItem weapon)
        {
            stats.blockingPDA = weapon.physicalDA;
            stats.blockingFDA = weapon.fireDA;
            stats.blockingMDA = weapon.magicDA;
            stats.blockingLDA = weapon.lightningDA;
            stats.blockingDDA = weapon.darkDA;
            stats.blockingStabilityRating = weapon.blockingStabilityRating;
        }
        public static void ClearBlockingAbsorption(CharacterStatsManager stats)
        {
            stats.blockingPDA = 0;
            stats.blockingFDA = 0;
            stats.blockingMDA = 0;
            stats.blockingLDA = 0;
            stats.blockingDDA = 0;
            stats.blockingStabilityRating = 0;
        }

        float BlockingWeaponStability()
        {
            if (character.cInventory == null)
                return 0f;

            WeaponItem blockingWeapon = null;
            if (character.isUsingRightHand)
                blockingWeapon = character.cInventory.rightWeapon;
            else if (character.isUsingLeftHand)
                blockingWeapon = character.cInventory.leftWeapon;

            if (blockingWeapon == null)
                return 0f;
            return blockingWeapon.stability;
        }

        public virtual void AttemptBlock(float guardBreakModifider, string blockAnimation, float pd, float fd, float md, float ld, float dd)
        {
            float staminaDA = (pd + fd + md + ld + dd) * guardBreakModifider * (1 - character.cStats.blockingStabilityRating);
            staminaDA *= 1f - BlockingWeaponStability();

            if (!character.cStats.DeductStamina(staminaDA))
                character.cStats.EmptyStamina();

            if (character.cStats.currentStamina <= 0)
            {
                character.cCombat.isBlocking = false;
                character.cCombat.ResetBlockingAbsorption();
                character.cAnimator.PlayTargetAnimation("Guard_Break", true);
                character.canBeRiposted = GuardBreak.OpensRiposte(character.cStats.currentStamina);
            }
            else
            {
                character.cAnimator.PlayTargetAnimation(blockAnimation, true);
            }
        }

        IEnumerator ForceMoveCharacterToEnemyBackStabPosition(CharacterManager characterPerformingBackStab, float dist)
        {
            for (float timer = 0.05f; timer < 0.5f; timer += 0.05f)
            {
                transform.rotation = Quaternion.LookRotation(characterPerformingBackStab.transform.forward);
                transform.parent = characterPerformingBackStab.transform;
                transform.localPosition = Vector3.forward * dist;
                transform.parent = character.parentTransform;
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator ForceMoveCharacterToEnemyRipostePosition(CharacterManager characterPerformingRiposte, float dist)
        {
            for (float timer = 0.05f; timer < 0.5f; timer += 0.05f)
            {
                transform.rotation = Quaternion.LookRotation(-characterPerformingRiposte.transform.forward);
                transform.parent = characterPerformingRiposte.transform;
                transform.localPosition = Vector3.forward * dist;
                transform.parent = character.parentTransform;
                yield return new WaitForSeconds(0.05f);
            }
        }

        public static bool ComesFromFront(Vector3 attackerPosition, Vector3 defenderPosition, Vector3 defenderForward)
        {
            Vector3 fromDefenderToAttacker = attackerPosition - defenderPosition;
            if (fromDefenderToAttacker.sqrMagnitude <= 0.0001f)
                return false;

            return Vector3.Dot(fromDefenderToAttacker.normalized, defenderForward) > 0.3f;
        }

        public void ResolveIncomingHit(
            CharacterManager attacker,
            string damageAnimation,
            IncomingDamage incoming,
            bool applyBlockAndPoise,
            float poiseDamage,
            float guardBreakModifider)
        {
            try
            {
            float pd = incoming.Segments.Physical * incoming.Multiplier;
            float fd = incoming.Segments.Fire * incoming.Multiplier;
            float md = incoming.Segments.Magic * incoming.Multiplier;
            float ld = incoming.Segments.Lightning * incoming.Multiplier;
            float dd = incoming.Segments.Dark * incoming.Multiplier;

            if (!applyBlockAndPoise)
            {
                RecordIncomingHit(false, false, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(damageAnimation, DamageOf(pd, fd, md, ld, dd));
                return;
            }

            bool successfulBlocked = false;
            if (attacker != null && character.cCombat.isBlocking)
                successfulBlocked = ComesFromFront(attacker.transform.position, character.transform.position, character.transform.forward);

            if (successfulBlocked)
            {
                AttemptBlock(guardBreakModifider, damageAnimation, pd, fd, md, ld, dd);
                pd *= (1 - character.cStats.blockingPDA);
                fd *= (1 - character.cStats.blockingFDA);
                md *= (1 - character.cStats.blockingMDA);
                ld *= (1 - character.cStats.blockingLDA);
                dd *= (1 - character.cStats.blockingDDA);
                RecordIncomingHit(true, false, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(null, DamageOf(pd, fd, md, ld, dd), playHurtSound: character.cStats.currentStamina <= 0);
                return;
            }

            if (character.cStats.attackPoiseActive)
            {
                bool holds = AttackPoise.Holds(character.cStats.attackPoise, poiseDamage, out float nextPoise);
                character.cStats.attackPoise = nextPoise;
                if (holds)
                {
                    RecordIncomingHit(false, false, pd, fd, md, ld, dd);
                    character.cStats.TakeDamage(null, DamageOf(pd, fd, md, ld, dd));
                    return;
                }
                character.cStats.attackPoiseActive = false;
                character.cStats.totalPoiseDefence = character.cStats.armorPoiseBonus;
                RecordIncomingHit(false, true, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(damageAnimation, DamageOf(pd, fd, md, ld, dd));
                return;
            }

            character.cStats.poiseResetTimer = character.cStats.totalPoiseResetTime;
            character.cStats.totalPoiseDefence -= poiseDamage;
            if (character.cStats.totalPoiseDefence > poiseDamage)
            {
                RecordIncomingHit(false, false, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(null, DamageOf(pd, fd, md, ld, dd));
            }
            else
            {
                RecordIncomingHit(false, true, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(damageAnimation, DamageOf(pd, fd, md, ld, dd));
            }
            }
            finally
            {
                if (character is EnemyManager hitEnemy)
                    hitEnemy.BreakReturnIfStillInRange();
            }
        }

        static IncomingDamage WeaponIncoming(WeaponItem weapon)
        {
            return new IncomingDamage
            {
                Multiplier = weapon.criticalAttackDM,
                Segments = DamageOf(weapon.pd, weapon.fd, weapon.md, weapon.ld, weapon.dd)
            };
        }

        static DamageSegments DamageOf(float pd, float fd, float md, float ld, float dd)
        {
            return new DamageSegments
            {
                Physical = pd,
                Fire = fd,
                Magic = md,
                Lightning = ld,
                Dark = dd
            };
        }

        void RecordIncomingHit(bool blocked, bool brokePoise, float pd, float fd, float md, float ld, float dd)
        {
            lastHitWasBlocked = blocked;
            lastHitBrokePoise = brokePoise;
            lastHitDamageTotal = pd + fd + md + ld + dd;
        }

        public void GetBackStabbed(CharacterManager characterPerformingBackStab, float dist)
        {
            // 0.标记状态
            character.isBeingBackStabbed = true;
            // 1.调整位置与角度
            StartCoroutine(ForceMoveCharacterToEnemyBackStabPosition(characterPerformingBackStab, dist));
            // 2.处理受伤
            WeaponItem weapon = characterPerformingBackStab.cInventory.rightWeapon;
            if (weapon == null) return;
            character.cCombat.ResolveIncomingHit(characterPerformingBackStab, "Back Stabbed", WeaponIncoming(weapon),
                false, 0f, 0f);
        }

        public void GetRiposte(CharacterManager characterPerformingRiposte, float dist)
        {
            // 0.标记状态
            character.isBeingRiposted = true;
            // 1.调整位置与角度
            StartCoroutine(ForceMoveCharacterToEnemyRipostePosition(characterPerformingRiposte, dist));
            // 2.处理受伤
            WeaponItem weapon = characterPerformingRiposte.cInventory.rightWeapon;
            if (weapon == null) return;
            character.cCombat.ResolveIncomingHit(characterPerformingRiposte, "Riposted", WeaponIncoming(weapon),
                false, 0f, 0f);
        }
    }
}