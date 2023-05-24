using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int slotNumber;
    public GameObject inventoryObject;
    public bool isEquiped = false;
    Inventory inventory;
    Button button;
    public TextMeshProUGUI itemName;
    public ItemState itemstate;
    ItemDataRead itemDataRead;
    public GameObject equpImage;
    Image myImage;
    string oldName;
    int oldAmount=0;
    private void Start()
    {
        itemName = this.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        button = this.gameObject.transform.GetComponent<Button>();
        inventory = inventoryObject.GetComponent<Inventory>();
        itemstate = this.gameObject.GetComponent<ItemState>();
        myImage = this.gameObject.GetComponent<Image>();
        itemDataRead = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<ItemDataRead>();
        equpImage = itemName.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        ChangeSprite(itemstate.id);
        DeleteItem();
    }
    public void ChangeSprite(int id)
    {

        if (((itemstate.itemName != null && oldName != itemstate.itemName) || oldAmount!=itemstate.amount))
        {

            SlotNamechange();
            oldName = itemstate.itemName;
            oldAmount= itemstate.amount;
        }
        else if (itemstate.itemName == null && oldName != itemstate.itemName)
        {
            string path = null;
            myImage.sprite = null;
            //myImage.sprite = Resources.Load<Sprite>(path);
            Debug.Log(path);
            oldName = null;
            SlotNamechange();
        }

    }
    void DeleteItem()
    {
        if (itemstate.amount == 0 && itemstate.itemType != ItemType.None)
        {
            itemDataRead.GetItemData(0, "Reset", itemstate);
        }
    }
    string ConsumableName()
    {
        string temp = null;
        switch (itemstate.consumKind)
        {
            case ConsumKind.None:
                return null;
            case ConsumKind.Potion:
                temp = "Potion of ";
                return temp;
            case ConsumKind.Scroll:
                temp = "Scroll of ";
                return temp;
            case ConsumKind.Book:
                temp = "Book of ";
                return temp;
            case ConsumKind.Evoke:
                temp = "Wand of ";
                return temp;
            default:
                return temp;
        }
    }
    void SlotNamechange()
    {
        if (itemstate.itemType == ItemType.Consumable)
        {
            if (itemstate.amount > 1)
            {
                itemName.text = ConsumableName() + itemstate.itemName + "(" + itemstate.amount + ")";
            }
            else
            {
                itemName.text = ConsumableName() + itemstate.itemName;
            }
        }
        else
        {
            itemName.text = itemstate.itemName;
        }
        itemName.color = Color.white;

    }
}
