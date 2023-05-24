# first_project
유니티2d 로그라이크 프로젝트

<H2>개발배경</H2>

2D 프로젝트에 로그라이크 장르가 그나마 혼자서 개발할 수 있을만한 규모이고, 거기에 사용되는
절차적 생성에 매력을 느껴 직접 개발해보고싶어 개발을 시작하게됐습니다.

이동 : 키패드 숫자
승인키 : 엔터
취소키 : ESC
인벤토리 : I
마법 : Z

<h2>맵생성</h2>

맵생성은 FieldManager 에서 관장하고있습니다
텅 빈 x,y좌표계를 생성 한 후
중앙의 30~70%정도의 좌표계에서 절반으로 나누는것을 
반복 해 방을 나눠줍니다
```C#
public void Divide(int x1, int x2, int y1, int y2, int n, int dungeonSize)
    {
        n++;
        if (n < 9)
        {
            if ((x2 - x1) >= (y2 - y1))
            {//세로분할
                int Drate = (int)(Random.Range(30, 70) * (x1 + x2) / 100);//(x2-x1)/2*()
                n++;
                Drate = RerollMakeWall(x1, x2, Drate, dungeonSize);
                for (int y = y1; y < y2; y++)
                {
                    wallPosList.Add(new Vector2(Drate, y));
                    emptyPosList.Remove(new Vector2(Drate, y));
                }
                if (x2 - x1 > 3 && y2 - y1 > 3)
                {
                    if (n == 8)
                    {
                        BSPNode corNode = new BSPNode(x1, Drate, y1, y2, n);
                        nodeList.Add(corNode);
                        MakeDoorVirt(Drate, y1, y2);
                    }
                    Divide(x1, Drate, y1, y2, n, dungeonSize);
                }
                if (x2 - Drate > 3 && y2 - y1 > 3)
                {
                    if (n == 8)
                    {
                        BSPNode corNode = new BSPNode(Drate, x2, y1, y2, n);
                        nodeList.Add(corNode);
                    }
                    Divide(Drate, x2, y1, y2, n, dungeonSize);
                }

            }
            else
            {//가로분할
                int Drate = (int)(Random.Range(30, 70) * (y1 + y2) / 100);
                n++;
                Drate = RerollMakeWall(y1, y2, Drate, dungeonSize);
                for (int x = x1; x < x2; x++)
                {
                    wallPosList.Add(new Vector2(x, Drate));
                    emptyPosList.Remove(new Vector2(x, Drate));
                }
                if ((x2 - x1 > 3) && (y2 - y1 > 3))
                {
                    if (n == 8)
                    {
                        BSPNode corNode = new BSPNode(x1, x2, y1, Drate, n);
                        nodeList.Add(corNode);
                        MakeDoorHori(x1, x2, Drate);
                    }
                    Divide(x1, x2, y1, Drate, n, dungeonSize);
                }
                if ((x2 - x1 > 3) && (y2 - y1 > 3))
                {
                    if (n == 8)
                    {
                        BSPNode corNode = new BSPNode(x1, x2, Drate, y2, n);
                        nodeList.Add(corNode);
                    }
                    Divide(x1, x2, Drate, y2, n, dungeonSize);
                }
            }
        }

    }
```
    
나누며 나눠진곳의 좌표를 기억해두고 각 벽에 문을 생성, 빈곳중 문 근처가 아닌곳들에 벽을 생성 해 미궁을 만들어줍니다.
마지막으로 각각 이전층과 연결되어 각 번호끼리 연결돼있는 계단을 만들어 위/아래층을 왔다갔다 할 수 있게 하되
같은 계단으로 내려가면 항상 같은곳으로 내려가지게 만듭니다.
이를 방문 하지 않은층이라면 생성을 해 반복하고 그게 아니라면 Json형식으로 각 오브젝트의 좌표를 저장 해 불러옵니다.
   
이후 빈 자리에 몬스터와 아이템을 생성합니다</br>
50%의 확률로 소모품/장비중 결정됩니다.
   
  
  
  
  
<h2>아이템 데이터</h2>


```C#
    void SpecificConsumable()
    {
        int SC = Random.Range(0, 100);
        if (SC > 50)
        {
            itemState.tier = normalDist.MakeRate(normalDist.consumDatas);
            itemState.itemType = ItemType.Consumable;
            GetItemRandomId("Consum");
        }
        else
        {
            itemState.tier = normalDist.MakeRate(normalDist.equipDatas);
            itemType = ItemType.Equipment;
            GetItemRandomId("Equip");
        }


        SpriteCheck();
        
    }
```
   
   아이템은 각 층에 영향을 받는 정규분포를 따르는 등급을 받게되며
   그에 따라 id를 부여받습니다
   
   id는 사전에 장비/소모품 데이터파일에 넣어뒀으며 이 id에 따라 어떤 아이템인지 결정합니다
   밑의 코드는 아이템의 종류별/티어별로 ID값을 가져와 저장하는 과정입니다
   
