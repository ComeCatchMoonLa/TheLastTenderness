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
            base.Start();

            currentItemBeingUsed = rightWeapon;
            if (rightWeapon.weaponType == WeaponType.melee_TH  || rightWeapon.weaponType == WeaponType.melee_THL)
                enemy.isTwoHandingWeapon = true;
        }
    }
}
