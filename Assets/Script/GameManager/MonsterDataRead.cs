using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDataRead : MonoBehaviour
{
    CSVReader reader;
    List<Dictionary<string, object>> monsterData;
    [SerializeField] SpawnManager spawnManager;
    MonsterState monsterState;
    void Start()
    {
        monsterData = CSVReader.Read("MonsterData");
        spawnManager = this.gameObject.transform.GetComponent<SpawnManager>();
    }
    public void initiateMonster()
    {
        if (spawnManager == null)
        {
            spawnManager = this.gameObject.transform.GetComponent<SpawnManager>();
        }
        int randomId;
        if (monsterData == null)
        {
            monsterData = CSVReader.Read("MonsterData");
        }
        if (spawnManager != null)
        {
            for (int i = 0; i < spawnManager.monsterList.Count; i++)
            {
                randomId = Random.Range(0, monsterData.Count);
                monsterState = spawnManager.monsterList[i].GetComponent<MonsterState>();
                monsterState.id = randomId;
                monsterState = spawnManager.monsterList[i].GetComponent<MonsterState>();
                monsterState.monsterName = monsterData[randomId]["name"].ToString();
                monsterState.transform.gameObject.name = monsterData[randomId]["name"].ToString();
                monsterState.baseDamage.damageValue = (int)monsterData[randomId]["atk"];
                monsterState.baseDamage.damageType = (DamageType)StringToInt.TypeStringToInt(monsterData[randomId]["damagetype"].ToString(), "DamageType");
                monsterState.defence = (int)monsterData[randomId]["def"];
                monsterState.MaxHp = (int)monsterData[randomId]["maxhp"];
                monsterState.Hp = (int)monsterData[randomId]["maxhp"];
                monsterState.MaxHp=((int)monsterData[randomId]["maxhp"]);
                monsterState.monsterActPoint = (int)monsterData[randomId]["actpoint"];
                monsterState.acc = (int)monsterData[randomId]["acc"];
                monsterState.exp = (int)monsterData[randomId]["exp"];
                monsterState.tier = (int)monsterData[randomId]["tier"];
                if (monsterData[monsterState.id]["Poison"].ToString() != "0")
                {
                    Resist resist = new Resist();
                    resist.Type = DamageType.Poison;
                    resist.level = (int)monsterData[monsterState.id]["Poison"];
                    monsterState.resist.Add(resist);
                }
                if (monsterData[monsterState.id]["Fire"].ToString() != "0")
                {
                    Resist resist = new Resist();
                    resist.Type = DamageType.Fire;
                    resist.level = (int)monsterData[monsterState.id]["Fire"];
                    monsterState.resist.Add(resist);
                }
                if (monsterData[monsterState.id]["Ice"].ToString() != "0")
                {
                    Resist resist = new Resist();
                    resist.Type = DamageType.Ice;
                    resist.level = (int)monsterData[monsterState.id]["Ice"];
                    monsterState.resist.Add(resist);
                }
                if (monsterData[monsterState.id]["Magic"].ToString() != "0")
                {
                    Resist resist = new Resist();
                    resist.Type = DamageType.Magic;
                    resist.level = (int)monsterData[monsterState.id]["Magic"];
                    monsterState.resist.Add(resist);
                }
            }
        }
        else if (spawnManager == null)
        {
            Debug.Log("SpawnManagerNull");
        }

    }
    public void GetMonsterData(MonsterState monsterState)
    {
        monsterState.monsterName = monsterData[monsterState.id]["name"].ToString();
        monsterState.transform.gameObject.name = monsterData[monsterState.id]["name"].ToString();
        monsterState.baseDamage.damageValue = (int)monsterData[monsterState.id]["atk"];
        monsterState.defence = (int)monsterData[monsterState.id]["def"];
        monsterState.MaxHp = (int)monsterData[monsterState.id]["maxhp"];
        monsterState.Hp = (int)monsterData[monsterState.id]["maxhp"];
        monsterState.MaxHp=((int)monsterData[monsterState.id]["maxhp"]);
        monsterState.monsterActPoint = (int)monsterData[monsterState.id]["actpoint"];
        monsterState.acc = (int)monsterData[monsterState.id]["acc"];
        monsterState.exp = (int)monsterData[monsterState.id]["exp"];
        monsterState.tier = (int)monsterData[monsterState.id]["tier"];
        if (monsterData[monsterState.id]["Poison"].ToString() != "0")
        {
            Resist resist = new Resist();
            resist.Type = DamageType.Poison;
            resist.level = (int)monsterData[monsterState.id]["Poison"];
            monsterState.resist.Add(resist);
        }
        if (monsterData[monsterState.id]["Fire"].ToString() != "0")
        {
            Resist resist = new Resist();
            resist.Type = DamageType.Fire;
            resist.level = (int)monsterData[monsterState.id]["Fire"];
            monsterState.resist.Add(resist);
        }
        if (monsterData[monsterState.id]["Ice"].ToString() != "0")
        {
            Resist resist = new Resist();
            resist.Type = DamageType.Ice;
            resist.level = (int)monsterData[monsterState.id]["Ice"];
            monsterState.resist.Add(resist);
        }
        if (monsterData[monsterState.id]["Magic"].ToString() != "0")
        {
            Resist resist = new Resist();
            resist.Type = DamageType.Magic;
            resist.level = (int)monsterData[monsterState.id]["Magic"];
            monsterState.resist.Add(resist);
        }

    }
}
