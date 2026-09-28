using System;
using UnityEngine;

namespace CatchMoon
{
    [Serializable]
    public struct FlaskSipRow
    {
        public int health;
        public int focus;
    }

    [CreateAssetMenu(menuName = "Item/Consumable/Flask Recovery Table")]
    public class FlaskRecoveryTable : ScriptableObject
    {
        public FlaskSipRow[] rows;
    }
}
