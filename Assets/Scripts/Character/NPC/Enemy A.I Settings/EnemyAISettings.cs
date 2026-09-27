using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "A.I/A.I Settings/Simple A.I Settings")]
    public class EnemyAISettings : ScriptableObject
    {
        [Header("A.I 类型")]
        public bool isBoss;

        [Header("速度设置")]
        public float rotationSpeed = 25f;           // 旋转速度

        [Header("视角设置")]
        public float maxViewAngel = 62f;            // 视角最大值(由正前方向右)
        public float minViewAngel = -62f;           // 视角最小值(由正前方向左)     

        [Header("连击设置")]
        public bool allowAIToPerformCombos = true;  // 是否允许AI连击
        [Range(0, 100)] public int comboLikeHood = 30;              // 可以连击的概率(百分比值)

        [Header("敌对半径")]
        [Tooltip("在此范围内时会围着玩家走, 在半径外察觉半径内时会追向玩家, 在察觉半径外时会脱离战斗")]
        public float aggroRadius = 5f;

        [Header("察觉半径")]
        public float detectionRadius = 20f;

        [Header("战斗风格")]
        public NPCCombatStyle combatStyle;

        [Header("格挡设置")]
        public bool allowAIToPerformBlock;
        [Range(0, 100)] public int blockLikelyHood;    // 随机到block的概率

        [Header("闪避设置")]
        public bool allowAIToPerformDodge;
        [Range(0, 100)] public int dodgeLikelyHood;    // 随机到dodge的概率

        [Header("弹反设置")]
        public bool allowAIToPerformParry;
        [Range(0, 100)] public int parryLikelyHood;    // 随机到parry的概率
    }
}