```C#
   void GetItemRandomId(string itemtype)
    {
        //IDList IdList = new IDList();
        //string fileName = itemtype + "IDList" + itemState.tier;
        //string dataPath = Application.dataPath + "/Save/" + fileName + ".Json";
        //string data = File.ReadAllText(dataPath);
        //IdList = JsonUtility.FromJson<IDList>(data);
        //int temp = Random.Range(0, IdList.idList.Count);
        //GetItemData(IdList.idList[temp],itemtype,itemState);
        IDList idList = new IDList();
        List<Dictionary<string, object>> idValues;
        if (itemtype == "Equip")
        {
            idValues = dataRead.equipItemDatas;
            for (int i = 0; i < idValues.Count; i++)
            {
                if (itemState.tier == int.Parse(idValues[i]["tier"].ToString()))
                {
                    idList.idList.Add(int.Parse(idValues[i]["id"].ToString()));
                }
            }
        }
        else if (itemtype == "Consum")
        {
            idValues = dataRead.consumItemDatas;
            for(int i =0; i < idValues.Count; i++)
            {
                if(itemState.tier == int.Parse(idValues[i]["tier"].ToString()))
                {
                    idList.idList.Add(int.Parse(idValues[i]["id"].ToString()));
                }
            }
        }
        int temp = Random.Range(0, idList.idList.Count);
        GetItemData(idList.idList[temp], itemtype, itemState);
    }
```
각 아이템의 데이터는 ItemDataRead 스크립트의 함수로 값을 집어넣어줍니다
이때 각각 장비의 종류등은 string값으로 들어오는것을 StringToInt 스크립트에서 변환하여 
enum형식으로 데이터를 넣습니다.
이후 대부분의 enum형식으로 지정된 데이터들은 장비와 같은 과정으로 변환되어 저장됩니다.
```C#
    public void EquipDataRead(string itemType)
    {
        string file = itemType + "ItemData";
        if (itemType == "Equip")
        {
            equipItemDatas = CSVReader.Read(file);
            for (int i = 0; i < equipItemDatas.Count; i++)
            {
                itemState = new ItemState();
                itemState.id = (int)equipItemDatas[i]["id"];
                itemState.itemName = equipItemDatas[i]["name"].ToString();
                itemState.itemType = (ItemType)((int)StringToInt.TypeStringToInt(equipItemDatas[i]["itemtype"].ToString(), "ItemType"));
                itemState.equipType = (EquipType)(StringToInt.TypeStringToInt(equipItemDatas[i]["equiptype"].ToString(), "EquipType"));
                itemState.weaponType = (WeaponType)(StringToInt.TypeStringToInt(equipItemDatas[i]["weapontype"].ToString(), "WeaponType"));
                itemState.armorType = (ArmorType)(StringToInt.TypeStringToInt(equipItemDatas[i]["armortype"].ToString(), "ArmorType"));
                itemState.equipSlot = equipItemDatas[i]["slot"].ToString();
                itemState.damage = (int)equipItemDatas[i]["damage"];
                itemState.def = (int)equipItemDatas[i]["defense"];
                itemState.range = (int)equipItemDatas[i]["range"];
                itemState.attSpeed = float.Parse(equipItemDatas[i]["attspeed"].ToString());
                itemState.tier = (int)equipItemDatas[i]["tier"];
                itemState.option=(equipItemDatas[i]["option"].ToString());
                itemState.consumKind = ConsumKind.None;
                itemState.conditionType = ConditionType.none;
                itemState.conditionName = "None";
                itemState.panalty = (int)equipItemDatas[i]["panalty"];
                itemState.sh = (int)equipItemDatas[i]["sh"];
                itemState.weight =float.Parse(equipItemDatas[i]["weight"].ToString());
                itemState.acc = (int)equipItemDatas[i]["acc"];
                itemState.damageReduce = (int)equipItemDatas[i]["damreduce"];
                if (itemType == "Equip")
                    equipItemDataList.Add(itemState);
                else if (itemType == "Consum")
                    consumItemDataList.Add(itemState);
            }
        }
```
장비의 경우 착용 슬롯을 어떻게 하는가 고민을 하다 우연히 다른 게임의 DB를 찾게되어 보던 와중
슬롯을 2진수로 나타낸것을 보고 영향을 받아 슬롯을 2의 배수로 표현하게되었습니다.
이를 더하고 빼는 과정을 위해 TuneEquipSlot에서 변환합니다.
```C#
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
```
    
소모품의 특수기능 (힐링 순간이동등)은 엑셀파일에서 미리 결정해둔 Fucntion의 string형식의 값에 따라 바뀌게 되며
예를들어 상처 치유 포션같은경우는 Fuction값에 HealingWonds를 입력해뒀기 때문에 Fuction스크립트의 
HealingWonds함수를 가져오게됩니다.
```C#
    public void HealingWounds()
    {
        int healAmount = 20;
        if (playerState.Hp + healAmount <= playerState.playerMaxMp)
        {
            playerState.Hp += healAmount;
        }
        else
        {
            playerState.Hp = playerState.MaxHp;
        }
    }
```



<h2>시야시스템</h2>

2D로 진행하다보니 시야가 꽤 큰 고민이었습니다만 LayCast와 오브젝트의 Layer를 변경하는 식으로 해결 했습니다.
이는 IsinSight스크립트에서 처리합니다.

플레이어에게 시야값을 대신할 서클콜라이더를 만들고 이에 들어오거나 나갈 때 처리합니다.

```C#
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CastRay();
            oldPlayerPos = transform.position;
            if (this.transform.tag == "Monster" && oldPos != this.transform.position)
            {
                CastRay();
                oldPos = transform.position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inSight = false;
            isRayCasted = false;
            ChangeLayer();
            ChangeColor();
            if (this.transform.tag == "Monster" && oldPos != this.transform.position)
            {
                CastRay();
                oldPos = transform.position;
            }
        }

    }
```
