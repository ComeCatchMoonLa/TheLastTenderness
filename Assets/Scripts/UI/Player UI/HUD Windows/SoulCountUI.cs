using UnityEngine;
using TMPro;

namespace CatchMoon
{
    public class SoulCountUI : MonoBehaviour
    {
        TextMeshProUGUI soulCntText;

        private void Awake()
        {
            soulCntText = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            #region ºÏ≤‚ø’“˝”√“Ï≥£
            if (soulCntText == null)
                Debug.LogError("soulCountText == null");
            #endregion
        }

        public void SetSoulCountText(int soulCount)
        {
            soulCntText.text = soulCount.ToString();
        }
    }
}