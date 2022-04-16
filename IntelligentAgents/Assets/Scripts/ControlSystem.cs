using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlSystem : MonoBehaviour
{
    public Text[]  texts;

    // agent name
    // village name
    // gold
    // wood
    // rock
    // energy
    // tot-a
    // tot-b
    // gold-a
    // gold-a
    // rock-a
    // rock-b
    // wood-a
    // wood-b

    // Start is called before the first frame update
    void Start()
    {
        texts[0].text = LiveData.Agent_Name;
        texts[1].text = LiveData.Agent_Village;
        texts[2].text = LiveData.Agent_Gold.ToString();
        texts[3].text = LiveData.Agent_Wood.ToString();
        texts[4].text = LiveData.Agent_Rock.ToString();
        texts[5].text = LiveData.Agent_Energy.ToString();
        texts[6].text = LiveData.villageA_total.ToString();
        texts[7].text = LiveData.villageB_total.ToString();
        texts[8].text = LiveData.villageA_gold.ToString();
        texts[9].text = LiveData.villageB_gold.ToString();
        texts[10].text = LiveData.villageA_rock.ToString();
        texts[11].text = LiveData.villageB_rock.ToString();
        texts[12].text = LiveData.villageA_wood.ToString();
        texts[13].text = LiveData.villageB_wood.ToString();
        texts[14].text = LiveData.Agent_Pots.ToString();
    }

    public void UpdateVals(){
        texts[0].text = LiveData.Agent_Name;
        texts[1].text = LiveData.Agent_Village;
        texts[2].text = LiveData.Agent_Gold.ToString();
        texts[3].text = LiveData.Agent_Wood.ToString();
        texts[4].text = LiveData.Agent_Rock.ToString();
        texts[5].text = LiveData.Agent_Energy.ToString();
        texts[6].text = LiveData.villageA_total.ToString();
        texts[7].text = LiveData.villageB_total.ToString();
        texts[8].text = LiveData.villageA_gold.ToString();
        texts[9].text = LiveData.villageB_gold.ToString();
        texts[10].text = LiveData.villageA_rock.ToString();
        texts[11].text = LiveData.villageB_rock.ToString();
        texts[12].text = LiveData.villageA_wood.ToString();
        texts[13].text = LiveData.villageB_wood.ToString();
        texts[14].text = LiveData.Agent_Pots.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        texts[0].text = LiveData.Agent_Name;
        texts[1].text = LiveData.Agent_Village;
        texts[2].text = LiveData.Agent_Gold.ToString();
        texts[3].text = LiveData.Agent_Wood.ToString();
        texts[4].text = LiveData.Agent_Rock.ToString();
        texts[5].text = LiveData.Agent_Energy.ToString();
        texts[6].text = LiveData.villageA_total.ToString();
        texts[7].text = LiveData.villageB_total.ToString();
        texts[8].text = LiveData.villageA_gold.ToString();
        texts[9].text = LiveData.villageB_gold.ToString();
        texts[10].text = LiveData.villageA_rock.ToString();
        texts[11].text = LiveData.villageB_rock.ToString();
        texts[12].text = LiveData.villageA_wood.ToString();
        texts[14].text = LiveData.Agent_Pots.ToString();
    }
}
