using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class IsCanMove : MonoBehaviour
{
    public GameObject[] Cobject;
    public bool[] isCanMove = new bool[10];
    public bool canmove = false;
    public bool canattacktemp = false;
    public Vector2[] closePos = new Vector2[10];
    public bool[] canAttack = new bool[10];
    //public static bool[] iscanmove;
    public void CanItMove()
    {
        for (int i = 0; i < Cobject.Length; i++)
        {
            if (Cobject[i] == null)
            {
                isCanMove[i] = true;
                canAttack[i] = false;
            }
            else
            {
                isCanMove[i] = false;
                if (Cobject[i].transform.tag == "Monster")
                {
                    canAttack[i] = true;
                }
            }
        }
    }

    void Update()
    {
        CanItMove();
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Monster")
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
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Monster")
        {
            if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
            {
                Cobject[1] = collision.gameObject;
                isCanMove[1] = false;
                if (Cobject[1].transform.tag == "Monster")
                    canAttack[1] = true;
            }
            if ((this.gameObject.transform.position.x == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
            {
                Cobject[2] = collision.gameObject;
                isCanMove[2] = false;
                if (Cobject[2].transform.tag == "Monster")
                    canAttack[2] = true;

            }
            if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y - 1 == collision.gameObject.transform.position.y))
            {
                Cobject[3] = collision.gameObject;
                isCanMove[3] = false;
                if (Cobject[3].transform.tag == "Monster")
                    canAttack[3] = true;

            }
            if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y == collision.gameObject.transform.position.y))
            {
                Cobject[4] = collision.gameObject;
                isCanMove[4] = false;
                if (Cobject[4].transform.tag == "Monster")
                    canAttack[4] = true;

            }
            if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y == collision.gameObject.transform.position.y))
            {
                Cobject[6] = collision.gameObject;
                isCanMove[6] = false;
                if (Cobject[6].transform.tag == "Monster")
                    canAttack[6] = true;

            }
            if ((this.gameObject.transform.position.x - 1 == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
            {
                Cobject[7] = collision.gameObject;
                isCanMove[7] = false;
                if (Cobject[7].transform.tag == "Monster")
                    canAttack[7] = true;

            }
            if ((this.gameObject.transform.position.x == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
            {
                Cobject[8] = collision.gameObject;
                isCanMove[8] = false;
                if (Cobject[8].transform.tag == "Monster")
                    canAttack[8] = true;

            }
            if ((this.gameObject.transform.position.x + 1 == collision.transform.position.x) && (this.gameObject.transform.position.y + 1 == collision.gameObject.transform.position.y))
            {
                Cobject[9] = collision.gameObject;
                isCanMove[9] = false;
                if (Cobject[9].transform.tag == "Monster")
                    canAttack[9] = true;

            }
        }
    }
}

