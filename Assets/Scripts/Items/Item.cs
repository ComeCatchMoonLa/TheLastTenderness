using UnityEngine;

namespace CatchMoon
{
    public class Item : ScriptableObject
    {
        [Header("物品类型")]
        public ItemType itemType;
        public bool isRing;
        public bool isCovenantMark;
        public bool isKey;
        [Header("物品信息")]
        public Sprite itemIcon;
        public string itemName;
        [Header("item在Hierarchy窗口中的名称")]
        [Tooltip("用于切换模型")] public string transformName;
    }
}