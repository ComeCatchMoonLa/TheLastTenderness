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
            #region 检测空引用异常
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