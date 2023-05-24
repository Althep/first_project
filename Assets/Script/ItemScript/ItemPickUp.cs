using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using System.Net;
using System.Linq;
using System.Security.Cryptography;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] bool canGet = true;
    GameObject player;
    GameObject inventorySlot;
    GameObject inventoryObj;
    [SerializeField]Inventory inventory;
    UIScript upscript;
    public int slotNumber = 0;
    GameObject inventoryPanel;
    TextMeshProUGUI itemName;
    SpawnManager spawnManager;
    ItemState itemState;
    ItemDataRead itemDataRead;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        inventoryObj = GameObject.Find("Inventory");
        inventoryPanel = inventoryObj.transform.GetChild(0).transform.GetChild(0).gameObject;
        inventory = inventoryObj.GetComponent<Inventory>();
        spawnManager = GameObject.FindWithTag("GameManager").transform.GetComponent<SpawnManager>();
        itemState = this.transform.GetComponent<ItemState>();
        itemDataRead = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<ItemDataRead>();
    }
    private void Update()
    {
        if (this.transform.position == player.transform.position)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GetSlotNumber();
                GetItem();
            }

        }
    }
    void CanItThrow()
    {

    }

    void GetSlotNumber()
    {
        for (int i = 0; i < inventory.emptySlot.Length; i++)
        {
            if (inventory.emptySlot[i])
            {
                slotNumber = i;
                canGet = true;

                return;
            }
        }
        Debug.Log("Inventory Full!");
        canGet = false;
    }
    void GetItem()
    {
        ItemState slotItem;
        int id = 0;
        if (canGet)
        {
            for (int i = 0; i < inventory.inventorySlots.Count; i++)
            {
                if (inventory.inventorySlots[i].transform.GetComponent<ItemState>().itemName != null)
                {
                    if ((inventory.inventorySlots[i].transform.GetComponent<ItemState>().itemName == itemState.itemName) && (itemState.itemType == ItemType.Consumable) && inventory.inventorySlots[i].transform.GetComponent<ItemState>().itemName != null)
                    {
                        slotItem = inventory.inventorySlots[i].transform.GetComponent<ItemState>();
                        InventorySlot inventorySlot;
                        inventorySlot = inventory.inventorySlots[i];
                        slotItem.amount++;
                        spawnManager.itemList.Remove(this.gameObject);
                        Destroy(this.gameObject);
                        return;
                    }
                }
                
            }
            slotItem = inventory.inventorySlots[slotNumber].transform.GetComponent<ItemState>();
            if (itemState.itemType == ItemType.Consumable)
            {
                id = itemState.id;
                itemDataRead.GetItemData(id, "Consum", inventory.slotDatas[slotNumber]);
                slotItem.amount = 1;
                slotItem.option = itemState.option;
            }
            else if (itemState.itemType == ItemType.Equipment)
            {
                id = itemState.id;
                itemDataRead.GetItemData(id, "Equip", inventory.slotDatas[slotNumber]);
                slotItem.amount = 1;
                slotItem.option = itemState.option;
            }
            inventory.inventorySlots[slotNumber].transform.GetComponent<Image>().sprite = this.transform.GetComponent<SpriteRenderer>().sprite;
            inventory.emptySlot[slotNumber] = false;
            spawnManager.itemList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }


    
    
}

