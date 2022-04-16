using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject credsPanel;
    public GameObject options;
    public GameObject TimesUpTxt;
    public GameObject GameOverTxt;
    public GameObject WinnerTxt;
    public VillageScript village;

    public void EndGame()
    {
        titlePanel.SetActive(false);
        credsPanel.SetActive(true);
    }

    public void Yes()
    {
        titlePanel.SetActive(false);
        options.SetActive(true);
    }

    public void RestartGame(bool yes)
    {
        if(yes)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }else{
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void Start()
    {
        if(GameData.Winner || GameData.TimesUp)
        {
            titlePanel.SetActive(true);
            options.SetActive(false);
            credsPanel.SetActive(false);
            if(GameData.Winner)
            {
                WinnerTxt.SetActive(true);
                TimesUpTxt.SetActive(false);
            } 
            if(GameData.TimesUp)
            {
                WinnerTxt.SetActive(false);
                TimesUpTxt.SetActive(true);
            }
            GameOverTxt.SetActive(false);
                
            VillageScript villageA = GameData.VillageA;
            VillageScript villageB = GameData.VillageB;

            createTxTResults(villageA, villageB);
        }
        if(GameData.GameOver)
        {
            titlePanel.SetActive(true);
            options.SetActive(false);
            credsPanel.SetActive(false);
            WinnerTxt.SetActive(false);
            TimesUpTxt.SetActive(false);
            GameOverTxt.SetActive(true);
        }
        for(int i=0; i< GameData.agents.Count; i++){
            Destroy(GameData.agents[i]);
        }
        Destroy(GameData.VillageA);
        Destroy(GameData.VillageB);

    }

    string AgentMoves(AgentAI agent)
    {
        string moves = "";
        string nextAction = agent.actions[0];
        Debug.Log("Actions Count: " + agent.actions.Count);
        int times = 0;
        bool ended = false;
        for(int i = 1; i< agent.actions.Count; i++)
        {
            if( i + 1 == agent.actions.Count)
            {
                ended = true;
                nextAction = agent.actions[i];
            }else{
                nextAction = agent.actions[i+1];
            }
            string action = agent.actions[i];
            if(!action.Equals(nextAction) || ended)
            {
                if(times == 0)
                {
                    moves += action + " \n";
                }else{
                    moves += action +" times "+times + " \n";
                    times = 0;
                }
            }else{
                times ++;
            }
        }
        // Debug.Log(moves);
        return moves;
    }

    void createTxTResults(VillageScript villageA, VillageScript villageB)
    {

        string agentResults = "";

        for(int i =0; i < GameData.agents.Count; i++)
        {
            AgentAI agent = GameData.agents[i];
            agentResults += "\n Agent name: "+agent.name+" \n" +@"
            Total Moves: "+agent.steps+ " \n" +@"
            Total resource contribution: "+agent.resourceContributed+ " \n" + @"
                Moves:"+ "\n"+ AgentMoves(agent);
        }

        string winner;
        // Check winner
        if(villageA.TotalResources > villageB.TotalResources)
            winner = villageA.name;
        else{
            winner = villageB.name;
        }
        string Results = "Village "+ winner + " won ! \n" +@"
            Results:" + "\n" + @"
            Village_A Total Resources = "+ villageA.TotalResources+  "\n" +@"
            Village_A Gold = " + villageA.gold + "\n" +@"
            Village_A Wood = " + villageA.wood + "\n" +@"
            Village_A Rock = " + villageA.rock + "\n" +@"
            ======================================================= "+"\n" + @"
            Village_B Total Resources = "+ villageB.TotalResources+  "\n" +@"
            Village_B Gold = " + villageB.gold + "\n" +@"
            Village_B Wood = " + villageB.wood + "\n" +@"
            Village_B Rock = " + villageB.rock + "\n" +@"
            
            ======================================================= "+"\n" + @"
            Stats:
            Mutations Achieved = " + GameData.mutations_count + "\n" +@"
            Cross Confirmations = " + GameData.confirmed_crosses+  "\n" +@"
            Completed Trades = " + GameData.trades_count+  "\n" +@"
            Time pass sinced the start of the game = " +Time.time + " seconds. \n" +@"
            
            ======================================================= "+"\n" + @"
            
            Agents Analysis: " +"\n"+ @"
            "+ agentResults;

        Debug.Log(Application.dataPath);
        // write results to text file

        string path = Application.dataPath + "/results.txt";
        using(var stream = new FileStream(path, FileMode.Truncate))
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Results);
        }
    }
}
