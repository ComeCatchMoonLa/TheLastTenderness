using UnityEngine;

namespace CatchMoon
{
    [CreateAssetMenu(menuName ="A.I/Actions/Simle A.I Actions/Attack Action")]
    public class EnemyAttackAction : EnemyActions
    {
        public bool canCombo;
        public EnemyAttackAction comboAction;

        public int attackScore = 3;
        public float recoveryTime = 2f;

        public float maxAttackAngle = 35f;
        public float minAttackAngle = -35f;

        public float maxDistNeededToAttack = 1f;
        public float minDistNeededToAttack = 0f;
    }
}