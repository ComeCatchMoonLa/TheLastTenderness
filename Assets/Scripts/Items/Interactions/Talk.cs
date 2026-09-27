using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class Talk : Interactable
    {
        [Header("�Ի�����")]
        [SerializeField] EnemyManager npc;

        [Header("Player�Ի�ʱվ��λ��")]
        [SerializeField] Transform playerStandingPoint;

        [Header("�Ի�����")]
        public List<DialogueTextData> talkContent;

        public override void Interact(PlayerManager player)
        {
            npc.gameObject.tag = "Untagged";
            // ����Player��Transform
            player.rigidBody.linearVelocity = Vector3.zero;
            player.transform.position = playerStandingPoint.position;
            player.transform.LookAt(npc.transform, Vector3.up);
            // ����Player��UI
            player.ui.hud.Hide();
            player.ui.popUps.talkUI.PopUp();
            player.ui.popUps.talkUI.SetTalkContent(talkContent);
            // �����������
            player.input.inputActions.Disable();
            player.input.inputActions.Interacte.Enable();
            player.input.inputActions.Locomotion.CameraRotate.Enable();
            // �����Ի�(�����ı�������)
            player.storyNpc = npc;
            if (player.ui.escWin.GetSettingWin().gameSettingsData.auto)
            {
                if (player.ui.escWin.GetSettingWin().gameSettingsData.wordForWord)
                    player.ui.popUps.talkUI.Start_UpdateDialogue_Auto_WFW(npc);
                else
                    player.ui.popUps.talkUI.Start_UpdateDialogue_Auto_SBS(npc);
            }
            else
            {
                if (player.ui.escWin.GetSettingWin().gameSettingsData.wordForWord)
                    player.ui.popUps.talkUI.Start_UpdateDialogue_WFW_Helper(npc);
                else
                    player.ui.popUps.talkUI.UpdateDialogueSBS_Helper(npc);
            }
        }
    }
}