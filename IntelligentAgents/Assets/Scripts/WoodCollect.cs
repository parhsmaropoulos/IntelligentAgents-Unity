using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollect : MonoBehaviour
{
    private int remainingWood = 200;
    private int woodValue = 2;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        AgentAI agent = collision.GetComponent<AgentAI>();

        if(agent != null)
        {
            Vector2 point = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
            Vector2 target = new Vector2(Mathf.Round(agent.primaryTarget[0]), Mathf.Round(agent.primaryTarget[1]));
            
            //Αdd gold value to agent
            if( remainingWood > 0)
            {
                if(!agent.woodPositions.Contains(point))
                    {
                        agent.woodPositions.Add(point);
                        agent.primaryTarget[0] -= 1;
                        agent.primaryTarget[1] -= 1;
                    }
                if (point == target){
                    this.Collect(collision);
                }
            }
            else
            {
                if(agent.woodPositions.Contains(point))
                    agent.woodPositions.Remove(point);
            }

        }
    }

    public void Collect(Collider2D collision)
    {
        AgentAI agent = collision.GetComponent<AgentAI>();
        

        if(remainingWood > 0)
        {
                if(agent.resource_master == "wood")
                        woodValue ++;

                agent.wood += woodValue;
                agent.carrying += woodValue;

                remainingWood -= woodValue;
                
                // if(remainingWood <= 0)
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
