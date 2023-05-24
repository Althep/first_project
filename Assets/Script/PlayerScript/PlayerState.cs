using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public enum ConditionType 
{
    none,
    start,
    update,
    end
}

public class Condition
{
    public string conditionName;
    public int startTurn;
    public int endTurn;
    public ConditionType conditionType;
}
class PlayerJson
{
    public int playerActPoint;
    public int maxHp;
    public int currentHp;
    public int moveSpeed;
    public int attSpeed;
    public int maxMp;
    public int currentMp;
    public int playervision;
    public int playerBaseAtk;
    public int str;
    public int dex;
    public int intelligence;
    public Vector3 playerPos;
    public GameObject playerGameObject;
}
public class PlayerState : AliveObj
{
    [SerializeField]public static bool isAiming=false;
    public static bool isStoped = false;
    public static bool isPlayerSpawnd = false;
    public static bool isThrow = false;
    public static bool isCast = false;
    public static int playerMoveSpeed;
    public static int playerMoveSpeed_;
    public static int playerAttSpeed;
    public static int playerAttSpeed_;
    public static int playerVision = 15;
    public static int playerCastSpeed;
    public static int playerCastSpeed_;
    public static Damage baseDamage_=new Damage();
    public static int addBaseDamageValue = 1;
    public static int addAccValue = 1;
    int oldAcc = 0;
    int oldAddBased = 0;  
    public static Damage addDamage_ = new Damage();
    public static int currentExp;
    public int nextLevelExp=20;
    public int playerMaxMp ;
    public int level = 1;
    [SerializeField] int lastHpRegenTurn=0;
    [SerializeField] int lastMpRegenTurn = 0;
    public int hpRegenRate = 10;
    public int mpRegenRate = 5;
    public static int playerMp;
    int oldPlayerHp;
    int oldPlayerMp;
    public int str=10;
    public int oldstr = 0;
    public int inte=10;
    public int oldinte = 0;
    public int dex=10;
    public int olddex = 0;
    public string playerName;
    public float maxWeight;
    public float weight;
    string HpText;
    string MpText;
    public int playerEquip;
    [SerializeField] public List<int> equipSolots = new List<int>();
    Text playerHpText;
    Text playerMpText;
    Slider playerHpSlider;
    Slider playerMpSlider;
    GameObject playerGameObject;
    [SerializeField]public List<Condition> playerCondition = new List<Condition>();
    public List<string> equipOptions = new List<string>();
    public string condition;
    private void Start()
    {
        moveSpeed = 100;
        attSpeed = 100;
        castSpeed = 100;
        MaxHp = 20;
        playerMaxMp = 2;
        playerName = "asdf";
        Hp = MaxHp;
        playerMp = playerMaxMp;
        playerMoveSpeed = moveSpeed;
        playerMoveSpeed_ = playerMoveSpeed;
        playerAttSpeed = attSpeed;
        playerAttSpeed_ = playerAttSpeed;
        playerCastSpeed = 100;
        playerCastSpeed_ = playerCastSpeed;
        baseDamage.damageValue = 1;
        baseDamage.damageType = DamageType.None;
        baseDamage_ = baseDamage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Save();
        }
        LevelUp();
        Regen();
        ConditionCheck();
        StateUpdate();
    }
    void LevelUp()
    {
        if (nextLevelExp <= currentExp)
        {
            level++;
            currentExp -= nextLevelExp;
            nextLevelExp = (int)(nextLevelExp*1.3f);
        }
    }
    
    void Regen()
    {
        if (lastHpRegenTurn + hpRegenRate <= TurnManage.totallTurn)
        {
            if (Hp + 1 <= MaxHp)
            {
                Hp += 1;
            }
            lastHpRegenTurn = TurnManage.totallTurn;
        }
        if (lastMpRegenTurn + mpRegenRate <= TurnManage.totallTurn)
        {
            if (playerMp + 1 <= playerMaxMp)
            {
                playerMp += 1;
            }
            lastMpRegenTurn = TurnManage.totallTurn;
        }
    }
    void ConditionCheck()
    {
        if (playerCondition.Count != 0)
        {
            for(int i = 0; i < playerCondition.Count; i++)
            {

            }
        }
    }
    public void Save()
    {
        PlayerJson playerJson = new PlayerJson();
        playerJson.attSpeed = attSpeed;
        playerJson.moveSpeed = moveSpeed;
        playerJson.maxHp = MaxHp;
        playerJson.maxMp = playerMaxMp;
        playerJson.currentHp= Hp;
        playerJson.currentMp = playerMp;
        playerJson.playervision = playerVision;
        playerJson.playerBaseAtk = baseDamage.damageValue;
        playerJson.str = str;
        playerJson.dex = dex;
        playerJson.intelligence = inte;
        playerJson.playerGameObject = this.gameObject;

        string Json = JsonUtility.ToJson(playerJson);
        string fileName = "Player";
        string path = Application.dataPath + "\\Save" + fileName + ".Json";
        File.WriteAllText(path, Json);
    }
    public void StateUpdate()
    {
        if(olddex!=dex || oldstr !=str || oldinte != inte || oldAddBased!=addBaseDamageValue || oldAcc!=addAccValue)
        {
            olddex = dex;
            oldstr = str;
            oldinte = inte;
            oldAddBased = addBaseDamageValue;
            baseDamage.damageValue = ((str - (str - 10)) / 4)+addBaseDamageValue + ((dex - (dex - 10)) / 6);
            oldAcc = addAccValue;
            acc = (dex - ((dex - 10) / 2)) + addAccValue+ (str - ((str - 10) / 4));
        }
    }
}
