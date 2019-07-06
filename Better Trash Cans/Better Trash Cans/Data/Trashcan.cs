using BetterTrashCans.Framework;
using StardewValley;
using System.Collections.Generic;
using System.Linq;

namespace BetterTrashCans.Data
{
    //  Trash Can           Index
    //==============================
    //  Jodi/Sam            0
    //  Haley/Emily         1
    //  Mayor Lewis         2
    //  Library/Gunthers    3
    //  Clint's             4    
    //  Stardrop Saloon     5
    //  Evelyn/George       6
    
    
    

    public enum TRASHCANS
    {
        JODI_SAM=0,
        EMILY_HALEY,
        MAYOR_LEWIS,
        GUNTHER,
        CLINT,
        STARDROP_SALOON,
        EVELYN_GEORGE
    }

    public class Trashcan 
    {
        public TRASHCANS TrashcanGroupID { get; set; }

        public int LastTimeChecked { get; set; }

        public int LastTimeFoundItem { get; set; }
        //public double GroupChance { get; set; }

        //public bool Enabled { get; set; }

        //public bool ManualOverride { get; set; }

        public List<TrashTreasure> treasureList { get; set; }

        public Trashcan(TRASHCANS id)
        {
            this.TrashcanGroupID = id;
            //this.GroupChance = chance;
            //this.Enabled = enabled;
            //this.ManualOverride = manualOverride;
        }

        //public double GetWeight()
        //{
        //    return this.GroupChance;
        //}

        //public bool GetEnabled()
        //{
        //    return this.Enabled;
        //}

        //public void SetEnableFlagOnAllTreasures(bool enable)
        //{
        //    if (!this.ManualOverride)
        //    {
        //        foreach (var treasure in this.treasureList)
        //        {
        //            treasure.Enabled = enable;
        //        }
        //        CheckEnableFlag();
        //    }
        //}

        //public void SetEnableTreasure(int itemID, bool enable)
        //{
        //    if (!this.ManualOverride)
        //    {
        //        foreach (var treasure in this.treasureList)
        //        {
        //            if (treasure.Id == itemID)
        //            {
        //                treasure.Enabled = enable;
        //                break;
        //            }
        //        }
        //        CheckEnableFlag();
        //    }
        //}

        //public void CheckGroupTreasuresStatus()
        //{
        //    if (!this.ManualOverride)
        //    {
        //        if (this.GroupID == TRASHCANS.Artifacts)
        //        {
        //            SetArtifactTreasuresEnableFlag();
        //        }
        //        if (this.GroupID == TRASHCANS.GeodeMinerals)
        //        {
        //            SetGeodeMineralsTreasuresEnableFlag();
        //        }
        //    }
        //}

        //private void CheckEnableFlag()
        //{
        //    if (!this.ManualOverride)
        //    {
        //        this.Enabled = (this.treasureList.Count(loot => loot.Enabled) != 0);
        //    }
        //}


        //private void SetArtifactTreasuresEnableFlag()
        //{
        //    if (!this.ManualOverride)
        //    {
        //        // Need to find a cleaner way...
        //        if (this.GroupID == TRASHCANS.Artifacts)
        //        {
        //            foreach (var treasure in this.treasureList)
        //            {
        //                treasure.Enabled = !Game1.player.archaeologyFound.ContainsKey(treasure.Id);
        //            }
        //            CheckEnableFlag();
        //        }
        //    }
        //}

        //private void SetGeodeMineralsTreasuresEnableFlag()
        //{
        //    if (!this.ManualOverride)
        //    {
        //        // Need to find a cleaner way...
        //        if (this.GroupID == TRASHCANS.GeodeMinerals)
        //        {
        //            foreach (var treasure in this.treasureList)
        //            {
        //                treasure.Enabled = Game1.player.mineralsFound.ContainsKey(treasure.Id);
        //            }
        //            CheckEnableFlag();
        //        }
        //    }
        //}
    }
}
