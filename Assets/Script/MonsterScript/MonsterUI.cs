using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    MonsterState monsterState;
    public TextMeshProUGUI monsterName;
    public Slider slider;
    [SerializeField] Camera cam;
    int oldHp;
    int oldLayer;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        monsterState = this.GetComponent<MonsterState>();
        monsterName.text = monsterState.monsterName;
        cam = GameObject.Find("MainCamera").transform.GetComponent<Camera>();
        canvas = this.transform.GetChild(1).transform.GetComponent<Canvas>();
        canvas.worldCamera = cam;
        slider.fillRect.GetComponent<Image>().color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlider();
        LayerChange();
    }
    void UpdateSlider()
    {
        if (oldHp != monsterState.Hp)
        {
            float temp = (float)monsterState.Hp / (float)monsterState.MaxHp;
            if (temp <= 0.25f)
            {
                slider.value = 0.25f;
                slider.fillRect.GetComponent<Image>().color = Color.red;
            }
            else if (temp <= 0.5f)
            {
                slider.value = 0.5f;
                slider.fillRect.GetComponent<Image>().color = Color.yellow;
            }
            else if (temp <= 0.75f)
            {
                slider.value = 0.75f;
                slider.fillRect.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                slider.value = 1f;
                slider.fillRect.GetComponent<Image>().color = Color.green;
            }
            oldHp = monsterState.Hp;
        }

    }
    void LayerChange()
    {
        if (oldLayer != this.gameObject.layer)
        {
            canvas.gameObject.layer = this.gameObject.layer;
            oldLayer = this.gameObject.layer;
            if (canvas.gameObject.layer != 3)
            {
                canvas.gameObject.SetActive(false);
            }
            else
            {
                canvas.gameObject.SetActive(true);
            }
        }
            
    }
}
