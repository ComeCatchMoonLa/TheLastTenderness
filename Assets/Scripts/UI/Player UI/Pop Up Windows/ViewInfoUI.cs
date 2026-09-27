using UnityEngine;
using TMPro;

namespace CatchMoon
{
    public class ViewInfoUI : MonoBehaviour, PopUpInterface
    {
        TextMeshProUGUI textMeshProUGUI;

        private void Awake()
        {
            textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }
        private void Start()
        {
            #region ºÏ≤‚ø’“˝”√“Ï≥£
            if (textMeshProUGUI == null)
                Debug.LogError("textMeshProUGUI == null");
            #endregion
        }

        public void PopUp()
        {
            textMeshProUGUI.enabled = true;
        }
        public void Close()
        {
            textMeshProUGUI.enabled = false;
        }
        public bool HadClosed()
        {
            return !textMeshProUGUI.enabled;
        }

        public void SetText(TextData textData)
        {
            textMeshProUGUI.text = textData.text;
        }
    }
}