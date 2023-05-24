using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isvisited : MonoBehaviour
{
    public bool isVisit=false;
    IsinSight IsinSight;
    private void Start()
    {
        IsinSight = this.transform.GetComponent<IsinSight>();
        IsinSight.ChangeLayer();
        IsinSight.ChangeColor();
        isVisit = IsinSight.visited;
    }
}
