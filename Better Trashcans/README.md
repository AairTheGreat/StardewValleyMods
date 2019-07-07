[Better Trashcans](https://github.com/AairTheGreat/StardewValleyMods/tree/master/Better%20Trashcans) is a [Stardew Valley](http://stardewvalley.net/) mod which changes the way the town's trashcans work.
                                                                                                           
**This documentation is for modders and player. See the [Nexus page - Not Setup Yet](https://www.nexusmods.com/stardewvalley/mods/4161) if you want the compiled mod.**
                                                                                                           
## Contents
* [Install](#install)
* [Introduction](#introduction)
* [Configuration Setting](#configuration-setting)
  * [Overview of config json file](#overview-of-config-json-file)
    - [enableMod](#enableMod)
    - [useCustomTrashcanTreasure](#useCustomTrashcanTreasure)
    - [allowMultipleItemsPerDay](#allowMultipleItemsPerDay)
    - [allowTrashCanRecheck](#allowTrashCanRecheck)
    - [baseChancePercent](#baseChancePercent)
    - [FriendshipPoints](#FriendshipPoints)
    - [LinusFriendshipPoints](#LinusFriendshipPoints)
    - [WaitTimeIfFoundNothing](#WaitTimeIfFoundNothing)
    - [WaitTimeIfFoundSomething](#WaitTimeIfFoundSomething)
  * [Overview of Trashcans json file](#overview-of-trashcans-json-file)
    - [Trashcan Data](#trashcan-data)
    - [Treasure List Data](#Treasure-List)  
* [Troubleshooting](#troubleshooting)  
  * [Bad Edits to config json file](#Bad-Edits-to-config-json-file)
  * [Bad Edits to treasure json file](#Bad-Edits-to-treasure-json-file)
* [Localization](#localization)
* [See Also](#see-also)

## Install
1. If needed, [Install the latest version of SMAPI](https://smapi.io/).
2. Install [this mod from Nexus mods -NOT SETUP YET](https://www.nexusmods.com/stardewvalley/mods/4161).
3. Run the game using SMAPI.

## Introduction
### What is Better Trashcans?
This mod changes behavior of the town's trashcans and hopefully for the better.  

Better Trash Cans allows you to: 
* Fine-tune each trashcan item list
* Allow user to check the trashcans multiple times a day.
* While in a multiplayer game, each farmer/farmhand can get items from the cans.
* Various configurable settings

## Configuration Setting
### Overview of config json file
Once this mod is installed and been started at least once, you can adjust some settings.  

If you don't have a config.json file, then the config file will be created when you first run Stardew Valley with this mod.

You should not need to adjust the configuration settings but if you do, here are what the setting are inside the config.json file:
#### enableMod
Sets the mod as enabled or disabled.  Normally should be set to true unless you are having issues and want to test something without removing the mod.  
- Default Value: true 
#### useCustomTrashcanTreasure
Uses the custom treasure list.  If set to false, then the base game item list is used.  
- Default Value: true 
#### allowMultipleItemsPerDay
Allows to get multiple items from the same trashcan.
- Default Value: true 
#### allowTrashCanRecheck
Allows you check the same trash can multiple times.
- Default Value: true 
#### baseChancePercent
What is the chance to get something from a trashcan.  The player's daily luck does factor into this.
- Default Value: 0.20 (Base Game Value)
#### FriendshipPoints
The amount of friendship points lost (or gained) if someone sees you, other than Linus.    
- Default Value: -25 (Base Game Value) 
#### LinusFriendshipPoints
The amount of friendship points lost (or gained) if Linus sees you.    
- Default Value: 5 (Base Game Value) 
#### WaitTimeIfFoundNothing
Per trashcan, The amount of time in minutes that you have to wait to try again if you have not found anything.
- Default Value: 60
#### WaitTimeIfFoundSomething
Per trashcan, the amount of time in minutes that you have to wait to try again if you did found something.  
- Default Value: 240 

### Overview of Trashcans json file
Everyone loves trashcan diving!  Therefore, it's time to talk about how the configuration file is put together.  Here is the general workflow:
1. Player goes and finds a trashcan.
2. The game generates a random number.
3. The mod compares that number to see if a the player is a lucky winner of some loot.
4. The games generates another random number.
5. The mod then compares that number with the treasure list of the trashcan.
6. Player gets selected treasure. 

The Trashcans.json file is what controls this and it is located in the "Better Trashcans\DataFiles" mod folder.

#### Trashcan Data
The possible treasure is grouped by trashcan and each trashcan has a list of treasure and a corresponding chance.  The mod has the following groups:
  
Each group has the following properties:
#### TrashcanID
This is the internal id used with the mod.  This should not be changed within the Trashcans.json file.  If it's changed, it will break the mod.  
#### LastTimeChecked
When the mod is running this is the last game time the trashcan was checked. Resets each day to -1.
#### LastTimeFoundItem 
When the mod is running this is the last game time the trashcan gave an item. Resets each day to -1.
#### Treasure List 
This is the treasure that can be selected if the trashcan decides to give out an item.  More information on treasure data setting is below.

Field                  | Purpose
---------------------- | -------
`Id`                 |Game internal ID for the object.  You can change this if you know the item id you want.
`Name`                | What humans know the items as.  Not really used by the mod, just useful if you don't know the item ID.
`Chance`          | The chance this treasure is selected if it's treasure group is selected.  Value range: (0.0 - 1.0)
`MinAmount`        | The minimum number of items you can pan per panning spot.
`MAxAmount`               | The maximum number of items you can pan per panning spot.
`Enabled`          | If the treasure is enable to be selected to become the player's treasure.  Note: If you change this setting, you should set the treasure group setting ManualOverride to true.

Here is an example of a treasure group entry looks like:
```
  "JODI_SAM": {
    "TrashcanID": "JODI_SAM",
    "LastTimeChecked": -1,
    "LastTimeFoundItem": -1,
    "treasureList": [
      {
        "Id": 72,
        "Name": "Diamond ",
        "Chance": 0.005,
        "Enabled": true,
        "MinAmount": 1,
        "MaxAmount": 1
      },
      {
        "Id": 88,
        "Name": "Coconut",
        "Chance": 0.0075,
        "Enabled": true,
        "MinAmount": 1,
        "MaxAmount": 1
      }
	]
```

## Troubleshooting

### Bad edits to config json file
It possible that you decided to edit the config file and now it's not working as expected.  To get back to the default config.json file:
1. Stop Stardew Valley, if running.
2. Delete the config.json file.
3. Start Stardew Valley the defaut.json file will be recreated.

### Bad edits to Trashcans json file
It possible that you decided to edit the treasure config file and now it's not working as expected.  To get back to the default treasure.json file:
1. Stop Stardew Valley, if running.
2. Delete the treasure.json file.
3. Start Stardew Valley the defaut.json file will be recreated.
 
## Localization
No real need to localize this mod.

## Other Mod Conflicts
There is a minor conflict with the [Automate]((https://www.nexusmods.com/stardewvalley/mods/1063) mod.  It will only use it's copy of the game logic item list.
Depending on your config settings, you will be able to still check the trashcans manually and will get the mod items.  

## Thank You!
* [Concerned Ape](https://twitter.com/concernedape) - Creator of Stardew Valley.
* [Pathoschild](https://smapi.io/) - Creator of the Stardew Modding API.
* [Stardew Wiki](https://stardewvalleywiki.com) - To the people maintaining this very useful site.
* [Stardew ID List](https://stardewids.com/) - To the people maintaining this very useful site.
* Sylverlyf for giving me the idea for this mod.
* To my testers: SparkyTheCat and My Better Half  -- Thank you!

## See Also
* [Stardew Valley](https://www.stardewvalley.net/) - Home page and blog
* [Stardew Valley Mods Nexus Page](https://www.nexusmods.com/stardewvalley/mods)
