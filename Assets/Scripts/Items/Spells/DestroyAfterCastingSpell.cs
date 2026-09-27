using UnityEngine;

namespace CatchMoon
{
    public class DestroyAfterCastingSpell : MonoBehaviour
    {
        CharacterManager character;

        private void Awake()
        {
            character = GetComponentInParent<CharacterManager>();
        }

        private void Update()
        {
            if (!character.cCombat.isUsingSpell)
            {
                Destroy(gameObject);
            }
        }
    }
}
