using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CatchMoon
{
    public class TalkUI : MonoBehaviour, PopUpInterface
    {
        PlayerManager player;

        [SerializeField] TextMeshProUGUI talkContentText;
        [SerializeField] TextMeshProUGUI talkerNameText;

        [Header("对话内容列表")]
        public List<DialogueTextData> talkContent;

        [Header("播放完文本后的停留缓冲时间")]
        public float waitTime = 0.8f;

        [Header("当前句子已播放完整(文字+语音)")]
        [SerializeField] bool sentenceIsComplete;

        [System.NonSerialized] public Coroutine talkCoroutine;

        private void Awake()
        {
            player = transform.root.GetComponent<PlayerManager>();
        }
        private void Start()
        {
            #region 检测空引用异常
            if (player == null)
                Debug.LogError("player == null");

            if (talkContentText == null)
                Debug.LogError("textMeshProUGUI == null");
            if (talkerNameText == null)
                Debug.LogError("talkerNameText == null");
            #endregion
        }

        public void PopUp()
        {
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void AdvanceDialogue(EnemyManager npc, bool starting)
        {
            // 窗口未激活时 Awake 还没跑，Update 仍会每帧进来。
            if (player == null)
                player = transform.root.GetComponent<PlayerManager>();
            if (player == null)
                return;

            bool auto = player.ui.escWin.GetSettingWin().gameSettingsData.auto;
            bool wordForWord = player.ui.escWin.GetSettingWin().gameSettingsData.wordForWord;
            if (auto)
            {
                if (!starting)
                    return;
                if (wordForWord)
                    Start_UpdateDialogue_Auto_WFW(npc);
                else
                    Start_UpdateDialogue_Auto_SBS(npc);
                return;
            }

            if (wordForWord)
            {
                if (starting)
                    Start_UpdateDialogue_WFW_Helper(npc);
                else
                    Handle_UpdateDialogue_NonAuto_WFW(npc);
                return;
            }

            if (starting)
            {
                PlayTalkerEmptyPose(npc);
                UpdateDialogueSBS_Helper(npc);
            }
            else
                Handle_UpdateDialogue_NonAuto_SBS(npc);
        }

        public void SetTalkContent(List<DialogueTextData> talkContent)
        {
            // 设置npc对话内容
            this.talkContent = new List<DialogueTextData>();
            foreach(DialogueTextData textData in talkContent)
                this.talkContent.Add(textData);
        }
        
        /// <summary>
        /// 设置对话人的状态(对话人名称、flag)
        /// </summary>
        void SetTalkerStatus(EnemyManager npc)
        {
            if (talkContent[0].isPlayerSaid)
            {
                npc.talkWithSB = false;
                talkerNameText.text = "你";
                player.talkWithSB = true;
            }
            else
            {
                player.talkWithSB = false;
                talkerNameText.text = npc.eStats.cName;
                npc.talkWithSB = true;
            }
        }

        void PlayTalkerEmptyPose(EnemyManager npc)
        {
            if (talkContent[0].isPlayerSaid)
                npc.animator.Play("NonCombat Whole Body Empty");
            else
                player.animator.Play("NonCombat Whole Body Empty");
        }

        /// <summary>
        /// 自动逐句播放剧情
        /// </summary>
        IEnumerator UpdateDialogue_Auto_SBS(EnemyManager npc)
        {
            while (talkContent.Count > 0)
            {
                float duration = talkContent[0].duration;
                PlayTalkerEmptyPose(npc);
                UpdateDialogueSBS_Helper(npc);

                yield return new WaitForSeconds(duration);
            }

            HandleTalkEnd(npc);
        }
        public void Start_UpdateDialogue_Auto_SBS(EnemyManager npc)
        {
            talkCoroutine = StartCoroutine(UpdateDialogue_Auto_SBS(npc));
        }
        /// <summary>
        /// 自动逐字播放剧情
        /// </summary>
        IEnumerator UpdateDialogue_Auto_WFW(EnemyManager npc)
        {
            while (talkContent.Count > 0)
            {
                SetTalkerStatus(npc);
                PlayTalkerEmptyPose(npc);

                string newSentence = string.Empty;
                for (int i = 0; i < talkContent[0].text.Length; i++)
                {
                    newSentence += talkContent[0].text[i];
                    talkContentText.text = newSentence;
                    yield return new WaitForSeconds((talkContent[0].duration - waitTime) / talkContent[0].text.Length);
                }
                talkContent.RemoveAt(0); // 处理完该段文本后，在对话内容列表中移除该段文本
                yield return new WaitForSeconds(waitTime);
            }
            HandleTalkEnd(npc);
        }
        public void Start_UpdateDialogue_Auto_WFW(EnemyManager npc)
        {
            talkCoroutine = StartCoroutine(UpdateDialogue_Auto_WFW(npc));
        }
        /// <summary>
        /// 非自动逐句播放剧情
        /// </summary>
        public void Handle_UpdateDialogue_NonAuto_SBS(EnemyManager npc)
        {
            if (talkContent == null) return;

            if (talkContent.Count > 0)
            {
                if (player.input.interacte_Tap_Input)
                {
                    player.input.interacte_Tap_Input = false;

                    PlayTalkerEmptyPose(npc);
                    UpdateDialogueSBS_Helper(npc);
                }
            }
            else
            {
                if (player != null && player.storyNpc != null)
                {
                    if (player.input.interacte_Tap_Input)
                    {
                        player.input.interacte_Tap_Input = false;

                        HandleTalkEnd(npc);
                    }
                }
            }
        }
        /// <summary>
        /// 非自动逐字播放剧情
        /// </summary>
        public void Handle_UpdateDialogue_NonAuto_WFW(EnemyManager npc)
        {
            if (talkContent == null) return;

            if (talkContent.Count > 0)
            {
                if (player.input.interacte_Tap_Input)
                {
                    player.input.interacte_Tap_Input = false;

                    StopCoroutine(talkCoroutine);
                    if (sentenceIsComplete)
                    {
                        Start_UpdateDialogue_WFW_Helper(npc);
                    }
                    else
                    {
                        talkContentText.text = talkContent[0].text;
                        talkContent.RemoveAt(0); // 处理完该段文本后，在对话内容列表中移除该段文本
                        sentenceIsComplete = true;
                    }
                }
            }
            else
            {
                if (player != null && player.storyNpc != null)
                {
                    if (player.input.interacte_Tap_Input)
                    {
                        player.input.interacte_Tap_Input = false;

                        HandleTalkEnd(npc);
                    }
                }
            }
        }
        
        /// <summary>
        /// 逐字更新对话文本 [辅助函数]
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        IEnumerator UpdateDialogue_WFW_Helper(EnemyManager npc)
        {
            // 设置说话人的名字
            SetTalkerStatus(npc);
            PlayTalkerEmptyPose(npc);

            sentenceIsComplete = false;
            string newSentence = string.Empty;
            for (int i = 0; i < talkContent[0].text.Length; i++)
            {
                newSentence += talkContent[0].text[i];
                talkContentText.text = newSentence;
                yield return new WaitForSeconds((talkContent[0].duration - waitTime) / talkContent[0].text.Length);
            }
            talkContent.RemoveAt(0); // 处理完该段文本后，在对话内容列表中移除该段文本
            sentenceIsComplete = true;
        }
        public void Start_UpdateDialogue_WFW_Helper(EnemyManager npc)
        {
            talkCoroutine = StartCoroutine(UpdateDialogue_WFW_Helper(npc));
        }
        /// <summary>
        /// 逐句更新对话文本 [辅助函数]
        /// </summary>
        public void UpdateDialogueSBS_Helper(EnemyManager npc)
        {
            SetTalkerStatus(npc);
            talkContentText.text = talkContent[0].text;
            talkContent.RemoveAt(0);
        }

        /// <summary>
        /// [处理] 跳过对话输入
        /// </summary>
        public void Handle_SkipTalk_Input(EnemyManager npc)
        {
            if (talkContent == null || talkContent.Count == 0 || talkCoroutine == null) return;

            if (player.input.interacte_Hold_Input)
            {
                player.input.interacte_Hold_Input = false;

                StopCoroutine(talkCoroutine);
                talkContent.Clear();
                HandleTalkEnd(npc);
            }
        }
        
        /// <summary>
        /// 处理对话结束
        /// </summary>
        void HandleTalkEnd(EnemyManager npc)
        {
            // npc 状态
            npc.talkWithSB = false;
            npc.gameObject.tag = "Interactable";
            // npc 动画
            npc.animator.Play("NonCombat Whole Body Empty");
            // player 状态
            player.talkWithSB = false;
            // player UI
            player.ui.hud.Show();
            player.ui.popUps.talkUI.Close();
            // player 输入
            player.input.inputActions.Enable();
            // player 动画
            player.animator.Play("NonCombat Whole Body Empty");
            // player交互的剧情npc
            player.storyNpc = null;
        }
    }
}