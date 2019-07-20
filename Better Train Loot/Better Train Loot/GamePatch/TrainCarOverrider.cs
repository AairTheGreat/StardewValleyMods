using BetterTrainLoot.Data;
using BetterTrainLoot.Framework;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BetterTrainLoot.GamePatch
{
   static class TrainCarOverrider
   {
        public static void postfix_getTrainTreasure(Vector2 globalPosition)
        {
            if (Game1.random.NextDouble() < 0.003 && ((double)globalPosition.X > 256.0 && (double)globalPosition.X < (double)(Game1.currentLocation.map.DisplayWidth - 256)))
            {                
                if (BetterTrainLootMod.Instance.trainType != TRAINS.UNKNOWN)
                {                    
                    CheckForTreasure(BetterTrainLootMod.Instance.trainType, globalPosition);
                }                
            }
        }

        private static void CheckForTreasure(TRAINS trainType, Vector2 globalPosition)
        {
            double chance = (trainType != TRAINS.PRESENT_TRAIN) ? BetterTrainLootMod.Instance.config.baseChancePercent + Game1.dailyLuck : 3.0 * BetterTrainLootMod.Instance.config.baseChancePercent + Game1.dailyLuck;

            if ((Game1.random.NextDouble() <= chance && BetterTrainLootMod.Instance.config.useCustomTrainTreasure && BetterTrainLootMod.numberOfRewardsPerTrain < BetterTrainLootMod.Instance.config.maxNumberOfItemsPerTrain)
                || BetterTrainLootMod.Instance.config.enableMaxTreasurePerTrain)
            {                
                Item reward = GetCustomTrainTreasure(trainType);

                if (reward.ParentSheetIndex != -1)
                {
                    Game1.createObjectDebris(reward.ParentSheetIndex, (int)globalPosition.X / 64, (int)globalPosition.Y / 64, (int)((double)globalPosition.Y + 320.0), 0, 1f, (GameLocation)null);
                    BetterTrainLootMod.numberOfRewardsPerTrain++;
                }
            }
        }

        private static Item GetCustomTrainTreasure(TRAINS index)
        {
            TrainData trainData = BetterTrainLootMod.Instance.trainCars[index];

            // Possible treasure based on selected treasure group selected above.
            List<TrainTreasure> possibleLoot = new List<TrainTreasure>(trainData.treasureList)
                .Where(loot => loot.Enabled && loot.IsValid())
                .OrderBy(loot => loot.Chance)
                .ThenBy(loot => loot.Id)
                .ToList();

            if (possibleLoot.Count == 0)
            {
                BetterTrainLootMod.Instance.Monitor.Log($"   Group: {trainData.TrainCarID}, No Possible Loot Found... check the logic");
            }

            TrainTreasure treasure = possibleLoot.ChooseItem(Game1.random);
            int id = treasure.Id;

            // Lost books have custom handling  -- No default lost books... but someone might configure them
            if (id == 102) // LostBook Item ID
            {
                if (Game1.player.archaeologyFound == null || !Game1.player.archaeologyFound.ContainsKey(102) || Game1.player.archaeologyFound[102][0] >= 21)
                {
                    possibleLoot.Remove(treasure);
                }
                Game1.showGlobalMessage("You found a lost book. The library has been expanded.");
            }

            Item reward;
            // Create reward item
            if (id >= 504 && id <= 515)
            {
                //This is boot
                reward = new Boots(id);  //Not sure if this is needed
            }
            if (id >= 516 && id <= 534)
            {
                //This is ring
                reward = new Ring(id);  //Not sure if this is needed
            }
            else
            {
                reward = (Item)new StardewValley.Object(id, 1);
            }
            
            return reward;
        }             
    }
}
