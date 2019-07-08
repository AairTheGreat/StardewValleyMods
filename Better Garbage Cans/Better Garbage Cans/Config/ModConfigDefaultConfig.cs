﻿namespace BetterGarbageCans.Config
{
    public static class ModConfigDefaultConfig
    {
        public static ModConfig CreateDefaultConfig(string file)
        {
            ModConfig config = new ModConfig()
            {
                enableMod = true,
                allowMultipleItemsPerDay = true,
                allowGarbageCanRecheck = true,
                baseChancePercent = 0.25,   //5% more than Base Game 
                useCustomGarbageCanTreasure = true,
                FriendshipPoints = -25,     //Base Game 
                LinusFriendshipPoints = 5,  //Base Game 
                WaitTimeIfFoundNothing = 60,
                WaitTimeIfFoundSomething = 240,
                baseTrashChancePercent = 0.25,
                enableBirthdayGiftTrash = true,
                birthdayGiftChancePercent = 0.75
            };

            BetterGarbageCansMod.Instance.Helper.Data.WriteJsonFile(file, config);
            return config;
        }
    }
}
