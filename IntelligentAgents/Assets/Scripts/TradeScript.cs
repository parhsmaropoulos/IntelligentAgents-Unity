using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeScript : MonoBehaviour
{
    AgentAI agentAI;
    List<Vector2> curr_knowledge;
    List<Vector2> neighbor_knowledge;

    // Map trade cost
    // int MapCost = 10;
    // int PotCost = 20;
    // Start is called before the first frame update
    void Start()
    {
        agentAI = GetComponent<AgentAI>();
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if(Time.timeSinceLevelLoad > 5)
        {
            AgentAI neighbor = coll.GetComponent<AgentAI>();
            if(neighbor != null)
            {
            
                if(agentAI.gold >= GameData.map_cost )
                {
                    // if(neighbor.village_citizen == agentAI.village_citizen)
                    // {
                            // Same village.
                            if(WantToBuy(neighbor))
                            {
                                MakeMapTrade(neighbor);
                            }else{
                                return;
                            }
                    // }else{
                    //     // Not same.
                        
                    // }
                }
            

                if(agentAI.gold >= GameData.pot_cost && agentAI.energy_pots < 3 && neighbor.energy_pots > 1)
                {
                    if(neighbor.village_citizen == agentAI.village_citizen)
                    {
                        agentAI.gold -= GameData.pot_cost;
                        agentAI.energy_pots ++;

                        neighbor.gold += GameData.pot_cost;
                        neighbor.energy_pots --;
                        string action1 = "Just bought a pot!";
                        string action2 = "Just sold a pot!";
                        GameData.trades_count++;
                        agentAI.actions.Add(action1);
                        neighbor.actions.Add(action2);
                        return;
                    }else{
                        // Not same.
                    }
                }
                return;
            }
            return;
        }
        return;
    }



    void MakeMapTrade(AgentAI neighbor){
        // Add general 
        for(int i=0; i < neighbor_knowledge.Count; i++)
        {
            Vector2 point = neighbor_knowledge[i];
            if(!agentAI.knowledge.Contains(point))
                agentAI.knowledge.Add(point);
            // Check if new knowledge contains potspos, goldpos etc..
            if(neighbor.potPositions.Contains(point) && !agentAI.potPositions.Contains(point))
            {
                agentAI.potPositions.Add(point);
            }
            if(neighbor.goldPositions.Contains(point) && !agentAI.goldPositions.Contains(point))
            {
                agentAI.potPositions.Add(point);
            }
            if(neighbor.rockPositions.Contains(point) && !agentAI.rockPositions.Contains(point))
            {
                agentAI.rockPositions.Add(point);
            }
            if(neighbor.woodPositions.Contains(point) && !agentAI.woodPositions.Contains(point))
            {
                agentAI.woodPositions.Add(point);
            }
        }
        agentAI.gold -= GameData.map_cost;
        neighbor.gold += GameData.map_cost;
        string action1 = "Just bought a map!";
        string action2 = "Just sold a map!";
        agentAI.actions.Add(action1);
        neighbor.actions.Add(action2);
        return;
    }
    bool WantToBuy(AgentAI neighbor){
        int new_knowledge= 0;
        curr_knowledge = agentAI.knowledge;
        neighbor_knowledge = neighbor.knowledge;
        // Debug.Log(neighbor_knowledge);
        for(int i=0; i<neighbor_knowledge.Count;i++)
        {
            if(!curr_knowledge.Contains(neighbor_knowledge[i])){
                new_knowledge++;
            }
        }
        // Determine how many new places will be the limit to want the map
        if(new_knowledge > 20)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
