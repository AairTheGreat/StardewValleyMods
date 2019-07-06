using BetterTrashCans.Config;
using BetterTrashCans.Data;
using BetterTrashCans.GamePatch;
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
namespace BetterTrashCans
{
    public class BetterTrashCansMod : Mod
    {
        public static BetterTrashCansMod Instance { get; private set; }
        internal static Multiplayer multiplayer;

        internal HarmonyInstance harmony { get; private set; }

        internal ModConfig config;
        internal Dictionary<TREASURE_GROUP, TreasureGroup> treasureGroups;

        public override void Entry(IModHelper helper)
        {
            Instance = this;
            config = this.Helper.Data.ReadJsonFile<ModConfig>("config.json") ?? ModConfigDefaultConfig.CreateDefaultConfig("config.json");

            if (config.enableMod)
            {
                harmony = HarmonyInstance.Create("com.aairthegreat.mod.trashcan");
                harmony.Patch(typeof(Town).GetMethod("checkAction"), new HarmonyMethod(typeof(TrashCanOverrider).GetMethod("prefix_betterTrashCans")));

                helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;

                Type type = typeof(Game1);
                FieldInfo info = type.GetField("multiplayer", BindingFlags.NonPublic | BindingFlags.Static);
                multiplayer = info.GetValue(null) as Multiplayer;
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
