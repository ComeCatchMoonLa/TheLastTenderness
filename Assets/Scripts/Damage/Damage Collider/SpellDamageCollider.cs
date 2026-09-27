using UnityEngine;

namespace CatchMoon
{
    public class SpellDamageCollider : DamageCollider
    {
        public GameObject muzzleParticles;
        public GameObject projectileParticles;
        public GameObject impactParticles;

        bool hasCollided = false;

        CharacterStatsManager spellTarget;

        Vector3 impactNormal; // 用来旋转<碰撞粒子系统>

        protected override void Start()
        {
            projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
            projectileParticles.transform.parent = transform;

            if (muzzleParticles != null)
            {
                muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
                Destroy(muzzleParticles, 0.1f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!hasCollided)
            {
                hasCollided = true;

                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
                {
                    spellTarget = collision.transform.GetComponent<CharacterStatsManager>();

                    if (spellTarget != null && spellTarget.teamID != teamID)
                        spellTarget.TakeDamage(damageAnimation: "Damage_ForwardRight_01", fd: fd);

                }
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectileParticles);
                Destroy(impactParticles, 1f);
                Destroy(gameObject, 1f);
            }
        }
    }
}