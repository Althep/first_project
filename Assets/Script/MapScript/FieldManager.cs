using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public enum TileThem
{
    Tile_0,
    Tile_1,
    Tile_2,
}

public enum StructType
{
    Wall,
    Floor,
    StairUp,
    StairDown,
    Door
}
public enum DungeonTheme
{
    cave,
    woods,
    sea,
    residential
}
public class FloorPos
{
    public List<StructType> type = new List<StructType>();
    public List<Vector3> Pos = new List<Vector3>();
    public List<bool> visited = new List<bool>();
    public List<int> upStairNumber = new List<int>();
    public List<int> downStairNumber = new List<int>();
    public List<int> spriteNumber = new List<int>();
}

class BSPNode
{
    public int x1, x2, y1, y2, drate, n, divideDirection;

    public BSPNode(int x1_, int x2_, int y1_, int y2_, int n_)
    {
        this.x1 = x1_;
        this.x2 = x2_;
        this.y1 = y1_;
        this.y2 = y2_;
        this.n = n_;
    }
}



public class FieldManager : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject go;
    public int dungeonSize;
    int wallErasePoint = 20;
    int wallMakePoint = 30;
    public Vector2[] stairUp_POS = new Vector2[3];
    public Vector2[] stairDown_POS = new Vector2[3];
    public bool isDoorClose = true;
    GameManager gameManager;
    public Sprite WallSprite;
    public Sprite DoorSprite;
    public MonsterManager monsterManager;
    public SpawnManager spawnManager;
    public GameObject[] wallObjArray;
    public GameObject[] doorObjArray;
    public GameObject[] tileObjArray;
    public GameObject[] stairUpObjArray;
    public GameObject[] stairDownObjArray;
    public GameObject gameManagerPrepab;
    public List<GameObject> upStairList = new List<GameObject>();
    public List<GameObject> downStairList = new List<GameObject>();
    [SerializeField]DungeonTheme dungeonTheme;

    public GameObject[] itemList;
    [SerializeField] GameObject[] monsterList;
    List<BSPNode> nodeList = new List<BSPNode>();
    public List<Vector2> emptyPosList = new List<Vector2>();
    List<Vector2> wallPosList = new List<Vector2>();
    List<Vector2> doorPosList = new List<Vector2>();
    List<GameObject> wallObjList = new List<GameObject>();
    List<GameObject> tileObjList = new List<GameObject>();
    List<GameObject> doorObjList = new List<GameObject>();
    public int RerollMakeWall(int x1, int x2, int Drate, int dungeonSize)
    {
        while (Drate > dungeonSize - 3 || Drate > dungeonSize - 3 || Drate < 3 || x2 - Drate < 3 || Drate - x1 < 3)
        {
            Drate = (int)(Random.Range(30, 70) * (x1 + x2) / 100);
        }
        return Drate;
    }
    public void MakeDoorVirt(int x, int y1, int y2)
    {
        int randomy = Random.Range(y1, y2);
        wallPosList.Remove(new Vector2(x, randomy));
        doorPosList.Add(new Vector2(x, randomy));
        emptyPosList.Remove(new Vector2(x, randomy));
    }
    public void MakeDoorHori(int x1, int x2, int y)
    {
        int randomx = Random.Range(x1, x2);
        wallPosList.Remove(new Vector2(randomx, y));
        doorPosList.Add(new Vector2(randomx, y));
        emptyPosList.Remove(new Vector2(randomx, y));
    }
    public void TwistDungeon(int dungeonSize)
    {
        //기존 생성 벽지우기
        for (int i = 0; i < wallPosList.Count; i++)
        {
            int point = Random.Range(0, 100);
            if (point < wallErasePoint)
            {
                wallPosList.RemoveAt(i);
            }
        }
        //벽 생성

        for (int i = 0; i < emptyPosList.Count; i++)
        {
            int randomNum = Random.Range(0, 100);
            if (randomNum < wallMakePoint)
            {
                wallPosList.Add(emptyPosList[i]);
                emptyPosList.RemoveAt(i);
            }
        }

    }
    #region BSPseperate
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
    
    #endregion
    public void GenerateDungeon()
    {

        int n = 0;
        int dsize = Random.Range(3, 6);
        GameObject go;
        dungeonSize = dsize * 10;
        for (int y = 0; y < dungeonSize; y++)
        {
            for (int x = 0; x < dungeonSize; x++)
            {
                emptyPosList.Add(new Vector2(x, y));
                go=Instantiate(tileObjArray[0], new Vector3(x, y, 2), Quaternion.identity);
                tileObjList.Add(go);
            }
        }

        Divide(0, dungeonSize, 0, dungeonSize, n, dungeonSize);
        TwistDungeon(dungeonSize);
        for (int i = 0; i < wallPosList.Count; i++)
        {
            go=Instantiate(wallObjArray[0], wallPosList[i], Quaternion.identity);
            wallObjList.Add(go);
            //floorSaveData.list.Add(floorData);
            //tempNum ++;
        }
        for (int i = 0; i < doorPosList.Count; i++)
        {
            go = Instantiate(doorObjArray[0], doorPosList[i], Quaternion.identity);
            doorObjList.Add(go);
        }
        //던전의 끝부분 생성
        for (int i = -1; i < dungeonSize + 1; i++)
        {
            go = Instantiate(wallObjArray[0], new Vector3(i, -1, 0), Quaternion.identity);
            wallObjList.Add(go);
            go = Instantiate(wallObjArray[0], new Vector3(i, dungeonSize, 0), Quaternion.identity);
            wallObjList.Add(go);
        }
        for (int j = -1; j < dungeonSize + 1; j++)
        {
            go = Instantiate(wallObjArray[0], new Vector3(-1, j, 0), Quaternion.identity);
            wallObjList.Add(go);
            go = Instantiate(wallObjArray[0], new Vector3(dungeonSize, j, 0), Quaternion.identity);
            wallObjList.Add(go);
        }
        MakeStairsUp();
        MakeStairsDown();
    }
    #region RememberEmpty

    #endregion
    void MakeStairsUp()
    {
        for (int i = 0; i < 3; i++)
        {

            int RandomPos = Random.Range(0, emptyPosList.Count);
            stairUp_POS[i] = emptyPosList[RandomPos];
            upStairList.Add(Instantiate(stairUpObjArray[0], stairUp_POS[i], Quaternion.identity));
            emptyPosList.RemoveAt(RandomPos);
            if (spawnManager == null)
            {
                spawnManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnManager>();
            }
            spawnManager.PlayerSpawned();
        }

    }
    void MakeStairsDown()
    {
        for (int i = 0; i < 3; i++)
        {
            int rCorr = Random.Range(0, emptyPosList.Count);
            stairDown_POS[i] = emptyPosList[rCorr];
            downStairList.Add(Instantiate(stairDownObjArray[0], stairDown_POS[i], Quaternion.identity));
            emptyPosList.RemoveAt((int)rCorr);

        }
    }
    public void ItemSpawn()
    {
        GameObject go;
        int isItemSpawn;
        int randomSpawnPointEmpty;
        for (int i = 0; i < emptyPosList.Count; i++)
        {
            isItemSpawn = Random.Range(0, 1000);
            if (isItemSpawn >= 970)
            {
                randomSpawnPointEmpty = Random.Range(0, emptyPosList.Count);
                go = Instantiate(itemList[0], emptyPosList[randomSpawnPointEmpty], Quaternion.identity);
                spawnManager.itemList.Add(go);
            }
        }
    }
    public void MakeNumbersStair()
    {
        for (int i = 0; i < upStairList.Count; i++)
        {
            upStairList[i].GetComponent<Stair>().stairKind = StairKind.upStair;
            upStairList[i].GetComponent<Stair>().stairNumber = i;
        }
        for (int i = 0; i < downStairList.Count; i++)
        {
            downStairList[i].GetComponent<Stair>().stairKind = StairKind.downStair;
            downStairList[i].GetComponent<Stair>().stairNumber = i;
        }

    }
    private void Awake()
    {
        go = GameObject.FindWithTag("GameManager");
        if (go == null)
        {
            Instantiate(gameManagerPrepab);
            go = GameObject.FindWithTag("GameManager");
        }
        else
        {
            go = GameObject.FindWithTag("GameManager");
        }
        
    }
    void Start()
    {
        gameManager = go.GetComponent<GameManager>();
        InitiateDungeron();
        ChangePlayerPos();
    }
    public void SaveFloor()
    {
        FloorPos floorData = new FloorPos();
        Isvisited isinsight;
        for(int i = 0; i < wallObjList.Count; i++)
        {
            floorData.Pos.Add(wallObjList[i].transform.position);
            floorData.type.Add(StructType.Wall);
            isinsight = wallObjList[i].transform.GetComponent<Isvisited>();
            floorData.visited.Add(isinsight.isVisit);
            floorData.spriteNumber.Add(wallObjList[i].GetComponent<WallSpriteChange>().spriteNumber);
        }
        for(int i = 0; i < doorObjList.Count; i++)
        {
            floorData.Pos.Add(doorObjList[i].transform.position);
            floorData.type.Add(StructType.Door);
            isinsight = doorObjList[i].transform.GetComponent<Isvisited>();
            floorData.spriteNumber.Add(0);
            floorData.visited.Add(isinsight.isVisit);
        }
        for(int i = 0; i < tileObjList.Count; i++)
        {
            floorData.Pos.Add(tileObjList[i].transform.position);
            floorData.type.Add(StructType.Floor);
            isinsight = tileObjList[i].transform.GetComponent<Isvisited>();
            floorData.spriteNumber.Add(tileObjList[i].transform.GetComponent<FloorSpriteChange>().randomIndex);
            floorData.visited.Add(isinsight.isVisit);
        }
        for(int i =0; i < upStairList.Count; i++)
        {
            floorData.Pos.Add(upStairList[i].transform.position);
            floorData.type.Add(StructType.StairUp);
            isinsight = upStairList[i].transform.GetComponent<Isvisited>();
            floorData.upStairNumber.Add(upStairList[i].transform.GetComponent<Stair>().stairNumber);
            floorData.spriteNumber.Add(0);
            floorData.visited.Add(isinsight.isVisit);
        }
        for (int i = 0; i < downStairList.Count; i++)
        {
            floorData.Pos.Add(downStairList[i].transform.position);
            floorData.type.Add(StructType.StairDown);
            isinsight = downStairList[i].transform.GetComponent<Isvisited>();
            floorData.downStairNumber.Add(downStairList[i].transform.GetComponent<Stair>().stairNumber);
            floorData.spriteNumber.Add(0);
            floorData.visited.Add(isinsight.isVisit);
        }
        string Json = JsonUtility.ToJson(floorData);
        string fileName = "Floor" + gameManager.nowFloor;
        string path = Application.dataPath + "/Save/" + fileName + ".Json";
        File.WriteAllText(path, Json);
    }
    public void LoadFloor(int floor)
    {
        FloorPos floorData = new FloorPos();
        string fileName = "Floor" + floor;
        string path = Application.dataPath + "/Save/" + fileName + ".Json";
        string data = File.ReadAllText(path);
        floorData = JsonUtility.FromJson<FloorPos>(data);
        if (floorData != null)
        {
            LoadVisitedFloor(floorData);
            gameManager.transform.GetComponent<SpawnManager>().LoadMonster(floor);
            gameManager.transform.GetComponent<SpawnManager>().LoadItem(floor);
        }
        else
        {
            Debug.Log("DataError!");
        }
        gameManager.nowFloor = gameManager.nextFloor;

    }
    public void LoadVisitedFloor(FloorPos floorData)
    {
        GameObject go;
        int n = 0;
        int k = 0;
        for (int i = 0; i < floorData.type.Count; i++)
        {
            switch (floorData.type[i])
            {
                case StructType.Wall:
                    go = Instantiate(wallObjArray[0], floorData.Pos[i], Quaternion.identity);
                    go.transform.GetComponent<IsinSight>().visited = floorData.visited[i];
                    go.transform.GetComponent<WallSpriteChange>().spriteNumber = floorData.spriteNumber[i];
                    wallObjList.Add(go);
                    //floorData.Pos.Add(go.transform.position);
                    //floorData.type.Add(StructType.Wall);
                    break;
                case StructType.Floor:
                    go = Instantiate(tileObjArray[0], floorData.Pos[i], Quaternion.identity);
                    go.transform.GetComponent<IsinSight>().visited = floorData.visited[i];
                    go.transform.GetComponent<FloorSpriteChange>().randomIndex = floorData.spriteNumber[i];
                    tileObjList.Add(go);
                    //floorData.Pos.Add(go.transform.position);
                    //floorData.type.Add(StructType.Floor);
                    break;
                case StructType.StairUp:
                    go = Instantiate(stairUpObjArray[0], floorData.Pos[i], Quaternion.identity);
                    
                    go.transform.GetComponent<Stair>().stairKind = StairKind.upStair;
                    go.transform.GetComponent<Stair>().stairNumber = floorData.upStairNumber[n];
                    go.transform.GetComponent<IsinSight>().visited = floorData.visited[i];
                    if (n <= 2)
                    {
                        n++;
                    }
                    upStairList.Add(go);
                    //floorData.Pos.Add(go.transform.position);
                    //floorData.type.Add(StructType.StairUp);
                    break;
                case StructType.StairDown:
                    go = Instantiate(stairDownObjArray[0], floorData.Pos[i], Quaternion.identity);
                    go.transform.GetComponent<Stair>().stairKind = StairKind.downStair;
                    go.transform.GetComponent<Stair>().stairNumber = floorData.upStairNumber[k];
                    go.transform.GetComponent<IsinSight>().visited = floorData.visited[i];
                    
                    if (k <= 2)
                    {
                        k++;
                    }
                    downStairList.Add(go);
                    //floorData.Pos.Add(go.transform.position);
                    //floorData.type.Add(StructType.StairDown);
                    break;
                case StructType.Door:
                    go = Instantiate(doorObjArray[0], floorData.Pos[i], Quaternion.identity);
                    //floorData.Pos.Add(go.transform.position);
                    go.transform.GetComponent<IsinSight>().visited = floorData.visited[i];
                    doorObjList.Add(go);
                    //floorData.type.Add(StructType.Door);
                    break;
                default:
                    Debug.Log("LoadError!");
                    break;
            }

        }

    }
    public void InitiateDungeron()
    {
        int temp = 0;
        int num = System.Enum.GetValues(typeof(DungeonTheme)).Length;
        temp = Random.Range(0,num);
        dungeonTheme = (DungeonTheme)temp;
        if (gameManager.savedFloor.Contains(gameManager.nextFloor))
        {
            LoadFloor(gameManager.nextFloor);
        }
        else
        {
            GenerateNewDungeon();
        }
       

    }
    void GenerateNewDungeon()
    {
        GenerateDungeon();
        ItemSpawn();
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();
        spawnManager.PlayerSpawned();
        MakeNumbersStair();
        spawnManager.MonsterSpawn();
        Player = GameObject.FindWithTag("Player");
        if (GameManager.instance == null)
        {
            Player.transform.position = upStairList[0].transform.position;
        }
        gameManager.nowFloor = gameManager.nextFloor;
    }
    public void ChangePlayerPos()
    {
        if (gameManager.stairKind == StairKind.downStair)
        {
            if(Player == null)
            {
                Player = GameObject.FindWithTag("Player");
            }
            if(upStairList.Count == 0)
            {
                Debug.Log("upStairListEmpty");
            }
            else
            {
                Player.transform.position = upStairList[gameManager.stairNumber].transform.position;
            }
            
        }
        else if (gameManager.stairKind == StairKind.upStair)
        {
            if (Player == null)
            {
                Player = GameObject.FindWithTag("Player");
            }
            if (upStairList.Count == 0)
            {
                Debug.Log("downStairListEmpty");
            }
            else
            {
                Player.transform.position = downStairList[gameManager.stairNumber].transform.position;
            }
        }
    }
    

}
