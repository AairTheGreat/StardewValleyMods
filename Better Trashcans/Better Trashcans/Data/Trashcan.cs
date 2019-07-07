using System.Collections.Generic;

namespace BetterTrashcans.Data
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
        public TRASHCANS TrashcanID { get; set; }

        public int LastTimeChecked { get; set; }

        public int LastTimeFoundItem { get; set; }

        public List<TrashTreasure> treasureList { get; set; }

        public Trashcan(TRASHCANS id)
        {
            this.TrashcanID = id;
            this.LastTimeChecked = -1;
            this.LastTimeFoundItem = -1;
        }
    }
}
