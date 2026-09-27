using UnityEngine;

namespace CatchMoon
{
    public class PlayerArmorManager : MonoBehaviour
    {
        PlayerManager player;

        HeadModelChanger headModelChanger;
        TorsoModelChanger torsoModelChanger;
        HipModelChanger hipModelChanger;

        [Header("默认赤裸模型")]
        [SerializeField] string nakedTorsoModelName;
        [SerializeField] string nakedHipsModelName;

        [Header("当前盔甲")]
        public HeadArmorItem currentHeadArmor;     // 当前头部铠甲
        public TorsoArmorItem currentTorsoArmor;   // 当前躯干铠甲
        public HipsArmorItem currentHipsArmor;     // 当前下身铠甲 

        private void Awake()
        {
            player = GetComponent<PlayerManager>();

            headModelChanger = GetComponentInChildren<HeadModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipModelChanger>();

            if (!headModelChanger || !torsoModelChanger || !hipModelChanger) Debug.LogError("null");
        }

        private void Start()
        {
            EquipAllArmorModels();
        }

        public void EquipAllArmorModels()
        {
            // Head 盔甲
            headModelChanger.UnEquipAllModels();
            if (currentHeadArmor != null)
            {
                headModelChanger.EquipModelByName(currentHeadArmor.transformName);
                player.pStats.headArmorPDA = currentHeadArmor.physicalDA;
            }
            else
            {
                player.pStats.headArmorPDA = 0;
            }
            // Torso 盔甲
            torsoModelChanger.UnEquipAllModels();
            if (currentTorsoArmor != null)
            {
                torsoModelChanger.EquipModelByName(currentTorsoArmor.transformName);
                player.pStats.torsoArmorPDA = currentTorsoArmor.physicalDA;
            }
            else
            {
                torsoModelChanger.EquipModelByName(nakedTorsoModelName);
                player.pStats.torsoArmorPDA = 0;
            }
            // Hip 盔甲
            hipModelChanger.UnEquipAllModels();
            if (currentHipsArmor != null)
            {
                hipModelChanger.EquipModelByName(currentHipsArmor.transformName);
                player.pStats.hipsArmorPDA = currentHipsArmor.physicalDA;
            }
            else
            {
                hipModelChanger.EquipModelByName(nakedHipsModelName);
                player.pStats.hipsArmorPDA = 0;
            }
        }
    }
}