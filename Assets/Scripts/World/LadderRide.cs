using UnityEngine;

namespace CatchMoon
{
    public class LadderRide : MonoBehaviour
    {
        public bool onLadder;
        public float along;
        public float bottom;
        public float top;
        public float floor;
        const float fastDrop = 2f;

        public bool climbSpeedFilled;
        public float climbSpeed;
        bool climbSpeedLogged;

        public void Mount(float feetY, float ladderBottom, float ladderTop)
        {
            onLadder = true;
            bottom = ladderBottom;
            top = ladderTop;
            along = feetY;
            if (along > top) along = top;
            floor = along;
            int mask = LayerMask.environment | LayerMask.npc | LayerMask.player;
            Vector3 origin = transform.position;
            origin.y += 0.2f;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 8f, mask) && hit.point.y < floor)
                floor = hit.point.y;
        }

        public void Tick(float input, bool dodgeHeld, bool stickDown, bool stickCentered, out bool jumped)
        {
            LadderState state = new LadderState
            {
                onLadder = onLadder,
                along = along,
                bottom = bottom,
                top = top,
                floor = floor
            };
            state = LadderMove.Step(state, input, dodgeHeld, stickDown, stickCentered, out jumped);
            onLadder = state.onLadder;
            along = state.along;
        }

        void Update()
        {
            if (!onLadder) return;
            PlayerManager player = GetComponent<PlayerManager>();
            if (player == null || player.input == null) return;
            bool down = player.input.vertical < -0.5f;
            bool fast = player.input.DodgeHeld && down;
            float input = 0f;
            if (climbSpeedFilled && climbSpeed > 0f)
            {
                float rate = fast ? climbSpeed * fastDrop : climbSpeed;
                input = fast ? -rate * Time.deltaTime : player.input.vertical * rate * Time.deltaTime;
            }
            else if (!climbSpeedLogged && Mathf.Abs(player.input.vertical) > 0.01f)
            {
                Debug.LogError($"{name}: climbSpeed 未填");
                climbSpeedLogged = true;
            }
            bool centered = player.input.moveAmount <= 0.1f;
            bool wasOn = onLadder;
            Tick(input, fast ? false : player.input.DodgeHeld, false, centered, out bool jumped);
            if (!onLadder)
            {
                if (wasOn)
                {
                    if (jumped)
                        LeaveToAir(player);
                    else
                        LeaveToGround(player);
                }
                return;
            }
            Vector3 position = transform.position;
            position.y = along;
            transform.position = position;
        }

        void LeaveToAir(PlayerManager player)
        {
            player.isInAir = true;
            player.isInteracting = false;
            player.canRotate = true;
            if (player.input != null)
                player.input.KeepDodgeFromRolling();
            if (player.rigidBody != null)
                player.rigidBody.linearVelocity = Vector3.zero;
        }

        void LeaveToGround(PlayerManager player)
        {
            Vector3 position = transform.position;
            if (position.y > along)
                position.y = along;
            int mask = LayerMask.environment | LayerMask.npc | LayerMask.player;
            Vector3 origin = position;
            origin.y += 0.2f;
            if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 8f, mask))
            {
                position.y = hit.point.y;
                player.isInAir = false;
                player.isInteracting = false;
                player.canRotate = true;
                if (player.input != null)
                    player.input.KeepDodgeFromRolling();
            }
            else
                player.isInAir = true;
            transform.position = position;
            if (player.rigidBody != null)
                player.rigidBody.linearVelocity = Vector3.zero;
        }
    }
}
