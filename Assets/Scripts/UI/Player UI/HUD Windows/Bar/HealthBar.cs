namespace CatchMoon
{
    public class HealthBar : BaseBar
    {
        public void SetMaxHP(float maxHealth)
        {
            SetMaxValue(maxHealth);
        }
        public void SetCurrentHP(float currentHealth) 
        {
            SetCurrentValue(currentHealth);
        }
    }
}