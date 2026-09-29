namespace CatchMoon
{
    public enum ItemType
    {
        equipment,
        spell,
        consumable,
    }
    public enum EquipmentType
    {
        weapon,
        armor,
    }
    public enum WeaponType
    {
        unarmed,
        melee_OH_DualWield,
        melee_OH_LH,
        melee_OH_RH,
        melee_OH_Shield,
        melee_TH,
        melee_THL,
        pyromancyCaster,
        faithCaster,
        spellCaster,
        bow,
    }
    public enum ArmorType
    {
        head,
        torso,
        hips,
    }
    public enum SpellType
    {
        miracle,
        pyromancy,
        sorcery,
    }
    public enum AmmoType
    {
        arrow,
    }
    public enum FlaskType
    {
        estus,
        ashen,
    }

    public enum EquipmentSlotType
    {
        weapon_RH_Slot_1,
        weapon_RH_Slot_2,
        weapon_LH_Slot_1,
        weapon_LH_Slot_2,
        armor_Head_Slot,
        armor_Torso_Slot,
        armor_Hips_Slot,
        weapon_RH_Slot_3,
        weapon_RH_Slot_4,
        weapon_LH_Slot_3,
        weapon_LH_Slot_4,
    }
    public enum AttackType
    {
        light_1,
        light_2,
        heavy_1,
        heavy_2,
        critical,
    }

    public enum CharacterType
    {
        player,
        npc,
    }
    public enum NPCType
    {
        combat,
        story,
    }
    public enum NPCCombatStyle
    {
        melee,
        archer,
    }

    public enum StateMachineBehaviourType
    {
        onStateEnter,
        onStateExit,
    }

    public enum ItemSlotType
    {
        leftHandSlot,
        rightHandSlot,
        backSlot,
    }

    public enum TapWinType
    {
        equipment,
        inventory,
        skill,
        map,
        status,
    }

    public enum SettingsWinType
    {
        gameSettings,
        display,
        sound,
        control,
    }
    public enum AchievementWinType
    {
        statistical,
        achievementCard,
    }

    public enum InventoryWinType
    {
        weapon,
        armor,
        spell,
        consumable,
    }

    // ESC窗口模式下, 打开的子窗口类型(即表示所处的UI层级，用于控制Back操作)
    public enum EscWinOpenedWinType
    {
        notEscWinMode, // 表示非ESC窗口模式
        escWin,
        settingsWin,
        achievementWin,
        achievementCard,
    }

    public enum ChangeLockOnTargetMode
    {
        nearest, // 最近
        minHP,   // 血量最低
    }
}