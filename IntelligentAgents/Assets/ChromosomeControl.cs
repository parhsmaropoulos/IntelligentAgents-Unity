using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChromosomeControl : MonoBehaviour
{
    public int[] chromosomes = new int[11];
    Rigidbody rb;
    AgentAI ai_agent;
    // Start is called before the first frame update
    void Start()
    {
        ai_agent  = GetComponent<AgentAI>();
        // Initialize chromosome for the agent
        for(int i = 0; i < chromosomes.Length ; i++)
        {   
            chromosomes[i] = Random.Range(0,2);
        }

        ai_agent.chromosomes = chromosomes;
        GetChromosomeAbilities(false);

    }
    // Get abilities based on the chromosomes of each agent
    public void GetChromosomeAbilities(bool update)
    {
        if(update){
            ai_agent  = GetComponent<AgentAI>();
        }
        GetMoveSpeed();
        GetMaster();
        GetTransferLimit();
        GetStartingGold();
        GetStartingEnergyPots();
        GetStartingEnergy();

    }

    void GetMoveSpeed()
    {
        int chrom = chromosomes[0];
        if (chrom == 0)
            ai_agent.speed = 100;
        else
            ai_agent.speed = 200;
    }

    void GetMaster()
    {
        int chrom_a = chromosomes[1];
        int chrom_b = chromosomes[2];

        if(chrom_a == 0)
        {  
            if(chrom_b == 0)
            {
                ai_agent.resource_master = "wood";
            }else{
                ai_agent.resource_master = "rock";
            }

        }else{
            if(chrom_b == 0)
            {   
                ai_agent.resource_master = "gold";
            }else{
                ai_agent.resource_master = "all";
            }
        }

    }

    void GetTransferLimit()
    {
        int chrom_a = chromosomes[3];
        int chrom_b = chromosomes[4];

        if(chrom_a == 0)
        {  
            if(chrom_b == 0)
            {
                ai_agent.carry_limit = 1;
            }else{
                ai_agent.carry_limit = 2;
            }

        }else{
            if(chrom_b == 0)
            {
                ai_agent.carry_limit = 3;    
            }else{
                ai_agent.carry_limit = 4;
            }
        }
    }
    
    void GetStartingGold()
    {
        int chrom_a = chromosomes[5];
        int chrom_b = chromosomes[6];

        if(chrom_a == 0)
        {  
            if(chrom_b == 0)
            {
                ai_agent.gold = 10;
            }else{
                ai_agent.gold = 20;
            }

        }else{
            if(chrom_b == 0)
            {
                ai_agent.gold = 40;
            }else{
                ai_agent.gold = 80;
            }
        }
    }

    void GetStartingEnergyPots()
    {
        int chrom_a = chromosomes[7];
        int chrom_b = chromosomes[8];

        if(chrom_a == 0)
        {  
            if(chrom_b == 0)
            {
                ai_agent.energy_pots = 1;
            }else{
                ai_agent.energy_pots = 2;
            }

        }else{
            if(chrom_b == 0)
            {
                ai_agent.energy_pots = 3;
            }else{
                ai_agent.energy_pots = 4;
            }
        }
    }

    void GetStartingEnergy()
    {
        int chrom_a = chromosomes[9];
        int chrom_b = chromosomes[10];

        if(chrom_a == 0)
        {  
            if(chrom_b == 0)
            {
                ai_agent.energy = 50;
            }else{
                ai_agent.energy = 100;
            }

        }else{
            if(chrom_b == 0)
            {
                ai_agent.energy = 200;
            }else{
                ai_agent.energy = 400;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
