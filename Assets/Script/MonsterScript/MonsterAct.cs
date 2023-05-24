using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAct : MonoBehaviour
{

    Vector3 myPos;
    Vector3 targetPos;
    [SerializeField]Vector3 nextPos;
    [SerializeField]Vector3 playerlastPos;
    List<Node> pathList;
    MonsterState mystate;
    AstarTest astar;
    IsinSight insight;
    GameObject playerObj;
    PlayerState playerState;
    bool canAttack = false;
    float distance;
    int wakeupRate = 30;
    int sleepRate = 10;
    [SerializeField] List<Vector3> PatrolPos;
    private void Start()
    {
        mystate = GetComponent<MonsterState>();
        astar = GetComponent<AstarTest>();
        playerObj = GameObject.FindWithTag("Player");
        playerState = playerObj.transform.GetComponent<PlayerState>();
        targetPos = this.transform.position;
        playerlastPos = this.transform.position;
        insight = this.transform.GetComponent<IsinSight>();
        distance = Vector2.Distance(playerObj.transform.position, this.transform.position);
        nextPos = this.transform.position;
    }



    public void MonsterMove()
    {
        while (mystate.monsterTurn >= 1)
        {
            CanAttack();
            if (mystate.monsterTurn >= 1&&distance>=mystate.attackRange)
            {
                switch (mystate.wakenLevel)
                {
                    case WakenLevel.sleep:
                        nextPos = MonsterSleep();
                        break;
                    case WakenLevel.chase:
                        nextPos = MonsterChase();
                        break;
                    case WakenLevel.patrol:
                        nextPos = MonsterPatrol();
                        break;
                    case WakenLevel.searching:
                        nextPos = MonsterSearching();
                        break;
                    default:
                        break;
                }
                if (!canAttack)
                {
                    this.transform.position = nextPos;
                }
            }
            mystate.monsterTurn--;
        }
    }

    void CanAttack()
    {
        distance = Vector2.Distance(playerObj.transform.position, this.transform.position);
        if (mystate != null)
        {
            if (mystate.attackRange >= distance)
            {
                canAttack = true;
            }
        }
        if (canAttack)
        {

            MonsterAttack();

        }
    }
    public void MonsterAttack()
    {
        if (mystate.wakenLevel != WakenLevel.sleep && mystate.monsterTurn >= 1)
        {
            if (mystate.IsHit(mystate,playerState))
            {
                playerState.Hp -= mystate.HitDamage(playerState.AttackDamage(), playerState);
                Dialog.instance.color = Color.white;
                Dialog.instance.UpdateDialog(mystate.monsterName+" attack you!");
                canAttack = false;
                mystate.monsterTurn--;
            }
            else
            {
                canAttack = false;
                mystate.monsterTurn--;
                Dialog.instance.color = Color.white;
                Dialog.instance.UpdateDialog(mystate.monsterName + " try to attack you but missed");
            }
        }
    }
    private Vector3 MonsterPatrol()
    {
        //int temp = 0;
        //temp = Random.Range(0, sleepRate);
        //if (temp <= sleepRate)
        //{
        //    //Debug.Log("WakenLevelChange");
        //    //mystate.wakenLevel = WakenLevel.sleep;
        //    return this.transform.position;
        //}
        //else
        //{
            List<Vector3> returnPos = new List<Vector3>();
            int n = 0;
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    returnPos.Add(new Vector3(this.transform.position.x + x, this.transform.position.y + y));
                    PatrolPos = returnPos;
                    foreach (Collider2D col in Physics2D.OverlapCircleAll(this.transform.position + new Vector3(x, y), 0.4f))
                        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Player" || col.gameObject.tag == "Monster") returnPos.Remove(col.transform.position);
                    n++;
                }
            }
            if (returnPos.Count > 0)
            {
                n = Random.Range(0, returnPos.Count);
                nextPos = returnPos[n];
                return returnPos[n];
            }
            else
            {
                return this.gameObject.transform.position;
            }
        //}

    }
    private Vector3 MonsterChase()
    {
        //if (insight.inSight)
        //{
        //    astar.PathFinding(playerlastPos)
        //}
        astar.PathFinding(playerObj.transform.position);
        pathList = astar.FinalNodeList;
        if (pathList.Count > 1)
        {
            nextPos = new Vector3(pathList[1].x, pathList[1].y,this.transform.position.z);
            pathList.RemoveAt(1);
        }
        else if (pathList.Count == 1)
        {
            nextPos = new Vector3(pathList[0].x, pathList[0].y, this.transform.position.z);
            pathList.RemoveAt(0);
        }
        else if (pathList.Count == 0)
        {
            astar.PathFinding(playerObj.transform.position);
            pathList = astar.FinalNodeList;
            if (pathList.Count > 1)
            {
                nextPos = new Vector3(pathList[1].x, pathList[1].y, this.transform.position.z);
            }
            else
            {
                mystate.wakenLevel = WakenLevel.searching;
            }
        }
        return nextPos;
    }

    private Vector3 MonsterSleep()
    {
        int temp = 0;
        temp = Random.Range(0, wakeupRate);
        if (temp < wakeupRate)
        {
            mystate.wakenLevel = WakenLevel.patrol;
        }


        return this.transform.position;
    }
    Vector3 MonsterSearching()
    {
        astar.PathFinding(playerlastPos);
        pathList = astar.FinalNodeList;
        if (pathList.Count > 1)
        {
            nextPos = new Vector3(pathList[1].x, pathList[1].y, this.transform.position.z);
            pathList.RemoveAt(1);
        }
        else if (pathList.Count == 1)
        {
            nextPos = new Vector3(pathList[0].x, pathList[0].y, this.transform.position.z);
            pathList.RemoveAt(0);
        }
        else
        {
            astar.PathFinding(playerlastPos);
            pathList = astar.FinalNodeList;
            if (pathList.Count > 1)
            {
                nextPos = new Vector3(pathList[1].x, pathList[1].y, this.transform.position.z);
                pathList.RemoveAt(1);
            }
            else if (pathList.Count == 1)
            {
                nextPos = new Vector3(pathList[0].x, pathList[0].y, this.transform.position.z);
                pathList.RemoveAt(0);
            }
            else
            {
                mystate.wakenLevel = WakenLevel.patrol;
                MonsterPatrol();
            }
        }
        return nextPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerNoise")
        {
            if (mystate.wakenLevel == WakenLevel.patrol)
            {
                mystate.wakenLevel = WakenLevel.searching;
                targetPos = collision.gameObject.transform.position;
                playerlastPos = collision.transform.position;
            }
            else if (mystate.wakenLevel == WakenLevel.sleep)
            {
                int temp = 0;
                temp = Random.Range(0, wakeupRate);
                if (temp < wakeupRate)
                {
                    mystate.wakenLevel = WakenLevel.patrol;
                }
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerNoise" || collision.gameObject.tag == "Player")
        {
            if ((mystate.wakenLevel == WakenLevel.patrol || mystate.wakenLevel == WakenLevel.searching) && insight.inSight)
            {
                mystate.wakenLevel = WakenLevel.chase;
                playerlastPos = collision.transform.position;
            }
            else if (!insight.inSight && mystate.wakenLevel == WakenLevel.chase)
            {
                mystate.wakenLevel = WakenLevel.searching;
                playerlastPos = playerObj.transform.position;
            }
            if (mystate.wakenLevel == WakenLevel.chase)
            {
                playerlastPos = playerObj.transform.position;
            }
            if (mystate.wakenLevel == WakenLevel.searching)
            {
                playerlastPos = playerObj.transform.position;
            }
            if (mystate.wakenLevel != WakenLevel.sleep && insight.inSight)
            {
                mystate.wakenLevel = WakenLevel.chase;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.transform.tag == "Player")
        {
            
            if (mystate.wakenLevel == WakenLevel.chase&&!insight.inSight)
            {
                mystate.wakenLevel = WakenLevel.searching;
                targetPos = collision.gameObject.transform.position;
                playerlastPos = collision.transform.position;
            }
        }

    }

}
