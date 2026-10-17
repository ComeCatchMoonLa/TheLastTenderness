using TMPro;
using UnityEngine;

namespace CatchMoon
{
    public class StatusWinManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI body;

        public void Open()
        {
            gameObject.SetActive(true);
            if (body == null)
            {
                Debug.LogError("Status Window: body == null");
                return;
            }

            PlayerManager player = PlayerUIManager.FindPlayer(this);
            if (player == null || player.pStats == null)
            {
                Debug.LogError("Status Window: player.pStats == null");
                return;
            }

            PlayerStatsManager stats = player.pStats;
            int soulLevel = SoulLevel.From(
                stats.healthLevel,
                stats.focusLevel,
                stats.staminaLevel,
                stats.vitalityLevel,
                stats.strengthLevel,
                stats.dexterityLevel,
                stats.intelligenceLevel,
                stats.faithLevel,
                stats.luckLevel);
            body.text = StatusPageLines.Format(
                stats.healthLevel,
                stats.staminaLevel,
                stats.focusLevel,
                stats.poiseLevel,
                stats.strengthLevel,
                stats.dexterityLevel,
                stats.intelligenceLevel,
                stats.faithLevel,
                stats.soulCount) + "\n灵魂等级 " + soulLevel;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
