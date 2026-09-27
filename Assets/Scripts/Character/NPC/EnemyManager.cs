using UnityEngine;
using UnityEngine.AI;

namespace CatchMoon
{
    public class EnemyManager : CharacterManager
    {
        [HideInInspector] public EnemyAnimatorManager eAnimator;
        [HideInInspector] public EnemyCombatManager eCombat;
        [HideInInspector] public EnemyEffectsManager eEffects;
        [HideInInspector] public EnemyInventoryManager eInventory;
        [HideInInspector] public EnemyStatsManager eStats;
        [HideInInspector] public EnemyWeaponSlotManager eWeaponSlot;

        [Header("npc类型设置")]
        public bool isStoryNpc;
        public bool isCombatNpc;

        [Header("A.I 设置")]
        public bool enableAI = true; // 是否启用AI
        public EnemyAISettings aiSettings; // AI设置

        [Header("A.I 导航")]
        public NavMeshAgent navmeshAgent;

        [Header("A.I 状态")]
        [SerializeField] State currentState;

        [Header("A.I Flags")]
        public bool isPreformingAction;             // 行为准备中
        public bool isPhaseShifting;                // 正在变换阶段

        [Header("A.I Timer")]
        public float currentRecoveryTime = 0;

        [Header("目标信息")]
        public CharacterManager currentTarget;
        public float distFromTarget; // 到目标的距离
        public Vector3 targetDir;    // 目标方向
        public float targetDirAngle; // 目标方向与正前方的夹角

        [HideInInspector] public float turnAngle;   // 旋转角度(原地旋转)
        [HideInInspector] public bool hadTurned;    // 已经完成了旋转

        protected override void Awake()
        {
            base.Awake();

            eAnimator = GetComponent<EnemyAnimatorManager>();
            eCombat = GetComponent<EnemyCombatManager>();
            eEffects = GetComponent<EnemyEffectsManager>();
            eInventory = GetComponent<EnemyInventoryManager>();
            eStats = GetComponent<EnemyStatsManager>();
            eWeaponSlot = GetComponent<EnemyWeaponSlotManager>();

            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        }
        protected override void Start()
        {
            base.Start();

            #region 判断空引用异常
            if (eAnimator == null)
                Debug.LogError($"{transform.name}: eAnimator == null");
            if (eCombat == null)
                Debug.LogError($"{transform.name}: eCombat == null");
            if (eEffects == null)
                Debug.LogError($"{transform.name}: eEffects == null");
            if (eInventory == null)
                Debug.LogError($"{transform.name}: eInventory == null");
            if (eStats == null)
                Debug.LogError($"{transform.name}: eStats == null");
            if (eWeaponSlot == null)
                Debug.LogError($"{transform.name}: eWeaponSlot == null");
            if (navmeshAgent == null)
                Debug.LogError($"{transform.name}: navmeshAgent == null");

            if (aiSettings == null)
                Debug.LogError($"{transform.name}: aiSettings == null");
            #endregion

            navmeshAgent.enabled = false;       // 禁用导航
            rigidBody.isKinematic = false;      // 不启用物理系统
        }
        protected override void Update()
        {
            if (eStats.isDead) return;
            base.Update();

            HandleRecoveryTimer();
            HandleStateMachine();

            UpdateAICombatInfo();
        }
        private void LateUpdate()
        {
            navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// [处理] 状态机
        /// </summary>
        private void HandleStateMachine()
        {
            if (currentState != null)
            {
                State nextState = currentState.Tick(this);

                if (nextState != null)
                    SwitchToNextState(nextState);
            }
        }

        /// <summary>
        /// 切换到下一个状态
        /// </summary>
        void SwitchToNextState(State state)
        {
            currentState = state;
        }

        /// <summary>
        /// [处理] 恢复计时器
        /// </summary>
        void HandleRecoveryTimer()
        {
            if (currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPreformingAction)
            {
                if (currentRecoveryTime <= 0)
                {
                    isPreformingAction = false;
                }
            }
        }

        public void UpdateAICombatInfo()
        {
            if (currentTarget != null)
            {
                distFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
                targetDir = currentTarget.transform.position - transform.position;
                targetDir.y = 0;
                targetDir.Normalize();
                targetDirAngle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
            }
        }
    }
}