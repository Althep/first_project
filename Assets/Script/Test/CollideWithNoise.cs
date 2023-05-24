using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithNoise : MonoBehaviour
{
    public GameObject PathFinder;
    MonsterState myState;
    public Vector3 playerPos;
    void Start()
    {
        myState = transform.GetComponent<MonsterState>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPos = collision.transform.position;
            if (myState == null)
            {
                myState = transform.GetComponent<MonsterState>();
            }
            if(myState.wakenLevel==WakenLevel.patrol)
            myState.wakenLevel = WakenLevel.searching;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerPos = collision.transform.position;
            myState.wakenLevel = WakenLevel.chase;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (myState.wakenLevel == WakenLevel.patrol|| myState.wakenLevel==WakenLevel.chase)
            myState.wakenLevel = WakenLevel.searching;
        }
    }
}
