using UnityEngine;

namespace CatchMoon
{
    public class PopUpWindowsManager : MonoBehaviour
    {
        PlayerManager player;

        public TalkUI talkUI;
        public ViewInfoUI viewInfoUI;
        public InteractUI interactUI;
        public CampFireLitPopUpUI campFireLitPopUpUI;

        private void Awake()
        {
            player = PlayerUIManager.FindPlayer(this);

            viewInfoUI = GetComponentInChildren<ViewInfoUI>();
            interactUI = GetComponentInChildren<InteractUI>();
            campFireLitPopUpUI = GetComponentInChildren<CampFireLitPopUpUI>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError("player == null");

            if (viewInfoUI == null)
                Debug.LogError("viewInfoUI == null");
            if (interactUI == null)
                Debug.LogError("interactUI == null");
            if (campFireLitPopUpUI == null)
                Debug.LogError("campFireLitPopUpUI == null");
            #endregion
        }

        public void HandleCloseViewInfoWindow()
        {
            if (!viewInfoUI.HadClosed())
            {
                if (player.input.interacte_Tap_Input)
                {
                    player.input.interacte_Tap_Input = false;

                    viewInfoUI.Close();
                }
            }
        }
    }
}