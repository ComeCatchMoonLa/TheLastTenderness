using UnityEngine;

namespace CatchMoon
{
    public class UIEnemyHealthBar : BaseBar
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();
            player = FindObjectOfType<PlayerManager>();
        }

        public float timeUntilBarIsHidden = 0f;

        public void SetMaxHealth(float maxHealth)
        {
            SetMaxValue(maxHealth);
        }

        public void SetCurrentHealth(float health)
        {
            SetCurrentValue(health);
        }

        private void Update()
        {
            HideHealthBar();

            FaceToScreen();
        }

        private void HideHealthBar()
        {
            timeUntilBarIsHidden -= Time.deltaTime;

            if (timeUntilBarIsHidden < 0f)
            {
                timeUntilBarIsHidden = 0f;
                slider.gameObject.SetActive(false);
            }

            if (slider.value <= 0f)
                Destroy(slider.gameObject);
        }

        /// <param name="showTime">Bar的显示时间</param>
        public void ShowBar(int showTime = 2)
        {
            timeUntilBarIsHidden = showTime;

            gameObject.SetActive(true);
        }

        void FaceToScreen()
        {
            if (gameObject.activeSelf)
            {
                Vector3 dir = transform.position - player.pCamera.cameraTransform.position;
                dir.y = 0;
                Quaternion rt = Quaternion.LookRotation(dir.normalized);
                transform.rotation = rt;
            }
        }
    }
}