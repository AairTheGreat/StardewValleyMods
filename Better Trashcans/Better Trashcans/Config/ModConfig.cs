namespace BetterTrashcans.Config
{
    public class ModConfig
    {
        public bool enableMod { get; set; }
        public bool useCustomTrashcanTreasure { get; set; }
        public bool allowMultipleItemsPerDay { get; set; }
        public bool allowTrashCanRecheck { get; set; }
        public double baseChancePercent { get; set; }
        public int FriendshipPoints { get; set; }
        public int LinusFriendshipPoints { get; set; }
        public int WaitTimeIfFoundNothing { get; set; }
        public int WaitTimeIfFoundSomething { get; set; }
        
    }
}
