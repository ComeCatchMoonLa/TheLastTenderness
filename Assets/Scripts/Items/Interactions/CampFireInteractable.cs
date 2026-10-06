using UnityEngine;

namespace CatchMoon
{
    public class CampFireInteractable : Interactable
    {
        [Header("篝火位置")]
        public Transform campFireTransform;

        [Header("篝火状态")]
        public bool hasBeenActived;
        [SerializeField] float hostileRadius;

        [Header("篝火特效")]
        public ParticleSystem activationFX;
        public ParticleSystem fireFX;
        public AudioClip campFireActivationSFX;

        AudioSource audioSource;

        private void Awake()
        {
            if(hasBeenActived)
            {
                fireFX.gameObject.SetActive(true);
                fireFX.Play();
                interactTipText = "休息";
            }
            else
            {
                interactTipText = "点火";
            }

            audioSource = GetComponent<AudioSource>();
        }

        public override void Interact(PlayerManager player)
        {
            if (hasBeenActived)
            {
                if (hostileRadius <= 0f)
                {
                    Debug.LogError($"{name}: hostileRadius 未填");
                    return;
                }
                if (!CampFireRest.Allow(hostileRadius, PursuingEnemyInside()))
                    return;

                LastBonfire.Remember(name, true);
                player.pStats.RestoreVitalsToMax();
                EnemyStatsManager[] resting = FindObjectsByType<EnemyStatsManager>(FindObjectsInactive.Include);
                for (int i = 0; i < resting.Length; i++)
                {
                    if (resting[i] != null)
                        resting[i].RestoreAfterRest();
                }
                player.pAnimator.PlayTargetAnimation("Pick Up Item", true);
            }
            else
            {
                player.pAnimator.PlayTargetAnimation("Pick Up Item", true);
                player.ui.popUps.campFireLitPopUpUI.PopUp();
                hasBeenActived = true;
                interactTipText = "休息";
                //activationFX.gameObject.SetActive(true);
                //activationFX.Play();
                fireFX.gameObject.SetActive(true);
                fireFX.Play();
                audioSource.PlayOneShot(campFireActivationSFX);
            }
        }

        bool PursuingEnemyInside()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, hostileRadius, LayerMask.npc);
            for (int i = 0; i < hits.Length; i++)
            {
                EnemyManager enemy = hits[i].GetComponentInParent<EnemyManager>();
                if (enemy != null && enemy.CurrentState is PursueTargetState)
                    return true;
            }
            return false;
        }
    }
}