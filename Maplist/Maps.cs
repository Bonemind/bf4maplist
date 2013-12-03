using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maplist
{
    class Maps
    {
        /// <summary>
        /// Array containing Maps
        /// </summary>
        String[] maps = new String[9];

        /// <summary>
        /// Array contining available modes
        /// </summary>
        String[] gametypes = new String[5];

        /// <summary>
        /// List containing banned combos.
        /// Format: [mapname] [mode]
        /// </summary>
        List<String> bannedCombos = new List<String>();

        /// <summary>
        /// List containing all modes for each map, does NOT exclude banned ones
        /// </summary>
        List<String> mapModes = new List<string>();

        /// <summary>
        /// A Rand object
        /// </summary>
        Random rnd = new Random();
        
        /// <summary>
        /// List containing the maps, filtered by banned modes.
        /// </summary>
        List<String> orderedMaps = new List<string>();

        public Maps()
        {
            maps[0] = "MP_Abandoned";
            maps[1] = "MP_Damage";
            maps[2] = "MP_Flooded";
            maps[3] = "MP_Journey";
            maps[4] = "MP_Naval";
            maps[5] = "MP_Prison";
            maps[6] = "MP_Resort";
            maps[7] = "MP_TheDish";
            maps[8] = "MP_Tremors";

            gametypes[0] = "Domination0";
            gametypes[1] = "ConquestLarge0";
            gametypes[2] = "TeamDeathMatch0";
            gametypes[3] = "RushLarge0";
            gametypes[4] = "Obliteration";

            bannedCombos.Add(maps[0] + " " + gametypes[0]);
            bannedCombos.Add(maps[0] + " " + gametypes[2]);
            bannedCombos.Add(maps[2] + " " + gametypes[0]);
            bannedCombos.Add(maps[2] + " " + gametypes[2]);
            bannedCombos.Add(maps[2] + " " + gametypes[3]);
            bannedCombos.Add(maps[6] + " " + gametypes[0]);
            bannedCombos.Add(maps[6] + " " + gametypes[3]);
            bannedCombos.Add(maps[7] + " " + gametypes[0]);
            bannedCombos.Add(maps[7] + " " + gametypes[2]);
        }

        /// <summary>
        /// Merges the maps and gametypes arrays
        /// This way we know all maps will be spaces out evenly and all modes are in the list
        /// </summary>
        public void orderMaps()
        {
            int mode = 0;
            //Adds all gamemodes to all maps
            while (orderedMaps.Count < 45)
            {
                foreach (String map in maps)
                {
                    String rndMode = gametypes[mode];
                    mode++;
                    if (mode >= gametypes.Length)
                    {
                        mode = 0;
                    }
                    orderedMaps.Add(map + " " + rndMode);
                    
                }
            }
            
            /*string[] mapArray = orderedMaps.ToArray();

            //Checks if each combination is a banned one, if so it tries to find a combination that is not banned
            //If after 5  tries no combination is found that would result in double modes, we return a double mode
            for (int i = 0; i < mapArray.Length; i++)
            {
                int prevIndex = i - 1;
                int nextIndex = i + 1;
                if (i == 0)
                {
                    prevIndex = mapArray.Length - 1;
                }

                if (i >= mapArray.Length - 1)
                {
                    nextIndex = 0;
                }

                while (!isAllowedMode(mapArray[i].Split(' ')[0], mapArray[i].Split(' ')[1]))
                {
                    mapArray[i] = mapArray[i].Split(' ')[0] + " " + getRandomMode(getModeFromLine(mapArray[prevIndex]), getModeFromLine(mapArray[nextIndex]));
                }

                mapArray[i] = mapArray[i] + " " + getModeRounds(getModeFromLine(mapArray[i]));
            }*/

            foreach (String map in bannedCombos)
            {
                try
                {
                    orderedMaps.Remove(map);
                }
                catch (Exception e)
                {

                }
            }


            foreach (String map in orderedMaps)
            {
                Console.WriteLine(map + " " + getModeRounds(getModeFromLine(map)));
            }
            Console.WriteLine("Mapcount:" + orderedMaps.Count);
        }

        /// <summary>
        /// Checks if a mode is banned
        /// </summary>
        /// <param name="map">The map</param>
        /// <param name="mode">The mode</param>
        /// <returns>True if it is, false otherwise</returns>
        private bool isAllowedMode(String map, String mode)
        {
            foreach (String combo in bannedCombos)
            {
                if ((map + " " + mode).Equals(combo))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a random mode that is not the previous or next mode
        /// Except if we have tried to find another one 5 times
        /// </summary>
        /// <param name="prevMode">The previous mode</param>
        /// <param name="nextMode">The next mode</param>
        /// <returns>A random mode, most propably not the same as prev or next</returns>
        private String getRandomMode(String prevMode, String nextMode)
        {
            int modeIndex = rnd.Next(0, gametypes.Length);
            int tryCount = 0;
            while (prevMode.Equals(gametypes[modeIndex]) || nextMode.Equals(gametypes[modeIndex]))
            {
                modeIndex = rnd.Next(0, gametypes.Length);
                tryCount++;
                if (tryCount >= 5)
                {
                    break;
                }
            }

            return gametypes[modeIndex];
        }

        /// <summary>
        /// Function to get the mode from a line in the format [map] [mode]
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private string getModeFromLine(String line)
        {
            return line.Split(' ')[1];
        }

        /// <summary>
        /// Returns the number of rounds a map and mode should run for
        /// </summary>
        /// <param name="mode">The mode</param>
        /// <returns>The number of rounds</returns>
        private int getModeRounds(String mode)
        {
            if (mode.Equals("RushLarge0"))
            {
                return 2;
            }
            return 1;
        }
    }

    
}
