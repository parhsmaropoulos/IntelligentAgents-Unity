using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
    private int remainingGold = 50;
    private int goldValue = 5;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        AgentAI agent = collision.GetComponent<AgentAI>();

        if(agent != null)
        {
            Vector2 point = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
            // Vector2 target = new Vector2(Mathf.Round(agent.primaryTarget.position.x), Mathf.Round(agent.primaryTarget.position.y)).normalized;
            Vector2 target = new Vector2(agent.primaryTarget[0], agent.primaryTarget[1]);
            //Αdd gold value to agent
            if( remainingGold > 0)
            {
                if(!agent.goldPositions.Contains(point))
                    {
                        agent.goldPositions.Add(point);
                    }
                if (point == target){
                    this.Collect(collision);
                    agent.primaryTarget[0] -= 1;
                    agent.primaryTarget[1] -= 1;
                }
            }
            else
            {
                if(agent.goldPositions.Contains(point))
                    // this.gameObject.SetActive(false);
                    agent.goldPositions.Remove(point);
            }

        }
    }

    public void Collect(Collider2D collision)
    {
        AgentAI agent = collision.GetComponent<AgentAI>();

        if(remainingGold > 0)
        {
                if(agent.resource_master == "gold")
                        goldValue ++;

                agent.gold += goldValue;
                agent.carrying += goldValue;

                remainingGold -= goldValue;
                // Debug.Log(remainingGold);

                // if(remainingGold <= 0)
                // {
                //     Destroy(this.gameObject);
                // }
        }else{
            Vector2 point = new Vector2(this.transform.position.x, this.transform.position.y).normalized;
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
