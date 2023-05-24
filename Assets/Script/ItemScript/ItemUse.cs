using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    GameObject gameManager;
    AddCondition addCondition;
    Function Function;
    TuneEquipSlot tuneEquip;
    PlayerState playerState;
    InventorySlot inventorySlot;
    Inventory inventory;
    int maxSlotCount = 9;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        addCondition = gameManager.GetComponent<AddCondition>();
        Function = gameManager.GetComponent<Function>();
        tuneEquip = gameManager.GetComponent<TuneEquipSlot>();
        playerState = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerState>();
        inventory = GameObject.Find("Inventory").gameObject.transform.GetComponent<Inventory>();
    }


    public void UseConsum(ItemState itemState)
    {
        if (itemState.itemType != ItemType.Consumable)
        {
            return;
        }
        switch (itemState.conditionType)
        {
            case ConditionType.none:
                GetStringToFunction(itemState.option);
                Debug.Log("Function!");
                break;
            case ConditionType.start:

                addCondition.AddConditionToPlayer(itemState.option, TurnManage.totallTurn);

                break;
            case ConditionType.update:

                addCondition.AddConditionToPlayer(itemState.option, TurnManage.totallTurn);

                break;
            case ConditionType.end:

                addCondition.AddConditionToPlayer(itemState.option, TurnManage.totallTurn);

                break;
            default:
                break;
        }
    }
    void GetStringToFunction(string function)
    {
        switch (function)
        {
            case "Heal":
                Function.Heal();
                break;
            case "HealingWounds":
                Function.HealingWounds();
                break;
            default:
                break;
        }
    }
    public void UseEquipment(ItemState itemState)
    {
        int slot = 0;
        int slotIndex = 0;
        if (itemState.itemType != ItemType.Equipment)
        {
            return;
        }
        slot = StringToInt.TypeStringToInt(itemState.equipSlot, "Slot");
        inventorySlot = itemState.transform.gameObject.GetComponent<InventorySlot>();
        slotIndex =  (int)MathF.Log(slot, 2);
        List<char> equipString = tuneEquip.StringToList(Convert.ToString(playerState.playerEquip, 2));
        if (!inventorySlot.isEquiped)
        {
            if (itemState.equipType != EquipType.Weapon && itemState.equipType != EquipType.Ring)
            {
                if (equipString[maxSlotCount - 1 - slotIndex] != '1')
                {
                    string test = null;
                    foreach (char c in equipString)
                    {
                        int j = 0;
                        test += c;
                        j++;
                    }
                    AddEquipState(itemState, slot);
                }

            }
            else if (itemState.equipType == EquipType.Weapon || itemState.equipType == EquipType.Ring || itemState.equipType == EquipType.Shield)
            {
                switch (itemState.equipType)
                {
                    case EquipType.Weapon:
                        string weaponSlotTemp = null;
                        int weaponSlotint = 0;
                        for (int i = maxSlotCount-2; i <= maxSlotCount-1; i++)
                        {
                            weaponSlotTemp += equipString[i];
                        }
                        weaponSlotint = Convert.ToInt32(weaponSlotTemp, 2);
                        if (equipString[maxSlotCount-2] != '1')
                        {
                            if ((weaponSlotint + slot) <= 2)
                            {
                                AddEquipState(itemState, slot);
                            }
                        }
                        
                        break;
                    case EquipType.Shield:
                        weaponSlotTemp = null;
                        weaponSlotint = 0;
                        for (int i = maxSlotCount - 2; i <= maxSlotCount - 1; i++)
                        {
                            weaponSlotTemp += equipString[i];
                        }
                        weaponSlotint = Convert.ToInt32(weaponSlotTemp, 2);
                        if (equipString[maxSlotCount - 2] != '1')
                        {
                            if ((weaponSlotint + slot) <= 2)
                            {
                                AddEquipState(itemState, slot);
                            }
                        }

                        break;
                    case EquipType.Ring:
                        weaponSlotTemp = null;
                        weaponSlotint = 0;
                        for (int i = maxSlotCount - 2-slotIndex; i <= maxSlotCount - 1-slotIndex; i++)
                        {
                            weaponSlotTemp += equipString[i];
                        }
                        weaponSlotint = Convert.ToInt32(weaponSlotTemp, 2);
                        if (equipString[maxSlotCount - 2-slotIndex] != '1')
                        {
                            if ((weaponSlotint + slot) <= 128)
                            {
                                AddEquipState(itemState, slot);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            playerState.playerEquip -= slot;
            playerState.addDamage.damageValue -= itemState.damage;
            playerState.def -= itemState.def;
            if (playerState.equipOptions.Contains(itemState.option))
            {
                playerState.equipOptions.Remove(itemState.option);
            }
            playerState.acc -= itemState.acc;
            playerState.sh -= itemState.sh;
            inventorySlot.isEquiped = false;
            inventorySlot.equpImage.SetActive(false);
            playerState.equipSolots.Remove(inventorySlot.slotNumber);
        }  

    }

    void AddEquipState(ItemState itemState,int slot)
    {
        playerState.playerEquip += slot;
        playerState.addDamage.damageValue += itemState.damage;
        playerState.def += itemState.def;
        playerState.equipOptions.Add(itemState.option);
        playerState.acc += itemState.acc;
        playerState.sh += itemState.sh;
        inventorySlot.isEquiped = true;
        inventorySlot.equpImage.SetActive(true);
        playerState.equipSolots.Add(inventorySlot.slotNumber);
    }
}
