using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class WorldEventManager : MonoBehaviour
    {
        WorldManager world;

        public List<FogWall> fogWalls;
        public CampFireInteractable rewardFire;
        public bool deviceOnDuringFight;
        public bool deviceStaysAfter;
        public bool deviceOn;

        public bool bossFightIsActive;   // 目前正在战斗的boss
        public bool bossHasBeenAwakened; // (播放boss出场动画,) boss被唤醒
        public bool bossHasBeenDefeated; // boss被击败

        private void Awake()
        {
            world = GetComponentInParent<WorldManager>();
        }

        public void ActivateBossFight(EnemyManager enemy)
        {
            if (!BossClear.CanOpenAgain(bossHasBeenDefeated)) return;
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            if (enemy != null && enemy.eStats != null)
                FogDoor.Begin(ref enemy.eStats.currentHP, enemy.eStats.maxHP);
            deviceOn = ArenaDevice.IsOn(true, deviceOnDuringFight, deviceStaysAfter);
            world.wUI.ActivateBossBar(enemy.eStats);

            foreach(FogWall fogWall in fogWalls)
                fogWall.ActivateFogWall();
        }

        public void BossHasBeenDefeated()
        {
            bool fogUp = true;
            bool bossStillHere = true;
            bool fireSealed = true;
            BossClear.Apply(ref fogUp, ref bossStillHere, ref fireSealed);
            bossHasBeenDefeated = true;
            bossFightIsActive = false;
            world.wUI.DeactivateBossBar();
            if (rewardFire != null)
                rewardFire.sealedUntilBossClear = fireSealed;
            deviceOn = ArenaDevice.IsOn(false, deviceOnDuringFight, deviceStaysAfter);

            foreach (FogWall fogWall in fogWalls)
                fogWall.DeactivateFogWall();
        }
    }
}