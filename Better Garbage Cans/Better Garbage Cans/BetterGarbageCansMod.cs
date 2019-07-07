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

namespace BetterGarbageCans
{
    public class BetterGarbageCansMod : Mod
    {
        public static BetterGarbageCansMod Instance { get; private set; }
        internal static Multiplayer multiplayer;

        internal HarmonyInstance harmony { get; private set; }

        internal ModConfig config;
        internal Dictionary<GARBAGE_CANS, GarbageCan> garbageCans;

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            config = this.Helper.Data.ReadJsonFile<ModConfig>("config.json") ?? ModConfigDefaultConfig.CreateDefaultConfig("config.json");

            if (config.enableMod)
            {
                harmony = HarmonyInstance.Create("com.aairthegreat.mod.garbagecan");
                harmony.Patch(typeof(Town).GetMethod("checkAction"), new HarmonyMethod(typeof(GarbageCanOverrider).GetMethod("prefix_betterGarbageCans")));

                string garbageCanFile = Path.Combine("DataFiles", "garbagecans.json");
                garbageCans = this.Helper.Data.ReadJsonFile<Dictionary<GARBAGE_CANS, GarbageCan>>(garbageCanFile) ?? GarbageCanDefaultConfig.CreateGarbageCans(garbageCanFile);


                helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;
                helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
                Type type = typeof(Game1);
                FieldInfo info = type.GetField("multiplayer", BindingFlags.NonPublic | BindingFlags.Static);
                multiplayer = info.GetValue(null) as Multiplayer;
            }
        }

        private void GameLoop_DayStarted(object sender, DayStartedEventArgs e)
        {
            // Update garbage can settings.
            foreach (int i in Enum.GetValues(typeof(GARBAGE_CANS)))
            {
                garbageCans[(GARBAGE_CANS)i].LastTimeChecked = -1;
                garbageCans[(GARBAGE_CANS)i].LastTimeFoundItem = -1;
            }
        }

        private void GameLoop_SaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            if(Context.IsWorldReady && Helper.ModRegistry.IsLoaded("Pathoschild.Automate"))
            {
                this.Monitor.Log("Found the Automate Mod, this mod is not compatible with this mod.");                   
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
