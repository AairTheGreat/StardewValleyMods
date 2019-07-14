using BetterTrainLoot.Data;
using System;
using System.Collections.Generic;

namespace BetterTrainLoot.Config
{
    public static class TrainDefaultConfig
    {
        public static Dictionary<TRAINS, TrainData> CreateTrainCarData(string file)
        {
            Dictionary<TRAINS, TrainData> groups = new Dictionary<TRAINS, TrainData>();
            groups.Add(TRAINS.RANDOM_TRAIN, CreateTrainLoot(TRAINS.RANDOM_TRAIN));
            groups.Add(TRAINS.JOJA_TRAIN, CreateTrainLoot(TRAINS.JOJA_TRAIN));
            groups.Add(TRAINS.COAL_TRAIN, CreateTrainLoot(TRAINS.COAL_TRAIN));
            groups.Add(TRAINS.PASSENGER_TRAIN, CreateTrainLoot(TRAINS.PASSENGER_TRAIN));
            groups.Add(TRAINS.PRISON_TRAIN, CreateTrainLoot(TRAINS.PRISON_TRAIN));
            groups.Add(TRAINS.CHRISTMAS_TRAIN, CreateTrainLoot(TRAINS.CHRISTMAS_TRAIN));

            BetterTrainLootMod.Instance.Helper.Data.WriteJsonFile(file, groups);

            return groups;
        }

        private static TrainData CreateTrainLoot(TRAINS id)
        {
           TrainData newGroup = new TrainData(id);
            newGroup.treasureList = GetTreasureList(id);
            return newGroup;
        }

        private static List<TrainTreasure> GetTreasureList(TRAINS id)
        {
            switch (id)
            {
                case TRAINS.RANDOM_TRAIN:
                    return GetRandomTrainTreasureList();    
                    
                case TRAINS.JOJA_TRAIN:
                    return GetJojaTrainTreasureList();      
                    
                case TRAINS.COAL_TRAIN:
                    return GetCoalTrainTreasureList(); 
                    
                case TRAINS.PASSENGER_TRAIN:
                    return GetPassengerTrainTreasureList();    
                    
                case TRAINS.PRISON_TRAIN:
                    return GetPrisonTrainTreasureList();   
                    
                case TRAINS.CHRISTMAS_TRAIN:
                    return GetPresentCarTreasureList();

                default:
                    return new List<TrainTreasure>();                    
            }
        }


