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
                player.pStats.headArmorFDA = currentHeadArmor.fireDA;
                player.pStats.headArmorMDA = currentHeadArmor.magicDA;
                player.pStats.headArmorLDA = currentHeadArmor.lightningDA;
                player.pStats.headArmorDDA = currentHeadArmor.darkDA;
            }
            else
            {
                player.pStats.headArmorPDA = 0;
                player.pStats.headArmorFDA = 0;
                player.pStats.headArmorMDA = 0;
                player.pStats.headArmorLDA = 0;
                player.pStats.headArmorDDA = 0;
            }
            // Torso 盔甲
            torsoModelChanger.UnEquipAllModels();
            if (currentTorsoArmor != null)
            {
                torsoModelChanger.EquipModelByName(currentTorsoArmor.transformName);
                player.pStats.torsoArmorPDA = currentTorsoArmor.physicalDA;
                player.pStats.torsoArmorFDA = currentTorsoArmor.fireDA;
                player.pStats.torsoArmorMDA = currentTorsoArmor.magicDA;
                player.pStats.torsoArmorLDA = currentTorsoArmor.lightningDA;
                player.pStats.torsoArmorDDA = currentTorsoArmor.darkDA;
            }
            else
            {
                torsoModelChanger.EquipModelByName(nakedTorsoModelName);
                player.pStats.torsoArmorPDA = 0;
                player.pStats.torsoArmorFDA = 0;
                player.pStats.torsoArmorMDA = 0;
                player.pStats.torsoArmorLDA = 0;
                player.pStats.torsoArmorDDA = 0;
            }
            // Hip 盔甲
            hipModelChanger.UnEquipAllModels();
            if (currentHipsArmor != null)
            {
                hipModelChanger.EquipModelByName(currentHipsArmor.transformName);
                player.pStats.hipsArmorPDA = currentHipsArmor.physicalDA;
                player.pStats.hipsArmorFDA = currentHipsArmor.fireDA;
                player.pStats.hipsArmorMDA = currentHipsArmor.magicDA;
                player.pStats.hipsArmorLDA = currentHipsArmor.lightningDA;
                player.pStats.hipsArmorDDA = currentHipsArmor.darkDA;
            }
            else
            {
                hipModelChanger.EquipModelByName(nakedHipsModelName);
                player.pStats.hipsArmorPDA = 0;
                player.pStats.hipsArmorFDA = 0;
                player.pStats.hipsArmorMDA = 0;
                player.pStats.hipsArmorLDA = 0;
                player.pStats.hipsArmorDDA = 0;
            }
        }
    }
}