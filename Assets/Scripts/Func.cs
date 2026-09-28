using UnityEngine;

namespace CatchMoon
{
    public static class OverlapQuery
    {
        // 装满说明这一下可能被截掉，加倍再查。平时不分配。
        public static int CollectOverlaps(Vector3 position, float radius, int layerMask, ref Collider[] results)
        {
            int count = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
            while (count == results.Length)
            {
                results = new Collider[results.Length * 2];
                count = Physics.OverlapSphereNonAlloc(position, radius, results, layerMask);
            }
            return count;
        }
    }

    static class TransformExtension
    {
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}
