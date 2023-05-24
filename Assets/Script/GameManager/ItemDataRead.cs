using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
class IDList
{
    public List<int> idList = new List<int>();
}

public class ItemDataRead : MonoBehaviour
{
    CSVReader reader;
    public List<Dictionary<string, object>> equipItemDatas;
    public List<Dictionary<string, object>> consumItemDatas;
    public List<Dictionary<string, object>> resetData;
    ItemState itemState;
    List<ItemState> equipItemDataList = new List<ItemState>();
    List<ItemState> consumItemDataList = new List<ItemState>();
    ItemState ResetData = new ItemState();
    List<ItemState> tierList = new List<ItemState>();
    int itemTierMax = 6;
    int itemTierMin = 1;
    private void Start()
    {
        EquipDataRead("Equip");
        EquipDataRead("Consum");
        
        SortByTier("Equip");
        SortByTier("Consum");
    }
    public void EquipDataRead(string itemType)
    {
        string file = itemType + "ItemData";
        if (itemType == "Equip")
        {
            equipItemDatas = CSVReader.Read(file);
            for (int i = 0; i < equipItemDatas.Count; i++)
            {
                itemState = new ItemState();
                itemState.id = (int)equipItemDatas[i]["id"];
                itemState.itemName = equipItemDatas[i]["name"].ToString();
                itemState.itemType = (ItemType)((int)StringToInt.TypeStringToInt(equipItemDatas[i]["itemtype"].ToString(), "ItemType"));
                itemState.equipType = (EquipType)(StringToInt.TypeStringToInt(equipItemDatas[i]["equiptype"].ToString(), "EquipType"));
                itemState.weaponType = (WeaponType)(StringToInt.TypeStringToInt(equipItemDatas[i]["weapontype"].ToString(), "WeaponType"));
                itemState.armorType = (ArmorType)(StringToInt.TypeStringToInt(equipItemDatas[i]["armortype"].ToString(), "ArmorType"));
                itemState.equipSlot = equipItemDatas[i]["slot"].ToString();
                itemState.damage = (int)equipItemDatas[i]["damage"];
                itemState.def = (int)equipItemDatas[i]["defense"];
                itemState.range = (int)equipItemDatas[i]["range"];
                itemState.attSpeed = float.Parse(equipItemDatas[i]["attspeed"].ToString());
                itemState.tier = (int)equipItemDatas[i]["tier"];
                itemState.option=(equipItemDatas[i]["option"].ToString());
                itemState.consumKind = ConsumKind.None;
                itemState.conditionType = ConditionType.none;
                itemState.conditionName = "None";
                itemState.panalty = (int)equipItemDatas[i]["panalty"];
                itemState.sh = (int)equipItemDatas[i]["sh"];
                itemState.weight =float.Parse(equipItemDatas[i]["weight"].ToString());
                itemState.acc = (int)equipItemDatas[i]["acc"];
                itemState.damageReduce = (int)equipItemDatas[i]["damreduce"];
                if (itemType == "Equip")
                    equipItemDataList.Add(itemState);
                else if (itemType == "Consum")
                    consumItemDataList.Add(itemState);
            }
        }
        else if (itemType == "Consum")
        {
            consumItemDatas = CSVReader.Read(file);
            for (int i = 0; i < consumItemDatas.Count; i++)
            {
                itemState = new ItemState();
                itemState.id = (int)consumItemDatas[i]["id"];
                itemState.itemName = consumItemDatas[i]["name"].ToString();
                itemState.itemType = (ItemType)((int)StringToInt.TypeStringToInt(consumItemDatas[i]["itemtype"].ToString(), "ItemType"));
                itemState.equipType = (EquipType)(StringToInt.TypeStringToInt(consumItemDatas[i]["equiptype"].ToString(), "EquipType"));
                itemState.weaponType = (WeaponType)(StringToInt.TypeStringToInt(consumItemDatas[i]["weapontype"].ToString(), "WeaponType"));
                itemState.armorType = (ArmorType)(StringToInt.TypeStringToInt(consumItemDatas[i]["armortype"].ToString(), "ArmorType"));
                itemState.equipSlot = consumItemDatas[i]["slot"].ToString();
                itemState.damage = (int)consumItemDatas[i]["damage"];
                itemState.def = (int)consumItemDatas[i]["defense"];
                itemState.range = (int)consumItemDatas[i]["range"];
                itemState.attSpeed = float.Parse(consumItemDatas[i]["attspeed"].ToString());
                itemState.tier = (int)consumItemDatas[i]["tier"];
                itemState.option=(consumItemDatas[i]["option"].ToString());
                itemState.consumKind = (ConsumKind)(StringToInt.TypeStringToInt(consumItemDatas[i]["consumkind"].ToString(), "ConsumKind"));
                itemState.conditionType = (ConditionType)(StringToInt.TypeStringToInt(consumItemDatas[i]["conditiontype"].ToString(), "ConditionType"));
                itemState.conditionName = consumItemDatas[i]["conditionname"].ToString();
                itemState.sh = 0;
                itemState.panalty = 0;
                itemState.acc = 0;
                switch (itemState.consumKind)
                {
                    case ConsumKind.None:
                        itemState.weight = 0;
                        break;
                    case ConsumKind.Potion:
                        itemState.weight = 0.3f;
                        break;
                    case ConsumKind.Scroll:
                        itemState.weight = 0.1f;
                        break;
                    case ConsumKind.Book:
                        itemState.weight = 2f;
                        break;
                    case ConsumKind.Evoke:
                        itemState.weight = 1f;
                        break;
                    default:
                        break;
                }
                if (itemType == "Equip")
                    equipItemDataList.Add(itemState);
                else if (itemType == "Consum")
                    consumItemDataList.Add(itemState);
            }
        }
        else if(itemType == "Reset")
        {
            file = "DataReset";
            resetData = CSVReader.Read(file);
            for (int i = 0; i < resetData.Count; i++)
            {
                itemState = new ItemState();
                itemState.id = (int)resetData[i]["id"];
                itemState.itemName = resetData[i]["name"].ToString();
                itemState.itemType = (ItemType)((int)StringToInt.TypeStringToInt(resetData[i]["itemtype"].ToString(), "ItemType"));
                itemState.equipType = (EquipType)(StringToInt.TypeStringToInt(resetData[i]["equiptype"].ToString(), "EquipType"));
                itemState.weaponType = (WeaponType)(StringToInt.TypeStringToInt(resetData[i]["weapontype"].ToString(), "WeaponType"));
                itemState.armorType = (ArmorType)(StringToInt.TypeStringToInt(resetData[i]["armortype"].ToString(), "ArmorType"));
                itemState.equipSlot = resetData[i]["slot"].ToString();
                itemState.damage = (int)resetData[i]["damage"];
                itemState.def = (int)resetData[i]["defense"];
                itemState.range = (int)resetData[i]["range"];
                itemState.attSpeed = float.Parse(resetData[i]["attspeed"].ToString());
                itemState.tier = (int)resetData[i]["tier"];
                itemState.option = (resetData[i]["option"].ToString());
                itemState.consumKind = (ConsumKind)(StringToInt.TypeStringToInt(resetData[i]["consumkind"].ToString(), "ConsumKind"));
                itemState.conditionType = (ConditionType)(StringToInt.TypeStringToInt(resetData[i]["conditiontype"].ToString(), "ConditionType"));
                itemState.conditionName = resetData[i]["conditionname"].ToString();
                itemState.acc = (int)resetData[i]["acc"];
                itemState.panalty = (int)resetData[i]["panalty"];
                itemState.weight = float.Parse(resetData[i]["weight"].ToString());
                itemState.sh = (int)resetData[i]["sh"];
                ResetData = itemState;
            }
        }

    }
    public void SortByTier(string itemType)
    {
        IDList idList = new IDList();
        if (itemType == "Equip")
        {
            for (int i = 0; i < itemTierMax; i++)
            {
                for (int j = 0; j < equipItemDataList.Count; j++)
                {
                    if (equipItemDataList[j].tier == i + 1)
                    {
                        idList.idList.Add(equipItemDataList[j].id);
                    }
                }
                string Json = JsonUtility.ToJson(idList);
                string fileName = itemType + "IDList" + (i + 1);
                string path = Application.dataPath + "/Save/" + fileName + ".Json";
                File.WriteAllText(path, Json);
                idList.idList.Clear();
            }
        }
        else if (itemType == "Consum")
        {
            for (int i = 0; i < itemTierMax; i++)
            {
                for (int j = 0; j < consumItemDataList.Count; j++)
                {
                    if (consumItemDataList[j].tier == i + 1)
                    {
                        idList.idList.Add(consumItemDataList[j].id);
                    }
                }
                string Json = JsonUtility.ToJson(idList);
                string fileName = itemType + "IDList" + (i + 1);
                string path = Application.dataPath + "/Save/" + fileName + ".Json";
                File.WriteAllText(path, Json);
                idList.idList.Clear();
            }
        }
    }
    public void GetItemData(int id, string itemtype, ItemState itemStates)
    {
        Dictionary<string, object> itemData = new Dictionary<string, object>();
        string file = itemtype + "ItemData";
        if (itemtype == "Equip")
        {
            itemStates.id = (int)equipItemDatas[id]["id"];
            itemStates.itemName = equipItemDatas[id]["name"].ToString();
            itemStates.itemType = (ItemType)((int)StringToInt.TypeStringToInt(equipItemDatas[id]["itemtype"].ToString(), "ItemType"));
            itemStates.equipType = (EquipType)(StringToInt.TypeStringToInt(equipItemDatas[id]["equiptype"].ToString(), "EquipType"));
            itemStates.weaponType = (WeaponType)(StringToInt.TypeStringToInt(equipItemDatas[id]["weapontype"].ToString(), "WeaponType"));
            itemStates.armorType = (ArmorType)(StringToInt.TypeStringToInt(equipItemDatas[id]["armortype"].ToString(), "ArmorType"));
            itemStates.equipSlot = equipItemDatas[id]["slot"].ToString();
            itemStates.damage = (int)equipItemDatas[id]["damage"];
            itemStates.def = (int)equipItemDatas[id]["defense"];
            itemStates.range = (int)equipItemDatas[id]["range"];
            itemStates.attSpeed = float.Parse(equipItemDatas[id]["attspeed"].ToString());
            itemStates.tier = (int)equipItemDatas[id]["tier"];
            itemStates.option=(equipItemDatas[id]["option"].ToString());
            itemStates.consumKind = ConsumKind.None;
            itemStates.conditionType = ConditionType.none;
            itemStates.conditionName = "None";
            itemStates.amount = 1;

        }
        if (itemtype == "Consum")
        {
            itemStates.id = (int)consumItemDatas[id]["id"];
            itemStates.itemName = consumItemDatas[id]["name"].ToString();
            itemStates.itemType = (ItemType)((int)StringToInt.TypeStringToInt(consumItemDatas[id]["itemtype"].ToString(), "ItemType"));
            itemStates.equipType = (EquipType)(StringToInt.TypeStringToInt(consumItemDatas[id]["equiptype"].ToString(), "EquipType"));
            itemStates.weaponType = (WeaponType)(StringToInt.TypeStringToInt(consumItemDatas[id]["weapontype"].ToString(), "WeaponType"));
            itemStates.armorType = (ArmorType)(StringToInt.TypeStringToInt(consumItemDatas[id]["armortype"].ToString(), "ArmorType"));
            itemStates.equipSlot = consumItemDatas[id]["slot"].ToString();
            itemStates.damage = (int)consumItemDatas[id]["damage"];
            itemStates.def = (int)consumItemDatas[id]["defense"];
            itemStates.range = (int)consumItemDatas[id]["range"];
            itemStates.attSpeed = float.Parse(consumItemDatas[id]["attspeed"].ToString());
            itemStates.tier = (int)consumItemDatas[id]["tier"];
            itemStates.option=(consumItemDatas[id]["option"].ToString());
            itemStates.consumKind = (ConsumKind)(StringToInt.TypeStringToInt(consumItemDatas[id]["consumkind"].ToString(), "ConsumKind"));
            itemStates.conditionType = (ConditionType)(StringToInt.TypeStringToInt(consumItemDatas[id]["conditiontype"].ToString(), "ConditionType"));
            itemStates.conditionName = consumItemDatas[id]["conditionname"].ToString();
            itemStates.amount = 1;
        }
        if(itemtype == "Reset")
        {
            file = "DataReset";
            itemStates.id = (int)ResetData.id;
            itemStates.itemName = ResetData.itemName;
            itemStates.itemType = ResetData.itemType;
            itemStates.equipType = ResetData.equipType;
            itemStates.weaponType = ResetData.weaponType;
            itemStates.armorType = ResetData.armorType;
            itemStates.equipSlot = ResetData.equipSlot;
            itemStates.damage = ResetData.damage;
            itemStates.def = ResetData.def;
            itemStates.range = ResetData.range;
            itemStates.attSpeed = ResetData.attSpeed;
            itemStates.tier = ResetData.tier;
            itemStates.option = ResetData.option;
            itemStates.consumKind = ResetData.consumKind;
            itemStates.conditionType = ResetData.conditionType;
            itemStates.conditionName = ResetData.conditionName;
            itemStates.amount = 0;
        }
    }
    public void MoveItemData(ItemState getItem,ItemState giveItem)
    {
        getItem.id = giveItem.id;
        switch (giveItem.itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Equipment:
                GetItemData(giveItem.id, "Equip", getItem);
                break;
            case ItemType.Consumable:
                GetItemData(giveItem.id, "Consum", getItem);
                break;
            default:
                break;
        }
        getItem.rarity = giveItem.rarity;
        getItem.option = giveItem.option;
        getItem.enhanced = giveItem.enhanced;
    }

}
