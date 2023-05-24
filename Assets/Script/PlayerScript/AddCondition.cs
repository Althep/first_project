using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConditionData 
{
    public int id;
    public string name;
    public string description;
    public string function;
    public int minDuration;
    public int maxDuration;
    public ConditionType type;
}


public class AddCondition : MonoBehaviour
{
    Condition condition;
    GameObject player;
    PlayerState playerState;
    Function consumEffects;
    List<ConditionData> conditionData = new List<ConditionData>();
    public List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerState = player.transform.GetComponent<PlayerState>();
        ReadConditionDB();
    }
    void ReadConditionDB()
    {
        ConditionData data = new ConditionData(); 
        datas = CSVReader.Read("Conditions");
        for (int i = 0; i < datas.Count; i++)
        {
            data.id = (int)datas[i]["id"];
            data.name = datas[i]["name"].ToString();
            data.description = datas[i]["descriotion"].ToString();
            data.function = datas[i]["function"].ToString();
            data.minDuration = (int)datas[i]["minduration"];
            data.maxDuration = (int)datas[i]["maxduration"];
            data.type = (ConditionType)StringToInt.TypeStringToInt(datas[i]["conditiontype"].ToString(),"ConditionType");
        }
    }
    public void AddConditionToPlayer(string conditionName, int startTurn)
    {
        condition = new Condition();
        int temp = 0;
        int duration = Random.Range((int)datas[temp]["minduration"], (int)datas[temp]["maxduration"]);
        bool iscontain = false ;
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i]["name"].ToString() == conditionName)
            {
                temp = i;
            }
        }
        
        condition.conditionName = conditionName;
        condition.startTurn = startTurn;
        
        
        for (int i = 0; i < playerState.playerCondition.Count; i++)
        {
            if (playerState.playerCondition[i].conditionName == conditionName)
            {
                playerState.playerCondition[i].endTurn += duration;
                iscontain = true;
                return;
            }
        }
        if(!iscontain)
        {
            condition.endTurn = startTurn + duration;
            condition.conditionType = (ConditionType)StringToInt.TypeStringToInt(datas[temp]["conditiontype"].ToString(), "ConditionType");
            playerState.playerCondition.Add(condition);
            playerState.condition += datas[temp]["name"];
        }
        
    }
    public void ActiveCondition()
    {
        ConditionType type =ConditionType.none;
        switch (type)
        {
            case ConditionType.none:

                break;
            case ConditionType.start:

                break;
            case ConditionType.update:
                break;
            case ConditionType.end:
                break;
            default:
                break;
        }
    }
}
