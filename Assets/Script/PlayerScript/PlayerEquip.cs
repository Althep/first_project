using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    public bool Weaphon = false;
    public bool Armor = false;
    public bool Glove = false;
    public bool Shield = false;
    public bool Helm = false;
    public bool Shoes = false;
    public bool leftRing = false;
    public bool rightRing=false;
    public bool Amulet=false;
    public List<GameObject> equipedItems = new List<GameObject>();
    public GameObject rightRingobj;
    public GameObject leftRingObj;
}
