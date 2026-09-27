using UnityEngine;

namespace CatchMoon
{
    public class AmmoDamageCollider : DamageCollider
    {
        public AmmoItem ammoItem;
        bool hasAlreadyPanetratedASurface; // 打了某个表面上
        GameObject penetratedModel;        // 打在某个表面后的模型(破损了)

        protected override void OnTriggerEnter(Collider collision)
        {
            base.OnTriggerEnter(collision);

            if (collision.isTrigger)
            {
                //if (collision.tag == someTag)
                //{
                //    Debug.Log("触发对应事件");
                //    Destroy(transform.root.gameObject);
                //}
            }
            else
            {
                if (collision.tag == "Player" || collision.tag == "Enemy") // 攻击角色
                {
                    CharacterManager damageTarget = collision.GetComponent<CharacterManager>();
                    if (damageTarget == null)
                    {
                        Debug.LogError("PlayerManager is null.");
                        return;
                    }

                    if (character == damageTarget || damageTarget.cStats.teamID == teamID) return;

                    if (!damageTarget.cStats.isInvulnerable)
                        CreatePenetratedAmmo(collision);
                }
                else
                {
                    CreatePenetratedAmmo(collision);
                }

                Destroy(transform.root.gameObject);
            }
        }
    
        void CreatePenetratedAmmo(Collider collision)
        {
            if (!hasAlreadyPanetratedASurface && penetratedModel == null)
            {
                hasAlreadyPanetratedASurface = true;
                GameObject penetratedAmmo = Instantiate(ammoItem.penetratedModel);
                penetratedAmmo.transform.position = collision.ClosestPointOnBounds(transform.position);
                penetratedAmmo.transform.rotation = Quaternion.LookRotation(new Vector3(transform.forward.x / collision.transform.localScale.x,
                    transform.forward.y / collision.transform.localScale.y, transform.forward.z / collision.transform.localScale.z));
                penetratedAmmo.transform.localScale = Vector3.one;
                penetratedAmmo.transform.parent = collision.transform;
                penetratedModel = penetratedAmmo;
            }
        }
    }
}
