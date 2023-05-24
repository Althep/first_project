using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class TuneEquipSlot : MonoBehaviour
{
    int slotCount=9;

    private void Start()
    {
    }
    public List <char> StringToList(string equipSlots)
    {
        List<char> numberDetach = new List<char>();
        char tempchar;
        char[] tempArray;
        int temp=0;
        foreach (char s in equipSlots)
        {
            numberDetach.Add(s);
        }
        tempArray = new char[numberDetach.Count];
        for (int i = 0; i<numberDetach.Count; i++)
        {
            tempchar = numberDetach[i];
            tempArray[i] = tempchar;
        }
        if (numberDetach.Count < slotCount)
        {
            temp = slotCount-numberDetach.Count;
        }
        numberDetach.Clear();
        for (int i = 0; i < temp; i++)
        {
            numberDetach.Add('0');
        }
        for(int i = 0; i < tempArray.Length; i++)
        {
            numberDetach.Add((char)tempArray[i]);
        }
        return numberDetach;
    }
    
}
