using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossScript : MonoBehaviour
{
    AgentAI agentAI, neightbor;
    public GameObject child;
    public Camera AiCamera;
    bool Triggerd;
    private int[] firstChromosomes, secondChromosomes;
    private int[] firstKidChromosomes = new int[11];
    private int[] secondKidChromosomes = new int[11];
    private double crossChance = 1.5;
    // Start is called before the first frame update
    void Start()
    {
        agentAI = GetComponent<AgentAI>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        neightbor = coll.GetComponent<AgentAI>();
        if(neightbor != null)
        {
            if(Time.realtimeSinceStartup >= 5)
            {
                // Debug.Log("Tried to cross");
                if(agentAI.man && !neightbor.man)
                {   
                    // Debug.Log("tried to cross");
                    
                    float rand = Random.Range(0f, 100f);
                    // Debug.Log(rand);
                    if (rand < crossChance){
                        MakeCross(neightbor);
                        string action1 = "Made a Cross with Agent name "+ neightbor.name;
                        string action2 = "Made a Cross with Agent name "+ agentAI.name;
                        neightbor.actions.Add(action2);
                        agentAI.actions.Add(action1);
                    }
                }
            }
        }
    }

    void MakeCross(AgentAI neightbor)
    {
        // 1st parent chomosomes
        firstChromosomes = agentAI.chromosomes;
        // 2nd parent chromosomes
        secondChromosomes = neightbor.chromosomes;

        // Where to split them
        int split = Random.Range(1,10);

        for(int i=0;i <11; i++)
        {
            if( i < split)
            {
                firstKidChromosomes[i] = firstChromosomes[i];
            }
            else
            {
                secondKidChromosomes[i] = firstChromosomes[i];
            }
        }
        for(int i=0; i < 11; i++)
        {
            if(i < split){
                firstKidChromosomes[i] = secondChromosomes[i];
            }else{
                secondKidChromosomes[i] = secondChromosomes[i];
            }
        }
        CreateChilds();
    }

    public void CreateChilds(){
        Vector3Int pos = new Vector3Int((int)this.transform.position.x, (int)this.transform.position.y, 0);

        GameObject child_1 = Instantiate(child, pos, Quaternion.identity);
        AgentAI child_1_ai = child_1.GetComponent<AgentAI>();
        Debug.Log("firs kid length"+ firstKidChromosomes.Length);
        child_1_ai.SetChildChromosomes(firstKidChromosomes);  
        child_1_ai.village_citizen = agentAI.village_citizen;  
            

        GameObject child_2 = Instantiate(child, pos, Quaternion.identity);
        AgentAI child_2_ai = child_2.GetComponent<AgentAI>();
        Debug.Log("2nd kid length"+ firstKidChromosomes.Length);

        child_2_ai.SetChildChromosomes(secondKidChromosomes);
        child_2_ai.village_citizen = neightbor.village_citizen;

        SetChildKnowledges(child_1_ai, child_2_ai, agentAI, neightbor);
        GameData.confirmed_crosses++;

        Camera aiCamera = Instantiate(AiCamera, new Vector3(pos[0], pos[1], 0), Quaternion.identity);
        CameraFollow camF = aiCamera.GetComponent<CameraFollow>();
        camF.target = child_1.transform;


        GameData.agents.Add(child_1_ai);
        GameData.agents.Add(child_2_ai);
        neightbor.AgentDeath();
        agentAI.AgentDeath();
    }   

    public void SetChildKnowledges(AgentAI child_1, AgentAI child_2, AgentAI parent_1, AgentAI parent_2)
    {
        List<Vector2> knowledge = parent_1.knowledge;
        List<Vector2> goldPos = parent_1.woodPositions;
        List<Vector2> woodPos = parent_1.goldPositions;
        List<Vector2> rockPos = parent_1.rockPositions;
        List<Vector2> potPos = parent_1.potPositions;
        for(int i=0; i<parent_2.knowledge.Count; i++)
        {
            if(!knowledge.Contains(parent_2.knowledge[i]))
            {
                knowledge.Add(parent_2.knowledge[i]);
            }
        }
        for(int i=0; i<parent_2.woodPositions.Count; i++)
        {
            if(!woodPos.Contains(parent_2.woodPositions[i]))
            {
                woodPos.Add(parent_2.woodPositions[i]);
            }
        }
        for(int i=0; i<parent_2.goldPositions.Count; i++)
        {
            if(!goldPos.Contains(parent_2.goldPositions[i]))
            {
                goldPos.Add(parent_2.goldPositions[i]);
            }
        }
        for(int i=0; i<parent_2.rockPositions.Count; i++)
        {
            if(!rockPos.Contains(parent_2.rockPositions[i]))
            {
                rockPos.Add(parent_2.rockPositions[i]);
            }
        }
        for(int i=0; i<parent_2.potPositions.Count; i++)
        {
            if(!potPos.Contains(parent_2.potPositions[i]))
            {
                potPos.Add(parent_2.potPositions[i]);
            }
        }

        int totalGold = parent_1.gold + parent_2.gold;
        int totalWood = parent_1.wood + parent_2.wood;
        int totalRock = parent_1.rock + parent_2.rock;
        int totalEnergyPots = parent_1.energy_pots + parent_2.energy_pots;
        
        // Random slice for Knowledge
        int mapRnd = Random.Range(0,knowledge.Count);
        for(int i = 0; i < knowledge.Count; i++)
        {
            if(i<mapRnd)
            {
                child_1.knowledge.Add(knowledge[i]);
            }else{
                child_2.knowledge.Add(knowledge[i]);
            }
        }

        // Random slice for WoodPos
        int woodPosRnd = Random.Range(0,woodPos.Count);
         for(int i = 0; i < woodPos.Count; i++)
        {
            if(i<woodPosRnd)
            {
                child_1.woodPositions.Add(woodPos[i]);
            }else{
                child_2.woodPositions.Add(woodPos[i]);
            }
        }
        // Random slice for GoldPos
        int goldPosRnd = Random.Range(0,goldPos.Count);
         for(int i = 0; i < goldPos.Count; i++)
        {
            if(i<goldPosRnd)
            {
                child_1.goldPositions.Add(goldPos[i]);
            }else{
                child_2.goldPositions.Add(goldPos[i]);
            }
        }
        // Random slice for RockPos
        int rockPosRnd = Random.Range(0,rockPos.Count);
         for(int i = 0; i < rockPos.Count; i++)
        {
            if(i<rockPosRnd)
            {
                child_1.rockPositions.Add(rockPos[i]);
            }else{
                child_2.rockPositions.Add(rockPos[i]);
            }
        }
        // Random slice for PotPos
        int potPosRnd = Random.Range(0,potPos.Count);
         for(int i = 0; i < potPos.Count; i++)
        {
            if(i<potPosRnd)
            {
                child_1.potPositions.Add(potPos[i]);
            }else{
                child_2.potPositions.Add(potPos[i]);
            }
        }
        // Random slice for Golds
        int goldsRnd = Random.Range(0,totalGold);
        child_1.gold += totalGold - goldPosRnd;
        child_2.gold += goldPosRnd;
        // Random slice for Wood
        int woodRnd = Random.Range(0,totalWood);
        child_1.wood += totalWood - woodPosRnd;
        child_2.wood += woodPosRnd;
        // Random slice for Rock
        int rockRnd = Random.Range(0,totalRock);
        child_1.rock += totalRock - rockPosRnd;
        child_2.rock += rockPosRnd;
        // Random slice for Pots
        int potsRnd = Random.Range(0,totalEnergyPots);
        child_1.energy_pots += totalEnergyPots - potsRnd;
        child_2.energy_pots += potsRnd;

        child_1.man = parent_1.man;
        child_2.man = parent_2.man;

        if(parent_1.name.Contains("Agent") && parent_2.name.Contains("Agent"))
        {
            child_1.name = "First_Gen_Kid";
            child_2.name = "First_Gen_Kid";
        }
        if(parent_1.name.Contains("First") || parent_2.name.Contains("First"))
        {
            child_1.name = "Second_Gen_Kid";
            child_2.name = "Second_Gen_Kid";
        }
        if(parent_1.name.Contains("Second") || parent_2.name.Contains("Second"))
        {
            child_1.name = "Third_Gen_Kid";
            child_2.name = "Third_Gen_Kid";
        }

        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

