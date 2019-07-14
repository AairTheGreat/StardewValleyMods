using BetterTrainLoot.Config;
using BetterTrainLoot.Data;
using BetterTrainLoot.GamePatch;
using Harmony;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterTrainLoot
{
    public class BetterTrainLootMod : Mod
    {
        public static BetterTrainLootMod Instance { get; private set; }
        internal HarmonyInstance harmony { get; private set; }

        private int maxNumberOfTrains;
        private int numberOfTrains = 0;
        private int trainCreateDelay = 7500;

        internal TRAINS trainType;

        private double pctChanceOfNewTrain = 0.0;
        private double basePctChanceOfTrain = 0.20;

        private bool overrideMaxTrains;
        private bool enableCreatedTrain = true;


        internal ModConfig config;
        internal Dictionary<TRAINS, TrainData> trainCars;

        private Railroad railroad;
        public override void Entry(IModHelper helper)
        {
            Instance = this;
            config = helper.Data.ReadJsonFile<ModConfig>("config.json") ?? ModConfigDefaultConfig.CreateDefaultConfig("config.json");

            if (config.enableMod)
            {
                helper.Events.Input.ButtonReleased += Input_ButtonReleased;
                helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
                helper.Events.GameLoop.TimeChanged += GameLoop_TimeChanged;
                helper.Events.GameLoop.SaveLoaded += GameLoop_SaveLoaded;

                harmony = HarmonyInstance.Create("com.aairthegreat.mod.trainloot");
                harmony.Patch(typeof(TrainCar).GetMethod("draw"), null, new HarmonyMethod(typeof(TrainCarOverrider).GetMethod("postfix_getTrainTreasure")));

                string trainCarFile = Path.Combine("DataFiles", "trains.json");
                trainCars = helper.Data.ReadJsonFile<Dictionary<TRAINS, TrainData>>(trainCarFile) ?? TrainDefaultConfig.CreateTrainCarData(trainCarFile);

            }
        }

        private void GameLoop_SaveLoaded(object sender, StardewModdingAPI.Events.SaveLoadedEventArgs e)
        {
            railroad = (Game1.getLocationFromName("Railroad") as Railroad);
        }

        private void GameLoop_TimeChanged(object sender, StardewModdingAPI.Events.TimeChangedEventArgs e)
        {
            if (railroad != null && Game1.player.currentLocation.IsOutdoors)
            {
                if (enableCreatedTrain && ((railroad.train.Value == null && numberOfTrains < maxNumberOfTrains) || overrideMaxTrains))
                {
                    if (Game1.random.NextDouble() <= pctChanceOfNewTrain || (railroad.train.Value == null && overrideMaxTrains))
                    {
                        railroad.setTrainComing(trainCreateDelay);
                        numberOfTrains++;
                        overrideMaxTrains = false;
                        trainType = TRAINS.UNKNOWN;
                        this.Monitor.Log($"Setting train... Choo choo... {Game1.timeOfDay}");
                        enableCreatedTrain = false;
                        if (railroad.train.Value != null)
                        {
                            this.Monitor.Log($"{Game1.timeOfDay}: Train type... {railroad.train.Value.type.Value} : {(TRAINS)railroad.train.Value.type.Value}");
                        }
                    }
                }
                else if(railroad.train.Value != null && !enableCreatedTrain)
                {
                    enableCreatedTrain = true;
                    this.Monitor.Log($"Can create train again... {Game1.timeOfDay}");
                    this.Monitor.Log($"{Game1.timeOfDay}: Train type... {railroad.train.Value.type.Value} : {(TRAINS)railroad.train.Value.type.Value}");
                    trainType = (TRAINS)railroad.train.Value.type.Value;
                }
            }
        }

        private void GameLoop_DayStarted(object sender, StardewModdingAPI.Events.DayStartedEventArgs e)
        {
            overrideMaxTrains = false;
            enableCreatedTrain = true;
            numberOfTrains = 0;
            pctChanceOfNewTrain = Game1.dailyLuck + basePctChanceOfTrain;
            switch (Game1.random.Next(0, 10))
            {
                case 0:
                    maxNumberOfTrains = 0;
                   
                    break;
                case 1:
                case 2:
                case 3:
                    maxNumberOfTrains = 1;
                    
                    break;
                case 4:
                case 5:
                case 6:
                    maxNumberOfTrains = 3;
                    
                    break;
                case 7:
                case 8:
                case 9:
                    maxNumberOfTrains = 5;
                   
                    break;
            }

            Monitor.Log($"Setting Max Trains to {maxNumberOfTrains}");
        }

        private void Input_ButtonReleased(object sender, StardewModdingAPI.Events.ButtonReleasedEventArgs e)
        {
            if (e.Button == SButton.Y)
            {
                this.Monitor.Log("Player press Y... Choo choo");
                //(Game1.getLocationFromName("Railroad") as Railroad).setTrainComing(7500);
                overrideMaxTrains = true;
                enableCreatedTrain = true;
            }
        }
       
        
    }
}
