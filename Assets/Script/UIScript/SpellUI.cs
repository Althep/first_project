using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    public GameObject scrollView;
    OnKey canMove;
    PlayerAim playerAimScript;
    public Scrollbar scrollbar;
    [SerializeField]public MagicData[] MagicSlots = new MagicData[20];
    Magics magicScript;
    public Vector3 targetPos;
    public GameObject contentsParent;
    public GameObject sellectImage;
    public GameObject player;
    int numberOfChilds;
    int oldSlotNumber = 0;
    int changedSlotNumber = 0;
    public int slotNumber;
    public bool aimActive;
    GameObject playerAim;
    TurnManage myTurnManage;
    PlayerAim aimScript;
    private void Start()
    {
        canMove = GameObject.FindWithTag("Player").transform.GetComponent<OnKey>();
        numberOfChilds = contentsParent.transform.childCount;
        slotNumber = 0;
        playerAim = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
        aimActive = false;
        playerAimScript= playerAim.transform.GetComponent<PlayerAim>();
        player = GameObject.FindGameObjectWithTag("Player");
        magicScript = this.transform.GetComponent<Magics>();
        aimScript = player.transform.GetComponent<PlayerAim>();
        myTurnManage = GameObject.FindGameObjectWithTag("GameManager").transform.GetComponent<TurnManage>();
    }

    private void Update()
    {
        OnkeyScrollView();
        OnkeySelect();
        ConfirmAndCancleMagic();
        
    }

    public void ScrollViewButton()
    {
        if (scrollView.activeSelf)
        {
            scrollView.SetActive(false);
            PlayerState.isStoped = false;
            PlayerState.isCast = false;
        }
        else if (!scrollView.activeSelf)
        {
            scrollView.SetActive(true);
            PlayerState.isStoped = true;
            PlayerState.isCast = true;
        }
    }

    void OnkeyScrollView()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ScrollViewButton();
            
        }
    }

    void OnkeySelect()
    {
        if (scrollView.activeSelf&&PlayerState.isCast == true)
        {
            PlayerState.isStoped = true;
            if (Input.GetKeyDown("[6]"))
            {
                oldSlotNumber = slotNumber;
                slotNumber++;
            }
            else if (Input.GetKeyDown("[4]"))
            {
                oldSlotNumber = slotNumber;
                slotNumber--;
            }
            else if (Input.GetKeyDown("[8]"))
            {
                oldSlotNumber = slotNumber;
                slotNumber -= 2;
            }
            else if (Input.GetKeyDown("[2]"))
            {
                oldSlotNumber = slotNumber;
                slotNumber += 2;
            }
            if (slotNumber != changedSlotNumber)
            {
                if (slotNumber <= 0)
                {
                    slotNumber = 0;
                }
                else if (slotNumber >= numberOfChilds - 1)
                {
                    slotNumber = numberOfChilds - 1;
                }
                sellectImage.transform.SetParent(contentsParent.transform.GetChild(slotNumber));
                sellectImage.transform.localPosition = new Vector3(0, 0, 0);
                ScrollVarPositionChange();
                changedSlotNumber = slotNumber;
            }

        }
    }
    void ScrollVarPositionChange()
    {
        scrollbar.value = (10 - slotNumber / 2) * 0.1f;
        if (scrollbar.value <= 0.2)
        {
            scrollbar.value = 0;
        }

    }
    void ConfirmAndCancleMagic()
    {
        if (scrollView.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (MagicSlots[slotNumber] != null)
                {
                    if (PlayerState.playerMp >= MagicSlots[slotNumber].requireMp)
                    {
                        PlayerState.isAiming = true;
                        PlayerState.isCast = true;
                        aimScript.time = Time.time;
                        scrollView.SetActive(false);
                        aimActive = true;
                    }
                    else
                    {
                        Dialog.instance.color = Color.white;
                        
                        Dialog.instance.UpdateDialog("You don't have enough MP");
                        PlayerState.isCast = false;
                    }
                }
                else
                {
                    Debug.Log("didn't exist");
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                scrollView.SetActive(false);
                PlayerState.isStoped = false;
            }
        }
        AimMagic(MagicSlots[slotNumber]);
    }
    void AimMagic(MagicData magicdata)
    {
        if (aimActive&&PlayerState.isCast == true)
        {
            Debug.Log("aa");
            if (Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("done!");
                targetPos = playerAim.transform.position;
                aimActive = false;
                switch (magicdata.targetType)
                {
                    case TargetType.target:
                        if(magicdata.missileType == MissileType.missile)
                        {
                            magicScript.TargetMisileMagic(targetPos, player.transform.position, MagicSlots[slotNumber]);
                            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerCastSpeed_));
                            PlayerState.isCast = false;
                        }
                        break;
                    case TargetType.notarget:
                        PlayerState.isCast = false;
                        break;
                    default:
                        PlayerState.isCast = false;
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                aimActive = false;
                PlayerState.isCast = false;
            }
            
        }

    }
}