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

                if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
                {
                    spellTarget = collision.transform.GetComponent<CharacterStatsManager>();
                    CharacterManager target = collision.transform.GetComponent<CharacterManager>();

                    if (spellTarget != null && spellTarget.teamID != teamID)
                    {
                        bool fromFront = target != null && CharacterCombatManager.ComesFromFront(
                            transform.position, target.transform.position, target.transform.forward);
                        if (target != null && SpellOnShield.UseShield(target.cCombat.isBlocking, fromFront))
                        {
                            target.cCombat.ResolveIncomingHit(null, "Block - Hit", new IncomingDamage
                            {
                                Multiplier = 1f,
                                Segments = new DamageSegments
                                {
                                    Physical = pd,
                                    Fire = fd,
                                    Magic = md,
                                    Lightning = ld,
                                    Dark = dd
                                }
                            }, true, poiseBreak, guardBreakModifider, true);
                        }
                        else
                        {
                            spellTarget.TakeDamage("Damage_ForwardRight_01", new DamageSegments
                            {
                                Physical = pd,
                                Fire = fd,
                                Magic = md,
                                Lightning = ld,
                                Dark = dd
                            });
                        }
                    }
                }
                impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));

                Destroy(projectileParticles);
                Destroy(impactParticles, 1f);
                Destroy(gameObject, 1f);
            }
        }
    }
}