namespace CatchMoon
{
    public static class SettingsSubWindowNav
    {
        public static SettingsWinType Left(SettingsWinType current)
        {
            switch (current)
            {
                case SettingsWinType.gameSettings:
                    return SettingsWinType.control;
                case SettingsWinType.display:
                    return SettingsWinType.gameSettings;
                case SettingsWinType.sound:
                    return SettingsWinType.display;
                default:
                    return SettingsWinType.sound;
            }
        }

        public static SettingsWinType Right(SettingsWinType current)
        {
            switch (current)
            {
                case SettingsWinType.gameSettings:
                    return SettingsWinType.display;
                case SettingsWinType.display:
                    return SettingsWinType.sound;
                case SettingsWinType.sound:
                    return SettingsWinType.control;
                default:
                    return SettingsWinType.gameSettings;
            }
        }
    }
}
