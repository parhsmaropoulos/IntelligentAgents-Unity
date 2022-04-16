using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class ButtonHandler : MonoBehaviour
{   
    [SerializeField]
    private GameObject MapHint;
    [SerializeField]
    private GameObject AgentHint;
    [SerializeField]
    private GameObject PotHint;
    [SerializeField]
    private GameObject GoldHint;
    [SerializeField]
    private GameObject TradeMapHint;
    [SerializeField]
    private GameObject TradePotHint;
    [SerializeField]
    private GameObject  ErrorMessage;
    [SerializeField]
    private GameObject  WrongSizeErrorMessage;
public void start()
    {
        ErrorMessage.SetActive(false);
        WrongSizeErrorMessage.SetActive(false);
        try{
            GameData.map_size_x = int.Parse(GameObject.Find("Xsize").GetComponent<InputField>().text);
            GameData.map_size_y = int.Parse(GameObject.Find("Ysize").GetComponent<InputField>().text);
            if(GameData.useTxt == true)
            {
                StreamReader reader  = new StreamReader(Application.dataPath + "/Level.txt", true);
                int line = 0;
                int x = 0;
                while(!reader.EndOfStream){
                    string lin = reader.ReadLine();
                    string[] values = lin.Split(',');
                    x = values.Length;
                    line++;
                }
                if(GameData.map_size_x != x || GameData.map_size_y != line)
                {
                    WrongSizeErrorMessage.SetActive(true);
                    return;
                }
            }else{

            }
            // GameData.map_size_x = x;
            // GameData.map_size_y = line;
            GameData.no_of_agents = int.Parse(GameObject.Find("AgentsNumber").GetComponent<InputField>().text);
            GameData.no_of_pots = int.Parse(GameObject.Find("PotsNumber").GetComponent<InputField>().text);
            GameData.no_of_golds = int.Parse(GameObject.Find("GoldsNumber").GetComponent<InputField>().text);
            GameData.pot_cost = int.Parse(GameObject.Find("PotCostNumber").GetComponent<InputField>().text);
            GameData.map_cost = int.Parse(GameObject.Find("MapCostNumber").GetComponent<InputField>().text);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }catch(FormatException)
        {
            ErrorMessage.SetActive(true);
        }

    }
    
    public void useTxt(){
        GameData.useTxt = !GameData.useTxt;
    }
    public void Start()
    {
        WrongSizeErrorMessage.SetActive(false);
        MapHint.SetActive(false);
        AgentHint.SetActive(false);
        PotHint.SetActive(false);
        GoldHint.SetActive(false);
        TradeMapHint.SetActive(false);
        TradePotHint.SetActive(false);
        ErrorMessage.SetActive(false);
    }

    public void startWithTxt(){
        StreamReader reader  = new StreamReader(Application.dataPath + "/Level.txt", true);
        int line = 0;
        while(!reader.EndOfStream){
            string lin = reader.ReadLine();
            string[] values = lin.Split(',');
            line++;
        }
    }
    public void OnValid(InputField inp)
    {
        switch(inp.name){
            case  "Xsize" :
                if(int.Parse(inp.text) < 100)
                {
                    MapHint.SetActive(true);
                }else{
                    MapHint.SetActive(false);
                }
                break;
            case "Ysize" :
                 if(int.Parse(inp.text) < 100)
                {
                    MapHint.SetActive(true);
                }else{
                    MapHint.SetActive(false);
                }
                break;
            case "AgentsNumber" :
                if(int.Parse(inp.text) < 4 || int.Parse(inp.text) > 10)
                {
                    AgentHint.SetActive(true);
                }else{
                    AgentHint.SetActive(false);
                }
                break;
            case "PotsNumber" :
                if (inp.text == null)
                {
                    PotHint.SetActive(true);
                }else{
                    PotHint.SetActive(false);
                }
                break;
            case "GoldsNumber" :
                if (inp.text == null)
                {
                    GoldHint.SetActive(true);
                }else{
                    GoldHint.SetActive(false);
                }
                break;
            case "PotCostNumber" :
            if (inp.text == null)
                {
                    TradePotHint.SetActive(true);
                }else{
                    TradePotHint.SetActive(false);
                }
                break;
            case "MapCostNumber" :
                if (inp.text == null)
                {
                    TradeMapHint.SetActive(true);
                }else{
                    TradeMapHint.SetActive(false);
                }
                break;
        }
    }
}


    
