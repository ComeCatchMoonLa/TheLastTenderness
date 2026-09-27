using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Critical Attack Action")]
    public class CriticalAttackAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.cStats.isInvulnerable) return;

            Ray ray = new(character.cCombat.criticalAttackRayCastStartPoint.transform.position, character.transform.TransformDirection(Vector3.forward));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, character.cCombat.criticalAttackRange, LayerMask.player | LayerMask.npc))
            {
                CharacterManager attackTarget = hit.transform.GetComponent<CharacterManager>();
                Vector3 dirFormCharacterToEnemy = character.transform.position - attackTarget.transform.position;
                dirFormCharacterToEnemy.y = 0;
                float dot = Vector3.Dot(dirFormCharacterToEnemy.normalized, attackTarget.transform.forward);

                //Debug.Log($"Dot: {dot:N1}");

                if (attackTarget.canBeRiposted)
                {
                    if (0.8f <= dot && dot <= 1f)
                    {
                        AttempRiposte(character, attackTarget);
                        attackTarget.canBeRiposted = false;
                    }
                }

                if (-1f <= dot && dot <= -0.8f)
                {
                    AttempBackStab(character, attackTarget);
                }
            }
        }

        void AttempRiposte(CharacterManager character, CharacterManager attackTarget)
        {
            if (character.isPerformingRiposted || attackTarget.isBeingRiposted) return;

            character.isPerformingRiposted = true;
            character.cStats.isInvulnerable = true;
            character.cAnimator.PlayTargetAnimation("Riposte", true, false, true);

            float dist = character.cCollider.radius + attackTarget.cCollider.radius;
            attackTarget.cCombat.GetRiposte(character, dist);

        }

        void AttempBackStab(CharacterManager character, CharacterManager attackTarget)
        {
            if (character.isPerformingBackSttbbed || attackTarget.isBeingBackStabbed) return;

            character.isPerformingBackSttbbed = true;
            character.cStats.isInvulnerable = true;
            character.cAnimator.PlayTargetAnimation("Back Stab", true, false, true);

            float dist = character.cCollider.radius + attackTarget.cCollider.radius;
            attackTarget.cCombat.GetBackStabbed(character,dist);
        }
    }
}