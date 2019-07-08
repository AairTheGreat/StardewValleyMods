using BetterGarbageCans.Config;
using BetterGarbageCans.Data;
using BetterGarbageCans.GamePatch;
using Harmony;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace BetterGarbageCans
{
    public class BetterGarbageCansMod : Mod
    {
        public static BetterGarbageCansMod Instance { get; private set; }
        internal static Multiplayer multiplayer;

        internal HarmonyInstance harmony { get; private set; }

        internal ModConfig config;
        internal Dictionary<GARBAGE_CANS, GarbageCan> garbageCans;
        internal List<NPC> allNPCCharacters = new List<NPC>();
        internal List<NPC> allNPCCharactersWithBirthdaysThisSeason = new List<NPC>();
        internal Item birthdayItem = null;
        internal GARBAGE_CANS birthdayCan;

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            config = helper.Data.ReadJsonFile<ModConfig>("config.json") ?? ModConfigDefaultConfig.CreateDefaultConfig("config.json");
            
            if (config.enableMod)
            {
                harmony = HarmonyInstance.Create("com.aairthegreat.mod.garbagecan");
                harmony.Patch(typeof(Town).GetMethod("checkAction"), new HarmonyMethod(typeof(GarbageCanOverrider).GetMethod("prefix_betterGarbageCans")));

                string garbageCanFile = Path.Combine("DataFiles", "garbage_cans.json");
                garbageCans = helper.Data.ReadJsonFile<Dictionary<GARBAGE_CANS, GarbageCan>>(garbageCanFile) ?? GarbageCanDefaultConfig.CreateGarbageCans(garbageCanFile);

                AddTrashToCans(config.baseTrashChancePercent);
                helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
                helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
                Type type = typeof(Game1);
                FieldInfo info = type.GetField("multiplayer", BindingFlags.NonPublic | BindingFlags.Static);
                multiplayer = info.GetValue(null) as Multiplayer;
            }
        }

        private void AddTrashToCans(double trashChance)
        {
            foreach (int i in Enum.GetValues(typeof(GARBAGE_CANS)))
            {
                garbageCans[(GARBAGE_CANS)i].treasureList.Add(new TrashTreasure(168, "Trash", trashChance));
            }
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e)
        {
            ResetGarbageCanLastTimes();
            SetupTheBirthdayTrash();
        }

        private void ResetGarbageCanLastTimes()
        {
            // Update garbage can settings.
            foreach (int i in Enum.GetValues(typeof(GARBAGE_CANS)))
            {
                garbageCans[(GARBAGE_CANS)i].LastTimeChecked = -1;
                garbageCans[(GARBAGE_CANS)i].LastTimeFoundItem = -1;
            }

            if (birthdayItem != null)
            {
                foreach (TrashTreasure item in garbageCans[birthdayCan].treasureList)
                {
                    if (item.Id == birthdayItem.ParentSheetIndex)
                    {
                        item.Chance -= config.birthdayGiftChancePercent;
                    }
                }
                birthdayItem = null;
            }
        }

        private void SetupTheBirthdayTrash()
        {
            allNPCCharactersWithBirthdaysThisSeason = allNPCCharacters
                .Where(npc => (npc.CanSocialize && npc.Birthday_Season == Game1.CurrentSeasonDisplayName)
                || (npc.Name == "Dwarf" && npc.Birthday_Season == Game1.CurrentSeasonDisplayName)).ToList();

            foreach (NPC npc in allNPCCharactersWithBirthdaysThisSeason)
            {
                if (npc.Birthday_Day == Game1.dayOfMonth + 1 || npc.Birthday_Day == Game1.dayOfMonth)
                {
                    birthdayItem = (Item)npc.getFavoriteItem();
                    //Found a birthday.
                    //this.Monitor.Log($"NPC {npc.Name} Favorite Item: {npc.getFavoriteItem().Name}");
                    switch (npc.Name)
                    {
                        case "George":
                        case "Evelyn":
                        case "Alex":
                            SetBirthdayGift(GARBAGE_CANS.EVELYN_GEORGE, birthdayItem);
                            break;
                        case "Haley":
                        case "Emily":
                            SetBirthdayGift(GARBAGE_CANS.EMILY_HALEY, birthdayItem);
                            break;
                        case "Kent":
                        case "Vincent":
                        case "Jodi":
                        case "Sam":
                            SetBirthdayGift(GARBAGE_CANS.JODI_SAM, birthdayItem);
                            break;
                        case "Clint":
                            SetBirthdayGift(GARBAGE_CANS.CLINT, birthdayItem);
                            break;
                        case "Gus":
                            SetBirthdayGift(GARBAGE_CANS.STARDROP_SALOON, birthdayItem);
                            break;
                        default:
                            SetBirthdayGift(GARBAGE_CANS.MAYOR_LEWIS, birthdayItem);
                            break;
                    }                    
                }
            }
        }

        private void SetBirthdayGift(GARBAGE_CANS can, Item favItem)
        {
            birthdayCan = can;
            bool foundItem = false;
            foreach (TrashTreasure item in garbageCans[can].treasureList)
            {
                if (item.Id == favItem.ParentSheetIndex)
                {
                    item.Chance += config.birthdayGiftChancePercent;
                    foundItem = true;
                    break;
                }
            }

            if(!foundItem)
            {
                garbageCans[can].treasureList.Add(new TrashTreasure(favItem.ParentSheetIndex, favItem.Name, config.birthdayGiftChancePercent));
            }
        }

        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            if (Context.IsWorldReady )
            {
                allNPCCharacters = new List<NPC>();
                allNPCCharactersWithBirthdaysThisSeason = new List<NPC>();
                Utility.getAllCharacters(allNPCCharacters);
                if (Helper.ModRegistry.IsLoaded("Pathoschild.Automate"))
                {
                    this.Monitor.Log("Found the Automate Mod, this mod is not fully compatible with this mod.");
                }

                foreach (NPC npc in allNPCCharacters)
                {
                    if (npc.CanSocialize || npc.Name == "Dwarf")
                    {
                        this.Monitor.Log($"NPC {npc.Name} Favorite Item: {npc.getFavoriteItem().Name}");
                    }
                }
            }
        }        

        internal static void SendMulitplayerMessage(string mesageType, string playerName = null, string npcName = null)
        {
            if (multiplayer != null && playerName != null)
            {
                multiplayer.globalChatInfoMessage(mesageType, playerName, npcName);
            }
            else
            {
                multiplayer.globalChatInfoMessage(mesageType);
            }
        }
    }
}
