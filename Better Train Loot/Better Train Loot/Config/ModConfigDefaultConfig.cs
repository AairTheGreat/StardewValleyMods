namespace BetterTrainLoot.Config
{
    public static class ModConfigDefaultConfig
    {
        public static ModConfig CreateDefaultConfig(string file)
        {
            ModConfig config = new ModConfig()
            {
                enableMod = true,
                baseChancePercent = 0.275, // Base chance of getting an item   
                useCustomTrainTreasure = true,
                basePctChanceOfTrain = 0.15,
                trainCreateDelay = 10000  //Base Game Setting
            };

            BetterTrainLootMod.Instance.Helper.Data.WriteJsonFile(file, config);
            return config;
        }
    }
}
