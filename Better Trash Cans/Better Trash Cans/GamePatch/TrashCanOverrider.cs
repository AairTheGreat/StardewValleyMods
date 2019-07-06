
using BetterTrashCans.Data;
using BetterTrashCans.Framework;
using Microsoft.Xna.Framework;
using Netcode;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Dimensions;

namespace BetterTrashCans.GamePatch
{
   static class TrashCanOverrider
   {
        public static void prefix_betterTrashCans(Town __instance, Location tileLocation, ref Farmer who,
            ref IList<bool> ___garbageChecked) //ref NetArray<bool, NetBool> ___garbageChecked)
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
                            //BetterTrashCansMod.Instance.Monitor.Log($"prefix ran: index = {index}");
                            BetterTrashCansMod.Instance.trashcans[(TRASHCANS)index].LastTimeChecked = Game1.timeOfDay;
                            BetterTrashCansMod.Instance.Monitor.Log($"Checked {(TRASHCANS)index} - Game time of day: {Game1.timeOfDay}");
                            ___garbageChecked[index] = true;

                            __instance.playSound("trashcan");

                            CheckForNPCMessages(__instance, tileLocation, ref who);

                            Item reward;

                            if (BetterTrashCansMod.Instance.config.useCustomTrashcanTreasure)
                            {
                                reward = GetCustomTrashTreasure(index);                                
                            }
                            else
                            {
                                reward = GetDefaultTrashTreasure(index, tileLocation);
                            }

                            if (reward != null)
                            {
                                who.addItemByMenuIfNecessary(reward, (ItemGrabMenu.behaviorOnItemSelect)null);
                                BetterTrashCansMod.Instance.trashcans[(TRASHCANS)index].LastTimeFoundItem = Game1.timeOfDay;
                                BetterTrashCansMod.Instance.Monitor.Log($"Got treasure from {(TRASHCANS)index} - Game time of day: {Game1.timeOfDay}");
                            }
                        }
                    }
                }
            }
        }

        private static bool CanCheckTrashcan(TRASHCANS can)
        {
            bool canCheckForItems = false;

            //Has the trashcan been checked today?
            if (BetterTrashCansMod.Instance.trashcans[can].LastTimeChecked == -1) //LastTimeChecked defaults per day to -1
            {
                // You have never checked that can...
                canCheckForItems = true;
            }
            else if (BetterTrashCansMod.Instance.config.allowTrashCanRecheck) //You have check the trashcan before.... and you can recheck it
            {
                //See if enough time has pasted since last check
                if (CanCheckBasedOnLastCheckTime(can))
                {                    
                    if (BetterTrashCansMod.Instance.trashcans[can].LastTimeFoundItem == -1)
                    {
                        //Enough time has passed, and never found anything...
                        canCheckForItems = true;
                    } 
                    else if (BetterTrashCansMod.Instance.config.allowMultipleItemsPerDay) // So you have found at least something before
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
            return Game1.timeOfDay >= GetWaitTime(BetterTrashCansMod.Instance.trashcans[can].LastTimeChecked,
                        BetterTrashCansMod.Instance.config.WaitTimeIfFoundNothing);
        }

        private static bool CanCheckBasedOnLastFoundTime(TRASHCANS can)
        {
            return Game1.timeOfDay >= GetWaitTime(BetterTrashCansMod.Instance.trashcans[can].LastTimeFoundItem,
                BetterTrashCansMod.Instance.config.WaitTimeIfFoundSomething);
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
                BetterTrashCansMod.SendMulitplayerMessage("TrashCan", Game1.player.Name, character.Name);

                if (character.Name.Equals("Linus"))
                {
                    character.doEmote(32, true);
                    (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Linus"), true, true);
                    player.changeFriendship(BetterTrashCansMod.Instance.config.LinusFriendshipPoints, character as NPC);
                    changedFrienship = true;
                    BetterTrashCansMod.SendMulitplayerMessage("LinusTrashCan");
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
                    player.changeFriendship(BetterTrashCansMod.Instance.config.FriendshipPoints, character as NPC);
                }

                Game1.drawDialogue(character as NPC);
            }
        }

        private static Item GetCustomTrashTreasure(int index)
        {
            Random random = new Random((int)Game1.uniqueIDForThisGame / 2 + (int)Game1.stats.DaysPlayed + 777 + index + Game1.timeOfDay);
            int parentSheetIndex = -1;            
            if (random.NextDouble() < BetterTrashCansMod.Instance.config.baseChancePercent + Game1.dailyLuck)
            {
                parentSheetIndex = 74;
                BetterTrashCansMod.Instance.trashcans[(TRASHCANS)index].LastTimeFoundItem = Game1.timeOfDay;
            }

            if (parentSheetIndex > 0)
            {
                return (Item)new StardewValley.Object(parentSheetIndex, 1, false, -1, 0);
            }
            else
            {
                return null;
            }
        }

        private static Item GetDefaultTrashTreasure(int index, Location tileLocation)
        {
            int parentSheetIndex = -1;
            Random random = new Random((int)Game1.uniqueIDForThisGame / 2 + (int)Game1.stats.DaysPlayed + 777 + index);
            if (random.NextDouble() < BetterTrashCansMod.Instance.config.baseChancePercent + Game1.dailyLuck)
            {
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
                        parentSheetIndex = Utility.getRandomItemFromSeason(Game1.currentSeason, tileLocation.X * 653 + tileLocation.Y * 777, false);
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
            }
            if (parentSheetIndex > 0)
            {
                return (Item)new StardewValley.Object(parentSheetIndex, 1, false, -1, 0);
            }
            else
            {
                return null;
            }
        }

        internal static Item GetTreasure(TRASHCANS index)
        {
            List<Item> rewards = new List<Item>();

            //Treasure Groups
            //List<TrashcanGroup> possibleGroups = BetterTrashCansMod.Instance.treasureGroups.Values
            //    .Where(group => group.Enabled == true)
            //    .OrderBy(group => group.GroupChance)
            //    .ToList();

            Trashcan trashcan = BetterTrashCansMod.Instance.trashcans[index]; //possibleGroups.ChooseItem(Game1.random);
                
            // Possible treasure based on selected treasure group selected above.
            List<TrashTreasure> possibleLoot = new List<TrashTreasure>(trashcan.treasureList)
                .Where(loot => loot.Enabled)
                .OrderBy(loot => loot.Chance)
                .ThenBy(loot => loot.Id)
                .ToList();

            if (possibleLoot.Count == 0)
            {
                BetterTrashCansMod.Instance.Monitor.Log($"   Group: {trashcan.TrashcanGroupID}, No Possible Loot Found... check the logic");               
            }

            TrashTreasure treasure = possibleLoot.ChooseItem(Game1.random);
            int id = treasure.Id;

            // Lost books have custom handling  -- No default artifacts... but someonw might configure them
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
            //if (trashcan.TrashcanGroupID == TRASHCANS.Rings)                    
            //{
            //    reward = new Ring(id);
            //}
            //else if (trashcan.TrashcanGroupID == TRASHCANS.Boots)
            //{
            //    reward = new Boots(id);
            //}
            //else
            //{
                // Random quantity
                int count = Game1.random.Next(treasure.MinAmount, treasure.MaxAmount);
                reward = new StardewValley.Object(id, count);                
            //}

            return reward;
        }
    }
}
