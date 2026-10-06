namespace CatchMoon
{
    public class EndingMarker : Interactable
    {
        public bool stolenFlame;
        public bool eyesGiven;
        public bool keeperSignUsed;

        public override void Interact(PlayerManager player)
        {
            if (player == null || player.pStats == null) return;
            string ending = EndingChoice.Choose(stolenFlame, eyesGiven, keeperSignUsed);
            player.pStats.RecordEnding(ending);
        }
    }
}