        private static List<TrainTreasure> GetRandomTrainTreasureList()
        {
            List<TrainTreasure> treasures = new List<TrainTreasure>();
            treasures.Add(CreateTreasure(18, "Daffodil", 0.045, 600, 2600));
            treasures.Add(CreateTreasure(20, "Leek", 0.045, 600, 2600));
            treasures.Add(CreateTreasure(24, "Parsnip", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(72, "Diamond ", 0.005, 2100, 2600));
            treasures.Add(CreateTreasure(74, "Prismatic Shard", 0.001, 2100, 2600));
            treasures.Add(CreateTreasure(88, "Coconut", 0.0075, 600, 2600));
            treasures.Add(CreateTreasure(90, "Cactus Fruit", 0.0075,  600, 2600));
            treasures.Add(CreateTreasure(128, "Puffer Fish", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(132, "Bream", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(136, "Largemouth Bass", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(142, "Carp", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(143, "Catfish", 0.025, 600, 2600));
            treasures.Add(CreateTreasure(145, "Sunfish", 0.03, 600, 2600));
            treasures.Add(CreateTreasure(150, "Red Snapper", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(167, "Jojo Cola", 0.1,  1200, 2300));
            treasures.Add(CreateTreasure(174, "Large Egg", 0.02,  600, 1130));
            treasures.Add(CreateTreasure(200, "Vegetable Medley", 0.02,  1100, 2600));
            treasures.Add(CreateTreasure(206, "Pizza", 0.06,  1100, 2600));
            treasures.Add(CreateTreasure(211, "Pancake", 0.0375,  600, 1100));
            treasures.Add(CreateTreasure(214, "Crispy Bass", 0.02,  1200, 2600));
            treasures.Add(CreateTreasure(216, "Bread", 0.04,  1000, 2200));
            treasures.Add(CreateTreasure(220, "Chocolate Cake", 0.03,  600, 2600));
            treasures.Add(CreateTreasure(221, "Pink Cake", 0.025,  600, 2600));
            treasures.Add(CreateTreasure(222, "Rhubarb Pie", 0.022,  1100, 2600));
            treasures.Add(CreateTreasure(225, "Fried Eel", 0.021,  1200, 2600));
            treasures.Add(CreateTreasure(231, "Eggplant Parmesan", 0.025, 1100, 2600));
            treasures.Add(CreateTreasure(266, "Red Cabbage", 0.0075, 600, 2600));
            treasures.Add(CreateTreasure(276, "Pumpkin", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(284, "Beet", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(286, "Cherry Bomb", 0.04, 1100, 2400));
            treasures.Add(CreateTreasure(287, "Bomb", 0.03,  1230, 2530));
            treasures.Add(CreateTreasure(309, "Acorn", 0.06, 600, 2600));
            treasures.Add(CreateTreasure(310, "Maple Seed", 0.055, 600, 2600));
            treasures.Add(CreateTreasure(340, "Honey", 0.03, 600, 2600));
            treasures.Add(CreateTreasure(341, "Tea Set", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(348, "Wine ", 0.02, 1200, 2500));
            treasures.Add(CreateTreasure(376, "Poppy", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(398, "Grape", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(408, "Hazelnut", 0.025, 600, 2600));
            treasures.Add(CreateTreasure(428, "Cloth", 0.039, 600, 2600));
            treasures.Add(CreateTreasure(430, "Truffle", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(432, "Truffle Oil", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(440, "Wool", 0.065, 600, 2600));
            treasures.Add(CreateTreasure(442, "Duck Egg", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(444, "Duck Feather", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(446, "Rabbits Foot", 0.01, 600, 750));
            treasures.Add(CreateTreasure(466, "Deluxe Speed Grow", 0.009, 600, 2600));
            treasures.Add(CreateTreasure(562, "Tigers Eye", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(591, "Tulip", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(593, "Summer Spangle", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(595, "Fairy Rose", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(607, "Roasted Hazelnut", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(612, "Cranberry Candy", 0.02, 1200, 2100));
            treasures.Add(CreateTreasure(621, "Quality Spinkler", 0.0075, 2100, 2600));
            treasures.Add(CreateTreasure(628, "Cherry Sapling", 0.009, 1800, 2600));
            treasures.Add(CreateTreasure(637, "Pomegranate", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(649, "Fiddle Head Risotto", 0.025, 1100, 2600));
            treasures.Add(CreateTreasure(698, "Sturgeon", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(699, "Tiger Trout", 0.03, 600, 2600));
            treasures.Add(CreateTreasure(700, "Bullhead", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(701, "Tilapia", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(706, "Shad", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(724, "Maple Syrup", 0.01, 600, 1500));
            treasures.Add(CreateTreasure(725, "Oak Resin", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(787, "Battery Pack", 0.01, 1000, 2000));
            treasures.Add(CreateTreasure(797, "Pearl", 0.004, 600, 900));
            
            return treasures;
        }

        private static List<TrainTreasure> GetJojaTrainTreasureList()
        {
            List<TrainTreasure> treasures = new List<TrainTreasure>();
            treasures.Add(CreateTreasure(425, "Fairy Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(427, "Tulip Bulb", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(431, "Sunflower Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(453, "Poppy Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(466, "Deluxe Speed Grow", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(472, "Parsnip Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(485, "Red Cabbage Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(486, "Starfruit Seed", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(490, "Pumpkin Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(495, "Spring Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(496, "Summer Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(497, "Fall Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(498, "Winter Seeds", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(499, "Ancient Fruit Seed", 0.015, 1200, 2600));
            treasures.Add(CreateTreasure(628, "Cherry Sapling", 0.009, 1800, 2600));
            treasures.Add(CreateTreasure(630, "Orange Sapling", 0.009, 1730, 2530));
            treasures.Add(CreateTreasure(631, "Peach Sapling", 0.009, 1600, 2500));
            treasures.Add(CreateTreasure(632, "Pomegranate Sapling", 0.009, 2000, 2430));
            treasures.Add(CreateTreasure(633, "Apple Sapling", 0.009, 1200, 1700));           
            treasures.Add(CreateTreasure(745, "Strawberry Seeds", 0.008, 600, 2600));

            return treasures;
        }

        private static List<TrainTreasure> GetCoalTrainTreasureList()
        {
            List<TrainTreasure> treasures = new List<TrainTreasure>();
            treasures.Add(CreateTreasure(60, "Emerald", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(62, "Aquamarine", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(64, "Ruby", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(66, "Amethyst", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(68, "Topaz", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(70, "Jade", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(72, "Diamond", 0.0075, 1500, 2600));
            treasures.Add(CreateTreasure(74, "Prismatic Shard", 0.001, 2300, 2600));
            treasures.Add(CreateTreasure(80, "Quartz", 0.0175, 600, 2600));
            treasures.Add(CreateTreasure(82, "Fire Quartz", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(84, "Frozen Tear", 0.0125, 600, 2600));
            treasures.Add(CreateTreasure(86, "Earth Chrystal", 0.0115, 600, 2600));
            treasures.Add(CreateTreasure(334, "Copper Bar", 0.0175, 600, 2600));
            treasures.Add(CreateTreasure(337, "Iridium Bar", 0.005, 1700, 2600));
            treasures.Add(CreateTreasure(378, "Copper Ore", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(380, "Iron Ore", 0.0175, 600, 2600));
            treasures.Add(CreateTreasure(380, "Iron Bar", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(382, "Coal", 0.0139, 600, 2600));
            treasures.Add(CreateTreasure(384, "Gold Ore", 0.0125, 600, 2600));
            treasures.Add(CreateTreasure(384, "Gold Bar", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(386, "Iridium Ore", 0.0075, 600, 2600));
            treasures.Add(CreateTreasure(420, "Red Mushroom", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(422, "Purple Mushroom", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(517, "Glow Ring", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(518, "Small Megnic Ring", 0.01, 600, 2600));
            treasures.Add(CreateTreasure(527, "Iridium Band", 0.0075, 630, 1200));
            treasures.Add(CreateTreasure(535, "Geode", 0.014, 600, 2600));
            treasures.Add(CreateTreasure(536, "Frozen Geode", 0.013, 600, 2600));
            treasures.Add(CreateTreasure(537, "Magma Geode", 0.012, 600, 2600));
            treasures.Add(CreateTreasure(538, "Alamite", 0.008, 730, 1230));
            treasures.Add(CreateTreasure(539, "Bixite", 0.008, 700, 1200));
            treasures.Add(CreateTreasure(540, "Baryte", 0.008, 610, 810));
            treasures.Add(CreateTreasure(541, "Aerinite", 0.008, 1600, 1730));
            treasures.Add(CreateTreasure(542, "Calcite", 0.008, 1250, 1440));
            treasures.Add(CreateTreasure(543, "Dolomite", 0.008, 1000, 1120));
            treasures.Add(CreateTreasure(544, "Esperite", 0.008, 1330, 1500));
            treasures.Add(CreateTreasure(545, "Fluorapatite", 0.008, 1120, 1640));
            treasures.Add(CreateTreasure(546, "Geminite", 0.008, 830, 1120));
            treasures.Add(CreateTreasure(547, "Helvite", 0.008, 730, 1150));
            treasures.Add(CreateTreasure(548, "Jamborite", 0.008, 630, 1040));
            treasures.Add(CreateTreasure(549, "Jagoite", 0.008, 640, 850));
            treasures.Add(CreateTreasure(550, "Kyanite", 0.008, 940, 1140));
            treasures.Add(CreateTreasure(551, "Lunarite", 0.008, 1010, 1500));
            treasures.Add(CreateTreasure(551, "Lunarite", 0.008, 1300, 1810));
            treasures.Add(CreateTreasure(552, "Malachite", 0.008, 1330, 1550));
            treasures.Add(CreateTreasure(553, "Neptunite", 0.008, 1000, 1430));
            treasures.Add(CreateTreasure(554, "Lemon Stone", 0.008, 930, 1100));
            treasures.Add(CreateTreasure(555, "Nekoite", 0.008, 1000, 1300));
            treasures.Add(CreateTreasure(556, "Orpiment", 0.008, 730, 1200));
            treasures.Add(CreateTreasure(559, "Pyrite", 0.008, 700, 1130));
            treasures.Add(CreateTreasure(560, "Ocean Stone", 0.008, 730, 1200));
            treasures.Add(CreateTreasure(562, "Tigers Eye", 0.008, 600, 2400));
            treasures.Add(CreateTreasure(563, "Jasper", 0.008, 1300, 1850));
            treasures.Add(CreateTreasure(564, "Opal", 0.008, 600, 1300));
            treasures.Add(CreateTreasure(565, "Fire Opal", 0.008, 700, 1300));
            treasures.Add(CreateTreasure(566, "Celestine", 0.008, 600, 1200));
            treasures.Add(CreateTreasure(567, "Marble", 0.008, 730, 1130));
            treasures.Add(CreateTreasure(568, "Sandstone", 0.008, 800, 1300));
            treasures.Add(CreateTreasure(569, "Granite", 0.008, 900, 1200));
            treasures.Add(CreateTreasure(570, "Basalt", 0.008, 1130, 1550));
            treasures.Add(CreateTreasure(571, "Limestone", 0.008, 1200, 1430));
            treasures.Add(CreateTreasure(572, "Soapstone", 0.008, 1140, 1400));
            treasures.Add(CreateTreasure(573, "Hematite", 0.008, 910, 1150));
            treasures.Add(CreateTreasure(574, "Mudstone", 0.008, 1130, 1500));
            treasures.Add(CreateTreasure(575, "Obsidian", 0.008, 830, 1120));
            treasures.Add(CreateTreasure(576, "Slate", 0.008, 1230, 2500));
            treasures.Add(CreateTreasure(577, "Fairy Stone", 0.008, 1000, 1800));
            treasures.Add(CreateTreasure(749, "Omni Geode", 0.011, 600, 2600));
            
            return treasures;
        }

        private static List<TrainTreasure> GetPassengerTrainTreasureList()
        {
            List<TrainTreasure> treasures = new List<TrainTreasure>();
            treasures.Add(CreateTreasure(194, "Fried Egg", 0.015, 600, 1200));
            treasures.Add(CreateTreasure(200, "Vegetable Medley", 0.03, 1100, 2600));            
            treasures.Add(CreateTreasure(201, "Complete Breakfast", 0.02, 600, 1200));
            treasures.Add(CreateTreasure(202, "Fried Calamari", 0.015, 1100, 2600));
            treasures.Add(CreateTreasure(204, "Lucky Lunch", 0.015, 1130, 1530));
            treasures.Add(CreateTreasure(205, "Fried Mushroom", 0.02, 1130, 2200));
            treasures.Add(CreateTreasure(206, "Pizza", 0.015, 1100, 2600));
            treasures.Add(CreateTreasure(207, "Bean Hotpot", 0.015, 1200, 2500));
            treasures.Add(CreateTreasure(208, "Glazed Yams", 0.025, 1200, 2400));
            treasures.Add(CreateTreasure(212, "Salmon Dinner", 0.015, 1620, 2250));
            treasures.Add(CreateTreasure(213, "Fish Taco", 0.015, 1000, 2600));
            treasures.Add(CreateTreasure(214, "Crispy Bass", 0.015, 1300, 2500));
            treasures.Add(CreateTreasure(215, "Pepper Poppers", 0.015, 1130, 2500));
            treasures.Add(CreateTreasure(216, "Bread", 0.02, 1000, 1800));
            treasures.Add(CreateTreasure(218, "Tom Kha Soup", 0.015, 1200, 2000));
            treasures.Add(CreateTreasure(220, "Chocolate Cake", 0.025, 600, 2600));
            treasures.Add(CreateTreasure(221, "Pink Cake", 0.025, 600, 2600));
            treasures.Add(CreateTreasure(223, "Cookies", 0.038, 600, 2600));
            treasures.Add(CreateTreasure(224, "Spaghetti", 0.015, 1100, 2530));
            treasures.Add(CreateTreasure(227, "Sashimi", 0.015, 1300, 2530));
            treasures.Add(CreateTreasure(233, "Ice Cream", 0.02, 1200, 2130));
            treasures.Add(CreateTreasure(234, "Blueberry Tart", 0.015, 600, 1300));
            treasures.Add(CreateTreasure(235, "Autumn's Bounty", 0.025, 1100, 2200));
            treasures.Add(CreateTreasure(236, "Pumpkin Soup", 0.015, 1300, 2200));

            treasures.Add(CreateTreasure(239, "Stuffing", 0.02, 1130, 2500));
            treasures.Add(CreateTreasure(240, "Farmers Lunch", 0.025, 1130, 1530));
            treasures.Add(CreateTreasure(241, "Survival Burger", 0.04, 1100, 2500));
            treasures.Add(CreateTreasure(260, "Hot Pepper", 0.03, 600, 2600));

            treasures.Add(CreateTreasure(303, "Pale Ale", 0.04, 1300, 2500));
            treasures.Add(CreateTreasure(342, "Pickles", 0.02, 1030, 1500));
            treasures.Add(CreateTreasure(346, "Beer", 0.04, 1400, 2500));

            treasures.Add(CreateTreasure(348, "Wine", 0.01, 1500, 2600));
            treasures.Add(CreateTreasure(395, "Coffee", 0.05, 600, 2300));
            treasures.Add(CreateTreasure(403, "Field Snack", 0.0306, 600, 2600));
            treasures.Add(CreateTreasure(426, "Goat Cheese", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(459, "Mead", 0.021, 1200, 2500));
            treasures.Add(CreateTreasure(604, "Plum Pudding", 0.015, 1000, 1530));
            treasures.Add(CreateTreasure(605, "Artichoke Dip", 0.016, 1130, 2430));
            treasures.Add(CreateTreasure(607, "Roasted Hazelnuts", 0.015, 600, 2600));
            treasures.Add(CreateTreasure(608, "Pumpkin Pie", 0.02, 1600, 2430));
            treasures.Add(CreateTreasure(610, "Fruit Salad", 0.075, 1130, 2600));
            treasures.Add(CreateTreasure(611, "Blackberry Cobbler", 0.015, 1330, 2400));
            treasures.Add(CreateTreasure(612, "Cranberry Candy", 0.02, 1200, 2100));
            treasures.Add(CreateTreasure(649, "Fiddle Head Risotto", 0.014, 1100, 2200));
            treasures.Add(CreateTreasure(651, "Poppy Seed Muffin", 0.015, 600, 1330));
            treasures.Add(CreateTreasure(729, "Escargot", 0.015, 1100, 2600));
            treasures.Add(CreateTreasure(731, "Maple Bar", 0.008, 600, 2500));
            treasures.Add(CreateTreasure(732, "Crab Cakes", 0.015, 1300, 2300));
            treasures.Add(CreateTreasure(773, "Life Elixir", 0.01,  1000, 1800));

            treasures.Add(CreateTreasure(797, "Pearl", 0.004, 2200, 2600));

            return treasures;
        }

        private static List<TrainTreasure> GetPrisonTrainTreasureList()
        {
            List<TrainTreasure> treasures = new List<TrainTreasure>();
            treasures.Add(CreateTreasure(18, "Daffodil", 0.045, 600, 2600));
            treasures.Add(CreateTreasure(20, "Leek", 0.045, 600, 2600));
            treasures.Add(CreateTreasure(24, "Parsnip", 0.02, 600, 2600));
            treasures.Add(CreateTreasure(72, "Diamond", 0.005, 2330, 2530));
            treasures.Add(CreateTreasure(88, "Coconut", 0.025, 1000, 2530));
            treasures.Add(CreateTreasure(90, "Cactus Fruit", 0.0075, 600, 2600));
            treasures.Add(CreateTreasure(130, "Tuna", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(136, "Largemouth Bass", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(140, "Walleye", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(143, "Catfish", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(148, "Eel", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(186, "Large Milk", 0.005, 700, 1130));
            treasures.Add(CreateTreasure(276, "Pumpkin", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(348, "Wine", 0.045, 1400, 2500));

            treasures.Add(CreateTreasure(421, "Sunflower", 0.055, 600, 2600));

            treasures.Add(CreateTreasure(438, "Large Goats Milk", 0.005, 600, 1300));
            treasures.Add(CreateTreasure(446, "Rabits Foot", 0.005, 2100, 2600));
            treasures.Add(CreateTreasure(635, "Orange", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(636, "Peach", 0.005, 600, 2600));
            treasures.Add(CreateTreasure(698, "Sturgeon", 0.005, 1000, 2400));
            treasures.Add(CreateTreasure(715, "Lobster", 0.005, 900, 1900));
            treasures.Add(CreateTreasure(717, "Crab ", 0.005, 1230, 2100));
            treasures.Add(CreateTreasure(720, "Shrimp", 0.005, 1130, 2000));
            treasures.Add(CreateTreasure(721, "Snail", 0.005, 1300, 2400));
            treasures.Add(CreateTreasure(723, "Oyster", 0.005, 1300, 2300));


            return treasures;
        } 

        private static List<TrainTreasure> GetPresentCarTreasureList()
        {
            List<TrainTreasure> treasures = new List<TrainTreasure>();
            treasures.Add(CreateTreasure(74, "Prismatic Shard", 0.49, 600, 2600));            
            treasures.Add(CreateTreasure(797, "Pearl", 0.51, 600, 2600));

            return treasures;
        }

        private static TrainTreasure CreateTreasure(int id, string name, double chance, int startTime, int endTime)
        {
            return new TrainTreasure(id, name, chance, startTime, endTime, true);
        }        
    }
}
