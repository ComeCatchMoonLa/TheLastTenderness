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
            bool stolen = Usurpation.Holds(stolenFlame, player.pStats.darkSigilsCleared);
            string ending = EndingChoice.Choose(stolen, eyesGiven, keeperSignUsed);
            player.pStats.RecordEnding(ending);
        }
    }
}
