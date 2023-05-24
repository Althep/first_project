using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    FieldManager fieldManager;
    GameObject player;
    Vector3 oldplyaerPos;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
        //this.transform.position = new Vector3(fieldManager.dungeonSize / 2, fieldManager.dungeonSize / 2, -10);
        this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);
        this.transform.GetComponent<Camera>().orthographicSize = fieldManager.dungeonSize/2;
    }

    
    void Update()
    {
        if (oldplyaerPos != player.transform.position)
        {
            this.transform.position = new Vector3(player.transform.position.x,player.transform.position.y,-10);
            oldplyaerPos = player.transform.position;
        }
    }
}
