using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class WorldMapInitializer : MonoBehaviour
{
    private int[,] worldMap;
    public Vector3 tmapSize;


    public Tilemap basicMap;
    public Tilemap fogMap;
    public Tilemap obstaclesMap;
    public Tilemap borderMap;

    public GameObject VillageA;
    public GameObject VillageB;
    public GameObject gameMenu;
    public List<Vector2> WoodPos;
    public List<Vector2> RockPos;
    public GameObject Mutation;

    private VillageScript villaA;

    private VillageScript villaB;
    public GameObject Energy_Potion;
    public GameObject Gold;
    public GameObject Wood;
    public GameObject Rock;

    public GameObject Ai_Agent;

    public Camera AiCamera;
    [Range(1, 10)]
    public int numR = 2;

    private int count = 0;
    public Tile[] basicTiles;
    public Tile fogTile;
    public Tile[] obstaclesTiles;


    int width;
    int height;

    public void exitGame(){
        Application.Quit();
    }

    public void Continue(){
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void InitializeGame(int numR)
    {
        clearMap(false);
        float start_time = Time.time;
        width = (int)tmapSize.x;
        height = (int)tmapSize.y;
        if (worldMap == null)
        {
            worldMap = new int[width, height];
            InitPos();
        }

        // Set Basic Tiles and fog
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (worldMap[x, y] == 1)
                    basicMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), basicTiles[0]);
                fogMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), fogTile);
            }
        }
        // Set border Tiles
        for (int x = -width / 2; x < width / 2; x++)
        {
            borderMap.SetTile(new Vector3Int(x, height / 2 + 1, 0), basicTiles[1]);
            borderMap.SetTile(new Vector3Int(x, -height / 2, 0), basicTiles[1]);
        }
        for (int y = -height / 2; y < height / 2; y++)
        {
            borderMap.SetTile(new Vector3Int(width / 2 + 1, y, 0), basicTiles[1]);
            borderMap.SetTile(new Vector3Int(-width / 2, y, 0), basicTiles[1]);
        }
        if(GameData.useTxt == true)
        {
            ReadTxTFile();
        }

        GenVillages();
        GenPotions();
        GenGolds();
        GenWoods();
        GenRocks();
        GenAgents();
        // Gen resources()
        // Gen agents(){cameras too}
    }

    private void ReadTxTFile()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/Level.txt", true);
        int line = 0;
        while (!reader.EndOfStream)
        {
            string lin = reader.ReadLine();
            string[] values = lin.Split(',');
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == "W")
                {
                    WoodPos.Add(new Vector2(i, line));
                }
                if (values[i] == "R")
                {
                    RockPos.Add(new Vector2(i, line));
                }
            }
            line++;
        }
    }

    // Set villages at top bottom corner and top left corner
    public void GenVillages()
    {
        int village_a_x = -(int)Mathf.Round((2 * width) / 5);
        int village_a_y = -(int)Mathf.Round((2 * height) / 5);
        int village_b_x = (int)Mathf.Round((2 * width) / 5);
        int village_b_y = (int)Mathf.Round((2 * height) / 5);
        // Village a instatiate
        GameObject villageA = Instantiate(VillageA, new Vector3(village_a_x, village_a_y, 0), Quaternion.identity);
        villageA.name = "Village_A";
        villaA = villageA.GetComponent<VillageScript>();
        GameData.VillageA = villaA;
        // Village b instatiate
        GameObject villageB = Instantiate(VillageB, new Vector3(village_b_x, village_b_y, 0), Quaternion.identity);
        villageB.name = "Village_B";
        villaB = villageA.GetComponent<VillageScript>();
        GameData.VillageB = villaB;
        return;
    }

    public void GenPotions()
    {
        int num = GameData.no_of_pots;
        int x = 0;
        int y = 0;
        GameObject[] villages = GameObject.FindGameObjectsWithTag("Village");
        for (int i = 0; i < num; i++)
        {
            // Generate at random pos a potion
            x = Random.Range((-width / 2) + 2, width / 2 - 1);
            y = Random.Range((-height / 2) + 2, height / 2 - 1);
            for (int j = 0; j < villages.Length; j++)
            {
                if (x == villages[j].transform.position.x && y == villages[j].transform.position.y)
                {
                    x = x++;
                    y = y++;
                }
            }
            Instantiate(Energy_Potion, new Vector3Int(x, y, 0), Quaternion.identity);
        }

        return;
    }

    public void GenGolds()
    {
        int num = GameData.no_of_golds;
        int x = 0;
        int y = 0;
        GameObject[] villages = GameObject.FindGameObjectsWithTag("Village");
        for (int i = 0; i < num; i++)
        {
            // Generate at random pos a potion
            x = Random.Range((-width / 2) + 2, width / 2 - 1);
            y = Random.Range((-height / 2) + 2, height / 2 - 1);
            for (int j = 0; j < villages.Length; j++)
            {
                if (x == villages[j].transform.position.x && y == villages[j].transform.position.y)
                {
                    x = x++;
                    y = y++;
                }
            }
            Instantiate(Gold, new Vector3Int(x, y, 0), Quaternion.identity);
        }

        return;
    }

    public void GenRocks()
    {
        if (GameData.useTxt == false)
        {
            int x = 0;
            int y = 0;
            GameObject[] villages = GameObject.FindGameObjectsWithTag("Village");
            GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
            for (int i = 0; i < (0.3/100)*(width*height); i++)
            {
                // Generate at random pos a potion
                x = Random.Range((-width / 2) + 2, width / 2 - 1);
                y = Random.Range((-height / 2) + 2, height / 2 - 1);
                for (int j = 0; j < villages.Length; j++)
                {
                    if (x == villages[j].transform.position.x && y == villages[j].transform.position.y)
                    {
                        x = x++;
                        y = y++;
                    }
                }
                Instantiate(Rock, new Vector3Int(x, y, 0), Quaternion.identity);
            }
        }
        if (GameData.useTxt == true)
        {
            for (int i = 0; i < RockPos.Count; i++)
            {
                Instantiate(Rock, new Vector3Int((int)RockPos[i][0] - width / 2, (int)RockPos[i][1] - height / 2, 0), Quaternion.identity);
            }
        }
        return;
    }

    public void GenWoods()
    {
        if (GameData.useTxt == false)
        {
            int x = 0;
            int y = 0;
            GameObject[] villages = GameObject.FindGameObjectsWithTag("Village");
            GameObject[] resources = GameObject.FindGameObjectsWithTag("Resource");
            for (int i = 0; i < (0.3/100)*(width*height); i++)
            {
                // Generate at random pos a potion
                x = Random.Range((-width / 2) + 2, width / 2 - 1);
                y = Random.Range((-height / 2) + 2, height / 2 - 1);
                for (int j = 0; j < villages.Length; j++)
                {
                    if (x == villages[j].transform.position.x && y == villages[j].transform.position.y)
                    {
                        x = x++;
                        y = y++;
                    }
                }
                Instantiate(Wood, new Vector3Int(x, y, 0), Quaternion.identity);
            }
        }
        if (GameData.useTxt == true)
        {
            for (int i = 0; i < WoodPos.Count; i++)
            {
                Instantiate(Wood, new Vector3Int((int)WoodPos[i][0] - width / 2, (int)WoodPos[i][1] - height / 2, 0), Quaternion.identity);
            }
        }
        return;
    }

    public void GenAgents()
    {
        int cameraI = 1;
        int num = GameData.no_of_agents;
        int x = 0;
        int y = 0;
        GameObject mutation = Instantiate(Mutation, new Vector3(0, 0, 0), Quaternion.identity);
        mutation.SetActive(true);
        MutationScript mutScript = mutation.GetComponent<MutationScript>();
        GameObject[] villages = GameObject.FindGameObjectsWithTag("Village");
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < villages.Length; j++)
            {
                x = (int)villages[j].transform.position.x;
                y = (int)villages[j].transform.position.y;
                GameObject agent = Instantiate(Ai_Agent, new Vector3Int(x + 1, y + 1, 0), Quaternion.identity);
                agent.name = "Agent_" + j + "_" + i;

                AgentAI ai = agent.GetComponent<AgentAI>();
                GameData.agents.Add(ai);
                int gender = Random.Range(0, 2);
                // Debug.Log("ai no:" + i + "obj:" + ai);
                mutScript.agents.Add(ai);
                if (j == 0)
                {
                    ai.village_citizen = "Village_A";
                    ai.villagePosition = villages[j].transform;
                }
                else
                {
                    ai.village_citizen = "Village_B";
                    ai.villagePosition = villages[j].transform;
                }
                int choice = Random.Range(0, 2);
                if (choice == 0)
                    ai.man = true;
                else
                    ai.man = false;

                FogScript fogScrpt = agent.GetComponent<FogScript>();
                fogScrpt.tilemap = fogMap;
                Camera aiCamera = Instantiate(AiCamera, new Vector3(x, y, 0), Quaternion.identity);
                CameraFollow camF = aiCamera.GetComponent<CameraFollow>();
                camF.target = agent.transform;
                // aiCamera.targetDisplay = cameraI + 1;
                cameraI++;
            }
        }

        return;
    }





    // public int [,] genTilePos(int[,] oldMap)
    // {
    //     int[,] newMap = new int[width, height];

    //     int neighb;

    //     BoundsInt myB = new BoundsInt(-1,-1,0,3,3,1);
    //     for(int x = 0; x < width; x++)
    //     {
    //         for(int y = 0; y <height; y++)
    //         {
    //             neighb = 0;
    //             foreach(var b in myB.allPositionsWithin)
    //             {
    //                 if(b.x ==0 && b.y ==0) continue;
    //                 if(x+b.x >= 0 && x+b.x < width && y+b.y >= 0 && y+b.y < height)
    //                 {
    //                     neighb += oldMap[x +b.x, y+b.y];
    //                 }
    //                 else
    //                 {
    //                     neighb ++;
    //                 }
    //             }
    //             if(oldMap[x,y] == 1)
    //             {
    //                 if(neighb < de)
    //             }
    //         }
    //     }
    //     return newMap;
    // }

    public void Start()
    {
        gameMenu = Instantiate(gameMenu, new Vector3Int(0,0,0), Quaternion.identity);
        Button[] buttons = gameMenu.GetComponentsInChildren<Button>();
        for(int i = 0; i<buttons.Length;i++)
        {
            Debug.Log(buttons[i].name);
            if(buttons[i].name == "exit")
                buttons[i].onClick.AddListener(() => {this.exitGame();});
            if(buttons[i].name == "continue")
                buttons[i].onClick.AddListener(() => {this.Continue();});
        }

        gameMenu.SetActive(false);
        tmapSize = new Vector3Int(GameData.map_size_x, GameData.map_size_y, 0);
        InitializeGame(numR);
    }
    public void InitPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                worldMap[x, y] = 1;
            }
        }
    }

    public void clearMap(bool complete)
    {
        basicMap.ClearAllTiles();
        fogMap.ClearAllTiles();
        obstaclesMap.ClearAllTiles();

        if (complete)
        {
            worldMap = null;
            // Game ended!
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            if(Time.timeScale != 0)
            {
                gameMenu.SetActive(true);
                Time.timeScale = 0;
            }else{
                gameMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
