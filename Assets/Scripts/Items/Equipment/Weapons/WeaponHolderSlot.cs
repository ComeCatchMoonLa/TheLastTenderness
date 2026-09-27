using UnityEngine;

namespace CatchMoon
{
    /// <summary>
    /// 武器装备槽
    /// </summary>
    public class WeaponHolderSlot : MonoBehaviour
    {
        public ItemSlotType slotType;

        public Transform overrideParentWhileHolding; // 重载父节点, 使左右手武器能有不同的transform而不需要用两个perfab
        public Transform overrideParentWhileGetting; // 重载父节点, 使左右手武器能有不同的transform而不需要用两个perfab
        public WeaponItem currentWeapon;             // 当前武器
        public GameObject currentWeaponModel;        // 当前武器模型

        private void Awake()
        {
            #region 检错
            if (overrideParentWhileHolding == null)
                Debug.LogWarning($"{slotType}: overrideParentWhileHolding is null.");
            if (overrideParentWhileGetting == null)
                Debug.LogWarning($"{slotType}: overrideParentWhileGetting is null.");
            #endregion
        }

        /// <summary>
        /// 卸下武器
        /// </summary>
        public void UnloadWeapon()
        {
            if (currentWeaponModel == null) return;

            currentWeaponModel.SetActive(false);
        }

        /// <summary>
        /// 销毁武器
        /// </summary>
        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel == null) return;
            
            Destroy(currentWeaponModel);
            currentWeaponModel = null;
        }

        /// <summary>
        /// 加载武器模型
        /// </summary>
        /// <param name="weaponItem">武器预制件</param>
        public bool LoadWeaponModel(WeaponItem weaponItem)
        {
            if (weaponItem == null || weaponItem.modelPrefab == null)
            {
                Debug.LogError("model is null.");
                return false;
            }

            UnloadWeaponAndDestroy();

            GameObject model = Instantiate(weaponItem.modelPrefab);
            model.transform.parent = overrideParentWhileHolding;
            // 设置父节点后transform会变, 故重置其transform
            model.transform.ResetLocal();
            // 指定当前模型
            currentWeaponModel = model;
            return true;
        }
    }
}