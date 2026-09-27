using UnityEngine;
using UnityEngine.UI;

namespace CatchMoon
{
    public class BaseBar : MonoBehaviour
    {
        public Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }
        protected virtual void Start()
        {
            #region ºÏ≤‚ø’“˝”√“Ï≥£
            if (slider == null)
                Debug.LogError($"{transform.name}: slider == null");
            #endregion
        }

        protected void SetMaxValue(float maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }

        protected void SetCurrentValue(float currentValue)
        {
            slider.value = currentValue;
        }
    }
}