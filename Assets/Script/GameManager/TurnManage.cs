using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//플레이어가 이동키를 입력하면 몬스터에게 행동력을 뿌린다
public class TurnManage : MonoBehaviour
{
    public GameObject PlayerObject;
    public PlayerState playerState;
    MonsterState monsterState;
    SpawnManager spawnManager;
    MonsterAct monsterAct;
    [SerializeField]public List<GameObject> monsterList;
    public static int totallTurn=0;
    void Start()
    {

        spawnManager = GameObject.FindWithTag("GameManager").transform.GetComponent<SpawnManager>();
        PlayerObject = GameObject.FindWithTag("Player");
        monsterState = this.transform.GetComponent<MonsterState>();
        monsterList = new List<GameObject>();
        
        if (PlayerObject == null)
        {
            PlayerObject = GameObject.FindWithTag("Player");
        }
        playerState = PlayerObject.GetComponent<PlayerState>();
        if (spawnManager != null)
        {
            monsterList = spawnManager.monsterList;
        }
        totallTurn = 0;
    }

    void Update()
    {
        
    }
    public IEnumerator MonsterAct(int actPoint)
    {
        if(monsterList == null)
        {
            Debug.Log("MonsterList Is Null");
        }
        if (monsterList.Count != 0)
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (spawnManager == null)
                {
                    spawnManager = GameObject.FindWithTag("GameManager").transform.GetComponent<SpawnManager>();
                }
                monsterAct = monsterList[i].GetComponent<MonsterAct>();
                monsterState = monsterList[i].GetComponent<MonsterState>();
                monsterState.IsDead(monsterState.Hp);
                if (monsterState.isDead)
                {
                    PlayerState.currentExp += monsterState.exp;
                    monsterList.Remove(monsterState.gameObject);
                    Destroy(monsterState.gameObject);
                }
                MonsterGetActPoint(actPoint);
                monsterAct.MonsterMove();
                yield return null;
            }
        }
    }

    public void MonsterGetActPoint(int actPoint)
    {
        monsterState.currentMonsterActPoint += actPoint;
        monsterState.monsterTurn += (int)(monsterState.currentMonsterActPoint / monsterState.monsterActPoint);
        monsterState.currentMonsterActPoint %= monsterState.monsterActPoint;
    }

}
