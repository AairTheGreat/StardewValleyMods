using Harmony;
using Microsoft.Xna.Framework;
using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;


//  TrashCanMachine 
//private int? GetRandomTrash(int index)
namespace Better_Trash_Cans
{
    public class BetterTrashCansMod : Mod
    {
        public static BetterTrashCansMod Instance { get; private set; }
        internal HarmonyInstance harmony { get; private set; }

        internal static Multiplayer multiplayer;

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            harmony = HarmonyInstance.Create("com.aairthegreat.mod.trashcan");
            harmony.Patch(typeof(Town).GetMethod("checkAction"), new HarmonyMethod(typeof(BetterTrashCansMod).GetMethod("prefix_betterTrashCans")));

            helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;

            Type type = typeof(Game1);
            FieldInfo info = type.GetField("multiplayer", BindingFlags.NonPublic | BindingFlags.Static);
            multiplayer = info.GetValue(null) as Multiplayer;
        }

        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            if(Context.IsWorldReady && Helper.ModRegistry.IsLoaded("Pathoschild.Automate"))
            {
                this.Monitor.Log("Found the Automate Mod");
                IModInfo mod = Helper.ModRegistry.Get("Pathoschild.Automate");                
            }
        }

        public static void prefix_betterTrashCans(Town __instance, Location tileLocation, ref Farmer who,
            ref NetArray<bool, NetBool> ___garbageChecked) 
        {            
            if (__instance.map.GetLayer("Buildings").Tiles[tileLocation] != null)
            {
                if (__instance.map.GetLayer("Buildings").Tiles[tileLocation].TileIndex == 78)
                { 
                    string str = __instance.doesTileHaveProperty(tileLocation.X, tileLocation.Y, "Action", "Buildings");
                    int num;
                    if (str == null)
                        num = -1;
                    else
                        num = Convert.ToInt32(str.Split(' ')[1]);

                    int index = num;
                    if (index >= 0 && index < ___garbageChecked.Length && !___garbageChecked[index])
                    {                                                
                        Instance.Monitor.Log("prefix ran");
                        ___garbageChecked[index] = true;
                        __instance.playSound("trashcan");
                        Character character = Utility.isThereAFarmerOrCharacterWithinDistance(new Vector2((float)tileLocation.X, (float)tileLocation.Y), 7, (GameLocation)__instance);
                        if (character != null && character is NPC && !(character is Horse))
                        { 
                            if (multiplayer != null)
                                multiplayer.globalChatInfoMessage("TrashCan", Game1.player.Name, character.Name);

                            if (character.Name.Equals("Linus"))
                            {
                                character.doEmote(32, true);
                                (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Linus"), true, true);
                                who.changeFriendship(5, character as NPC);
                                if (multiplayer != null)
                                    multiplayer.globalChatInfoMessage("LinusTrashCan");
                            }
                            else if ((character as NPC).Age == 2)
                            {
                                character.doEmote(28, true);
                                (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Child"), true, true);
                                who.changeFriendship(-25, character as NPC);
                            }
                            else if ((character as NPC).Age == 1)
                            {
                                character.doEmote(8, true);
                                (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Teen"), true, true);
                                who.changeFriendship(-25, character as NPC);
                            }
                            else
                            {
                                character.doEmote(12, true);
                                (character as NPC).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Town_DumpsterDiveComment_Adult"), true, true);
                                who.changeFriendship(-25, character as NPC);
                            }
                            Game1.drawDialogue(character as NPC);
                        }
                        //Random random = new Random((int)Game1.uniqueIDForThisGame / 2 + (int)Game1.stats.DaysPlayed + 777 + index);
                        //if (random.NextDouble() < 0.2 + Game1.dailyLuck)
                        //{
                            int parentSheetIndex = 74;
                            //switch (random.Next(10))
                            //{
                            //    case 0:
                            //        parentSheetIndex = 168;
                            //        break;
                            //    case 1:
                            //        parentSheetIndex = 167;
                            //        break;
                            //    case 2:
                            //        parentSheetIndex = 170;
                            //        break;
                            //    case 3:
                            //        parentSheetIndex = 171;
                            //        break;
                            //    case 4:
                            //        parentSheetIndex = 172;
                            //        break;
                            //    case 5:
                            //        parentSheetIndex = 216;
                            //        break;
                            //    case 6:
                            //        parentSheetIndex = Utility.getRandomItemFromSeason(Game1.currentSeason, tileLocation.X * 653 + tileLocation.Y * 777, false);
                            //        break;
                            //    case 7:
                            //        parentSheetIndex = 403;
                            //        break;
                            //    case 8:
                            //        parentSheetIndex = 309 + random.Next(3);
                            //        break;
                            //    case 9:
                            //        parentSheetIndex = 153;
                            //        break;
                            //}
                            //if (index == 3 && random.NextDouble() < 0.2 + Game1.dailyLuck)
                            //{
                            //    parentSheetIndex = 535;
                            //    if (random.NextDouble() < 0.05)
                            //        parentSheetIndex = 749;
                            //}
                            //if (index == 4 && random.NextDouble() < 0.2 + Game1.dailyLuck)
                            //{
                            //    parentSheetIndex = 378 + random.Next(3) * 2;
                            //    random.Next(1, 5);
                            //}
                            //if (index == 5 && random.NextDouble() < 0.2 + Game1.dailyLuck && Game1.dishOfTheDay != null)
                            //    parentSheetIndex = (int)((NetFieldBase<int, NetInt>)Game1.dishOfTheDay.parentSheetIndex) != 217 ? (int)((NetFieldBase<int, NetInt>)Game1.dishOfTheDay.parentSheetIndex) : 216;
                            //if (index == 6 && random.NextDouble() < 0.2 + Game1.dailyLuck)
                            //    parentSheetIndex = 223;
                            who.addItemByMenuIfNecessary((Item)new StardewValley.Object(parentSheetIndex, 1, false, -1, 0), (ItemGrabMenu.behaviorOnItemSelect)null);                            
                        //}                        
                    }
                }
            }
        }
    }
}
