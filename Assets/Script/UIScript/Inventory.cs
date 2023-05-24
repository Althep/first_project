using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class Consumables
{

}

public class InventorySaveData
{
    public List<WeaponType> weaponTypes = new List<WeaponType>();
    public List<EquipType> equipTypes = new List<EquipType>();
    public List<Rarity> rarities = new List<Rarity>();
    public List<ArmorType> armors = new List<ArmorType>();
    public List<ItemType> itemTypes = new List<ItemType>();
    public List<int> damage = new List<int>();
    public List<int> defense = new List<int>();
    public List<float> range = new List<float>();
    public List<int> id = new List<int>();
    public List<int> slotNumber = new List<int>();
    public List<float> attSpeed = new List<float>();
    public List<int> tier = new List<int>();
    public List<string> name = new List<string>();
    public List<string> hands = new List<string>();//장비슬롯,손 바꾸기
    public List<string> slot = new List<string>();

}
public class ItemTypeaAndName
{
    ItemType type;
    int slotNumber;
    string name;
}

public class Inventory : MonoBehaviour
{

    public int maxSlot = 40;
    public int selectNumber = 0;
    int confirmInt;
    float time;
    //public int slotNumber;   
    public bool[] emptySlot;
    //public List<ItemTypeaAndName>InventoryList = new List<ItemTypeaAndName>();
    public List<string> nameList = new List<string>();
    PlayerAim playerAim;
    InventorySlot inventorySlot;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public List<ItemState> slotDatas = new List<ItemState>();
    public GameObject InventoryPanel;
    public GameObject confirmPanel;
    public GameObject player;
    PlayerState playerState;
    ItemUse itemUse;
    public GameObject selectObj;
    public GameObject ConfirmObj;
    Function function;
    public bool isThrow = false;

