namespace CatchMoon
{
    /// <summary>
    /// À¶Á¿
    /// </summary>
    public class FocusPointsBar : BaseBar
    {
        public void SetMaxMP(float maxFocusPoint)
        {
            SetMaxValue(maxFocusPoint);
        }
        public void SetCurrentMP(float currentFocusPoint)
        {
            SetCurrentValue(currentFocusPoint);
        }
    }
}