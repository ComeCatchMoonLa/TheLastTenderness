using UnityEngine;

namespace CatchMoon
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public ParticleSystem noramalWeaponTrial;

        public void PlayWeaponTrialFX()
        {
            noramalWeaponTrial.Play(); // 应该在攻击动画结束时, 执行Stop()
        }

        public void StopWeaponTrialFX()
        {
            noramalWeaponTrial.Stop();
            noramalWeaponTrial.Clear();
        }
    }
}
