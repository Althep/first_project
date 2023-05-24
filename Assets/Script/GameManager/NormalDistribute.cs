using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Runtime.Serialization;

public class Data
{
    public float rate;
}



public class NormalDistribute : MonoBehaviour
{
    GameObject go;
    List<int> tierList = new List<int>();
    float data;
    float sumRate;
    int tiers = 6;
    int rate = 0;
    int tier = 0;
    public List<float> equipDatas = new List<float>();
    public List<float> consumDatas = new List<float>();
    public List<float> monsterDatas = new List<float>();
    private void Start()
    {
        for (int i = 1; i < tiers + 1; i++)
        {
            tierList.Add(i);
        }
        NormalDist("equip");
        NormalDist("monster");
        NormalDist("consum");
    }

    public void NormalDist(string kind)
    {
        float mu;//평균
        float sigma;//표준편차
        int floor;
        floor = GameManager.instance.nowFloor;
        if (kind == "equip")
        {
            mu = Mathf.Log(floor, 3f);
            sigma = Mathf.Log(floor + 1, 5) + 1f;
            rateValuePrint(mu, sigma,kind);
        }
        else if (kind == "monster")
        {
            mu = Mathf.Log(floor, 2f);//평균
            sigma = Mathf.Log(floor + 1, 10);//표준편차
            rateValuePrint(mu, sigma,kind);
        }
        else if( kind == "consum")
        {
            mu = Mathf.Log(floor, 4f);
            sigma = Mathf.Log(floor + 1, 4) + 1.07f;
            rateValuePrint(mu, sigma, kind);
        }
    }
    public int MakeRate(List<float> RateDataList)
    {

        int sumRate=0;
        float randomCount = 0;
        int sumall=0;
        for(int i =0; i < RateDataList.Count; i++)
        {
            sumall += (int)RateDataList[i];
        }
        randomCount = UnityEngine.Random.Range(0, sumall);
        for (int i = 0; i < tierList.Count; i++)
        {
            sumRate += (int)RateDataList[i];
            if (randomCount <= sumRate)
            {
                tier = i+1;
                break;
            }
        }
        return tier;
    }

    //티어와 층계에 따라 함수 그래프 변환
    void rateValuePrint(float mu, float sigma, string kind)
    {
        float rateValue = 0;
        float temp;
        float temp1;
        float temp2;
        float sumrate=0;
        List<float> tempList = new List<float>();
        for (int j = 0; j < tierList.Count; j++)
        {
            rateValue = 1;
            temp = 1 / (sigma * math.pow((math.PI * 2), 1 / 2));
            temp2 = -math.pow((tierList[j] - mu), 2) / (2 * math.pow(sigma, 2));
            temp1 = math.pow(math.E, temp2);
            rateValue = temp1 / (temp * sigma);
            tempList.Add(rateValue);
            sumrate += rateValue;
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            if (kind == "equip")
                equipDatas.Add((tempList[i] / sumrate) * 10000);
            else if (kind == "monster")
                monsterDatas.Add((tempList[i] / sumrate) * 1000);
            else if( kind == "consum")
                consumDatas.Add((tempList[i] / sumrate) * 1000);
        }
    }
}
