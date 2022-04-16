using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VillageScript : MonoBehaviour
{
    public int TotalResources = 0;
    public int wood = 0;
    public int gold = 0;
    public int rock = 0;
    GameObject village;

    
    public void OnTriggerEnter2D(Collider2D coll)
    {
        AgentAI agent = coll.GetComponent<AgentAI>();

        Debug.Log(this.name);
        if(agent != null && agent.village_citizen == this.name )
        {
            wood += agent.wood;
            TotalResources += agent.wood;
            agent.resourceContributed += agent.wood;
            agent.wood = 0;

            rock += agent.rock;
            TotalResources += agent.rock;
            agent.resourceContributed += agent.rock;
            agent.rock = 0;

            if(agent.gold != 0)
            {
                gold += agent.gold/2;
                TotalResources += agent.gold/2;
                agent.resourceContributed += agent.gold/2;
                agent.gold /= 2;
                
            }
            // 100 resources of 5 mins
            if(TotalResources >= 3000 || Time.timeSinceLevelLoad >= 800)
            {
                VillageScript[] vilalges = FindObjectsOfType<VillageScript>();
                // Debug.Log(vilalges[0]);
                // Debug.Log(vilalges[1]);
                if(Time.timeSinceLevelLoad >= 800)
                {
                    GameData.TimesUp = true;
                }
                if(TotalResources >= 3000)
                {
                    GameData.Winner = true;
                }
                GameData.VillageB = vilalges[0];
                GameData.VillageA = vilalges[1];
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }

        UpdateCanvas();
    }

    public void UpdateCanvas(){
        if(this.name == "Village_A"){
            LiveData.villageA_total = TotalResources;
            LiveData.villageA_rock = rock;
            LiveData.villageA_gold = gold;
            LiveData.villageA_wood = wood;
        };
        if(this.name == "Village_B")
        {
            LiveData.villageB_total = TotalResources;
            LiveData.villageB_rock = rock;
            LiveData.villageB_gold = gold;
            LiveData.villageB_wood = wood;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
