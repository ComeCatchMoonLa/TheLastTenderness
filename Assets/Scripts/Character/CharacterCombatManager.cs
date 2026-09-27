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

                character.cStats.blockingStabilityRating = character.cInventory.rightWeapon.blockingStabilityRating;
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

        public virtual void AttemptBlock(DamageCollider attackingWeapon, string blockAnimation, float pd, float fd, float md, float ld, float dd)
        {
            float staminaDA = (pd + fd + md + ld + dd) * attackingWeapon.guardBreakModifider * (1 - character.cStats.blockingStabilityRating);

            character.cStats.DeductStamina(staminaDA);

            if (character.cStats.currentStamina <= 0)
            {
                character.cCombat.isBlocking = false;
                character.cCombat.ResetBlockingAbsorption();
                character.cAnimator.PlayTargetAnimation("Guard_Break_01", true);
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

        public void GetBackStabbed(CharacterManager characterPerformingBackStab, float dist)
        {
            // 0.标记状态
            character.isBeingBackStabbed = true;
            // 1.调整位置与角度
            StartCoroutine(ForceMoveCharacterToEnemyBackStabPosition(characterPerformingBackStab, dist));
            // 2.处理受伤
            WeaponItem weapon = character.cInventory.rightWeapon;
            character.cStats.TakeDamage("Back Stabbed", weapon.pd * weapon.criticalAttackDM, weapon.fd * weapon.criticalAttackDM,
                weapon.md * weapon.criticalAttackDM, weapon.ld * weapon.criticalAttackDM, weapon.dd * weapon.criticalAttackDM);
        }

        public void GetRiposte(CharacterManager characterPerformingRiposte, float dist)
        {
            // 0.标记状态
            character.isBeingRiposted = true;
            // 1.调整位置与角度
            StartCoroutine(ForceMoveCharacterToEnemyRipostePosition(characterPerformingRiposte, dist));
            // 2.处理受伤
            WeaponItem weapon = character.cInventory.rightWeapon;
            character.cStats.TakeDamage("Riposted", weapon.pd * weapon.criticalAttackDM, weapon.fd * weapon.criticalAttackDM,
                weapon.md * weapon.criticalAttackDM, weapon.ld * weapon.criticalAttackDM, weapon.dd * weapon.criticalAttackDM);
        }
    }
}