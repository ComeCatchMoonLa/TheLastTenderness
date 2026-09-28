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
        AudioClip lastDamageSoundPlayed;

        [Header("武器挥动的音效")]
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
                int remaining = CountClipsOtherThan(takingDamageSounds, lastDamageSoundPlayed);
                int index = remaining <= 1 ? 0 : Random.Range(0, remaining);
                lastDamageSoundPlayed = PickClipAvoidingPrevious(takingDamageSounds, lastDamageSoundPlayed, index);
                audioSource.PlayOneShot(lastDamageSoundPlayed, 0.4f);
            }
        }

        public virtual void PlayRandomWeaponWhoosh()
        {
            AudioClip[] weapWhooshSFX;
            if (character.isUsingRightHand)
                weapWhooshSFX = character.cInventory.rightWeapon.weaponWhooshesSound;
            else if (character.isUsingLeftHand)
                weapWhooshSFX = character.cInventory.leftWeapon.weaponWhooshesSound;
            else
                return;

            if (weapWhooshSFX.Length == 0) return;
            else if (weapWhooshSFX.Length == 1)
            {
                audioSource.PlayOneShot(weapWhooshSFX[0]);
            }
            else
            {
                int remaining = CountClipsOtherThan(weapWhooshSFX, lastWeaponWhooshPlayed);
                int index = remaining <= 1 ? 0 : Random.Range(0, remaining);
                lastWeaponWhooshPlayed = PickClipAvoidingPrevious(weapWhooshSFX, lastWeaponWhooshPlayed, index);
                audioSource.PlayOneShot(lastWeaponWhooshPlayed);
            }
        }

        public static AudioClip PickClipAvoidingPrevious(AudioClip[] clips, AudioClip previous, int indexAmongRemaining)
        {
            if (clips == null || clips.Length == 0) return null;
            if (clips.Length == 1) return clips[0];

            int remaining = CountClipsOtherThan(clips, previous);
            if (remaining == 0) return clips[0];
            if (indexAmongRemaining < 0) indexAmongRemaining = 0;
            if (indexAmongRemaining >= remaining) indexAmongRemaining = remaining - 1;

            for (int i = 0; i < clips.Length; i++)
            {
                if (clips[i] == previous) continue;
                if (indexAmongRemaining == 0) return clips[i];
                indexAmongRemaining--;
            }

            return clips[0];
        }

        static int CountClipsOtherThan(AudioClip[] clips, AudioClip previous)
        {
            int remaining = 0;
            for (int i = 0; i < clips.Length; i++)
            {
                if (clips[i] != previous)
                    remaining++;
            }
            return remaining;
        }
    }
}