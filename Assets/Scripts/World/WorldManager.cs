using UnityEngine;

namespace CatchMoon
{
    public class WorldManager : MonoBehaviour
    {
        public PlayerManager player;

        public WorldEventManager wEvent;
        public WorldUIManager wUI;

        private void Awake()
        {
            player = FindObjectOfType<PlayerManager>();

            wEvent = GetComponentInChildren<WorldEventManager>();
            wUI = GetComponentInChildren<WorldUIManager>();
        }
    }
}