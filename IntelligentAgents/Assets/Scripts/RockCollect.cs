using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollect : MonoBehaviour
{
    private int remainingRock = 200;
    private int rockValue = 2;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        AgentAI agent = collision.GetComponent<AgentAI>();

        if(agent != null)
        {
            Vector2 point = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
            Vector2 target = new Vector2(Mathf.Round(agent.primaryTarget[0]), Mathf.Round(agent.primaryTarget[1]));

            //Αdd gold value to agent
            if( remainingRock > 0)
            {
                if(!agent.rockPositions.Contains(point))
                agent.rockPositions.Add(point);
                if (point == target){
                    this.Collect(collision);
                    agent.primaryTarget[0] -= 1;
                    agent.primaryTarget[1] -= 1;
                }
            }
            else
            {
                if(agent.rockPositions.Contains(point))
                    agent.rockPositions.Remove(point);
            }

        }
    }

    public void Collect(Collider2D collision)
    {
        AgentAI agent = collision.GetComponent<AgentAI>();

        if(remainingRock > 0)
        {
                if(agent.resource_master == "rock")
                        rockValue ++;

                agent.rock += rockValue;
                agent.carrying += rockValue;

                remainingRock -= rockValue;
                
                // if(remainingRock <= 0)
                // {
                //     Destroy(this.gameObject);
                // }
        }else{
            Vector2 point = new Vector2(this.transform.position.x, this.transform.position.y);
            Debug.Log("it is empty!");
            if(agent.rockPositions.Contains(point))
                    agent.rockPositions.Remove(point);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
