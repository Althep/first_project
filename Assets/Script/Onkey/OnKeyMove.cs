using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OnKeyMove : MonoBehaviour
{
    private Transform myTr;
    private IsCanMove myCanMove;
    [SerializeField]private TurnManage myTurnManage;
    public float MoveDelay = 0.1f;
    PlayerState playerState;
    MonsterState myMonsterState;
    void Start()
    {
        myTr = this.GetComponent<Transform>();
        myCanMove = this.GetComponent<IsCanMove>();
        myTurnManage = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TurnManage>();
        playerState = this.gameObject.GetComponent<PlayerState>();  
    }

    void Update()
    {
        MoveDelay -= Time.deltaTime;
        if(myTurnManage == null)
        {
            myTurnManage=GameObject.FindGameObjectWithTag("GameManager").GetComponent<TurnManage>();
        }
        PlayerMove();
        PlayerAttack();
    }
    void PlayerMove()
    {

        if ((Input.GetKeyDown("[1]")) && myCanMove.isCanMove[1] && MoveDelay <= 0)
        {
            this.transform.Translate(-1, -1, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[2]")) && myCanMove.isCanMove[2] && MoveDelay <= 0)
        {
            myTr.Translate(0, -1, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[3]")) && myCanMove.isCanMove[3] && MoveDelay <= 0)
        {
            myTr.Translate(1, -1, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[4]")) && myCanMove.isCanMove[4] && MoveDelay <= 0)
        {
            myTr.Translate(-1, 0, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if (Input.GetKeyDown("[5]") && MoveDelay <= 0)
        {
            myTr.Translate(0, 0, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[6]")) && myCanMove.isCanMove[6] && MoveDelay <= 0)
        {
            myTr.Translate(1, 0, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[7]")) && myCanMove.isCanMove[7] && MoveDelay <= 0)
        {
            myTr.Translate(-1, +1, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[8]")) && myCanMove.isCanMove[8] && MoveDelay <= 0)
        {
            myTr.Translate(0, 1, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        if ((Input.GetKeyDown("[9]")) && myCanMove.isCanMove[9] && MoveDelay <= 0)
        {
            myTr.Translate(1, 1, 0);
            Vision.isCasted = false;
            for (int i = 0; i < myCanMove.Cobject.Length; i++)
            {
                myCanMove.Cobject[i] = null;
            }
            MoveDelay = 0.1f;
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
    }
    void PlayerAttack()
    {
        if ((Input.GetKeyDown("[1]")) && myCanMove.canAttack[1] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[1].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[1].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[2]")) && myCanMove.canAttack[2] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[2].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[2].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[3]")) && myCanMove.canAttack[3] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[3].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[3].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[4]")) && myCanMove.canAttack[4] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[4].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[4].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[6]")) && myCanMove.canAttack[6] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[6].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[6].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[7]")) && myCanMove.canAttack[7] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[7].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[7].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[8]")) && myCanMove.canAttack[8] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[8].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[8].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
        if ((Input.GetKeyDown("[9]")) && myCanMove.canAttack[9] && MoveDelay <= 0)
        {
            if (myCanMove.Cobject[9].tag == "Monster")
            {
                myMonsterState = myCanMove.Cobject[9].transform.GetComponent<MonsterState>();
                myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
                Debug.Log(myMonsterState.Hp);
                TurnManage.totallTurn += 1;
                StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerAttSpeed_));
            }
            MoveDelay = 0.1f;
        }
    }
}
