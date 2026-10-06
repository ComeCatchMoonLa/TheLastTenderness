namespace CatchMoon
{
    public class MessageMark : Interactable
    {
        public string body;
        public bool rated;

        public override void Interact(PlayerManager player)
        {
            OnlineMarksRoot root = GetComponentInParent<OnlineMarksRoot>();
            bool online = root != null && root.online;
            if (!OnlineMarks.TryRead(name, online, body, out string read)) return;
            interactTipText = read;
            if (OnlineMarks.TryRate(online, rated)) rated = true;
        }
    }
}
