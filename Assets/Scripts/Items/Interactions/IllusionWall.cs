using UnityEngine;

namespace CatchMoon
{
    public class IllusionWall : MonoBehaviour
    {
        public bool opened;

        public void Strike()
        {
            if (opened) return;
            opened = true;
            Collider[] cols = GetComponents<Collider>();
            for (int i = 0; i < cols.Length; i++)
                cols[i].enabled = false;
            Renderer[] renderers = GetComponents<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
                renderers[i].enabled = false;
        }

        public void NotifyContact(bool rolling)
        {
            if (rolling)
                Strike();
        }

        // 休息不重载这面墙。入口留着，避免休息路径把 opened 写成 false。
        public void AfterRest()
        {
        }

        void OnCollisionEnter(Collision collision)
        {
            CharacterManager character = collision.collider.GetComponent<CharacterManager>();
            if (character == null)
                character = collision.collider.GetComponentInParent<CharacterManager>();
            if (character == null) return;
            NotifyContact(character.isRolling);
        }
    }
}
