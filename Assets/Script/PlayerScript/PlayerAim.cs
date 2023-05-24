using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    Transform myTr;
    float aimRange;
    public Vector3 targetPos;
    Vector3 missileMovePos;
    SpellUI spellUI;
    Magics magics;
    float endTime = 0;
    public GameObject playerMissile;
    bool isMove = false;
    GameObject player;
    List<GameObject> targetPath = new List<GameObject>();
    Inventory inveotry;
    int k = 0;
    public float time=0;
    private void Start()
    {
        myTr = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).transform;
        spellUI = GameObject.Find("SpellBook").transform.GetComponent<SpellUI>();
        magics = spellUI.gameObject.transform.GetComponent<Magics>();
        playerMissile = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        inveotry = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
    }
    void Update()
    {
        MoveAim();
        AimObjTurn();
    }
    void AimObjTurn()
    {
        if (myTr.gameObject.activeSelf!= PlayerState.isAiming)
        {
            myTr.gameObject.SetActive(PlayerState.isAiming);
        }
        if (!myTr.gameObject.activeSelf&&myTr.localPosition!=Vector3.zero)
        {
            myTr.localPosition = Vector3.zero;
            
        }
        if (!myTr.gameObject.activeSelf && targetPath.Count > 0)
        {
            for (int i = 0; i < targetPath.Count; i++)
            {
                targetPath[i].transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
            targetPath.Clear();
        }
    }
    void MoveAim()
    {
        if (PlayerState.isAiming)
        {
            PlayerState.isStoped = PlayerState.isAiming;
            if (Input.GetKeyDown("[1]"))
            {
                myTr.Translate(-1, -1, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[2]"))
            {
                myTr.Translate(0, -1, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[3]"))
            {
                myTr.Translate(1, -1, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[4]"))
            {
                myTr.Translate(-1, 0, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[5]"))
            {
                myTr.Translate(0, 0, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[6]"))
            {
                myTr.Translate(1, 0, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[7]"))
            {
                myTr.Translate(-1, 1, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[8]"))
            {
                myTr.Translate(0, 1, 0);
                AmimColorChange();
            }
            else if (Input.GetKeyDown("[9]"))
            {
                myTr.Translate(1, 1, 0);
                AmimColorChange();
            }
            
            EnterAimPos();
        }
    }
    public void AmimColorChange()
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = myTr.position;
        for(int i =0; i < targetPath.Count; i++)
        {
            targetPath[i].transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        targetPath.Clear();
        float dist = Vector2.Distance(startPos, endPos);
        foreach(RaycastHit2D hit in Physics2D.RaycastAll(startPos, myTr.localPosition, dist))
        {
            targetPath.Add(hit.transform.gameObject);
            hit.transform.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    public void EnterAimPos()
    {
        if ( (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return))&&time+0.2<=Time.time)
        {
            PlayerState.isStoped = false;
            PlayerState.isAiming = false;
            targetPos = myTr.position;
            if(spellUI.aimActive)
            {
                spellUI.targetPos = targetPos;
            }
            else if (inveotry.isThrow)
            {
                inveotry.ThrowItem();
            }
            myTr.gameObject.SetActive(false);
            Debug.Log("setActiveFalse");
            for (int i = 0; i < targetPath.Count; i++)
            {
                targetPath[i].transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
            targetPath.Clear();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            targetPos = Vector3.zero;
            myTr.localPosition = targetPos;
            spellUI.targetPos = targetPos;
            PlayerState.isAiming = false;
            PlayerState.isStoped = false;
            Debug.Log(targetPos);
            myTr.gameObject.SetActive(false);
            for (int i = 0; i < targetPath.Count; i++)
            {
                targetPath[i].transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
            targetPath.Clear();
        }

    }
    public GameObject ThrowFindTarget(Vector3 startPos, Vector3 targetPos)
    {
        GameObject newTarget = null;
        bool isOther = false;
        float dist = Vector2.Distance(startPos, targetPos);
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(startPos, (targetPos - startPos), dist))
        {
            if (hit.transform.tag == "Monster" || hit.transform.tag == "Wall")
            {
                newTarget = hit.transform.gameObject;
                isOther = true;
            }
            if (isOther)
            {
                newTarget = hit.transform.gameObject;
                Debug.Log(newTarget.name);
                return newTarget;
            }
        }
        if(newTarget.transform.tag == "Wall")//newTarget이 벽이라면 플레이어의 위치에서 newTartget의 방향 -1의 거리에
        {
            Vector3 tempPos = (newTarget.transform.position - startPos);
            Vector3 target = new Vector3((int)tempPos.x * 0.9f, (int)(tempPos.y * 0.9f));
            RaycastHit2D col = Physics2D.CircleCast(target, 0.4f,Vector3.zero);
            newTarget = col.transform.gameObject;
        }
        return newTarget;
    }
    public GameObject FindTarget(Vector3 startPos, Vector3 targetPos)
    {
        GameObject newTarget = null;
        bool isOther = false;
        float dist  = Vector2.Distance(startPos, targetPos);

        foreach (RaycastHit2D hit in Physics2D.RaycastAll(startPos, (targetPos - startPos), dist))
        {
            if (hit.transform.tag == "Monster" || hit.transform.tag == "Wall")
            {
                isOther = true;
            }
            if (isOther)
            {
                newTarget = hit.transform.gameObject;
                Debug.Log(newTarget.name);
                return newTarget;
            }
        }

        return newTarget;
    }
    public void MoveMissile(Vector3 TargetPos)
    {
        playerMissile.transform.position = player.transform.position;
        missileMovePos = (TargetPos-playerMissile.transform.position)/10;
        if (k < 10&&playerMissile.activeSelf)
        {
            InvokeRepeating("Missile", 0, 0.01f);
        }
        if (k >= 10)
        {
            CancelInvoke("Missile");
            playerMissile.SetActive(false);
            k = 0;
        }
    }
    void Missile()
    {
        playerMissile.transform.position += missileMovePos;
        k++;
        if (k >= 10)
        {
            CancelInvoke("Missile");
            playerMissile.SetActive(false);
        }
    }
}
