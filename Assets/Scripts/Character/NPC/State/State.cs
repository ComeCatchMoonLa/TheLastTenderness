using UnityEngine;

namespace CatchMoon
{
    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyManager enemy);
    }
}