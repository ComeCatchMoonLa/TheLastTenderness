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
            WriteHandAbsorption();
            WritePhysicalKinds();
            WriteArmorPoise();
        }

        public void RefreshDurabilityFactors()
        {
            player.pStats.headDurabilityFactor = DurabilityTier.Piece(currentHeadArmor);
            player.pStats.torsoDurabilityFactor = DurabilityTier.Piece(currentTorsoArmor);
            player.pStats.hipsDurabilityFactor = DurabilityTier.Piece(currentHipsArmor);
            player.pStats.handDurabilityFactor = DurabilityTier.Piece(currentHandArmor);
        }

        public void WearOnHit()
        {
            DurabilityTier.ArmorHit(currentHeadArmor);
            DurabilityTier.ArmorHit(currentTorsoArmor);
            DurabilityTier.ArmorHit(currentHipsArmor);
            DurabilityTier.ArmorHit(currentHandArmor);
            RefreshDurabilityFactors();
        }

        void WriteArmorPoise()
        {
            float head = currentHeadArmor != null ? currentHeadArmor.poise : 0f;
            float torso = currentTorsoArmor != null ? currentTorsoArmor.poise : 0f;
            float hips = currentHipsArmor != null ? currentHipsArmor.poise : 0f;
            float hand = currentHandArmor != null ? currentHandArmor.poise : 0f;
            player.pStats.armorPoiseBonus = ArmorPoise.FromPieces(head, torso, hips, hand);
            if (!player.pStats.attackPoiseActive)
                player.pStats.totalPoiseDefence = player.pStats.armorPoiseBonus;
        }

        void WritePhysicalKinds()
        {
            WriteKind(PhysicalKind.Strike, out float head, out float torso, out float hips, out float hand);
            player.pStats.headArmorStrike = head;
            player.pStats.torsoArmorStrike = torso;
            player.pStats.hipsArmorStrike = hips;
            player.pStats.handArmorStrike = hand;
            WriteKind(PhysicalKind.Slash, out head, out torso, out hips, out hand);
            player.pStats.headArmorSlash = head;
            player.pStats.torsoArmorSlash = torso;
            player.pStats.hipsArmorSlash = hips;
            player.pStats.handArmorSlash = hand;
            WriteKind(PhysicalKind.Thrust, out head, out torso, out hips, out hand);
            player.pStats.headArmorThrust = head;
            player.pStats.torsoArmorThrust = torso;
            player.pStats.hipsArmorThrust = hips;
            player.pStats.handArmorThrust = hand;
        }

        void WriteKind(PhysicalKind kind, out float head, out float torso, out float hips, out float hand)
        {
            head = ArmorPhysical.Of(currentHeadArmor, kind);
            torso = ArmorPhysical.Of(currentTorsoArmor, kind);
            hips = ArmorPhysical.Of(currentHipsArmor, kind);
            hand = ArmorPhysical.Of(currentHandArmor, kind);
        }

        void WriteHandAbsorption()
        {
            if (currentHandArmor != null)
            {
                player.pStats.handArmorPDA = currentHandArmor.physicalDA;
                player.pStats.handArmorFDA = currentHandArmor.fireDA;
                player.pStats.handArmorMDA = currentHandArmor.magicDA;
                player.pStats.handArmorLDA = currentHandArmor.lightningDA;
                player.pStats.handArmorDDA = currentHandArmor.darkDA;
                return;
            }

            player.pStats.handArmorPDA = 0f;
            player.pStats.handArmorFDA = 0f;
            player.pStats.handArmorMDA = 0f;
            player.pStats.handArmorLDA = 0f;
            player.pStats.handArmorDDA = 0f;
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