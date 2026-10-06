using UnityEngine;

namespace CatchMoon
{
    public class DoorInteractable : Interactable
    {
        public bool opened;
        public Item key;
        Quaternion closedRotation;

        void Awake()
        {
            closedRotation = transform.rotation;
        }

        public override void Interact(PlayerManager player)
        {
            if (player == null) return;
            if (!KeyedDoor.Allowed(key != null, CarryingKey(player))) return;
            bool front = HingedDoor.FromFront(transform.forward, player.transform.position - transform.position);
            if (!HingedDoor.TryOpen(opened, front)) return;
            opened = true;
            ApplyOpen();
        }

        public void AfterRest()
        {
            opened = HingedDoor.AfterRest(opened);
            if (opened)
                ApplyOpen();
        }

        bool CarryingKey(PlayerManager player)
        {
            if (key == null || player.pInventory == null || player.pInventory.items == null) return false;
            for (int i = 0; i < player.pInventory.items.Count; i++)
            {
                if (player.pInventory.items[i] == key) return true;
            }
            return false;
        }

        void ApplyOpen()
        {
            transform.rotation = HingedDoor.Swing(closedRotation, true);
            Collider[] cols = GetComponents<Collider>();
            for (int i = 0; i < cols.Length; i++)
                cols[i].enabled = false;
        }
    }
}
