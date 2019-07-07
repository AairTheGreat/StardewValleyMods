namespace BetterTrashcans.Config
{
    public static class ModConfigDefaultConfig
    {
        public static ModConfig CreateDefaultConfig(string file)
        {
            ModConfig config = new ModConfig()
            {
                enableMod = true,
                allowMultipleItemsPerDay = true,
                allowTrashCanRecheck = true,
                baseChancePercent = 0.20,   //Base Game 
                useCustomTrashcanTreasure = true,
                FriendshipPoints = -25,     //Base Game 
                LinusFriendshipPoints = 5,  //Base Game 
                WaitTimeIfFoundNothing = 60,
                WaitTimeIfFoundSomething = 240
            };

            BetterTrashcansMod.Instance.Helper.Data.WriteJsonFile(file, config);
            return config;
        }
    }
}
