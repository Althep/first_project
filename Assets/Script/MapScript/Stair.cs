using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum StairKind
{
    upStair,
    downStair
}

public class Stair : MonoBehaviour
{
    public StairKind stairKind;
    public int stairNumber;
    FieldManager fieldManager;
    GameManager gameManager;
    public bool isWorked = false;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").transform.GetComponent<GameManager>();
        fieldManager = GameObject.FindWithTag("FieldManager").GetComponent<FieldManager>();
        if (stairKind == StairKind.upStair && !fieldManager.upStairList.Contains(this.gameObject))
        {
            fieldManager.upStairList.Add(this.gameObject);
        }
        else if (stairKind == StairKind.downStair && !fieldManager.downStairList.Contains(this.gameObject))
        {
            fieldManager.downStairList.Add(this.gameObject);
        }
    }
    private void Update()
    {
        if (player.transform.position == this.transform.position)
        {
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                if (!isWorked)
                {
                    if (stairKind == StairKind.upStair)
                    {
                        GameManager.instance.nextFloor = GameManager.instance.nowFloor - 1;
                    }
                    else if (stairKind == StairKind.downStair)
                    {
                        GameManager.instance.nextFloor = GameManager.instance.nowFloor + 1;
                    }

                    if (!GameManager.instance.savedFloor.Contains(GameManager.instance.nowFloor))
                    {
                        GameManager.instance.SaveToChangeFloor();
                        GameManager.instance.savedFloor.Add(GameManager.instance.nowFloor);
                    }
                    else
                    {
                        GameManager.instance.SaveToChangeFloor();
                    }
                    ChangeStairNumber();
                    isWorked = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.stairKind = stairKind;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        
        //������ ������ ���̺����Ϸ� ����� ���踦 �̵� �� ���ӸŴ������� ���踦 1�ø��ų� ������
    }
    public void ChangeStairNumber()
    {
        GameManager.instance.stairNumber = stairNumber;
        GameManager.instance.stairKind = stairKind;
    }

}




