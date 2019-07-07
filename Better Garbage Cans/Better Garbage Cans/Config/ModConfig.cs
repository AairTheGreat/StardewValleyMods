﻿namespace BetterGarbageCans.Config
{
    public class ModConfig
    {
        public bool enableMod { get; set; }
        public bool useCustomGarbageCanTreasure { get; set; }
        public bool allowMultipleItemsPerDay { get; set; }
        public bool allowGarbageCanRecheck { get; set; }
        public double baseChancePercent { get; set; }
        public int FriendshipPoints { get; set; }
        public int LinusFriendshipPoints { get; set; }
        public int WaitTimeIfFoundNothing { get; set; }
        public int WaitTimeIfFoundSomething { get; set; }
        
    }
}