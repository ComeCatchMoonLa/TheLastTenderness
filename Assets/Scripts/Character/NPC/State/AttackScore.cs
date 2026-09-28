using UnityEngine;

namespace CatchMoon
{
    public struct AttackWindow
    {
        public float MinDist;
        public float MaxDist;
        public float MinAngle;
        public float MaxAngle;
        public int Score;

        public bool Contains(float distance, float angle)
        {
            return distance <= MaxDist && distance >= MinDist
                && angle <= MaxAngle && angle >= MinAngle;
        }
    }

    public interface IAttackWindows
    {
        AttackWindow Window(int index);
    }

    public struct EnemyAttackWindows : IAttackWindows
    {
        public EnemyAttackAction[] Attacks;

        public AttackWindow Window(int index)
        {
            EnemyAttackAction attack = Attacks[index];
            return new AttackWindow
            {
                MinDist = attack.minDistNeededToAttack,
                MaxDist = attack.maxDistNeededToAttack,
                MinAngle = attack.minAttackAngle,
                MaxAngle = attack.maxAttackAngle,
                Score = attack.attackScore
            };
        }
    }

    public struct ItemAttackWindows : IAttackWindows
    {
        public ItemBasedAttackAction[] Attacks;

        public AttackWindow Window(int index)
        {
            ItemBasedAttackAction attack = Attacks[index];
            return new AttackWindow
            {
                MinDist = attack.minDistNeededToAttack,
                MaxDist = attack.maxDistNeededToAttack,
                MinAngle = attack.minAttackAngle,
                MaxAngle = attack.maxAttackAngle,
                Score = attack.attackScore
            };
        }
    }

    public static class AttackScore
    {
        public static int PickIndex<T>(int count, float distance, float angle, bool hasCurrent, bool abortIfHasCurrent, bool stopAfterAssign, T windows)
            where T : IAttackWindows
        {
            int maxScore = 0;
            for (int i = 0; i < count; ++i)
            {
                AttackWindow window = windows.Window(i);
                if (window.Contains(distance, angle))
                    maxScore += window.Score;
            }

            int randomValue = Random.Range(0, maxScore);
            return PickIndex(count, distance, angle, randomValue, hasCurrent, abortIfHasCurrent, stopAfterAssign, windows);
        }

        public static int PickIndex<T>(int count, float distance, float angle, int randomValue, bool hasCurrent, bool abortIfHasCurrent, bool stopAfterAssign, T windows)
            where T : IAttackWindows
        {
            int chosen = -1;
            int temporaryScore = 0;
            for (int i = 0; i < count; ++i)
            {
                AttackWindow window = windows.Window(i);
                if (!window.Contains(distance, angle))
                    continue;

                if (abortIfHasCurrent && hasCurrent)
                    return -1;

                temporaryScore += window.Score;
                if (temporaryScore > randomValue)
                {
                    chosen = i;
                    if (stopAfterAssign)
                        return chosen;
                }
            }

            return chosen;
        }
    }
}
