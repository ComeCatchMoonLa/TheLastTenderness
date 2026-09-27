using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        CharacterManager character;
        AudioSource audioSource;

        // 攻击音效
        // 受伤音效 受伤语音

        [Header("人物受伤的音效")]
        public AudioClip[] takingDamageSounds;
        List<AudioClip> potentialDamageSounds;
        AudioClip lastDamageSoundPlayed;

        [Header("武器挥动的音效")]
        List<AudioClip> potentialWeaponWhooshSounds;
        AudioClip lastWeaponWhooshPlayed;
        // 脚步音效

        protected void Awake()
        {
            character = GetComponent<CharacterManager>();

            audioSource = GetComponent<AudioSource>();
        }

        public virtual void PlayRandomDamageSoundsFX()
        {
            if (takingDamageSounds.Length == 0) return;
            else if (takingDamageSounds.Length == 1)
            {
                audioSource.PlayOneShot(takingDamageSounds[0]);
            }
            else
            {
                potentialDamageSounds = new List<AudioClip>();

                foreach (AudioClip sound in takingDamageSounds)
                    if (sound != lastDamageSoundPlayed)
                        potentialDamageSounds.Add(sound);

                int randomValue = Random.Range(0, potentialDamageSounds.Count);
                lastDamageSoundPlayed = potentialDamageSounds[randomValue];
                audioSource.PlayOneShot(lastDamageSoundPlayed, 0.4f);
            }
        }

        public virtual void PlayRandomWeaponWhoosh()
        {
            AudioClip[] weapWhooshSFX;
            if (character.isUsingRightHand)
                weapWhooshSFX = character.cInventory.rightWeapon.weaponWhooshesSound;
            else
                weapWhooshSFX = character.cInventory.leftWeapon.weaponWhooshesSound;

            if (weapWhooshSFX.Length == 0) return;
            else if (weapWhooshSFX.Length == 1)
            {
                audioSource.PlayOneShot(weapWhooshSFX[0]);
            }
            else
            {
                potentialWeaponWhooshSounds = new List<AudioClip>();

                foreach (AudioClip sound in weapWhooshSFX)
                    if (sound != lastWeaponWhooshPlayed)
                        potentialWeaponWhooshSounds.Add(sound);

                int random = Random.Range(0, potentialWeaponWhooshSounds.Count);
                lastWeaponWhooshPlayed = potentialWeaponWhooshSounds[random];
                audioSource.PlayOneShot(lastWeaponWhooshPlayed);
            }
        }
    }
}