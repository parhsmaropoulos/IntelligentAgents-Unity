using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationScript : MonoBehaviour
{
    public List<AgentAI> agents;
    public int[] mutations;
    private double mutationChance = 0.1f;

    private int tick = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        // mutations = new int[agents.Count * 11];
    }

    // Update is called once per frame
    void Update()
    {
        tick++;
        if(tick == 50)
        {
            mutations = new int[agents.Count * 11];
            // Try to mutate.
            for(int i= 0; i<mutations.Length; i++)
            {
                double chance= Random.Range(0f, 100f);
                if (chance <= mutationChance)
                {
                    // Debug.Log(i);
                    int chrom_position = i % 11;
                    int agent_position = (i / 11);
                    // Debug.Log(chrom_position);
                    // Debug.Log(agent_position);
                    UpdateChromosome(chrom_position, agent_position);
                }
            }
            tick = 0;
        }
        
    }

    void UpdateChromosome(int chrom_position, int agent_position)
    {
        if(agent_position == (agents.Count))
            agent_position --;
        if(agents[agent_position] != null)
        {
            agents[agent_position].UpdateChromosome(chrom_position);
            Debug.Log("Mutation Achived!");
            GameData.mutations_count ++;
        }
    }
}
