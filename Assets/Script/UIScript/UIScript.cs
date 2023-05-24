using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    GameObject player;
    PlayerState playerState;
    public TextMeshProUGUI playerNameText;
    public string playerNameString;
    public TextMeshProUGUI floor;
    public static string floorText="1";
    public string dungeonName = "Floor :";
    public string oldfloorText="0";
    TurnManage turnManage;
    #region Slidier
    int oldPlayerMaxHp;
    int oldPlayerMaxMp;
    int oldPlayerHp;
    int oldPlayerMp;
    int oldTurn;
    Slider playerHpSlider;
    Slider playerMpSlider;
    int oldPlayerLevel;
    int oldPlayerExp;
    int playerMaxHp;
    int playerMaxMp;
    string hpString;
    string mpString;
    Text playerHpText;
    Text playerMpText;
    #endregion 
    public TextMeshProUGUI Exp;
    public TextMeshProUGUI Turn;
    public TextMeshProUGUI Str;
    public TextMeshProUGUI Dex;
    public TextMeshProUGUI Int;
    public TextMeshProUGUI level;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerState = player.GetComponent<PlayerState>();
        SettingSlider();
        StartCoroutine(SettingName());
        floor.text = "Floor " + GameManager.instance.nowFloor.ToString();
        Exp = GameObject.Find("Exp").transform.GetComponent<TextMeshProUGUI>();
        Turn = GameObject.Find("Turn").transform.GetComponent<TextMeshProUGUI>();
        Str = GameObject.Find("Str").transform.GetComponent<TextMeshProUGUI>();
        Dex = GameObject.Find("Dex").transform.GetComponent<TextMeshProUGUI>();
        Int = GameObject.Find("Int").transform.GetComponent<TextMeshProUGUI>();
        level = GameObject.Find("Level").transform.GetComponent<TextMeshProUGUI>();
        Str.text = "Str : "+playerState.str.ToString();
        Dex.text = "Dex : "+playerState.dex.ToString();
        Int.text = "Int : "+playerState.inte.ToString();
        oldTurn = 1;
    }

    void Update()
    {
        UpdateSlider();
        UpdateText();
    }
    public void SettingSlider()
    {
        if (playerHpSlider == null || playerMpSlider == null)
        {
            playerHpSlider = GameObject.Find("HpSlider").transform.GetComponent<Slider>();
            playerMpSlider = GameObject.Find("MpSlider").transform.GetComponent<Slider>();
        }
        playerMaxHp = playerState.MaxHp;
        playerMaxMp = playerState.playerMaxMp;
        oldPlayerHp = playerState.MaxHp;
        playerHpSlider.maxValue = playerMaxHp;
        playerHpSlider.value = playerState.Hp;
        hpString = playerState.Hp + " / " + playerMaxHp;
        playerHpText = playerHpSlider.transform.GetComponentInChildren<Text>();
        playerHpText.text = hpString;
        playerMpSlider.maxValue = playerMaxMp;
        playerMpSlider.value = PlayerState.playerMp;
        mpString = PlayerState.playerMp + " / " + playerMaxMp;
        playerMpText = playerMpSlider.transform.GetComponentInChildren<Text>();
        playerMpText.text = mpString;
    }

    void UpdateSlider()
    {
        if(oldPlayerMaxHp != playerState.MaxHp)
        {
            playerHpSlider.maxValue = playerState.MaxHp;
            playerMaxHp = playerState.MaxHp;
            oldPlayerMaxHp = playerState.MaxHp;
            hpString = playerState.Hp + " / " + playerMaxHp;
            playerHpText.text = hpString;
        }
        if (oldPlayerMaxMp != playerState.playerMaxMp)
        {
            playerMpSlider.maxValue = playerState.playerMaxMp;
            oldPlayerMaxHp = playerState.MaxHp;
            mpString = PlayerState.playerMp + " / " + playerMaxMp;
            playerMpText.text = mpString;
        }
        if (oldPlayerHp != playerState.Hp)
        {
            if (oldPlayerHp != playerState.Hp)
            {
                playerHpSlider.value = playerState.Hp;
                oldPlayerHp = playerState.Hp;
                hpString = playerState.Hp + " / " + playerMaxHp;
                playerHpText.text = hpString;
            }
        }
        if (oldPlayerMp != PlayerState.playerMp)
        {
            playerMpSlider.value = PlayerState.playerMp;
            playerMaxMp = playerState.playerMaxMp;
            oldPlayerMp = PlayerState.playerMp;
            mpString = PlayerState.playerMp + " / " + playerMaxMp;
            playerMpText.text = mpString;
        }
    }
    void UpdateText()
    {
        if(floor.text != dungeonName + GameManager.instance.nowFloor.ToString())
        {
            floor.text = dungeonName + GameManager.instance.nowFloor.ToString();
        }
        if (oldTurn != TurnManage.totallTurn)
        {
            oldTurn = TurnManage.totallTurn;
            Turn.text = oldTurn.ToString();
        }
        if (oldPlayerExp != PlayerState.currentExp)
        {
            Exp.text = "Exp : " + PlayerState.currentExp.ToString() +" / " + playerState.nextLevelExp.ToString();
            oldPlayerExp = PlayerState.currentExp;
        }
        if (oldPlayerLevel != playerState.level)
        {
            level.text = "Level : "+playerState.level.ToString();
            oldPlayerLevel = playerState.level;

        }
        
    }
    IEnumerator SettingName()
    {
        yield return new WaitForSeconds(0.1f);
        playerNameString = playerState.playerName;
        playerNameText.text = playerState.playerName;
    }
    
}
