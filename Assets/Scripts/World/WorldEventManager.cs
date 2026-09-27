using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class WorldEventManager : MonoBehaviour
    {
        WorldManager world;

        public List<FogWall> fogWalls;

        public bool bossFightIsActive;   // 目前正在战斗的boss
        public bool bossHasBeenAwakened; // (播放boss出场动画,) boss被唤醒
        public bool bossHasBeenDefeated; // boss被击败

        private void Awake()
        {
            world = GetComponentInParent<WorldManager>();
        }

        public void ActivateBossFight(EnemyManager enemy)
        {
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            world.wUI.ActivateBossBar(enemy.eStats);

            foreach(FogWall fogWall in fogWalls)
                fogWall.ActivateFogWall();
        }

        public void BossHasBeenDefeated()
        {
            bossHasBeenDefeated = true;
            bossFightIsActive= false;
            world.wUI.DeactivateBossBar();

            foreach (FogWall fogWall in fogWalls)
                fogWall.DeactivateFogWall();
        }
    }
}