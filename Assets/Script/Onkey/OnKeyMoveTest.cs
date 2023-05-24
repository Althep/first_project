using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyMoveTest : MonoBehaviour
{
    private Transform myTr;
    private IsCanMove myCanMove;
    void Start()
    {
        myTr = this.GetComponent<Transform>();
    }
    void Update()
    {

        if ((Input.GetKeyDown("[1]")))
        {
            this.transform.Translate(-1, -1, 0);

        }
        if ((Input.GetKeyDown("[2]")))
        {
            myTr.Translate(0, -1, 0);

        }
        if ((Input.GetKeyDown("[3]")))
        {
            myTr.Translate(1, -1, 0);

        }
        if ((Input.GetKeyDown("[4]")))
        {
            myTr.Translate(-1, 0, 0);

        }
        if (Input.GetKeyDown("[5]"))
        {
            myTr.Translate(0, 0, 0);

        }
        if ((Input.GetKeyDown("[6]")))
        {
            myTr.Translate(1, 0, 0);

        }
        if ((Input.GetKeyDown("[7]")))
        {
            myTr.Translate(-1, +1, 0);

        }
        if ((Input.GetKeyDown("[8]")))
        {
            myTr.Translate(0, 1, 0);

        }
        if ((Input.GetKeyDown("[9]")))
        {
            myTr.Translate(1, 1, 0);

        }

    }
}
