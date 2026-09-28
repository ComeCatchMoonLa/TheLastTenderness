using UnityEngine;

namespace CatchMoon
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        PlayerManager player;

        [Header("Player FX")]
        public GameObject currentParticleFX;    // 用来播放当前影响玩家的效果的粒子系统, 例如中毒, 喝药水等
        public GameObject instantiatedFXModel;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }
        private void Update()
        {
            if (Ammo != null && !player.aimingMode && !player.animator.GetCurrentAnimatorStateInfo(4).IsName("Get Arrow")
                && !player.animator.GetCurrentAnimatorStateInfo(4).IsName("Aiming - Start"))
            {
                Destroy(Ammo);
                Ammo = null;
            }
        }
    }
}