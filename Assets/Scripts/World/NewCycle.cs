using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public static class NewCycle
    {
        public struct Snapshot
        {
            public int level;
            public bool fireLit;
            public bool doorOpen;
            public bool bossDown;
            public bool carryingKey;
        }

        public static Snapshot Next(Snapshot current)
        {
            current.fireLit = false;
            current.doorOpen = false;
            current.bossDown = false;
            current.carryingKey = false;
            return current;
        }

        public static int StripKeys(List<Item> items)
        {
            if (items == null) return 0;
            int removed = 0;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i] != null && items[i].isKey)
                {
                    items.RemoveAt(i);
                    removed++;
                }
            }
            return removed;
        }

        public static void Apply(PlayerInventoryManager inventory)
        {
            if (inventory != null && inventory.items != null)
            {
                List<Item> keys = new List<Item>();
                for (int i = 0; i < inventory.items.Count; i++)
                {
                    if (inventory.items[i] != null && inventory.items[i].isKey)
                        keys.Add(inventory.items[i]);
                }
                for (int i = 0; i < keys.Count; i++)
                    inventory.RemoveItem(keys[i]);
            }

            DoorInteractable[] doors = Object.FindObjectsByType<DoorInteractable>(FindObjectsInactive.Include);
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i] != null)
                    doors[i].CloseForNewCycle();
            }

            CampFireInteractable[] fires = Object.FindObjectsByType<CampFireInteractable>(FindObjectsInactive.Include);
            for (int i = 0; i < fires.Length; i++)
            {
                if (fires[i] != null)
                    fires[i].ExtinguishForNewCycle();
            }

            WorldEventManager[] events = Object.FindObjectsByType<WorldEventManager>(FindObjectsInactive.Include);
            for (int i = 0; i < events.Length; i++)
            {
                if (events[i] != null)
                    events[i].ResetForNewCycle();
            }

            OpenChest[] chests = Object.FindObjectsByType<OpenChest>(FindObjectsInactive.Include);
            for (int i = 0; i < chests.Length; i++)
            {
                if (chests[i] != null)
                    chests[i].CloseForNewCycle();
            }

            IllusionWall[] walls = Object.FindObjectsByType<IllusionWall>(FindObjectsInactive.Include);
            for (int i = 0; i < walls.Length; i++)
            {
                if (walls[i] != null)
                    walls[i].CloseForNewCycle();
            }

            EnemyStatsManager[] rested = Object.FindObjectsByType<EnemyStatsManager>(FindObjectsInactive.Include);
            for (int i = 0; i < rested.Length; i++)
            {
                if (rested[i] != null)
                    rested[i].RestoreAfterRest();
            }

            EnemyManager[] enemies = Object.FindObjectsByType<EnemyManager>(FindObjectsInactive.Include);
            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyManager enemy = enemies[i];
                if (enemy == null || enemy.aiSettings == null || !enemy.aiSettings.isBoss || enemy.eStats == null)
                    continue;
                enemy.eStats.StopBossHide();
                enemy.gameObject.SetActive(true);
                enemy.eStats.isDead = false;
                enemy.eStats.currentHP = enemy.eStats.maxHP;
            }
        }
    }
}
