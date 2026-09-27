using UnityEngine;

namespace CatchMoon
{
    public class CampFireInteractable : Interactable
    {
        [Header("篝火位置")]
        public Transform campFireTransform;

        [Header("篝火状态")]
        public bool hasBeenActived;

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
                // 打开篝火菜单
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
    }
}