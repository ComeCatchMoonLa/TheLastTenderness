using UnityEngine;

namespace CatchMoon
{
    public class LadderInteractable : Interactable
    {
        public bool bottomFilled;
        public float bottom;
        public bool topFilled;
        public float top;
        public bool climbSpeedFilled;
        public float climbSpeed;

        public override void Interact(PlayerManager player)
        {
            if (!bottomFilled)
            {
                Debug.LogError($"{name}: bottom 未填");
                return;
            }
            if (!topFilled)
            {
                Debug.LogError($"{name}: top 未填");
                return;
            }
            if (!climbSpeedFilled || climbSpeed <= 0f)
            {
                Debug.LogError($"{name}: climbSpeed 未填");
                return;
            }
            LadderRide ride = player.GetComponent<LadderRide>();
            if (ride == null)
                ride = player.gameObject.AddComponent<LadderRide>();
            ride.bottom = bottom;
            ride.top = top;
            ride.climbSpeedFilled = climbSpeedFilled;
            ride.climbSpeed = climbSpeed;
            ride.Mount(player.transform.position.y, bottom, top);
        }
    }
}
