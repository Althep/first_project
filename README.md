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
   
