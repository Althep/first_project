using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSaveData
{
    public List<string> monsterName = new List<string>();
    public List<int> maxHp = new List<int>();
    public List<int> currentHp = new List<int>();
    public List<int> id = new List<int>();
    public List<Vector3> monsterPos = new List<Vector3>();
}
public class ItemSaveData
{
    public List<ItemType> itemTypes = new List<ItemType>();
    public List<string> option = new List<string>();
    public List<int> itemid = new List<int>();
    public List<Rarity> rarity = new List<Rarity>();
    public List<Vector3> ItemPos = new List<Vector3>();
    public List<int> amount = new List<int>();
}
public class SaveDataScript
{
}
