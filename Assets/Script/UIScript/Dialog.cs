using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject dialog;
    public Scrollbar scrollbar;
    [SerializeField]TextMeshProUGUI dialogTextMeshPro;
    public static Dialog instance;
    int childnumber;
    public Color color;
    void Start()
    {
        instance = this;
    }
    
    public void UpdateDialog(string dialogText)
    {
        GameObject go;
        dialogTextMeshPro = dialog.GetComponent<TextMeshProUGUI>();
        this.dialogTextMeshPro.text = dialogText;
        dialogTextMeshPro.color = color;
        go = Instantiate(dialog);
        go.transform.SetParent(transform.GetChild(0).transform.GetChild(0));
        go.name = "newText";
        scrollbar.value = 0;
    }
    public string DialogMonsterHp(MonsterState monsterState)
    {
        string text = "text didn't input";
        float temp;
        temp = (float)monsterState.Hp / monsterState.MaxHp;
        if (temp >= 0.8f)
        {
            text = monsterState.name + " is " + "lightley wound";
            color = Color.white;
        }
        else if (temp <= 0.8 && temp >= 0.5f)
        {
            text = monsterState.name + " is " + "have wound";
            color = Color.white;
        }
        else if (temp > 0.2f)
        {
            text = monsterState.name + " heavily wound";
            color = Color.yellow;
        }
        else if (temp <= 0.2f)
        {
            text = monsterState.name + " is " + "almost dying";
            color = Color.red;
        }

        return text;
    }
}
