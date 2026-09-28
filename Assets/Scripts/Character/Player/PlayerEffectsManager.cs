using UnityEngine;

namespace CatchMoon
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        PlayerManager player;

        [Header("Player FX")]
        public GameObject currentParticleFX;    // 用来播放当前影响玩家的效果的粒子系统, 例如中毒, 喝药水等
        public GameObject instantiatedFXModel;

        static readonly int GetArrowState = Animator.StringToHash("Get Arrow");
        static readonly int AimingStartState = Animator.StringToHash("Aiming - Start");

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        private void Update()
        {
            if (Ammo == null || player.aimingMode)
                return;
            int state = player.animator.GetCurrentAnimatorStateInfo(4).shortNameHash;
            if (state == GetArrowState || state == AimingStartState)
                return;
            Destroy(Ammo);
            Ammo = null;
        }
    }
}