using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Vision : MonoBehaviour
{
    public int playerVision = PlayerState.playerVision;
    public static bool isCasted = false;
    [SerializeField] RaycastHit2D hit;
    IsinSight isInsight;
    Vector3 oldPlayerPos;
    private void Start()
    {
        oldPlayerPos = transform.position;
        this.transform.GetComponent<CircleCollider2D>().radius = playerVision;
    }

}
