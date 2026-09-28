using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Consumable/Bomb Item")]
    public class BombItem : ConsumableItem
    {
        [Header("Velocity")]
        public int upwardVelocity = 300;
        public int forwardVelocity = 500;
        public int bombMass = 1;

        [Header("Live Bomb Model")]
        public GameObject liveBombModel;

        [Header("Base Damage")]
        public int baseDamage = 10;
        public int explosiveDamage = 2;

        public override void AttemptToConsumableItem(PlayerManager player)
        {
            if (player.pCombat.isUsingConsumable) return;
            
            if (player.pInventory.TrySpendConsumable(this))
            {
                player.pInventory.consumableBeingUsed = this;

                if (player.pCamera.lockOnFlag)
                {
                    Vector3 targetDir = player.pCamera.curLockOnTarget.transform.position - player.transform.position;
                    targetDir.y = 0;
                    player.transform.rotation = Quaternion.LookRotation(targetDir.normalized);
                }

                player.pAnimator.PlayTargetAnimation(consumeAnimation, true);
                player.pWeaponSlot.rightHandSlot.UnloadWeapon();
                GameObject bombModel = Instantiate(itemModel, player.pWeaponSlot.rightHandSlot.overrideParentWhileHolding);
                player.pEffects.instantiatedFXModel = bombModel;
            }
            else
            {
                player.pAnimator.PlayTargetAnimation("Shrug", true);
            }
        }
        public override void SucessfullyUsedConsumable(PlayerManager player)
        {
            Destroy(player.pEffects.instantiatedFXModel.gameObject);
            player.pEffects.instantiatedFXModel = null;
            GameObject activeModelBomb = Instantiate(liveBombModel, player.pWeaponSlot.rightHandSlot.transform.position, player.pCamera.cameraPivotTransform.rotation);
            if (player.pCamera.curLockOnTarget != null)
                activeModelBomb.transform.LookAt(player.pCamera.curLockOnTarget.transform);
            else
                activeModelBomb.transform.rotation = Quaternion.Euler(player.pCamera.cameraPivotTransform.eulerAngles.x, player.transform.eulerAngles.y, 0);

            BombDamageCollider bombDC = activeModelBomb.GetComponentInChildren<BombDamageCollider>();

            bombDC.teamID = player.pStats.teamID;
            bombDC.explosionDamage = baseDamage;
            bombDC.explosionSplashDamage = explosiveDamage;
            bombDC.bombRigidBody.AddForce(activeModelBomb.transform.forward * forwardVelocity);
            bombDC.bombRigidBody.AddForce(activeModelBomb.transform.up * upwardVelocity);

            player.pWeaponSlot.LoadWeaponOnSlot(player.pInventory.rightWeapon, false);
        }
    }
}