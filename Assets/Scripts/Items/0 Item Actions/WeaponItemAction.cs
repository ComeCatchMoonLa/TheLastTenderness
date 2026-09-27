using UnityEngine;

namespace CatchMoon
{
    abstract public class WeaponItemAction : ScriptableObject
    {
        public abstract void PerformAction(CharacterManager character);
    }
}