using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionFunctions : MonoBehaviour
{
    PlayerState playerState;
    Function function;
    int normalMoveSpeed;
    int normalAttSpeed;
    private void Start()
    {
        playerState = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerState>();
        normalMoveSpeed = PlayerState.playerMoveSpeed;
        normalAttSpeed = PlayerState.playerAttSpeed;
        function = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<Function>();
    }
    public void Poison()
    {

    }
    public void Haste()
    {
        if (playerState.condition.Contains("Haste"))
        {
            Condition condition=new Condition();
            int temp = 0;
            for (int i = 0; i < playerState.playerCondition.Count; i++)
            {
                if (playerState.playerCondition[i].conditionName == "Haste")
                {
                    temp = i;
                    condition= playerState.playerCondition[i];
                    return;
                }
                else
                {
                    if (condition.startTurn == TurnManage.totallTurn)
                    {
                        normalMoveSpeed = PlayerState.playerMoveSpeed;
                        normalAttSpeed = PlayerState.playerAttSpeed;
                        if (PlayerState.playerMoveSpeed - 50 >= 50)
                        {
                            PlayerState.playerMoveSpeed -= 50;
                        }
                        else
                        {
                            PlayerState.playerMoveSpeed /= 2;
                        }
                        if (PlayerState.playerAttSpeed - 50 >= 50)
                        {
                            PlayerState.playerAttSpeed -= 50;
                        }
                        else
                        {
                            PlayerState.playerAttSpeed /= 2;
                        }
                    }
                    if (condition.endTurn == TurnManage.totallTurn)
                    {
                        PlayerState.playerMoveSpeed = normalMoveSpeed;
                        PlayerState.playerAttSpeed = normalAttSpeed;
                    }
                }
            }
            
        }
    }
    public void Teleport()
    {

    }
    public void ActCondition(ConditionType type,string conditionName)
    {
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
