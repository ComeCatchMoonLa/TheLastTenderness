namespace CatchMoon
{
    public class StaminaBar : BaseBar
    {
        public void SetMaxStamina(float maxStamina)
        {
            SetMaxValue(maxStamina);
        }

        public void SetCurrentStamina(float currentStamina)
        {
            SetCurrentValue(currentStamina);
        }
    }
}