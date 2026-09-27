using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Spell/Projectile Spell")]
    public class ProjectileSpell : SpellItem
    {
        [Header("Projectile Damage")]
        public int baseDamage;

        [Header("Projectile Physics")]
        public float projectileForwardVelocity;
        public float projectileUpwardVelocity;
        public float projectileMass;
        public bool isEffectByGravity;
        Rigidbody rigidbody;

        public override void AttemptToCastSpell(CharacterManager character)
        {
            if (character.characterType == CharacterType.player)
                AttempToCastSpellWithPlayer(character as PlayerManager);
            else if (character.characterType == CharacterType.npc)
                AttempToCastSpellWithAI(character as EnemyManager);    
        }
        void AttempToCastSpellWithPlayer(PlayerManager player)
        {
            if (player.pCombat.isUsingSpell) return;

            if (player.pStats.DeductMP(focusPointCost))
            {
                if (player.pCamera.lockOnFlag)
                {
                    Vector3 targetDir = player.pCamera.curLockOnTarget.transform.position - player.transform.position;
                    targetDir.y = 0;
                    player.transform.rotation = Quaternion.LookRotation(targetDir.normalized);
                }

                if (player.isUsingLeftHand)
                {
                    GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, player.pWeaponSlot.leftHandSlot.currentWeaponModel.transform.GetChild(0));
                }
                else
                {
                    GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, player.pWeaponSlot.rightHandSlot.currentWeaponModel.transform.GetChild(0));
                }
                player.pAnimator.PlayTargetAnimation(spellAnimation, true, mirrorAnim: player.isUsingLeftHand);
            }
        }
        void AttempToCastSpellWithAI(EnemyManager enemy)
        {
            if (enemy.eCombat.isUsingSpell) return;

            if (enemy.eStats.DeductMP(focusPointCost))
            {
                Vector3 targetDir = enemy.currentTarget.transform.position - enemy.transform.position;
                targetDir.y = 0;
                enemy.transform.rotation = Quaternion.LookRotation(targetDir.normalized);

                if (enemy.isUsingLeftHand)
                {
                    GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, enemy.eWeaponSlot.leftHandSlot.currentWeaponModel.transform.GetChild(0));
                }
                else
                {
                    GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, enemy.eWeaponSlot.rightHandSlot.currentWeaponModel.transform.GetChild(0));
                }
                enemy.eAnimator.PlayTargetAnimation(spellAnimation, true, mirrorAnim: enemy.isUsingLeftHand);
            }
        }


        public override void SuccessfullyCastSpell(CharacterManager character)
        {
            if (character.characterType == CharacterType.player)
                SuccessfullyCastSpellWithPlayer(character as PlayerManager);
            else if (character.characterType == CharacterType.npc)
                SuccessfullyCastSpellWithAI(character as EnemyManager);
        }
        void SuccessfullyCastSpellWithPlayer(PlayerManager player)
        {
            GameObject instantiatedSpellFx;
            if (player.isUsingLeftHand)
            {
                instantiatedSpellFx = Instantiate(spellCastFX, player.pWeaponSlot.leftHandSlot.transform.position, player.pCamera.cameraPivotTransform.rotation);
            }
            else
            {
                instantiatedSpellFx = Instantiate(spellCastFX, player.pWeaponSlot.rightHandSlot.transform.position, player.pCamera.cameraPivotTransform.rotation);
            }
            SpellDamageCollider spellDC = instantiatedSpellFx.GetComponent<SpellDamageCollider>();

            spellDC.teamID = player.pStats.teamID;
            spellDC.fd = baseDamage;

            if (player.pCamera.curLockOnTarget != null)
            {
                instantiatedSpellFx.transform.LookAt(player.pCamera.curLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFx.transform.rotation = Quaternion.Euler(player.pCamera.cameraPivotTransform.eulerAngles.x, player.transform.eulerAngles.y, 0);
            }

            rigidbody = instantiatedSpellFx.GetComponent<Rigidbody>();
            //spellDamageCollider = instantiatedSpellFx.GetComponent<SpellDamageCollider>();
            rigidbody.AddForce(instantiatedSpellFx.transform.forward * projectileForwardVelocity);
            rigidbody.AddForce(instantiatedSpellFx.transform.up * projectileUpwardVelocity);
            rigidbody.useGravity = isEffectByGravity;
            rigidbody.mass = projectileMass;
            instantiatedSpellFx.transform.parent = null;
        }
        void SuccessfullyCastSpellWithAI(EnemyManager enemy)
        {
            //GameObject instantiatedSpellFx;
            //if (enemy.isUsingLeftHand)
            //{
            //    instantiatedSpellFx = Instantiate(spellCastFX, enemy.eWeaponSlot.leftHandSlot.transform.position, enemy.cameraHandler.cameraPivotTransform.rotation);
            //}
            //else
            //{
            //    instantiatedSpellFx = Instantiate(spellCastFX, enemy.eWeaponSlot.rightHandSlot.transform.position, enemy.cameraHandler.cameraPivotTransform.rotation);
            //}
            //SpellDamageCollider spellDC = instantiatedSpellFx.GetComponent<SpellDamageCollider>();

            //spellDC.teamID = enemy.eStats.teamID;
            //spellDC.fd = baseDamage;

            //if (enemy.cameraHandler.currentLockOnTarget != null)
            //{
            //    instantiatedSpellFx.transform.LookAt(enemy.cameraHandler.currentLockOnTarget.transform);
            //}
            //else
            //{
            //    instantiatedSpellFx.transform.rotation = Quaternion.Euler(enemy.cameraHandler.cameraPivotTransform.eulerAngles.x, enemy.transform.eulerAngles.y, 0);
            //}

            //rigidbody = instantiatedSpellFx.GetComponent<Rigidbody>();
            //spellDamageCollider = instantiatedSpellFx.GetComponent<SpellDamageCollider>();
            //rigidbody.AddForce(instantiatedSpellFx.transform.forward * projectileForwardVelocity);
            //rigidbody.AddForce(instantiatedSpellFx.transform.up * projectileUpwardVelocity);
            //rigidbody.useGravity = isEffectByGravity;
            //rigidbody.mass = projectileMass;
            //instantiatedSpellFx.transform.parent = null;
        }
    }
}