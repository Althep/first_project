using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetMagic : MonoBehaviour
{
    int magicId;
    int slotNumer;
    SpellUI spellUI;
    Magics magics;
    MagicData magicData;
    GameObject spellScrollView;
    GameObject parentContents;
    GameObject contents;
    GameObject scrollView;
    Image contentsImage;
    TextMeshProUGUI contentsName;
    TextMeshProUGUI contentsDescription;
    TextMeshProUGUI contentsRequire;
    Sprite sprite;
    string dataPath;
    private void Start()
    {
        magicData = new MagicData();
        spellUI = GetComponent<SpellUI>();
        scrollView = spellUI.scrollView;
        magics = GetComponent<Magics>();
        slotNumer = spellUI.MagicSlots.Length;
        magicId = 1;
        magics.GetMagicData(magicData, magicId);
        slotNumer = 0;
        spellUI.MagicSlots[slotNumer] = magicData;
        Debug.Log(magicData);
        parentContents = scrollView.gameObject.transform.GetComponentInChildren<ContentSizeFitter>().gameObject;
        ContentsChange(1);
    }

    void ContentsChange(int i)
    {
        contents = parentContents.transform.GetChild(slotNumer).gameObject;
        contentsImage = contents.transform.GetChild(0).GetComponent<Image>();
        contentsName = contents.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        contentsRequire = contents.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        contentsDescription = contents.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        contentsName.text = spellUI.MagicSlots[slotNumer].name;
        contentsRequire.text = "Mp : "+spellUI.MagicSlots[slotNumer].requireMp.ToString()+ " SuccesRate : ";
        contentsDescription.text = spellUI.MagicSlots[slotNumer].description.ToString();
        dataPath = "AnimSprite\\Magics\\" + contentsName.text + "\\sprite";
        GetSprite(contentsName.text, dataPath);
        contentsImage.sprite = sprite;

    }
    public void GetSprite(string Name, string DataPath)
    {
        contentsImage.sprite = Resources.Load<Sprite>(dataPath);
        sprite = Resources.Load<Sprite>(dataPath);
    }
}
