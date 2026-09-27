using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Data/TextData/Dialogue")]
    public class DialogueTextData : TextData
    {
        [Header("是否为玩家说的")]
        public bool isPlayerSaid;

        [Header("这段文本的持续时间")]
        // = 播放文本的时间 + 播放完文本后的停留缓冲时间
        // = 播放语音的时间 + 播放完语音后的停留缓冲时间
        [Min(0.8f)] public float duration; // min为<播放完文本后的停留缓冲时间>, 参见talkUI中的waitTime
    }
}