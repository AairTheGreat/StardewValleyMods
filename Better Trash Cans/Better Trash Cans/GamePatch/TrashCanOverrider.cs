using BetterTrashcans.Data;
using BetterTrashcans.Framework;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Dimensions;

namespace BetterTrashcans.GamePatch
{
   static class TrashcanOverrider
   {
        public static void prefix_betterTrashCans(Town __instance, Location tileLocation, ref Farmer who, ref IList<bool> ___garbageChecked) 
        {
            if (__instance.map.GetLayer("Buildings").Tiles[tileLocation] != null)
            {
                if (__instance.map.GetLayer("Buildings").Tiles[tileLocation].TileIndex == 78)
                {
                    string str = __instance.doesTileHaveProperty(tileLocation.X, tileLocation.Y, "Action", "Buildings");
                    int index;
                    if (str == null)
                        index = -1;
                    else
                        index = Convert.ToInt32(str.Split(' ')[1]);
                    
                    if (index >= 0 && index < ___garbageChecked.Count)
                    {
                        if (CanCheckTrashcan((TRASHCANS)index))
                        {                            
                            BetterTrashcansMod.Instance.trashcans[(TRASHCANS)index].LastTimeChecked = Game1.timeOfDay;
                            BetterTrashcansMod.Instance.Monitor.Log($"Checked {(TRASHCANS)index} - Game time of day: {Game1.timeOfDay}");

                            ___garbageChecked[index] = true;
                            __instance.playSound("trashcan");

                            CheckForNPCMessages(__instance, tileLocation, ref who);
                            CheckForTreasure(index, ref who);
                        }
                    }
                }
            }
        }

        private static void CheckForTreasure(int index, ref Farmer player)
        {
            Random random = new Random((int)Game1.uniqueIDForThisGame / 2 + (int)Game1.stats.DaysPlayed + 777 + index + Game1.timeOfDay);
            if (random.NextDouble() < BetterTrashcansMod.Instance.config.baseChancePercent + Game1.dailyLuck)
            {
                Item reward = GetTreasure(index, random);

                if (reward != null)
                {
                    player.addItemByMenuIfNecessary(reward, (ItemGrabMenu.behaviorOnItemSelect)null);
                    BetterTrashcansMod.Instance.trashcans[(TRASHCANS)index].LastTimeFoundItem = Game1.timeOfDay;
                    BetterTrashcansMod.Instance.Monitor.Log($"Got treasure from {(TRASHCANS)index} - Game time of day: {Game1.timeOfDay}");
                }
            }
        }

        private static bool CanCheckTrashcan(TRASHCANS can)
        {
            bool canCheckForItems = false;

            //Has the trashcan been checked today?
            if (BetterTrashcansMod.Instance.trashcans[can].LastTimeChecked == -1) //LastTimeChecked defaults per day to -1
            {
                // You have never checked that can...
                canCheckForItems = true;
            }
            else if (BetterTrashcansMod.Instance.config.allowTrashCanRecheck) //You have check the trashcan before.... and you can recheck it
            {
                //See if enough time has pasted since last check
                if (CanCheckBasedOnLastCheckTime(can))
                {                    
                    if (BetterTrashcansMod.Instance.trashcans[can].LastTimeFoundItem == -1)
                    {
                        //Enough time has passed, and never found anything...
                        canCheckForItems = true;
                    } 
                    else if (BetterTrashcansMod.Instance.config.allowMultipleItemsPerDay) // So you have found at least something before
                    {
                        //See if enough time has pasted since last found item
                        canCheckForItems = CanCheckBasedOnLastFoundTime(can);
                    }
                }
            }

            return canCheckForItems;
        }

        private static bool CanCheckBasedOnLastCheckTime(TRASHCANS can)
        {
            return Game1.timeOfDay >= GetWaitTime(BetterTrashcansMod.Instance.trashcans[can].LastTimeChecked,
                        BetterTrashcansMod.Instance.config.WaitTimeIfFoundNothing);
        }

        private static bool CanCheckBasedOnLastFoundTime(TRASHCANS can)
        {
            return Game1.timeOfDay >= GetWaitTime(BetterTrashcansMod.Instance.trashcans[can].LastTimeFoundItem,
                BetterTrashcansMod.Instance.config.WaitTimeIfFoundSomething);
        }

        private static int GetWaitTime(int time, int addMinutes)
        {
            int hours = time / 100;
            int minutes = (time % 100) + addMinutes;

            return ((hours + minutes / 60) * 100) + (minutes % 60);
        }

