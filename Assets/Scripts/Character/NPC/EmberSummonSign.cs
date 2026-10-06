using UnityEngine;

namespace CatchMoon
{
    public class EmberSummonSign : MonoBehaviour
    {
        public GameObject helper;

        public bool TrySummon(bool emberLit)
        {
            if (helper == null)
            {
                Debug.LogError($"{name}: helper 未填");
                return false;
            }
            bool appears = EmberSummon.Appears(emberLit, true);
            helper.SetActive(appears);
            return appears;
        }

        public bool TrySummon(bool emberLit, int hostTeam)
        {
            if (!TrySummon(emberLit)) return false;
            CharacterStatsManager stats = helper.GetComponent<CharacterStatsManager>();
            if (stats != null)
                stats.teamID = PhantomTeam.Team(false, hostTeam, stats.teamID);
            return true;
        }
    }
}
