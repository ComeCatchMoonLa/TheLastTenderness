using System.Collections;
using UnityEngine;

namespace CatchMoon
{
    public class OpenChest : Interactable
    {
        Animator animator;

        [SerializeField] Transform playerStandingPosition;
        public GameObject itemSpawner;
        public WeaponItem itemInChest;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            #region ¼ì´í
            if (animator == null)
            {
                Debug.LogError("");
                return;
            }
            #endregion
        }

        public override void Interact(PlayerManager playerManager)
        {
            playerManager.OpenChestInteracting(playerStandingPosition, transform.position);

            animator.Play("Chest Open");

            StartCoroutine(SpawnItemInChest());
            PickUpItem pickUpItem = itemSpawner.GetComponent<PickUpItem>();
            if (pickUpItem != null)
            {
                pickUpItem.item = itemInChest;
            }
        }

        private IEnumerator SpawnItemInChest()
        {
            yield return new WaitForSeconds(1f);
            GameObject item = Instantiate(itemSpawner, transform);
            item.transform.localPosition = new Vector3(0f, 0.5f, 0f);

            gameObject.tag = "Untagged";
            gameObject.layer = Layer.environment;
            Destroy(this);
        }
    }
}