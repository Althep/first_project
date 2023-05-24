using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowUI : MonoBehaviour
{
    List<ItemState> slotDatas = new List<ItemState>();
    GameObject selectObj;
    int selectNumber;
    Inventory inventory;
    GameObject slotObj;
    List<GameObject> slots = new List<GameObject>();
    ItemState selectItemData;
    Function function;
    PlayerState playerState;
    int slotCount;
    void Start()
    {
        this.gameObject.SetActive(false);
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        selectNumber = 0;
        slotObj = this.transform.GetChild(0).gameObject;
        selectObj = slotObj.transform.GetChild(2).gameObject;
        function = GameObject.FindWithTag("GameManager").transform.GetComponent<Function>();
        playerState = GameObject.FindWithTag("Player").transform.GetComponent<PlayerState>();
    }

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            SlotChange();
        }
    }
    void AddSlotData()
    {
        
        if (this.gameObject.activeSelf)
        {
            for (int i = 0; i<inventory.slotDatas.Count; i++)
            {
                if (inventory.slotDatas[i].itemType == ItemType.Equipment||inventory.slotDatas[i].consumKind== ConsumKind.Potion)
                {
                    slotDatas.Add(inventory.slotDatas[i]);
                }
            }
        }
        if (slotDatas.Count>slots.Count)
        {
            for(int i =0; i<slotDatas.Count-slots.Count; i++)
            {
                GameObject go = Instantiate(slotObj);
                go.transform.SetParent(this.gameObject.transform);
            }
            slotCount = slotDatas.Count-1;
        }
    }
    void SlotChange()
    {
        if (Input.GetKeyDown("8"))
        {
            if (selectNumber<=0)
            {
                selectNumber = slotCount;
            }
            else
            {
                selectNumber=-1;
            }
        }
        else if (Input.GetKeyDown("2"))
        {
            if (selectNumber>=slotCount)
            {
                selectNumber=0;
            }
            else
            {
                selectNumber=+1;
            }
        }
        Transform obj = this.gameObject.transform.GetChild(selectNumber).transform;
        selectObj.transform.SetParent(obj);
    }
    void ActiveThrow()
    {
        if (this.gameObject.activeSelf&&Input.GetKeyUp(KeyCode.Return))
        {
            PlayerState.isAiming=true;
            PlayerState.isStoped=true;
        }
    }
    void SlotDataClear()
    {
        if (!this.gameObject.activeSelf)
        {
            selectNumber=0;
            slotDatas.Clear();
        }
    }
}
