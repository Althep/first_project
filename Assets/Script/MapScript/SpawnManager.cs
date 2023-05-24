using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;



public class SpawnManager : MonoBehaviour
{
    public GameObject Player;
    public FieldManager fieldManager;
    public List<GameObject> itemList = new List<GameObject>();
    public List<GameObject> monsterList = new List<GameObject>();
    public MonsterSaveData monsterSaveData;
    int monsterSpawnPoint = 10;
    public GameObject basicMonster;
    public MonsterDataRead monsterDataRead;
    public ItemDataRead itemDataRead;
    [SerializeField] public GameObject[] ItemArray = new GameObject[10];
    // Start is called before the first frame update
    void Start()
    {
        fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
        monsterDataRead = this.gameObject.GetComponent<MonsterDataRead>();
        itemDataRead = this.gameObject.GetComponent<ItemDataRead>();
    }

    public void MonsterSpawn()
    {
        int i = 0;
        int randomSpawnPointEmpty;
        while (i < monsterSpawnPoint)
        {
            randomSpawnPointEmpty = Random.Range(0, fieldManager.emptyPosList.Count);
            GameObject gameobj = Instantiate(basicMonster, fieldManager.emptyPosList[randomSpawnPointEmpty], Quaternion.identity);
            monsterList.Add(gameobj);
            fieldManager.emptyPosList.RemoveAt(randomSpawnPointEmpty);
            i++;
        }
        if (monsterDataRead == null)
        {
            monsterDataRead = this.gameObject.GetComponent<MonsterDataRead>();
        }
        monsterDataRead.initiateMonster();
    }
    public void PlayerSpawned()
    {
        if (fieldManager == null)
        {
            fieldManager = GameObject.Find("FieldManager").transform.GetComponent<FieldManager>();
        }
        if (!PlayerState.isPlayerSpawnd)
        {
            Instantiate(Player, new Vector3(fieldManager.stairUp_POS[0].x, fieldManager.stairUp_POS[0].y, 0), Quaternion.identity);
            PlayerState.isPlayerSpawnd = true;
        }
    }
    public void MonsterSave()
    {
        MonsterSaveData monsterSaveData = new MonsterSaveData();
        MonsterState monsterState;
        if (monsterList != null)
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                monsterState = monsterList[i].GetComponent<MonsterState>();
                monsterSaveData.maxHp.Add(monsterState.MaxHp);
                monsterSaveData.currentHp.Add(monsterState.Hp);
                monsterSaveData.id.Add(monsterState.id);
                monsterSaveData.monsterPos.Add(monsterList[i].transform.position);
            }
        }
        string Json = JsonUtility.ToJson(monsterSaveData);
        string fileName = "Monster" + GameManager.instance.nowFloor;
        string path = Application.dataPath + "/Save/" + fileName + ".Json";
        File.WriteAllText(path, Json);
    }
    public void LoadMonster(int floor)
    {
        MonsterSaveData monsterSaveData = new MonsterSaveData();
        string fileName = "Monster" + floor;
        string path = Application.dataPath + "/Save/" + fileName + ".Json";
        string data = File.ReadAllText(path);
        monsterSaveData = JsonUtility.FromJson<MonsterSaveData>(data);
        if (monsterSaveData != null)
        {
            LoadVisitedFloor(monsterSaveData);
        }
        else
        {
            Debug.Log("DataError!");
        }
    }
    public void LoadVisitedFloor(MonsterSaveData monsterSaveData)
    {
        GameObject go;
        MonsterState monsterState;
        for (int i = 0; i < monsterSaveData.id.Count; i++)
        {

            go = Instantiate(basicMonster, monsterSaveData.monsterPos[i], Quaternion.identity);
            monsterState = go.transform.GetComponent<MonsterState>();
            monsterState.id = monsterSaveData.id[i];
            monsterDataRead.GetMonsterData(monsterState);
            monsterState.Hp = monsterSaveData.currentHp[i];
            monsterList.Add(go);
        }

    }


    public void ItemSave()
    {
        ItemSaveData itemSaveData = new ItemSaveData();
        ItemState itemState;
        for (int i = 0; i < itemList.Count; i++)
        {
            itemState = itemList[i].transform.GetComponent<ItemState>();
            itemSaveData.itemTypes.Add(itemState.itemType);
            itemSaveData.itemid.Add(itemState.id);
            itemSaveData.rarity.Add(itemState.rarity);
            itemSaveData.option.Add(itemState.option);
            itemSaveData.ItemPos.Add(itemList[i].transform.position);
            itemSaveData.amount.Add(itemState.amount);
        }
        string Json = JsonUtility.ToJson(itemSaveData);
        string fileName = "Item" + GameManager.instance.nowFloor;
        string path = Application.dataPath + "/Save/" + fileName + ".Json";
        File.WriteAllText(path, Json);
    }
    public void LoadItem(int floor)
    {
        ItemSaveData itemSaveData = new ItemSaveData();
        string fileName = "Item" + floor;
        string path = Application.dataPath + "/Save/" + fileName + ".Json";
        string data = File.ReadAllText(path);
        itemSaveData = JsonUtility.FromJson<ItemSaveData>(data);
        if (itemSaveData != null)
        {
            LoadVisitedFloorItem(itemSaveData);
        }
        else
        {
            Debug.Log("DataError!");
        }
    }
    public void LoadVisitedFloorItem(ItemSaveData itemSaveData)
    {
        GameObject go;
        ItemState itemState;
        for (int i = 0; i < itemSaveData.itemTypes.Count; i++)
        {
            go = Instantiate(ItemArray[0], itemSaveData.ItemPos[i], Quaternion.identity);
            itemState = go.transform.GetComponent<ItemState>();
            itemState.itemType = itemSaveData.itemTypes[i];
            itemState.id = itemSaveData.itemid[i];
            if(itemState.itemType == ItemType.Equipment)
            {
                itemDataRead.GetItemData(itemState.id, "Equip", itemState);
            }
            else if (itemState.itemType == ItemType.Consumable)
            {
                itemDataRead.GetItemData(itemState.id,"Consum",itemState);
            }
            else
            {
                Debug.Log("Read Error!");
            }
            itemState.rarity = itemSaveData.rarity[i];
            itemState.option = itemSaveData.option[i];
            itemState.amount = itemSaveData.amount[i];
            itemList.Add(go);

        }
    }
}