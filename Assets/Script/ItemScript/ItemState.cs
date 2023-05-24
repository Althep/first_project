using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemState : MonoBehaviour
{
    public string itemName;
    public ItemType itemType;
    public EquipType equipType;
    public WeaponType weaponType;
    public ArmorType armorType;
    public Rarity rarity;
    public ConsumKind consumKind;//이후 equpSlot과 함께 int로 바꾸는 변환 만들것
    public string equipSlot;
    public int damage;
    public int def;
    public float range;
    public float attSpeed;
    public int tier;
    public int id;
    public int enhanced;
    public string conditionName;
    public string option;
    public int amount;
    public int panalty;
    public int sh;
    public float weight;
    public int acc;
    public int damageReduce;
    public ConditionType conditionType;
}
