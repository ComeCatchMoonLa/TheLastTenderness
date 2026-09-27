using UnityEngine;

namespace CatchMoon
{
    public class ViewInfo : Interactable
    {
        [SerializeField] TextData textData;
        public override void Interact(PlayerManager player)
        {
            player.ui.popUps.viewInfoUI.SetText(textData);
            player.ui.popUps.viewInfoUI.PopUp();
            Destroy(gameObject);
        }
    }
}
