using UnityEngine;

namespace CatchMoon
{
    public class DoorInteractable : Interactable
    {
        public bool opened;
        Quaternion closedRotation;

        void Awake()
        {
            closedRotation = transform.rotation;
        }

        public override void Interact(PlayerManager player)
        {
            if (player == null) return;
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

        void ApplyOpen()
        {
            transform.rotation = HingedDoor.Swing(closedRotation, true);
            Collider[] cols = GetComponents<Collider>();
            for (int i = 0; i < cols.Length; i++)
                cols[i].enabled = false;
        }
    }
}
