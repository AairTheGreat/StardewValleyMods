﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BetterPanning
{
    public class MapOreConfig
    {
        public int FileVersion { get; set; }
        public string AreaName { get; set; }
        public int NumberOfOreSpotsPerDay { get; set; }
        
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public bool CustomTreasure { get; set; }
        public List<Point> OreSpots { get; set; }

        private int numberOfTimesCollectedPerDay;

        internal int GetNumberOfTimesCollectedPerDay()
        {
            return numberOfTimesCollectedPerDay;
        }

        internal void UpdateCollectionCount()
        {
            numberOfTimesCollectedPerDay++;
        }

        internal void ResetCollectedPerDay()
        {
            numberOfTimesCollectedPerDay = 0;
        }
    }
}
