using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public List<GameObject> monsterList = new List<GameObject> ();
    Vector3 playerOldPos;
    Transform playerTr;

    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        playerOldPos = playerTr.position;
    }
    private void Update()
    {
        
    }
}
