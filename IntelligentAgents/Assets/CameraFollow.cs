using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    public int tick  =0;
    void FixedUpdate ()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }
        if(tick == 10){
            UpdateLiveData();
        }
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void Start()
    {
        AgentAI agent = target.GetComponent<AgentAI>();
        UpdateLiveData();
    }

    public void UpdateLiveData(){
        AgentAI agent = target.GetComponent<AgentAI>();

        LiveData.Agent_Name = agent.name;
        LiveData.Agent_Gold = agent.gold;
        LiveData.Agent_Energy = agent.energy;
        LiveData.Agent_Rock = agent.rock;
        LiveData.Agent_Wood = agent.wood;
        LiveData.Agent_Village = agent.village_citizen;
        LiveData.Agent_Pots = agent.energy_pots;
    }
}
