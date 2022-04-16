using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapEdgeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hey there");
        AgentAI agent = collider.GetComponent<AgentAI>();
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        if(agent != null)
        {
            rb.velocity = -rb.velocity;
            rb.angularVelocity = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
