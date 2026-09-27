using System.Collections;
using UnityEngine;

namespace CatchMoon
{
    public class CampFireLitPopUpUI : MonoBehaviour, PopUpInterface
    {
        CanvasGroup canvas;
        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (canvas == null)
                Debug.LogError("canvas == null");
            #endregion
        }

        public void PopUp()
        {
            StartCoroutine(FadeInPopUp());
        }

        IEnumerator FadeInPopUp()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            for (float fade = 0.05f; fade < 1f; fade += 0.05f)
            {
                canvas.alpha = fade;

                if (fade > 0.95f) // 最后一趟循环fade略大于0.95
                    StartCoroutine(FadeOutPopUp());
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator FadeOutPopUp()
        {
            yield return new WaitForSeconds(2f);

            for (float fade = 1f; fade > 0f; fade -= 0.05f)
            { 
                canvas.alpha = fade;

                if (fade < 0.05f) // 最后一趟循环fade略小于0.05
                {
                    Debug.Log(fade);
                    gameObject.SetActive(false);
                }

                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}