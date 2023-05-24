using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCloseOBJ : MonoBehaviour
{
    public GameObject[] Cobject = new GameObject[10];
    public bool[] isCanMove = new bool[10];
    public Vector3[] ePos = new Vector3[10];
    public List<Vector3> ePosList = new List<Vector3>();
    public bool isDone = false;
    MonsterState monster;
    void Start()
    {
        monster = this.transform.GetComponent<MonsterState>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    void RememberEPos()
    {
        for (int i = 0; i < ePos.Length; i++)
        {
            if (ePos[i] != null)
            {
                ePosList.Add(ePos[i]);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Player")
        {
            if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
            {
                Cobject[1] = collision.gameObject;
            }
            if ((this.gameObject.transform.position.x == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
            {
                Cobject[2] = collision.gameObject;

            }
            if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
            {
                Cobject[3] = collision.gameObject;
            }
            if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y == collision.gameObject.transform.position.y))
            {
                Cobject[4] = collision.gameObject;
            }
            if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y == collision.gameObject.transform.position.y))
            {
                Cobject[6] = collision.gameObject;
            }
            if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
            {
                Cobject[7] = collision.gameObject;
            }
            if ((this.gameObject.transform.position.x == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
            {
                Cobject[8] = collision.gameObject;
            }
            if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
            {
                Cobject[9] = collision.gameObject;
            }
            for (int i = 0; i < Cobject.Length; i++)
            {
                Cobject[i] = null;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        for (int i = 0; i < Cobject.Length; i++)
        {
            Cobject[i] = null;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(monster.monsterTurn >= 1)
        {
            if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Player")
            {

                if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
                {
                    Cobject[1] = collision.gameObject;
                    isCanMove[1] = false;
                }
                if ((this.gameObject.transform.position.x == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
                {
                    Cobject[2] = collision.gameObject;
                    isCanMove[2] = false;
                }
                if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
                {
                    Cobject[3] = collision.gameObject;
                    isCanMove[3] = false;
                }
                if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y == collision.gameObject.transform.position.y))
                {
                    Cobject[4] = collision.gameObject;
                    isCanMove[4] = false;
                }
                if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y == collision.gameObject.transform.position.y))
                {
                    Cobject[6] = collision.gameObject;
                    isCanMove[6] = false;
                }
                if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
                {
                    Cobject[7] = collision.gameObject;
                    isCanMove[7] = false;
                }
                if ((this.gameObject.transform.position.x == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
                {
                    Cobject[8] = collision.gameObject;
                    isCanMove[8] = false;
                }
                if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
                {
                    Cobject[9] = collision.gameObject;
                    isCanMove[9] = false;
                }
            }
        }
        


    }
}
