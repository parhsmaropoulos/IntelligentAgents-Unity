using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotCollect : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collison)
    {
        AgentAI agent = collison.GetComponent<AgentAI>();
        Rigidbody2D rb = collison.GetComponent<Rigidbody2D>();
        Vector2 point = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));

        if(agent != null)
        {
            if(agent.energy_pots < 4)
            {
                rb.velocity = Vector2.zero; rb.angularVelocity = 0f;
                agent.energy_pots++;
                Destroy(this.gameObject);
            }else{
                if(!agent.potPositions.Contains(point))
                {
                    agent.potPositions.Add(point);
                }
            }
        }
    }
}
