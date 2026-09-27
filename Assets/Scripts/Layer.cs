namespace CatchMoon
{
    public struct Layer
    {
        public const int defaultLayer = 0;
        public const int interactable = 7;
        public const int environment = 8;
        public const int npc = 9;
        public const int characterCollisionBlocker =  10;
        public const int player = 11;
    }

    public struct LayerMask
    {
        public const int defaultLayerMask = 1 << Layer.defaultLayer;
        public const int environment = 1 << Layer.environment;
        public const int npc = 1 << Layer.npc;
        public const int characterCollisionBlocker = 1 << Layer.characterCollisionBlocker;
        public const int player = 1 << Layer.player;
        public const int interactable = 1 << Layer.interactable;
    }
}