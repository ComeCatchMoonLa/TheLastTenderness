using UnityEngine;

namespace CatchMoon
{
    abstract public class Interactable : MonoBehaviour
    {
        public string interactTipText;

        abstract public void Interact(PlayerManager player);
    }
}