namespace BetterTrainLoot.Config
{
    public static class ModConfigDefaultConfig
    {
        public static ModConfig CreateDefaultConfig(string file)
        {
            ModConfig config = new ModConfig()
            {
                enableMod = true,
                baseChancePercent = 0.20, // Base chance of getting an item   
                useCustomTrainTreasure = true,
                enableMaxTreasurePerTrain = false,
                basePctChanceOfTrain = 0.15,
                maxNumberOfItemsPerTrain = 5,
                trainCreateDelay = 10000  //Base Game Setting
            };

            BetterTrainLootMod.Instance.Helper.Data.WriteJsonFile(file, config);
            return config;
        }
    }
}
