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
        public float pendingCriticalDamage;

        [Header("Transforms")]
        public Transform criticalAttackRayCastStartPoint;

        [Header("Flags")]
        public bool isAttacking;                // 正在攻击
        public bool isParrying;                 // 正在弹反
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
            {
                character.cStats.blockingPDA = character.cInventory.rightWeapon.physicalDA;
                character.cStats.blockingFDA = character.cInventory.rightWeapon.fireDA;
                character.cStats.blockingMDA = character.cInventory.rightWeapon.magicDA;
                character.cStats.blockingLDA = character.cInventory.rightWeapon.lightningDA;
                character.cStats.blockingDDA = character.cInventory.rightWeapon.darkDA;

                character.cStats.blockingStabilityRating = character.cInventory.rightWeapon.blockingStabilityRating;
            }
            else if (character.isUsingLeftHand)
            {
                character.cStats.blockingPDA = character.cInventory.leftWeapon.physicalDA;
                character.cStats.blockingFDA = character.cInventory.leftWeapon.fireDA;
                character.cStats.blockingMDA = character.cInventory.leftWeapon.magicDA;
                character.cStats.blockingLDA = character.cInventory.leftWeapon.lightningDA;
                character.cStats.blockingDDA = character.cInventory.leftWeapon.darkDA;

                character.cStats.blockingStabilityRating = character.cInventory.leftWeapon.blockingStabilityRating;
            }
        }
        public virtual void ResetBlockingAbsorption()
        {
            if (character.isUsingRightHand)
            {
                character.cStats.blockingPDA = 0;
                character.cStats.blockingFDA = 0;
                character.cStats.blockingMDA = 0;
                character.cStats.blockingLDA = 0;
                character.cStats.blockingDDA = 0;

                character.cStats.blockingStabilityRating = 0;
            }
            else if (character.isUsingLeftHand)
            {
                character.cStats.blockingPDA = 0;
                character.cStats.blockingFDA = 0;
                character.cStats.blockingMDA = 0;
                character.cStats.blockingLDA = 0;
                character.cStats.blockingDDA = 0;

                character.cStats.blockingStabilityRating = 0;
            }
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

        public void ResolveIncomingHit(
            CharacterManager attacker,
            string damageAnimation,
            float multiplier,
            bool applyBlockAndPoise,
            float poiseDamage,
            float guardBreakModifider,
            float pd, float fd, float md, float ld, float dd)
        {
            pd *= multiplier;
            fd *= multiplier;
            md *= multiplier;
            ld *= multiplier;
            dd *= multiplier;

            if (!applyBlockAndPoise)
            {
                RecordIncomingHit(false, false, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(damageAnimation, pd, fd, md, ld, dd);
                return;
            }

            bool successfulBlocked = false;
            if (attacker != null && character.cCombat.isBlocking)
            {
                Vector3 directionFromAttackerToTarget = attacker.transform.position - character.transform.position;
                if (directionFromAttackerToTarget.sqrMagnitude > 0.0001f)
                {
                    float dotValue = Vector3.Dot(directionFromAttackerToTarget.normalized, character.transform.forward);
                    successfulBlocked = dotValue > 0.3f;
                }
            }

            if (successfulBlocked)
            {
                AttemptBlock(guardBreakModifider, damageAnimation, pd, fd, md, ld, dd);
                pd *= (1 - character.cStats.blockingPDA);
                fd *= (1 - character.cStats.blockingFDA);
                md *= (1 - character.cStats.blockingMDA);
                ld *= (1 - character.cStats.blockingLDA);
                dd *= (1 - character.cStats.blockingDDA);
                RecordIncomingHit(true, false, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(null, pd, fd, md, ld, dd, playHurtSound: character.cStats.currentStamina <= 0);
                return;
            }

            character.cStats.poiseResetTimer = character.cStats.totalPoiseResetTime;
            character.cStats.totalPoiseDefence -= poiseDamage;
            if (character.cStats.totalPoiseDefence > poiseDamage)
            {
                RecordIncomingHit(false, false, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(null, pd, fd, md, ld, dd);
            }
            else
            {
                RecordIncomingHit(false, true, pd, fd, md, ld, dd);
                character.cStats.TakeDamage(damageAnimation, pd, fd, md, ld, dd);
            }
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
            character.cCombat.ResolveIncomingHit(characterPerformingBackStab, "Back Stabbed", weapon.criticalAttackDM,
                false, 0f, 0f, weapon.pd, weapon.fd, weapon.md, weapon.ld, weapon.dd);
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
            character.cCombat.ResolveIncomingHit(characterPerformingRiposte, "Riposted", weapon.criticalAttackDM,
                false, 0f, 0f, weapon.pd, weapon.fd, weapon.md, weapon.ld, weapon.dd);
        }
    }
}