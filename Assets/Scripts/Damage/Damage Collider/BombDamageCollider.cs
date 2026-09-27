using UnityEngine;

namespace CatchMoon
{
    public class BombDamageCollider : DamageCollider
    {
        [HideInInspector] public Rigidbody bombRigidBody;

        [Header("Ecplosive Damage & Radius")]
        public float explosiveRadius = 2f;
        public int explosionDamage;       // 爆炸伤害
        public int explosionSplashDamage; // 溅射伤害

        // magicExplosionDamage
        // lightningExplosionDamage

        bool hasCollided = false;
        public GameObject impactParticles;

        protected override void Awake()
        {
            base.Awake();
            bombRigidBody = GetComponent<Rigidbody>();

            #region 检错
            if (bombRigidBody == null)
            { Debug.LogError("bombRigidBody is null."); }
            #endregion
        }

        protected override void Start()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!hasCollided)
            {
                hasCollided = true;

                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
                {
                    CharacterStatsManager characterStatsManager = collision.transform.GetComponent<CharacterStatsManager>();
                    if (characterStatsManager != null && characterStatsManager.teamID != teamID)
                        characterStatsManager.TakeDamage("Damage_ForwardRight_01", fd: explosionDamage);
                }
                Explode();

                impactParticles = Instantiate(impactParticles, transform.position,Quaternion.identity);
                


                Destroy(transform.parent.parent.gameObject);
            }
        }

        void Explode()
        {
            Collider[] characters = Physics.OverlapSphere(transform.position, explosiveRadius);

            foreach(Collider objectsInExplosion in characters)
            {
                CharacterStatsManager characterStatsManager = objectsInExplosion.GetComponent<CharacterStatsManager>();
                if (characterStatsManager == null || characterStatsManager.teamID == teamID) continue;

                characterStatsManager.TakeDamage(damageAnimation: null, fd: explosionSplashDamage);
            }
        }
    }
}