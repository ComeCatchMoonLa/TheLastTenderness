using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class Talk : Interactable
    {
        [Header("对话对象")]
        [SerializeField] EnemyManager npc;

        [Header("Player对话时站的位置")]
        [SerializeField] Transform playerStandingPoint;

        [Header("对话内容")]
        public List<DialogueTextData> talkContent;
        public bool offersFlaskSplit;

        public Item wantedGift;
        public bool giftAdvanced;

        public bool Give(PlayerManager player, Item offered)
        {
            if (player == null || player.pInventory == null) return false;
            if (!GiftExchange.CanTake(player.pInventory.items, wantedGift, offered, giftAdvanced))
                return false;
            player.pInventory.RemoveItem(offered);
            giftAdvanced = true;
            return true;
        }

        public bool AllocateFlasks(PlayerManager player, int estus, int ash)
        {
            if (!offersFlaskSplit || player == null || player.pInventory == null) return false;
            return player.pInventory.TryAllocateFlasks(estus, ash);
        }

        public override void Interact(PlayerManager player)
        {
            npc.gameObject.tag = "Untagged";
            // 设置Player的Transform
            player.rigidBody.linearVelocity = Vector3.zero;
            player.transform.position = playerStandingPoint.position;
            player.transform.LookAt(npc.transform, Vector3.up);
            // 设置Player的UI
            player.ui.hud.Hide();
            player.ui.popUps.talkUI.PopUp();
            player.ui.popUps.talkUI.SetTalkContent(talkContent);
            // 禁用玩家输入
            player.input.inputActions.Disable();
            player.input.inputActions.Interacte.Enable();
            player.input.inputActions.Locomotion.CameraRotate.Enable();
            // 处理对话(更新文本及动画)
            player.storyNpc = npc;
            player.ui.popUps.talkUI.AdvanceDialogue(npc, true);
        }
    }
}