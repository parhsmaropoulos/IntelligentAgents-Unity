using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class AgentAI : MonoBehaviour
{

    private int world_map_y = 5;
    private int world_map_x = 110;
    public bool useAstar = false;

    public int[] chromosomes = new int[11];
    // public Transform primaryTarget;
    public Vector3 primaryTarget;
    // public Transform secondaryTarget;
    public Transform villagePosition;

    public List<Vector2> potPositions = new List<Vector2>();
    public List<Vector2> goldPositions = new List<Vector2>();
    public List<Vector2> woodPositions = new List<Vector2>();
    public List<Vector2> rockPositions = new List<Vector2>();



    public float speed = 2000f;
    public float nextWayPointDistance = 3f;

    public Transform AgentGFX;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    // Agent attrs
    public int gold = 0;
    public int wood = 0;
    public int rock = 0;
    public int energy = 200;
    public int energy_pots = 0;
    public int carrying = 0;

    public int steps;
    public int resourceContributed = 0;

    public List<string> actions;

    public bool man;

    public int carry_limit = 0;
    public List<Vector2> knowledge = new List<Vector2>();
    public string resource_master = "wood";
    public string village_citizen = "village-1";
// Every 50 fixedUpdates is one sec
    private int clock_tick = 0;
    private double crossChance = 50.0;


    Seeker seeker;
    Rigidbody2D rb;
    ChromosomeControl crctrl;
    FogScript fogScript;

    // Grid wm;

    



    // Start is called before the first frame update

    void Start()
    {
        DontDestroyOnLoad(this);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        crctrl = GetComponent<ChromosomeControl>();
        fogScript = GetComponent<FogScript>();
        // wm = GetComponent<Grid>();
        // TODO get gridmap

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, primaryTarget, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
       
        clock_tick++;
        if(clock_tick == 25)
        {
            energy --;
            checkPos();
            steps ++;
            clock_tick = 0;
        }
        Vector2 currentpos =new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
        AddKnowledge(currentpos);

    // ######
    // Check for target()
    // ######
        string action;
        if(useAstar == true)
        {
            Astar();
            action = "Astar to" + primaryTarget;
        }else{
            // if(!useAstar)
            // {
            CheckForTarget();
            // }
            MoveToUnknown();
            action = "Random movement";
        }

        if (energy <= 15)
        {
            if(energy_pots > 0){
                energy_pots--;
                energy += 100;
                action = "Used Pot!";
            }
        }
        if (energy <= 30){
            if(energy_pots ==0)
            {
                // Try to find potions
                findPots();
                action = "Trying to find pot..";
            }
        }
        if(energy == 0){
            AgentDeath();
            action = "Pretty much dead..:(";
            // Agent died.
        }
        if(clock_tick% 15 ==0)
        {
            actions.Add(action);
        }
    }

    private void checkPos(){
        if(this.transform.position.x > GameData.map_size_x/2 + 10|| this.transform.position.x < - GameData.map_size_x/2 - 10|| this.transform.position.y > GameData.map_size_y/2 + 10|| this.transform.position.y < -GameData.map_size_y/2 - 10){
            AgentDeath();
        }
    }
    public void findPots(){
        if(potPositions.Count > 0)
        {
            primaryTarget = potPositions[0];
            useAstar = true;
        }
    }

    public void AgentDeath(){
        // Destroy agent
        // Destroy(gameObject);
        int active = 0;
        this.gameObject.SetActive(false);
        for(int i =0; i<GameData.agents.Count;i++){
            if(GameData.agents[i].isActiveAndEnabled){
                active ++;
            }
        }
        Debug.Log("We still have " + active + "agents");
        if(active == 0){
            GameData.GameOver = true;
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
    void CheckForTarget(){
        // Check if its at max limit
        if( wood != 0 || rock != 0 )
        {
            primaryTarget = villagePosition.position;
            useAstar = true;
            return;
        }
        // Switch to master ersource
        switch(resource_master)
        {
            case "gold":
                if(goldPositions.Count > 0)
                {
                    // primaryTarget.position = goldPositions[0];
                    primaryTarget = goldPositions[0];
                    useAstar = true;
                }
                goRandomResource();
                break;
            case "rock":
                if(rockPositions.Count > 0)
                {
                    // primaryTarget.position = rockPositions[0];
                    primaryTarget = rockPositions[0];
                    useAstar = true;
                }
                goRandomResource();
                break;
            case "wood":
                if(woodPositions.Count >0)
                {
                    // primaryTarget.position = woodPositions[0];
                    primaryTarget = woodPositions[0];
                    useAstar = true;
                }
                goRandomResource();
                break;
            default:
                goRandomResource();
                break;
        }
    }

    void goRandomResource(){
        List<List<Vector2>> choices = new List<List<Vector2>>();
                if(woodPositions.Count>0)
                    choices.Add(woodPositions);
                if(goldPositions.Count>0)
                    choices.Add(goldPositions);
                if(rockPositions.Count>0)
                    choices.Add(rockPositions);
                
                if(choices.Count> 0)
                {
                    int choice = Random.Range(0,choices.Count);
                    primaryTarget = choices[choice][0];
                    // primaryTarget.position = choices[choice][0];
                    useAstar = true;
                }else{
                    useAstar = false;
                }
    }

    // void  Update(){
    //     CheckForTarget();
    //     if(useAstar == true)
    //     {
    //         Astar();
    //     };
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");
    //     Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
    //     transform.position += movement * Time.deltaTime * speed;
    //     Vector2 currentpos =new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
    //     AddKnowledge(currentpos);
    //     UpdateCanvas();

    // }
    void MoveToUnknown()
    {
        Vector2 currentpos =new Vector2(Mathf.Round(rb.position.x), Mathf.Round(rb.position.y));
        
        Vector2 rightBlock = new Vector2(currentpos[0]+1, currentpos[1]);
        Vector2 leftBlock = new Vector2(currentpos[0]-1, currentpos[1]);
        Vector2 downBlock = new Vector2(currentpos[0], currentpos[1]-1);
        Vector2 upBlock = new Vector2(currentpos[0], currentpos[1]+1);

        // Random move
        // 1 up
        // 2 down
        // 3 right
        // 4 left
        int choice = 0;
        int[] choices = new int[] {0,0,0,0};
        if(!knowledge.Contains(upBlock))
            choices[0] = 1;
        if(!knowledge.Contains(downBlock))
            choices[1] = 1;
        if(!knowledge.Contains(rightBlock))
            choices[2] = 1;
        if(!knowledge.Contains(leftBlock))
            choices[3] = 1;
        Vector2 rlForce = transform.right * speed * Time.deltaTime;
        Vector2 udForce = transform.up * speed * Time.deltaTime;

        // Complete random movement with more weight on
        // undiscovered places
        int i=0;
        
        while(i <4)
        {
            i++;
            choice = Random.Range(0,4);
            // Check if choice
            if(choices[choice] == 1)
                break;
        }
        
        switch(choice){
            case 0:
            // move up
                rb.AddForce(10*udForce);
                AddKnowledge(upBlock);
                return;
            case 1:
            // move down
                rb.AddForce(10*-udForce);
                AddKnowledge(downBlock);
                // Vector3Int newPos = new Vector3Int((int)Mathf.Round(rb.position.x),(int) Mathf.Round(rb.position.y), 0);
                // fogScript.ChangeTileTexture(newPos);
                return;
            case 2:
            // move right
                rb.AddForce(10*rlForce);
                AddKnowledge(rightBlock);
                // Vector3Int newPos = new Vector3Int((int)Mathf.Round(rb.position.x),(int) Mathf.Round(rb.position.y), 0);
                // fogScript.ChangeTileTexture(newPos);
                return;
            case 3:
            // move left
                rb.AddForce(10*-rlForce);
                AddKnowledge(leftBlock);
                // Vector3Int newPos = new Vector3Int((int)Mathf.Round(rb.position.x),(int) Mathf.Round(rb.position.y), 0);
                // fogScript.ChangeTileTexture(newPos);
                return;
            default:
            // default move up
                rb.AddForce(10*udForce);
                AddKnowledge(upBlock);
                // Vector3Int newPos = new Vector3Int((int)Mathf.Round(rb.position.x),(int) Mathf.Round(rb.position.y), 0);
                // fogScript.ChangeTileTexture(newPos);
                return;
        }

    }

    void Astar()
    {
        if(path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            useAstar = false;
            return;
        }else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

         if(rb.velocity.x >= 0.01f)
        {
            AgentGFX.localScale = new Vector3(-1f, 1f, 1f);

        }else if(rb.velocity.x <= -0.01f)
        {
            AgentGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        Vector2 currentpos =new Vector2(Mathf.Round(rb.position.x), Mathf.Round(rb.position.y));
        AddKnowledge(currentpos);
    }

    void AddKnowledge(Vector2 newPoint)
    {
        if(!knowledge.Contains(newPoint))
        {
            knowledge.Add(newPoint);
            Vector3Int newPos = new Vector3Int ((int)Mathf.Round(rb.position.x),(int) Mathf.Round(rb.position.y), 0);
            fogScript.ChangeTileTexture(newPos);
            return;
        } 
    }

    public void UpdateChromosome(int chrom_position)
    {
        string action = "CHROMS UPDATE!!";
        actions.Add(action);
        if(chromosomes[chrom_position] == 0)
        {
            chromosomes[chrom_position] = 1;
        }else{
            chromosomes[chrom_position] = 0;
        }

        crctrl.chromosomes = chromosomes;
        crctrl.GetChromosomeAbilities(true);
    }

    public void SetChildChromosomes(int[] chroms)
    {
        ChromosomeControl crctrl = GetComponent<ChromosomeControl>();
        for(int i =0; i<11; i++)
        {
            Debug.Log("edw "+ i);
            chromosomes[i] = chroms[i];
            crctrl.chromosomes[i] = chroms[i];
        }

        crctrl.GetChromosomeAbilities(true);
        return;
    }

    void UpdateCanvas()
    {
        LiveData.Agent_Name = name;
        LiveData.Agent_Gold = gold;
        LiveData.Agent_Energy = energy;
        LiveData.Agent_Rock = rock;
        LiveData.Agent_Wood = wood;
        LiveData.Agent_Village = village_citizen;
    }
}
