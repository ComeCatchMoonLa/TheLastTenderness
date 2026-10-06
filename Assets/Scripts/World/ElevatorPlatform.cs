using UnityEngine;

namespace CatchMoon
{
    public class ElevatorPlatform : Interactable
    {
        public Vector3 home;
        public Vector3 away;
        public bool goingAway = true;

        public override void Interact(PlayerManager player)
        {
            Pull();
        }

        public void Pull()
        {
            if (!Elevator.Next(name, home, away, goingAway, out Vector3 position, out bool nextGoingAway)) return;
            transform.localPosition = position;
            goingAway = nextGoingAway;
        }
    }
}
