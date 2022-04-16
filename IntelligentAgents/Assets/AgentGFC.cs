using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AgentGFC : MonoBehaviour
{
    public AIPath aIPath;

    // Update is called once per frame
    void Update()
    {
        if(aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if(aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
