using UnityEngine;

namespace CatchMoon
{
    public class CampFireInteractable : Interactable
    {
        [Header("篝火位置")]
        public Transform campFireTransform;

        [Header("篝火状态")]
        public bool hasBeenActived;
        public bool sealedUntilBossClear;
        public bool nextCycleInsteadOfRest;
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
                if (nextCycleInsteadOfRest
                    && player != null
                    && player.pStats != null
                    && CycleGate.Allow(player.pStats.recordedEnding))
                {
                    nextCycleInsteadOfRest = false;
                    NewCycle.Apply(player.pInventory);
                    return;
                }
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
                DoorInteractable[] doors = FindObjectsByType<DoorInteractable>(FindObjectsInactive.Include);
                for (int i = 0; i < doors.Length; i++)
                {
                    if (doors[i] != null)
                        doors[i].AfterRest();
                }
                player.pAnimator.PlayTargetAnimation("Pick Up Item", true);
            }
            else
            {
                if (sealedUntilBossClear) return;
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

        public void ExtinguishForNewCycle()
        {
            hasBeenActived = false;
            interactTipText = "点火";
            if (fireFX != null)
                fireFX.gameObject.SetActive(false);
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