        private static void CheckForNPCMessages(GameLocation location, Location tileLocation, ref Farmer player)
        {
            bool changedFrienship = false;
            Character character = Utility.isThereAFarmerOrCharacterWithinDistance(new Vector2((float)tileLocation.X, (float)tileLocation.Y), 7, location);
            if (character != null && character is NPC && !(character is Horse))
            {
                BetterTrashcansMod.SendMulitplayerMessage("TrashCan", Game1.player.Name, character.Name);

                if (character.Name.Equals("Linus"))
                {
                    character.doEmote(32, true);
                    (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Linus"), true, true);
                    player.changeFriendship(BetterTrashcansMod.Instance.config.LinusFriendshipPoints, character as NPC);
                    changedFrienship = true;
                    BetterTrashcansMod.SendMulitplayerMessage("LinusTrashCan");
                }
                else if ((character as NPC).Age == 2)
                {
                    character.doEmote(28, true);
                    (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Child"), true, true);                    
                }
                else if ((character as NPC).Age == 1)
                {
                    character.doEmote(8, true);
                    (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Teen"), true, true);                
                }
                else
                {
                    character.doEmote(12, true);
                    (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Adult"), true, true);                
                }
                if (!changedFrienship)
                {
                    player.changeFriendship(BetterTrashcansMod.Instance.config.FriendshipPoints, character as NPC);
                }

                Game1.drawDialogue(character as NPC);
            }
        }

        private static Item GetCustomTrashTreasure(TRASHCANS index)
        {
            Trashcan trashcan = BetterTrashcansMod.Instance.trashcans[index];

            // Possible treasure based on selected treasure group selected above.
            List<TrashTreasure> possibleLoot = new List<TrashTreasure>(trashcan.treasureList)
                .Where(loot => loot.Enabled)
                .OrderBy(loot => loot.Chance)
                .ThenBy(loot => loot.Id)
                .ToList();

            if (possibleLoot.Count == 0)
            {
                BetterTrashcansMod.Instance.Monitor.Log($"   Group: {trashcan.TrashcanID}, No Possible Loot Found... check the logic");
            }

            TrashTreasure treasure = possibleLoot.ChooseItem(Game1.random);
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

            int count = Game1.random.Next(treasure.MinAmount, treasure.MaxAmount);
            reward = (Item) new StardewValley.Object(id, count);
            
            return reward;
        }

        private static Item GetDefaultTrashTreasure(int index, Random random, int X, int Y)
        {
            int parentSheetIndex = 168;
            switch (random.Next(10))
            {
                case 0:
                    parentSheetIndex = 168;
                    break;
                case 1:
                    parentSheetIndex = 167;
                    break;
                case 2:
                    parentSheetIndex = 170;
                    break;
                case 3:
                    parentSheetIndex = 171;
                    break;
                case 4:
                    parentSheetIndex = 172;
                    break;
                case 5:
                    parentSheetIndex = 216;
                    break;
                case 6:
                    parentSheetIndex = Utility.getRandomItemFromSeason(Game1.currentSeason, X * 653 + Y * 777, false);
                    break;
                case 7:
                    parentSheetIndex = 403;
                    break;
                case 8:
                    parentSheetIndex = 309 + random.Next(3);
                    break;
                case 9:
                    parentSheetIndex = 153;
                    break;
            }

            if (index == 3 && random.NextDouble() < 0.2 + Game1.dailyLuck)
            {
                parentSheetIndex = 535;
                if (random.NextDouble() < 0.05)
                    parentSheetIndex = 749;
            }

            if (index == 4 && random.NextDouble() < 0.2 + Game1.dailyLuck)
            {
                parentSheetIndex = 378 + random.Next(3) * 2;
                random.Next(1, 5);
            }

            if (index == 5 && random.NextDouble() < 0.2 + Game1.dailyLuck && Game1.dishOfTheDay != null)
                parentSheetIndex = Game1.dishOfTheDay.ParentSheetIndex != 217 ? Game1.dishOfTheDay.ParentSheetIndex : 216;

            if (index == 6 && random.NextDouble() < 0.2 + Game1.dailyLuck)
                parentSheetIndex = 223;
            
            return (Item)new StardewValley.Object(parentSheetIndex, 1);
        }

        internal static Item GetTreasure(int index, Random random, int X = 0, int Y = 0)
        {
            if (BetterTrashcansMod.Instance.config.useCustomTrashcanTreasure)
            {
                return GetCustomTrashTreasure((TRASHCANS) index);
            }
            else
            {
                return GetDefaultTrashTreasure(index, random, X, Y);
            }
        }
    }
}
