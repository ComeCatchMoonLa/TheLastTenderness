using TMPro;

namespace CatchMoon
{
    public class UIBossHealthBar : BaseBar
    {
        public TextMeshProUGUI bossName;

        protected override void Awake()
        {
            base.Awake();

            bossName = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void Start()
        {
            base.Start();

            SetUIHealthBarToInactive();
        }

        public void SetBossName(string name)
        {
            bossName.text = name;
        }

        public void SetBossMaxHealth(float maxHealth)
        {
            SetMaxValue(maxHealth);
        }

        public void SetBossCurrentHealth(float currentHealth)
        {
            SetCurrentValue(currentHealth);
        }

        public void SetUIHealthBarToActive()
        {
            slider.gameObject.SetActive(true);
        }

        public void SetUIHealthBarToInactive()
        {
            slider.gameObject.SetActive(false);
        }
    }
}