    private void Start()
    {

        emptySlot = new bool[maxSlot];
        InventoryPanel = this.transform.GetChild(0).gameObject;
        InventoryPanel.SetActive(false);
        confirmPanel = GameObject.Find("ConfirmPanel");
        playerAim = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerAim>();
        itemUse = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<ItemUse>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerState = player.transform.GetComponent<PlayerState>();
        function = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<Function>();
        for (int i = 0; i < emptySlot.Length; i++)
        {
            emptySlot[i] = true;
            inventorySlot = InventoryPanel.transform.GetChild(0).transform.GetChild(i).transform.GetComponent<InventorySlot>();
            inventorySlots.Add(inventorySlot);
            inventorySlot.slotNumber = i;
            slotDatas.Add(inventorySlots[i].transform.GetComponent<ItemState>());
        }
        confirmPanel.SetActive(false);
    }
    private void Update()
    {
        InventorySetActive();
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveInventory();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadInventory();
        }
        MoveSelsectOBJ();
        MoveConfirmOBJ();
        ThrowItem();
    }

    void InventorySetActive()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryPanel.activeSelf)
            {
                InventoryPanel.SetActive(false);
                PlayerState.isStoped=false;
            }
            else if (!InventoryPanel.activeSelf)
            {
                InventoryPanel.SetActive(true);
                PlayerState.isStoped = true;
            }
        }
        if (InventoryPanel.activeSelf)
        {
            PlayerState.isStoped=true;
        }
        else if (!InventoryPanel.activeSelf)
        {
            PlayerState.isStoped = false;
        }
    }

    void MoveSelsectOBJ()
    {
        if (InventoryPanel.activeSelf&&!confirmPanel.activeSelf)
        {
            if (Input.GetKeyDown("[6]"))
            {
                if (selectNumber < 39)
                    selectNumber += 1;
                else { selectNumber = 0; }
                selectObj.transform.SetParent(inventorySlots[selectNumber].transform);
                selectObj.transform.localPosition = Vector3.zero;
            }
            else if (Input.GetKeyDown("[4]"))
            {
                if (selectNumber > 0)
                    selectNumber -= 1;
                else { selectNumber = 39; }
                selectObj.transform.SetParent(inventorySlots[selectNumber].transform);
                selectObj.transform.localPosition = Vector3.zero;
            }
            else if (Input.GetKeyDown("[8]"))
            {
                if (selectNumber > 10)
                    selectNumber -= 11;
                else { selectNumber = 0; }
                selectObj.transform.SetParent(inventorySlots[selectNumber].transform);
                selectObj.transform.localPosition = Vector3.zero;
            }
            else if (Input.GetKeyDown("[2]"))
            {
                if (selectNumber < 28)
                    selectNumber += 11;
                else { selectNumber = 39; }
                selectObj.transform.SetParent(inventorySlots[selectNumber].transform);
                selectObj.transform.localPosition = Vector3.zero;
            }
            else if (Input.GetKeyUp(KeyCode.Return) && time+0.5f <= Time.time)
            {
                if (inventorySlots[selectNumber].transform.GetComponent<ItemState>().itemName != null)
                {
                    confirmPanel.SetActive(true);
                    if (inventorySlots[selectNumber].isEquiped)
                    {
                        confirmPanel.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "UnEquip";
                    }
                    else if (inventorySlots[selectNumber].itemstate.itemType == ItemType.Equipment)
                    {
                        confirmPanel.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Equip";
                    }
                    else
                    {
                        confirmPanel.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Use";
                    }
                    return;
                }
                else if (inventorySlots[selectNumber].transform.GetComponent<ItemState>().itemName == null)
                {
                    Debug.Log("itemDataNull");

                }
            }
        }

    }
    public void MoveConfirmOBJ()
    {
        if (confirmPanel.activeSelf)
        {
            if (Input.GetKeyDown("[8]"))
            {
                if (confirmInt - 1 >= 0)
                {
                    confirmInt -= 1;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                }
                else if (confirmInt - 1 < 0)
                {
                    confirmInt = 3;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                }
            }
            else if (Input.GetKeyDown("[2]"))
            {
                if (confirmInt + 1 < 4)
                {
                    confirmInt += 1;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                }
                else if (confirmInt + 1 > 3)
                {
                    confirmInt = 0;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                }
            }
            ConfirmObj.transform.localPosition = Vector3.zero;
        }
        if (confirmPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            switch (confirmInt)
            {
                case 0:
                    EvokeItem();
                    confirmInt = 0;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                    InventoryPanel.SetActive(false);
                    time = Time.time;
                    return;
                case 1:
                    isThrow = true;
                    PlayerState.isAiming = true;
                    confirmInt = 0;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                    InventoryPanel.SetActive(false);
                    time = Time.time;
                    playerAim.time=time;

                    return;
                case 2:
                    DropItem();
                    confirmInt = 0;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                    InventoryPanel.SetActive(false);
                    time = Time.time;
                    return;
                case 3:
                    confirmPanel.SetActive(false);
                    confirmInt = 0;
                    ConfirmObj.transform.SetParent(confirmPanel.transform.GetChild(confirmInt));
                    time = Time.time;
                    return;
                default:
                    CancleConfirm();
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancleConfirm();
        }
        if (!InventoryPanel.activeSelf)
        {
            confirmPanel.SetActive(false);
        }
    }
    public void CancleConfirm()
    {
        confirmPanel.SetActive(false);
    }
    public void EvokeItem()
    {
        if (slotDatas.Count != 0)
        {
            ItemState itemState = inventorySlots[selectNumber].transform.GetComponent<ItemState>();
            if (itemState.itemType == ItemType.Consumable)
            {
                itemUse.UseConsum(itemState);
                if (itemState.amount > 1)
                {
                    itemState.amount -= 1;
                    Debug.Log("amunt-1");
                }
                else if (itemState.amount <= 1)
                {
                    itemState.amount = 0;
                    itemState = null;
                    emptySlot[selectNumber] = true;
                    Debug.Log("amunt 0");
                }
            }
            else if (itemState.itemType == ItemType.Equipment)
            {

                itemUse.UseEquipment(itemState);




            }
        }
    }
    public void ThrowItem()
    {
        if (isThrow)
        {
            InventoryPanel.SetActive(false);
            ItemState item = inventorySlots[selectNumber].transform.GetComponent<ItemState>();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Vector3 target = playerAim.targetPos;
                if (playerAim.ThrowFindTarget(player.transform.position, target)!=null&&target!=null)
                    function.ThrowItem(item, playerState, playerAim.ThrowFindTarget(player.transform.position, target), inventorySlots[selectNumber].transform.GetComponent<Image>().sprite);
                Debug.Log(playerAim.ThrowFindTarget(player.transform.position, target).name);
            }

        }

    }
    private void DropItem()
    {


    }
    public void SaveInventory()
    {
        //InventorySaveData inventorySaveData = new InventorySaveData();
        //ItemState item;
        //for (int i = 0; i < slotItem.Count; i++)
        //{
        //    item = slotItem[i].itemData;
        //    if(item != null)
        //    {
        //        inventorySaveData.name.Add(item.itemName);
        //        inventorySaveData.weaponTypes.Add(item.weaponType);
        //        inventorySaveData.equipTypes.Add(item.equipType);
        //        inventorySaveData.rarities.Add(item.rarity);
        //        inventorySaveData.armors.Add(item.armorType);
        //        inventorySaveData.itemTypes.Add(item.itemType);
        //        inventorySaveData.damage.Add(item.damage);
        //        inventorySaveData.defense.Add(item.def);
        //        inventorySaveData.range.Add(item.range);
        //        inventorySaveData.id.Add(item.id);
        //        inventorySaveData.attSpeed.Add(item.attSpeed);
        //        inventorySaveData.tier.Add(item.tier);
        //        inventorySaveData.slotNumber.Add(slotItem[i].slotIndex);
        //    }
        //}
        //string Json = JsonUtility.ToJson(inventorySaveData);
        //string fileName = "Inventory";
        //string path = Application.dataPath + "/Save/" + fileName + ".Json";
        //File.WriteAllText(path, Json);
    }
    public void LoadInventory()
    {
        //for(int i = 0; i < slotItem.Count; i++)
        //{
        //    emptySlot[i] = true;
        //    this.transform.GetChild(0).GetChild(0).GetChild(i).GetComponent<Image>().sprite = null;
        //    this.transform.GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
        //}
        //for(int i = 0; i < slotItem.Count / 2; i++)
        //{
        //    if (slotItem[i]!.itemData != null)
        //    {
        //        slotItem[i].slotIndex = 0;
        //        slotItem[i].itemData = null;
        //    }
        //}
        //InventorySaveData InventoryData = new InventorySaveData();
        //string fileName = "Inventory";
        //string path = Application.dataPath + "/Save/" + fileName + ".Json";
        //string data = File.ReadAllText(path);
        //Debug.Log(fileName);
        //InventoryData = JsonUtility.FromJson<InventorySaveData>(data);
        //if (InventoryData != null)
        //{
        //    LoadandMakeInventory(InventoryData);
        //}
        //else
        //{
        //    Debug.Log("DataError!");
        //}
    }
    public void LoadandMakeInventory(InventorySaveData itemSaveData)
    {
        //GameObject go;
        //SpawnManager spawnManager = GameObject.FindWithTag("GameManager").transform.GetComponent<SpawnManager>();
        //for (int i = 0; i < itemSaveData.itemTypes.Count; i++)
        //{
        //    ItemState itemstate = new ItemState();
        //    itemstate.itemName = itemSaveData.name[i];
        //    itemstate.weaponType = itemSaveData.weaponTypes[i];
        //    itemstate.equipType = itemSaveData.equipTypes[i];
        //    itemstate.rarity=itemSaveData.rarities[i];
        //    itemstate.armorType=itemSaveData.armors[i];
        //    itemstate.itemType=itemSaveData.itemTypes[i];
        //    itemstate.damage=itemSaveData.damage[i];
        //    itemstate.def=itemSaveData.defense[i];
        //    itemstate.range = itemSaveData.range[i];
        //    itemstate.id=itemSaveData.id[i];
        //    itemstate.attSpeed=itemSaveData.attSpeed[i];
        //    itemstate.tier=itemSaveData.tier[i];
        //    slotItem[i].slotIndex = itemSaveData.slotNumber[i];
        //    slotItem[itemSaveData.slotNumber[i]].itemData = itemstate;
        //    emptySlot[itemSaveData.slotNumber[i]] = false;
        //}
    }

}
