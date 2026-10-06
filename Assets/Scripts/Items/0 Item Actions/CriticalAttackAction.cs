using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Critical Attack Action")]
    public class CriticalAttackAction : WeaponItemAction
    {
        public enum CriticalStrikeChoice
        {
            Backstab,
            Riposte,
            LightAttack
        }

        public static CriticalStrikeChoice Choose(bool hit, float dot, bool canBeRiposted)
        {
            if (hit && canBeRiposted && dot >= 0.8f && dot <= 1f)
                return CriticalStrikeChoice.Riposte;
            if (hit && dot >= -1f && dot <= -0.8f)
                return CriticalStrikeChoice.Backstab;
            return CriticalStrikeChoice.LightAttack;
        }

        public override void PerformAction(CharacterManager character)
        {
            if (character.cStats.isInvulnerable) return;

            bool hit = false;
            float dot = 0f;
            bool canBeRiposted = false;
            CharacterManager attackTarget = null;
            Ray ray = new(character.cCombat.criticalAttackRayCastStartPoint.transform.position, character.transform.TransformDirection(Vector3.forward));
            if (Physics.Raycast(ray, out RaycastHit rayHit, character.cCombat.criticalAttackRange, LayerMask.player | LayerMask.npc))
            {
                attackTarget = rayHit.transform.GetComponent<CharacterManager>();
                if (attackTarget != null)
                {
                    hit = true;
                    Vector3 dirFormCharacterToEnemy = character.transform.position - attackTarget.transform.position;
                    dirFormCharacterToEnemy.y = 0;
                    dot = Vector3.Dot(dirFormCharacterToEnemy.normalized, attackTarget.transform.forward);
                    canBeRiposted = attackTarget.canBeRiposted;
                }
            }

            switch (Choose(hit, dot, canBeRiposted))
            {
                case CriticalStrikeChoice.Riposte:
                    AttempRiposte(character, attackTarget);
                    attackTarget.canBeRiposted = false;
                    break;
                case CriticalStrikeChoice.Backstab:
                    AttempBackStab(character, attackTarget, dot);
                    break;
                default:
                    PlayLightAttack(character);
                    break;
            }
        }

        void PlayLightAttack(CharacterManager character)
        {
            WeaponItem weapon = character.cInventory != null ? character.cInventory.rightWeapon : null;
            if (weapon == null || weapon.oh_tap_e_action == null || weapon.oh_tap_e_action == this)
            {
                string name = weapon != null ? weapon.itemName : character.transform.name;
                Debug.LogError($"{name}: oh_tap_e_action");
                return;
            }

            weapon.oh_tap_e_action.PerformAction(character);
        }

        void AttempRiposte(CharacterManager character, CharacterManager attackTarget)
        {
            if (character.isPerformingRiposted || attackTarget.isBeingRiposted) return;

            character.isPerformingRiposted = true;
            character.cStats.isInvulnerable = true;
            character.cAnimator.PlayTargetAnimation("Riposte", true, new AnimationOptions { Mirror = true });

            float dist = character.cCollider.radius + attackTarget.cCollider.radius;
            attackTarget.cCombat.GetRiposte(character, dist);

        }

        void AttempBackStab(CharacterManager character, CharacterManager attackTarget, float dot)
        {
            bool already = character.isPerformingBackStabbed || attackTarget.isBeingBackStabbed;
            if (!BackstabLock.TryEnter(true, dot, already, out _)) return;

            character.isPerformingBackStabbed = true;
            character.cStats.isInvulnerable = true;
            character.cAnimator.PlayTargetAnimation("Back Stab", true, new AnimationOptions { Mirror = true });

            float dist = character.cCollider.radius + attackTarget.cCollider.radius;
            attackTarget.cCombat.GetBackStabbed(character,dist);
        }
    }
}