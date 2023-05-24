using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum WakenLevel
{
    sleep,
    chase,
    patrol,
    searching,
}
public enum AliveState { Alive, Dead }


public class MonsterState : AliveObj
{
    public int defence = 0;
    public string monsterName = "";
    public int monsterActPoint = 10;//행동포인트
    public float currentMonsterActPoint = 0;//현재 가지고있는 행동포인트
    public int id;
    public float attackRange = 1.5f;//공격범위
    public int exp;
    public int tier;
    int wakenRate = 80;//초기 생성시 깨어있을 확률
    public int monsterTurn = 0;//가지고있는 턴
    public float distWithPlayer;
    int noticeRate;//몬스터가 플레이어를 인식 할 확률
    int searchingTurn = 0;
    int minSearchingTurn = 10;
    int maxSearchingTurn = 15;
    public bool canAttack = false;
    public Vector3 oldPlayerPos;
    [SerializeField] Vector2 playerLastPos;
    Vector3 nextPos;
    public Transform myTr;
    public WakenLevel wakenLevel = WakenLevel.sleep;
    MonsterDataRead dataRead;
    AstarTest astar;
    CollideWithNoise cwn;
    FindCloseOBJ findCloseOBJ;
    MonsterAct monsterAct;
    Transform playerTr;
    IsinSight insight;
    TurnManage turnManager;
    private void Start()
    {
        moveSpeed = 100;
        attSpeed = 100;
        castSpeed = 100;
        monsterActPoint = moveSpeed;
        isDead = false;
        myTr = GetComponent<Transform>();
        astar = GetComponent<AstarTest>();
        cwn = GetComponent<CollideWithNoise>();
        insight = this.transform.GetComponent<IsinSight>();
        monsterAct = this.transform.GetComponent<MonsterAct>();
        int iswaken = Random.Range(0, 100);
        findCloseOBJ = GetComponent<FindCloseOBJ>();
        playerTr = GameObject.FindWithTag("Player").transform;
        MaxHp = Hp;
        if (iswaken < 80)
        {
            wakenLevel = WakenLevel.patrol;
        }
        oldPlayerPos = playerTr.position;
    }



    public override void IsDead(int Hp)
    {
        base.IsDead(Hp);
        if (isDead)
        {

        }
    }



}
