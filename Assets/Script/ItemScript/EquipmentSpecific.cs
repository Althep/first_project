using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public enum EquipType
{
    None,
    Weapon,
    Armor,
    Glove,
    Shield,
    Helm,
    Shoes,
    Ring,
    Amulet
}
public enum Rarity
{
    Normal,
    Enchanted,
    RandomArtifact,
    Artifact
}
public enum WeaponType
{
    NotWeaPon,
    ShortSword,
    LongSword,
    Axe,
    Hammer
}
public enum ArmorType
{
    NotArmor,
    Cloth,
    LightArmor,
    HeavyArmor,
    Shield
}
public enum ItemType
{
    None,
    Equipment,
    Consumable
}
public enum ConsumKind
{
    None,
    Potion,
    Scroll,
    Book,
    Evoke
}
public class EquipmentSpecific : MonoBehaviour
{
    Sprite sprite;
    public ItemType itemType;
    ItemSpriteChange itemSpriteChange;
    ItemState itemState;
    NormalDistribute normalDist;
    ItemDataRead dataRead;
    private void Start()
    {
        dataRead = GameObject.FindWithTag("GameManager").GetComponent<ItemDataRead>();
        normalDist = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<NormalDistribute>();
        itemState = this.gameObject.GetComponent<ItemState>();
        if (!GameManager.instance.savedFloor.Contains(GameManager.instance.nowFloor))
        {
            SpecificConsumable();
            itemState = this.gameObject.GetComponent<ItemState>();
        }
        else
        {
            itemSpriteChange = GameObject.FindWithTag("GameManager").GetComponent<ItemSpriteChange>();
            SpriteCheck();
        }
        this.gameObject.name = itemState.itemName;
    }
    void SpecificConsumable()
    {
        int SC = Random.Range(0, 100);
        if (SC > 50)
        {
            itemState.tier = normalDist.MakeRate(normalDist.consumDatas);
            itemState.itemType = ItemType.Consumable;
            GetItemRandomId("Consum");
        }
        else
        {
            itemState.tier = normalDist.MakeRate(normalDist.equipDatas);
            itemType = ItemType.Equipment;
            GetItemRandomId("Equip");
        }


        SpriteCheck();
        
    }
    void GetItemRandomId(string itemtype)
    {
        //IDList IdList = new IDList();
        //string fileName = itemtype + "IDList" + itemState.tier;
        //string dataPath = Application.dataPath + "/Save/" + fileName + ".Json";
        //string data = File.ReadAllText(dataPath);
        //IdList = JsonUtility.FromJson<IDList>(data);
        //int temp = Random.Range(0, IdList.idList.Count);
        //GetItemData(IdList.idList[temp],itemtype,itemState);
        IDList idList = new IDList();
        List<Dictionary<string, object>> idValues;
        if (itemtype == "Equip")
        {
            idValues = dataRead.equipItemDatas;
            for (int i = 0; i < idValues.Count; i++)
            {
                if (itemState.tier == int.Parse(idValues[i]["tier"].ToString()))
                {
                    idList.idList.Add(int.Parse(idValues[i]["id"].ToString()));
                }
            }
        }
        else if (itemtype == "Consum")
        {
            idValues = dataRead.consumItemDatas;
            for(int i =0; i < idValues.Count; i++)
            {
                if(itemState.tier == int.Parse(idValues[i]["tier"].ToString()))
                {
                    idList.idList.Add(int.Parse(idValues[i]["id"].ToString()));
                }
            }
        }
        int temp = Random.Range(0, idList.idList.Count);
        GetItemData(idList.idList[temp], itemtype, itemState);
    }
    public void GetItemData(int id,string itemtype,ItemState itemStates)
    {
        List<Dictionary<string, object>> data;
        if (itemtype=="Equip")
            data = dataRead.equipItemDatas;
        else if (itemtype=="Consum")
            data = dataRead.consumItemDatas;
        else
            data = null;
        if (data != null)
        {
            itemStates.id = (int)data[id]["id"];
            itemStates.itemName = data[id]["name"].ToString();
            itemStates.itemType = (ItemType)((int)StringToInt.TypeStringToInt(data[id]["itemtype"].ToString(), "ItemType"));
            itemStates.equipType = (EquipType)(StringToInt.TypeStringToInt(data[id]["equiptype"].ToString(), "EquipType"));
            itemStates.weaponType = (WeaponType)(StringToInt.TypeStringToInt(data[id]["weapontype"].ToString(), "WeaponType"));
            itemStates.armorType = (ArmorType)(StringToInt.TypeStringToInt(data[id]["armortype"].ToString(), "ArmorType"));
            itemStates.equipSlot = data[id]["slot"].ToString();
            itemStates.damage = (int)data[id]["damage"];
            itemStates.def = (int)data[id]["defense"];
            itemStates.range = (int)data[id]["range"];
            itemStates.attSpeed = float.Parse(data[id]["attspeed"].ToString());
            itemStates.tier = (int)data[id]["tier"];
            itemStates.option=((string)data[id]["option"].ToString());
            itemStates.consumKind = (ConsumKind)(StringToInt.TypeStringToInt(data[id]["consumkind"].ToString(), "ConsumKind"));
            itemStates.conditionType = (ConditionType)(StringToInt.TypeStringToInt(data[id]["conditiontype"].ToString(), "ConditionType"));
            itemStates.conditionName = data[id]["conditionname"].ToString();
        }
        else
        {
            Debug.Log("DataNull!");
        }
        SpecificRarity();
    }
    void SpecificRarity()
    {
        int rar = Random.Range(0, 100);
        if (rar >= 98)
        {
            itemState.rarity = Rarity.Artifact;
        }
        else if (rar >= 90)
        {
            itemState.rarity = Rarity.RandomArtifact;
        }
        else if (rar >= 70)
        {
            itemState.rarity = Rarity.Enchanted;
        }
        else
        {
            itemState.rarity = Rarity.Normal;
        }

    }
    void ChangeItemSprite(string path)
    {
        sprite = Resources.Load<Sprite>(path);
        if (sprite == null) { Debug.Log(11); }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    void SpriteCheck()
    {
        string path = null;
        switch (itemState.itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Equipment:
                path = "AnimSprite\\Items\\" + itemState.id;
                break;
            case ItemType.Consumable:
                switch (itemState.consumKind)
                {
                    case ConsumKind.None:
                        break;
                    case ConsumKind.Potion:
                        path = "AnimSprite\\Items\\potion_0";
                        break;
                    case ConsumKind.Scroll:
                        path = "AnimSprite\\Items\\scroll_0";
                        break;
                    case ConsumKind.Book:
                        path = "AnimSprite\\Items\\book_0";
                        break;
                    case ConsumKind.Evoke:
                        path = "AnimSprite\\Items\\evoke_0";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        ChangeItemSprite(path);
    }

}
