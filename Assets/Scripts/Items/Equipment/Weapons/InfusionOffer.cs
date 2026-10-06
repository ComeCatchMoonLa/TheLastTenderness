namespace CatchMoon
{
    public static class InfusionOffer
    {
        public static bool CanSelect(string requiredCoal, string[] handedIn)
        {
            if (requiredCoal == null || requiredCoal.Length == 0) return true;
            if (handedIn == null) return false;
            for (int i = 0; i < handedIn.Length; i++)
            {
                if (handedIn[i] == requiredCoal) return true;
            }
            return false;
        }
    }
}
