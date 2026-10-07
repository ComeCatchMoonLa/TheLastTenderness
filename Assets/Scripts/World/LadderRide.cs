using UnityEngine;

namespace CatchMoon
{
    public class LadderRide : MonoBehaviour
    {
        public bool onLadder;
        public float along;
        public float bottom;
        public float top;

        public bool climbSpeedFilled;
        public float climbSpeed;
        bool climbSpeedLogged;

        public void Mount(float ladderBottom, float ladderTop)
        {
            onLadder = true;
            bottom = ladderBottom;
            top = ladderTop;
            along = ladderBottom;
        }

        public void Tick(float input, bool dodgeHeld, bool stickDown, bool stickCentered, out bool jumped)
        {
            LadderState state = new LadderState
            {
                onLadder = onLadder,
                along = along,
                bottom = bottom,
                top = top
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
            float input = 0f;
            if (climbSpeedFilled && climbSpeed > 0f)
                input = player.input.vertical * climbSpeed * Time.deltaTime;
            else if (!climbSpeedLogged && Mathf.Abs(player.input.vertical) > 0.01f)
            {
                Debug.LogError($"{name}: climbSpeed 未填");
                climbSpeedLogged = true;
            }
            bool centered = player.input.moveAmount <= 0.1f;
            bool down = player.input.vertical < -0.5f;
            Tick(input, player.input.DodgeHeld, down, centered, out bool jumped);
            Vector3 position = transform.position;
            position.y = along;
            transform.position = position;
            if (jumped)
            {
                onLadder = false;
                player.isInAir = true;
            }
        }
    }
}
