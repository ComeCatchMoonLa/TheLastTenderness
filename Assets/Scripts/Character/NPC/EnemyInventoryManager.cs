using UnityEngine;

namespace CatchMoon
{
    public class EnemyInventoryManager : CharacterInventoryManager
    {
        EnemyManager enemy;

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<EnemyManager>();
        }
        protected override void Start()
        {
            WeaponItem startingRightWeapon = weaponsInRightHandSlot[currentRightWeaponIdx];
            if (startingRightWeapon != null &&
                (startingRightWeapon.weaponType == WeaponType.melee_TH || startingRightWeapon.weaponType == WeaponType.melee_THL))
                enemy.isTwoHandingWeapon = true;

            base.Start();

            currentItemBeingUsed = rightWeapon;
        }
    }
}
