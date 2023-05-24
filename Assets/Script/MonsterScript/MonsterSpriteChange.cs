using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpriteChange : MonoBehaviour
{
    string monsterName;
    Sprite sprite;
    string dataPath;
    Animator animator;
    RuntimeAnimatorController controller;
    void Start()
    {
        monsterName = this.gameObject.transform.GetComponent<MonsterState>().monsterName;
        sprite = this.gameObject.transform.GetComponent<SpriteRenderer>().sprite;
        dataPath = "AnimSprite\\Monsters\\" + monsterName + "\\Controller";
        animator = GetComponent<Animator>();
        GetAnimator(monsterName,dataPath);
        animator.Play("Idle");
    }

    void GetAnimator(string Name, string DataPath)
    {
        controller = Resources.Load<RuntimeAnimatorController>(dataPath);
        animator.runtimeAnimatorController = controller;
    }

}
