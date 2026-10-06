using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item Actions/Weapon Item Actions/Shoot Arrow Action")]
    public class ShootArrowAction : WeaponItemAction
    {
        public override void PerformAction(CharacterManager character)
        {
            if (character.characterType == CharacterType.player)
                PerformActionForPlayer(character as PlayerManager);
            else if (character.characterType == CharacterType.npc)
                PerformActionForAI(character as EnemyManager);   
        }
        
        void PerformActionForPlayer(PlayerManager player)
        {
            if (!player.aimingMode || player.pStats.currentStamina <= 0)
                return;

            if (!player.pInventory.TrySpendAmmo(player.pInventory.currentAmmo))
            {
                player.pInventory.currentAmmo = null;
                return;
            }

            // 播放射箭动画
            player.animator.Play("Shoot Arrow");

            // 销毁loadModel
            Destroy(player.pEffects.Ammo);
            player.pEffects.Ammo = null;

            // 生成liveModel
            GameObject liveArrow = Instantiate(player.pInventory.currentAmmo.liveItemModel, player.pWeaponSlot.rightHandSlot.overrideParentWhileHolding);
            liveArrow.transform.localPosition = Vector3.zero;

            Ray ray = player.pCamera.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hitPoint;
            if (Physics.Raycast(ray, out hitPoint, 100f, ~(LayerMask.characterCollisionBlocker | LayerMask.player), QueryTriggerInteraction.Ignore))
            {
                liveArrow.transform.LookAt(hitPoint.point);
                //Debug.Log(hitPoint.transform.name);
            }
            else
            {
                liveArrow.transform.rotation = Quaternion.Euler(player.pCamera.cameraPivotTransform.localEulerAngles.x, player.transform.eulerAngles.y, 0);
            }
            liveArrow.transform.parent = null;

            Rigidbody rigidBody = liveArrow.GetComponentInChildren<Rigidbody>();
            rigidBody.AddForce(liveArrow.transform.forward * player.cInventory.currentAmmo.forwardVeclocity);
            rigidBody.useGravity = player.pInventory.currentAmmo.useGravity;
            rigidBody.mass = player.pInventory.currentAmmo.mass;

            AmmoDamageCollider arrowDamageCollider = liveArrow.GetComponentInChildren<AmmoDamageCollider>();
            arrowDamageCollider.character = player;
            arrowDamageCollider.teamID = player.pStats.teamID;
            arrowDamageCollider.ammoItem = player.pInventory.currentAmmo;
            AmmoItem ammo = player.pInventory.currentAmmo;
            bool fullDraw = BowShot.FullDraw(ammo.name, ammo.fullDrawTimeFilled, ammo.fullDrawTime, player.aimHeld);
            arrowDamageCollider.pd = BowShot.Damage(ammo.name, fullDraw, ammo.physicalDamage, ammo.fullDrawDamageFilled, ammo.fullDrawDamage);

            if (player.pInventory.AmmoRemaining(player.pInventory.currentAmmo) == 0)
                player.pInventory.currentAmmo = null;
        }

        void PerformActionForAI(EnemyManager enemy)
        {
            if (!enemy.eCombat.isAiming || enemy.eStats.currentStamina <= 0)
                return;

            // 播放射箭动画
            enemy.animator.Play("Shoot Arrow");

            // 销毁loadModel
            Destroy(enemy.eEffects.Ammo);
            enemy.eEffects.Ammo = null;

            // 生成liveModel
            GameObject liveArrow = Instantiate(enemy.eInventory.currentAmmo.liveItemModel, enemy.eWeaponSlot.rightHandSlot.overrideParentWhileHolding);
            liveArrow.transform.localPosition = Vector3.zero;
            liveArrow.transform.LookAt(enemy.currentTarget.lockOnTransform);
            liveArrow.transform.parent = null;

            Rigidbody rigidBody = liveArrow.GetComponentInChildren<Rigidbody>();
            rigidBody.AddForce(liveArrow.transform.forward * enemy.cInventory.currentAmmo.forwardVeclocity);
            rigidBody.useGravity = enemy.eInventory.currentAmmo.useGravity;
            rigidBody.mass = enemy.eInventory.currentAmmo.mass;

            AmmoDamageCollider arrowDamageCollider = liveArrow.GetComponentInChildren<AmmoDamageCollider>();
            arrowDamageCollider.character = enemy;
            arrowDamageCollider.teamID = enemy.eStats.teamID;
            arrowDamageCollider.ammoItem = enemy.eInventory.currentAmmo;

            arrowDamageCollider.pd = enemy.eInventory.currentAmmo.physicalDamage;
        }
    }
}