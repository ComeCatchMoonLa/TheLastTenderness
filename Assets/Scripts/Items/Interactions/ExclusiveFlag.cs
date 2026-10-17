namespace CatchMoon
{
    public static class ExclusiveFlag
    {
        public static void Raise(ref bool raised, ref bool otherOpen, ref bool otherFailed)
        {
            raised = true;
            otherOpen = false;
            otherFailed = true;
        }

        public static bool CanTalk(bool open, bool failed)
        {
            return open && !failed;
        }
    }
}
