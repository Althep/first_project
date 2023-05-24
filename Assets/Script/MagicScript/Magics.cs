using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum TargetType
{
    target,
    notarget
}
public enum MagicDamageType
{
    none,
    physics,
    fire

}
public enum Ecole
{
    conjuration
}
public enum MagicType
{
    attack,
    buff

}
public enum MissileType 
{ 
    missile,
    scan
}

public class MagicData
{
    public TargetType targetType;
    public MagicDamageType damageType;
    public Ecole ecole;
    public MagicType magicType;
    public MissileType missileType;
    public int id;
    public string name;
    public int damage;
    public int range;
    public int scale;
    public int level;
    public int requireMp;
    public string description;
}

public class Magics : MonoBehaviour
{
    CSVReader reader;
    [SerializeField]List<Dictionary<string, object>> magicData;
    List<GameObject> targets = new List<GameObject>();
    public GameObject target;
    GameObject player;
    GameObject playerMissile;
    GetMagic getMagic;
    PlayerAim playerAim;
    Dialog dialog;
    Color color;
    void Start()
    {
        color = Color.white;
        magicData = CSVReader.Read("MagicData");
        player = GameObject.FindGameObjectWithTag("Player");
        playerMissile = player.transform.GetChild(2).gameObject;
        getMagic = this.transform.GetComponent<GetMagic>();
        playerAim = player.transform.GetComponent<PlayerAim>();
        dialog = GameObject.Find("DialogScrollView").transform.GetComponent<Dialog>();
    }

    public void GetMagicData(MagicData magic,int magicId)
    {
        if (magicData == null)
        {
            magicData = CSVReader.Read("MagicData");
        }
        magic.name = magicData[magicId]["name"].ToString();
        magic.damage = (int)magicData[magicId]["damage"];
        magic.range = (int)magicData[magicId]["range"];
        magic.scale = (int)magicData[magicId]["scale"];
        magic.level = (int)magicData[magicId]["level"];
        magic.requireMp = (int)magicData[magicId]["mp"];
        magic.description = (string)magicData[magicId]["description"];
        magic.targetType = (TargetType)StringToInt.TypeStringToInt(magicData[magicId]["targettype"].ToString(), "TargetType");
        magic.damageType = (MagicDamageType)StringToInt.TypeStringToInt(magicData[magicId]["damagetype"].ToString(), "MagicDamageType");
        magic.ecole = (Ecole)StringToInt.TypeStringToInt(magicData[magicId]["ecole"].ToString(), "Ecole");
        magic.magicType = (MagicType)StringToInt.TypeStringToInt(magicData[magicId]["magictype"].ToString(), "MagicType");
    }

    public void TargetMisileMagic(Vector3 targetPos,Vector3 startPos, MagicData magicData)
    {
        GameObject targetObj = playerAim.FindTarget(startPos, targetPos);
        MonsterState monsterState;
        string path = "AnimSprite\\Magics\\" + magicData.name+ "\\sprite";
        playerMissile.transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        if(magicData.magicType == MagicType.attack)
        {
            if (targetObj.transform.tag == "Monster"&&PlayerState.playerMp>=magicData.requireMp)
            {
                monsterState = targetObj.transform.GetComponent<MonsterState>();
                monsterState.Hp -= magicData.damage;
                PlayerState.playerMp -= magicData.requireMp;
                playerAim.playerMissile.SetActive(true);
                playerAim.MoveMissile(targetObj.transform.position);
                dialog.UpdateDialog(dialog.DialogMonsterHp(monsterState));
            }
            else if(targetObj== null||targetObj.tag!="Monster"&&PlayerState.playerMp>=magicData.requireMp)
            {
                PlayerState.playerMp -= magicData.requireMp;
                playerAim.playerMissile.SetActive(true);
                playerAim.MoveMissile(targetObj.transform.position);
                
            }
            else if (PlayerState.playerMp < magicData.requireMp)
            {
                dialog.UpdateDialog("Not enough MP");
            }
        }
    }
    
}
