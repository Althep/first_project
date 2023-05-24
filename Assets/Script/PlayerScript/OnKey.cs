using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class OnKey : MonoBehaviour
{
    public bool canMove = false;
    public bool canAttack;
    Transform myTr;
    Vector3 targetPos = Vector3.zero;
    TurnManage myTurnManage;
    [SerializeField] GameObject monsterObj;
    [SerializeField] List<Collider2D> col;
    PlayerState playerState;
    Dialog dialog;
    Color color;
    // Start is called before the first frame update
    private void Start()
    {
        targetPos = this.transform.position;
        myTr = this.transform;
        myTurnManage = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TurnManage>();
        dialog = GameObject.FindObjectOfType<Dialog>();
        playerState = this.gameObject.GetComponent<PlayerState>();
    }

    private void Update()
    {
        if (!PlayerState.isAiming && !PlayerState.isStoped)
            StartCoroutine(GetTargetPos());

    }
    IEnumerator GetTargetPos()
    {
        if (Input.GetKeyDown("[1]"))
        {
            targetPos = new Vector3(this.transform.position.x - 1, this.transform.position.y - 1, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[2]"))
        {
            targetPos = new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[3]"))
        {
            targetPos = new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[4]"))
        {
            targetPos = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[5]"))
        {
            targetPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[6]"))
        {
            targetPos = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[7]"))
        {
            targetPos = new Vector3(this.transform.position.x - 1, this.transform.position.y + 1, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[8]"))
        {
            targetPos = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        else if (Input.GetKeyDown("[9]"))
        {
            targetPos = new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z);
            MakeCollider();
            TurnManage.totallTurn += 1;
            StartCoroutine(myTurnManage.MonsterAct(PlayerState.playerMoveSpeed_));
        }
        canMove = true;
        yield return null;
    }
    void MakeCollider()
    {
        col = new List<Collider2D>();
        col.AddRange(Physics2D.OverlapCircleAll(targetPos, 0.4f));
        if (col.Count > 1)
        {
            for (int i = 0; i < col.Count; i++)
            {
                if (col[i].gameObject.tag == "Wall")
                {
                    canMove = false;
                }
                else if (col[i].gameObject.tag == "Monster")
                {
                    canMove = false;
                    canAttack = true;
                    monsterObj = col[i].gameObject;
                }

            }
        }
        if (canAttack)
        {
            targetPos = transform.position;
            AttackMonster(monsterObj);
            canAttack = false;
            col.Clear();
        }
        else if (canMove)
        {
            PlayerMove();
            col.Clear();
        }

    }
    void PlayerMove()
    {
        Vector3 movePos = targetPos - myTr.position;

        myTr.Translate(movePos);
    }
    void AttackMonster(GameObject monsterObj)
    {
        MonsterState myMonsterState = monsterObj.transform.GetComponent<MonsterState>();
        if (playerState.IsHit(playerState, myMonsterState))
        {
            myMonsterState.Hp -= playerState.HitDamage(playerState.AttackDamage(), myMonsterState);
            dialog.color = Color.white;
            dialog.UpdateDialog(dialog.DialogMonsterHp(myMonsterState));
        }
        else
        {
            dialog.color = Color.white;
            Dialog.instance.UpdateDialog("You try to attack "+myMonsterState.monsterName+" but missed");
        }
    }

    
}

