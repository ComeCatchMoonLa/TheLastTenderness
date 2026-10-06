using UnityEngine;

namespace CatchMoon
{
    public static class RecordedFire
    {
        public static bool TryMatch(string place, string[] names, Vector3[] at, int count, out Vector3 position)
        {
            position = default;
            if (string.IsNullOrEmpty(place) || names == null || at == null || count <= 0)
                return false;
            int n = count;
            if (n > names.Length) n = names.Length;
            if (n > at.Length) n = at.Length;
            for (int i = 0; i < n; i++)
            {
                if (names[i] != place) continue;
                position = at[i];
                return true;
            }
            return false;
        }

        public static void Move(Transform body, string place)
        {
            if (body == null) return;
            CampFireInteractable[] fires = Object.FindObjectsByType<CampFireInteractable>(FindObjectsInactive.Include);
            int n = fires.Length;
            string[] names = new string[n];
            Vector3[] at = new Vector3[n];
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (fires[i] == null) continue;
                names[count] = fires[i].name;
                at[count] = fires[i].transform.position;
                count++;
            }
            if (!TryMatch(place, names, at, count, out Vector3 position))
                return;
            body.position = position;
        }
    }
}
