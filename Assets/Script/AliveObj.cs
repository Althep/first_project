using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum DamageType
{
    None,
    Poison,
    Fire,
    Ice,
    Magic,
    Fure

}
public class Resist
{
    public DamageType Type;
    public int level;

}


public class Damage
{
    public DamageType damageType;
    public int damageValue;
}


public class AliveObj : MonoBehaviour
{
    public int MaxHp;
    public int Hp;
    public Damage addDamage = new Damage();
    public Damage baseDamage =new Damage();
    public List<Resist> resist = new List<Resist>();
    public bool isDead = false;
    public int acc;
    public int def;
    public int reduceDamage;
    public int moveSpeed;
    public int attSpeed;
    public int castSpeed;
    public int panalty;
    public int sh;
    public virtual void IsDead(int HP)
    {
        if (HP <= 0)
        {
            isDead = true;
        }
    }
    public virtual bool IsHit(AliveObj attObj,AliveObj hitObj)
    {
        bool temp = false;
        bool dodgetemp = TryDodge(attObj,hitObj);
        bool blockTemp = TryBlock(attObj, hitObj);
        if (!dodgetemp || !blockTemp)
        {
            temp = true;
        }
        else
        {
            temp = false;
        }
        return temp;
    }
    public bool ThrowHit(AliveObj attObj, AliveObj hitObj)
    {
        bool ishit=false;
        int rate = Random.Range(0,0);
        bool dodgetemp = TryDodgeMissile(attObj, hitObj);
        bool blockTemp = TryBlockMissile(attObj, hitObj);
        if(!dodgetemp&&!blockTemp)
        {
            ishit = true;
        }
        else
        {
            ishit = false;
        }

        return ishit;
    }
    public bool TryDodgeMissile(AliveObj attObj, AliveObj hitObj)
    {
        bool isDodge = false;
        int dodge = 10;
        int rate = Random.Range(0, 100);
        dodge = 10 + (10 + hitObj.def) / (2 + attObj.panalty + attObj.acc);
        if (dodge > 75)
        {
            dodge = 75;
        }
        if (rate >= dodge)
        {
            isDodge = true;
        }
        else
        {
            isDodge = false;
        }
        return isDodge;
    }
    public bool TryBlockMissile(AliveObj attObj,AliveObj hitObj)
    {
        bool isBlocked = false;
        int rate = Random.Range(0, 100);
        int block = 10;
        if (hitObj.sh != 0)
        {
            if (hitObj.transform.tag == "Player")
            {
                block = 10 - ((10 - (hitObj.transform.GetComponent<PlayerState>().dex + hitObj.transform.GetComponent<PlayerState>().str / 2) + hitObj.def + hitObj.sh / (2 + attObj.acc)));
            }
            else if (hitObj.transform.tag == "Monster")
            {
                block = 10 - (10 - hitObj.transform.GetComponent<MonsterState>().tier + hitObj.def + hitObj.sh) / (2 + attObj.acc);
            }
        }
        if (block > 75)
        {
            block = 75;
        }
        if (block <= rate)
        {
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }
        return isBlocked;

    }

    public bool TryDodge(AliveObj attObj, AliveObj hitObj)
    {
        bool isDodged = false;
        int dodge = 10;
        int rate = Random.Range(0, 100);
        dodge = 10 + (10 + hitObj.def) / (5 + attObj.panalty + attObj.acc);
        if (dodge > 75)
        {
            dodge = 75;
        }
        if (rate >= dodge)
        {
            isDodged = false;
        }
        else
        {
            isDodged = true;
        }

        return isDodged;
    }
    public bool TryBlock(AliveObj attObj, AliveObj hitObj)
    {
        bool isBlocked = false;
        int rate = Random.Range(0, 100);
        int block = 10;
        if (hitObj.sh != 0)
        {
            if (hitObj.transform.tag == "Player")
            {
                block = 10 - ((10 - (hitObj.transform.GetComponent<PlayerState>().dex+hitObj.transform.GetComponent<PlayerState>().str/2) + hitObj.def+hitObj.sh / (5+attObj.acc)));
            }
            else if(hitObj.transform.tag == "Monster")
            {
                block = 10-(10- hitObj.transform.GetComponent<MonsterState>().tier+hitObj.def+hitObj.sh)/(5+attObj.acc);
            }
        }
        if (block > 75)
        {
            block = 75;
        }
        if (block <= rate)
        {
            isBlocked = false;
        }
        else
        {
            isBlocked = true;
        }
        return isBlocked;
    }
    public virtual Damage AttackDamage()
    {
        Damage tempD = new Damage();
        tempD.damageValue = Random.Range(0, baseDamage.damageValue+1) + Random.Range(0, addDamage.damageValue);
        if (addDamage.damageType == DamageType.None)
            tempD.damageType = baseDamage.damageType;
        else if (baseDamage.damageType == DamageType.None)
            tempD.damageType = addDamage.damageType;
        else if (addDamage.damageType != DamageType.None)
            tempD.damageType = addDamage.damageType;
        else if (addDamage.damageType != DamageType.None && baseDamage.damageType != DamageType.None)
        {
            int temp = Random.Range(0, 3);
            if (temp == 0)
            {
                tempD.damageType = baseDamage.damageType;
            }
            else
                tempD.damageType = addDamage.damageType;
        }
        else
        {
            tempD.damageType = DamageType.None;
        }
        return tempD;
    }
    public virtual int HitDamage(Damage damage, AliveObj targetObj)
    {
        int returnDamage = 0;
        if (damage.damageType != DamageType.Fure)
        {
            if (targetObj.resist.Count > 0)
            {
                for (int i = 0; i < targetObj.resist.Count; i++)
                {
                    if (damage.damageType == targetObj.resist[i].Type)
                    {
                        returnDamage = (int)(damage.damageValue - ((float)damage.damageValue * ((float)targetObj.resist[i].level*0.33f)));
                    }
                }
            }
            else
            {
                returnDamage = damage.damageValue;
            }

            if (damage.damageType == DamageType.Fure)
            {
                returnDamage = damage.damageValue;
            }

        }
        return returnDamage;
    }

}
