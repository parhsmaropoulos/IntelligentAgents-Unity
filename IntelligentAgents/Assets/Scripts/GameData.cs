using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData {

        public static int map_size_x { get; set;}
        public static int map_size_y  { get; set;}
        public static int no_of_agents { get; set;}
        public static int no_of_pots { get; set;}
        public static int no_of_golds { get; set;}
        public static  int pot_cost { get; set;}
        public static int map_cost { get; set;}
        public static int mutations_count { get; set;}
        public static int confirmed_crosses { get; set;}
        public static VillageScript VillageA { get; set; }
        public static VillageScript VillageB { get; set; }
        public static List<AgentAI> agents {get; set;}
        public static bool GameOver;
        public static bool Winner;
        public static bool TimesUp;
        public static int trades_count;
        public static bool useTxt = false;
        static GameData()
        {
                agents = new List<AgentAI>();
                
                VillageA = new VillageScript();
                VillageB = new VillageScript();
        }

}
