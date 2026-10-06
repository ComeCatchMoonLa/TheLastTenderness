using UnityEngine;

namespace CatchMoon
{
    public class PlayerArmorManager : MonoBehaviour
    {
        PlayerManager player;

        HeadModelChanger headModelChanger;
        TorsoModelChanger torsoModelChanger;
        HipsModelChanger hipModelChanger;

        [Header("默认赤裸模型")]
        [SerializeField] string nakedTorsoModelName;
        [SerializeField] string nakedHipsModelName;

        [Header("当前盔甲")]
        public HeadArmorItem currentHeadArmor;     // 当前头部铠甲
        public TorsoArmorItem currentTorsoArmor;   // 当前躯干铠甲
        public HandArmorItem currentHandArmor;
        public HipsArmorItem currentHipsArmor;     // 当前下身铠甲 

        private void Awake()
        {
            player = GetComponent<PlayerManager>();

            headModelChanger = GetComponentInChildren<HeadModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipsModelChanger>();

            if (!headModelChanger || !torsoModelChanger || !hipModelChanger) Debug.LogError("null");
        }

        private void Start()
        {
            EquipAllArmorModels();
        }

        public void EquipAllArmorModels()
        {
            EquipPart(headModelChanger, currentHeadArmor, null,
                ref player.pStats.headArmorPDA, ref player.pStats.headArmorFDA, ref player.pStats.headArmorMDA,
                ref player.pStats.headArmorLDA, ref player.pStats.headArmorDDA);
            EquipPart(torsoModelChanger, currentTorsoArmor, nakedTorsoModelName,
                ref player.pStats.torsoArmorPDA, ref player.pStats.torsoArmorFDA, ref player.pStats.torsoArmorMDA,
                ref player.pStats.torsoArmorLDA, ref player.pStats.torsoArmorDDA);
            EquipPart(hipModelChanger, currentHipsArmor, nakedHipsModelName,
                ref player.pStats.hipsArmorPDA, ref player.pStats.hipsArmorFDA, ref player.pStats.hipsArmorMDA,
                ref player.pStats.hipsArmorLDA, ref player.pStats.hipsArmorDDA);
        }

        void EquipPart(ModelChanger changer, ArmorItem armor, string nakedModelName,
            ref float pda, ref float fda, ref float mda, ref float lda, ref float dda)
        {
            changer.UnEquipAllModels();
            if (armor != null)
            {
                changer.EquipModelByName(armor.transformName);
                pda = armor.physicalDA;
                fda = armor.fireDA;
                mda = armor.magicDA;
                lda = armor.lightningDA;
                dda = armor.darkDA;
                return;
            }

            if (!string.IsNullOrEmpty(nakedModelName))
                changer.EquipModelByName(nakedModelName);
            pda = 0;
            fda = 0;
            mda = 0;
            lda = 0;
            dda = 0;
        }
    }
}