using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName = "Item/Ammo")]
    public class AmmoItem : Item
    {
        public AmmoType ammoType;

        [Header("Ammo Velocity")]
        public float forwardVeclocity;
        public float upwardVeclocity;
        public float mass;
        public bool useGravity = false;

        [Header("Ammo Capacity")]
        public int maxCnt; // 数量上限
        public int cnt;

        [Header("Ammo Base Damage")]
        public int physicalDamage;
        //public int magicDamage;
        //public int fireDamage;
        //public int darkDamage;
        //public int lightDamage;

        [Header("Item Models")]
        public GameObject loadedItemModel; // 射出前的模型
        public GameObject liveItemModel;   // 射出后的模型
        public GameObject penetratedModel; // 射中后的模型
    }
}