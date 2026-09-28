using UnityEngine;

namespace CatchMoon
{
    public class WeaponFX : MonoBehaviour
    {
        [Header("Weapon FX")]
        public ParticleSystem normalWeaponTrail;

        public void PlayWeaponTrialFX()
        {
            normalWeaponTrail.Play(); // 应该在攻击动画结束时, 执行Stop()
        }

        public void StopWeaponTrialFX()
        {
            normalWeaponTrail.Stop();
            normalWeaponTrail.Clear();
        }
    }
}
