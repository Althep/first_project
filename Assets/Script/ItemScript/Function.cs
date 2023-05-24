using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : MonoBehaviour
{
    FieldManager fieldManager;
    GameObject player;
    PlayerState playerState;
    TurnManage turnManage;
    AddCondition addCondition;
    PlayerAim playerAim;
    public GameObject Cloud;
    Inventory inventory;
    SpawnManager spawnManager;
    private void Start()
    {
        fieldManager = GameObject.FindWithTag("FieldManager").transform.GetComponent<FieldManager>();
        player = GameObject.FindWithTag("Player");
        playerState = player.transform.GetComponent<PlayerState>();
        turnManage = GameObject.FindWithTag("GameManager").transform.GetComponent<TurnManage>();
        playerAim = player.transform.GetComponent<PlayerAim>();
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        spawnManager = GameObject.FindWithTag("GameManager").transform.GetComponent<SpawnManager>();
    }

    public void ThrowItem(ItemState itemState, AliveObj attObj, GameObject target, Sprite sprite)
    {
        Damage damage = new Damage();
        int range = 0;
        playerAim.playerMissile.transform.GetComponent<SpriteRenderer>().sprite = sprite;
        if (inventory.isThrow)
        {
            switch (itemState.itemType)
            {
                case ItemType.Equipment:
                    range = (int)itemState.weight / playerState.str;
                    Debug.Log(111);
                    if (range <= 0)
                    {
                        range = 1;
                    }
                    else if (range >= 7)
                    {
                        range = 7;
                    }
                    damage.damageValue = (int)((itemState.weight / 2) + (playerState.str / 4));
                    if (damage.damageValue <=1) damage.damageValue = 1;
                    damage.damageType = DamageType.None;
                    Debug.Log(2);
                    if (target.transform.tag == "Monster")
                    {
                        target.transform.GetComponent<AliveObj>().Hp-=attObj.HitDamage(damage, target.transform.GetComponent<AliveObj>());
                        playerAim.playerMissile.transform.GetComponent<SpriteRenderer>().sprite = sprite;
                        playerAim.playerMissile.SetActive(true);
                        playerAim.MoveMissile(target.transform.position);
                        GameObject go = Instantiate(spawnManager.ItemArray[0], ThrowItemGetPos(attObj.transform.position, target.transform.position), Quaternion.identity);
                        go.name = "adf";
                        Dialog.instance.UpdateDialog("You throw "+itemState.itemName+" to "+target.name);
                        inventory.isThrow=false;
                        Debug.Log(1111);
                        Debug.Log("DamageValue"+damage.damageValue);
                        PlayerState.isStoped=false;
                        //go.transform.GetComponent<ItemState>() = itemState;
                    }
                    else if (target.transform.tag == "Wall")
                    {
                        Debug.Log("Wall!");
                        inventory.isThrow=false;
                        PlayerState.isStoped=false;
                    }
                    break;
                case ItemType.Consumable:
                    switch (itemState.consumKind)
                    {
                        case ConsumKind.Potion:
                            playerAim.MoveMissile(target.transform.position);
                            Debug.Log("Cloude");
                            inventory.isThrow=false;
                            PlayerState.isStoped=false;
                            //MakeCloudes(itemState, target);
                            break;
                        case ConsumKind.Scroll:
                            break;
                        case ConsumKind.Book:
                            break;
                        case ConsumKind.Evoke:
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        

    }
    public void MakeCloudes(ItemState itemState, GameObject target)
    {
        GameObject go = Instantiate(Cloud, target.transform.position, Quaternion.identity);
        itemState.amount--;
        if (itemState.amount <= 1)
        {
            itemState.amount = 0;
            itemState = null;
            inventory.emptySlot[inventory.selectNumber] = true;
            Debug.Log("amunt 0");
        }
    }

    public void Heal()
    {
        int healAmount = 10;
        if (playerState.Hp + healAmount <= playerState.playerMaxMp)
        {
            playerState.Hp += healAmount;
            Debug.Log("healed");
        }
        else
        {
            playerState.Hp = playerState.MaxHp;
            Debug.Log("healed");
        }
        playerState.playerCondition.Clear();
    }
    public void HealingWounds()
    {
        int healAmount = 20;
        if (playerState.Hp + healAmount <= playerState.playerMaxMp)
        {
            playerState.Hp += healAmount;
        }
        else
        {
            playerState.Hp = playerState.MaxHp;
        }
    }
    public void Teleport(GameObject targetObj)
    {
        bool iscontainObj = false;
        bool canTele = false;
        Vector3 targetPos = Vector3.zero;
        int randomInt;
        while (!canTele)
        {
            randomInt = Random.Range(0, fieldManager.emptyPosList.Count);
            targetPos = fieldManager.emptyPosList[randomInt];
            foreach (Collider2D col in Physics2D.OverlapCircleAll(targetPos, 0.4f))
            {
                if (col.transform.tag == "Monster" || col.transform.tag == "Wall")
                {
                    iscontainObj = true;
                }
            }
            if (iscontainObj)
            {
                iscontainObj = false;
            }
            else
            {
                canTele = true;
            }
        }
        targetObj.transform.position = targetPos;
    }
    void Posion()
    {

    }
    Vector3 ThrowItemGetPos(Vector3 startPos, Vector3 targetPos)
    {
        Vector3 returnPos = Vector3.zero;
        Vector3 temp = targetPos - startPos;
        returnPos = new Vector3((int)(startPos.x + (temp.x * 0.9f)), (int)(startPos.y + (temp.y * 0.9f)));

        return returnPos;
    }

    public void ItemDataMove(ItemState giveState, ItemState getState)
    {




    }
}
