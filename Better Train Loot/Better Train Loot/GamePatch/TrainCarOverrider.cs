using BetterTrainLoot.Data;
using BetterTrainLoot.Framework;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Dimensions;

namespace BetterTrainLoot.GamePatch
{
   static class TrainCarOverrider
   {
        public static void postfix_getTrainTreasure(TrainCar __instance, Vector2 globalPosition)
        {
            if (__instance.loaded.Value > 0 && Game1.random.NextDouble() < 0.003 && ((double)globalPosition.X > 256.0 && (double)globalPosition.X < (double)(Game1.currentLocation.map.DisplayWidth - 256)))
            {
                BetterTrainLootMod.Instance.Monitor.Log($"postFix Running: G_Postion: {globalPosition.X}, {globalPosition.Y} loaded: {__instance.loaded.Value}  resourceType: {__instance.resourceType.Value}");
                if (BetterTrainLootMod.Instance.trainType != TRAINS.UNKNOWN)
                {
                    BetterTrainLootMod.Instance.Monitor.Log($"Train type: {BetterTrainLootMod.Instance.trainType}");
                    CheckForTreasure(BetterTrainLootMod.Instance.trainType, globalPosition);
                }
                --__instance.loaded.Value;
                //int objectIndex = -1;
                //switch (__instance.resourceType.Value)
                //{
                //    case 0:
                //        objectIndex = 74;
                //        break;
                //    case 1:
                //        objectIndex = 74; //(int)this.color.R > (int)this.color.G ? 378 : ((int)this.color.G > (int)this.color.B ? 380 : ((int)this.color.B > (int)this.color.R ? 384 : 378));
                //        break;
                //    case 2:
                //        objectIndex = 74;
                //        break;
                //    case 6:
                //        objectIndex = 74;
                //        break;
                //    case 7:
                //        objectIndex = 74; // Game1.currentSeason.Equals("winter") ? 536 : (Game1.stats.DaysPlayed <= 120U || (int)this.color.R <= (int)this.color.G ? 535 : 537);
                //        break;
                //}
                //if (objectIndex != -1)
                //    Game1.createObjectDebris(objectIndex, (int)globalPosition.X / 64, (int)globalPosition.Y / 64, (int)((double)globalPosition.Y + 320.0), 0, 1f, (GameLocation)null);
            }
        }

        private static void CheckForTreasure(TRAINS trainType, Vector2 globalPosition)
        {
            Item reward = GetCustomTrainTreasure(trainType);

            if (reward.ParentSheetIndex != -1)
                  Game1.createObjectDebris(reward.ParentSheetIndex, (int)globalPosition.X / 64, (int)globalPosition.Y / 64, (int)((double)globalPosition.Y + 320.0), 0, 1f, (GameLocation)null);


                //Random random = new Random((int)Game1.uniqueIDForThisGame / 2 + (int)Game1.stats.DaysPlayed + 777 + index + Game1.timeOfDay);
                //if (random.NextDouble() < BetterTrainLootMod.Instance.config.baseChancePercent + Game1.dailyLuck)
                //{
                //    if (BetterTrainLootMod.Instance.config.useCustomGarbageCanTreasure)
                //    {

                //        Item reward = GetCustomTrainTreasure((TRAIN_CARS)index);

                //        if (reward != null)
                //        {
                //            //player.addItemByMenuIfNecessary(reward, (ItemGrabMenu.behaviorOnItemSelect)null);

                //            //BetterTrainLootMod.Instance.Monitor.Log($"Got treasure from {(GARBAGE_CANS)index} - Game time of day: {Game1.timeOfDay}");
                //        }
                //    }
                //}
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

            // Lost books have custom handling  -- No default lost books... but someonw might configure them
            if (id == 102) // LostBook Item ID
            {
                if (Game1.player.archaeologyFound == null || !Game1.player.archaeologyFound.ContainsKey(102) || Game1.player.archaeologyFound[102][0] >= 21)
                {
                    possibleLoot.Remove(treasure);
                }
                Game1.showGlobalMessage("You found a lost book. The library has been expanded.");
            }

            // Create reward item
            Item reward;
            reward = (Item) new StardewValley.Object(id, 1);
            
            return reward;
        }       
    }
}
