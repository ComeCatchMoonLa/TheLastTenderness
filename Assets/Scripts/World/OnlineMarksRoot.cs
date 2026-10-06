using UnityEngine;

namespace CatchMoon
{
    public class OnlineMarksRoot : MonoBehaviour
    {
        public bool online = true;
        public GameObject message;
        public GameObject bloodstain;

        public void Apply()
        {
            if (message == null)
            {
                Debug.LogError($"{name}: message 未填");
                return;
            }
            if (bloodstain == null)
            {
                Debug.LogError($"{name}: bloodstain 未填");
                return;
            }
            bool shown = OnlineMarks.Shown(online);
            message.SetActive(shown);
            bloodstain.SetActive(shown);
        }
    }
}
