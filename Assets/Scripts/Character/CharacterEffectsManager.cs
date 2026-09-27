using UnityEngine;

namespace CatchMoon
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Ammo FX")]
        [Tooltip("飞行中的弹药")]
        public GameObject Ammo;

        [Header("Damage FX")]
        public GameObject bloodSplatterFX;

        [Header("Weapon FX")]
        public WeaponFX leftWeaponFX;
        public WeaponFX rightWeaponFX;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        /// <summary>
        /// 播放武器拖尾特效
        /// </summary>
        public virtual void PlayWeaponTrialFX()
        {
            if (character.isUsingLeftHand)
            {
                if (leftWeaponFX != null)
                    leftWeaponFX.PlayWeaponTrialFX();
            }
            else
            {
                if (rightWeaponFX != null)
                    rightWeaponFX.PlayWeaponTrialFX();
            }
        }

        /// <summary>
        /// 播放血溅特效
        /// </summary>
        /// <param name="bloodSplatterLocation"></param>
        public virtual void PlayBloodSplatter(Vector3 bloodSplatterLocation)
        {
            GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
        }
    }
}