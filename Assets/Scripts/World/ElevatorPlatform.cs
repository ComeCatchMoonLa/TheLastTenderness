using UnityEngine;

namespace CatchMoon
{
    [DefaultExecutionOrder(-200)]
    public class ElevatorPlatform : Interactable
    {
        public Vector3 home;
        public Vector3 away;
        public bool goingAway = true;

        const float rideSpeed = 2f;

        Rigidbody body;
        Vector3 rideLocal;
        Vector3 rideTo;
        bool moving;

        public float VerticalSpeed { get; private set; }
        public float StepY { get; private set; }

        void Awake()
        {
            RebaseFloors();
            EnsureBody();
            EnsureCall();
        }

        void FixedUpdate()
        {
            if (!moving)
            {
                VerticalSpeed = 0f;
                StepY = 0f;
                return;
            }

            float step = rideSpeed * Time.fixedDeltaTime;
            Vector3 localNext = Vector3.MoveTowards(rideLocal, rideTo, step);
            Vector3 worldNow = transform.parent == null ? rideLocal : transform.parent.TransformPoint(rideLocal);
            Vector3 worldNext = transform.parent == null ? localNext : transform.parent.TransformPoint(localNext);
            float dt = Time.fixedDeltaTime;
            StepY = worldNext.y - worldNow.y;
            VerticalSpeed = dt > 0f ? StepY / dt : 0f;
            rideLocal = localNext;
            body.MovePosition(worldNext);

            if (localNext == rideTo)
                moving = false;
        }

        void RebaseFloors()
        {
            Vector3 start = transform.localPosition;
            if (start == home || start == away) return;
            Vector3 span = away - home;
            home = start;
            away = start + span;
        }

        void EnsureBody()
        {
            body = GetComponent<Rigidbody>();
            if (body == null)
                body = gameObject.AddComponent<Rigidbody>();
            body.isKinematic = true;
            body.useGravity = false;
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        }

        void EnsureCall()
        {
            if (transform.Find("Elevator Call") != null) return;

            GameObject call = new GameObject("Elevator Call");
            call.layer = Layer.interactable;
            call.tag = "Interactable";
            call.transform.SetParent(transform, false);
            Vector3 lossy = transform.lossyScale;
            float invX = lossy.x != 0f ? 1f / lossy.x : 1f;
            float invY = lossy.y != 0f ? 1f / lossy.y : 1f;
            float invZ = lossy.z != 0f ? 1f / lossy.z : 1f;
            call.transform.localScale = new Vector3(invX, invY, invZ);
            BoxCollider box = call.AddComponent<BoxCollider>();
            box.isTrigger = true;
            box.size = new Vector3(Mathf.Abs(lossy.x), 2f, Mathf.Abs(lossy.z));
            box.center = new Vector3(0f, 1f, 0f);
            ElevatorCall callInteract = call.AddComponent<ElevatorCall>();
            callInteract.interactTipText = interactTipText;
        }

        public override void Interact(PlayerManager player)
        {
            Pull();
        }

        public void Pull()
        {
            if (moving) return;
            if (!Elevator.Next(name, home, away, goingAway, out Vector3 position, out bool nextGoingAway)) return;
            rideLocal = transform.localPosition;
            rideTo = position;
            goingAway = nextGoingAway;
            moving = true;
        }
    }

    public class ElevatorCall : Interactable
    {
        public override void Interact(PlayerManager player)
        {
            ElevatorPlatform platform = GetComponentInParent<ElevatorPlatform>();
            platform.Pull();
        }
    }
}
