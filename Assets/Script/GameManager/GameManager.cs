using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public StairKind stairKind;
    public GameObject fieldManagerPrepab;
    public GameObject player;
    public GameObject canvas;
    public FieldManager fieldManager;
    public SpawnManager spawnManager;
    public PlayerState playerState;
    public TurnManage turnManage;
    public Inventory inventory;
    public int nowFloor = 1;
    public int nextFloor = 1;
    public int oldfloor = 0;
    public int stairNumber = 0;
    public List<int> savedFloor;
    public static GameManager instance;
    [SerializeField] List<GameObject> gos = new List<GameObject>();
    public List<TileThem> tileThems = new List<TileThem>();
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject != null)
        {
            player = GameObject.FindWithTag("Player");
            playerState = player.GetComponent<PlayerState>();
            fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
            spawnManager = this.gameObject.transform.GetComponent<SpawnManager>();
            turnManage = this.gameObject.transform.GetComponent<TurnManage>();
            savedFloor = new List<int>();
            if (nowFloor != 1)
            {
                savedFloor.Add(nowFloor);
            }
            instance = this;
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(this.gameObject);
            inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        }
        GetTileThem();

    }
    private void Update()
    {
        if (fieldManager == null && this.gameObject != null)
        {
            fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
        }

    }
    public void SaveToChangeFloor()
    {
        if (this.gameObject != null)
        {
            fieldManager.SaveFloor();
            LoadNextScen();
        }

        //SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetSceneByName("Scen2"));
    }

    public void LoadNextScen()
    {
        spawnManager.MonsterSave();
        spawnManager.ItemSave();
        inventory.SaveInventory();
        spawnManager.itemList.Clear();
        turnManage.monsterList.Clear();
        playerState.Save();
        GetTileThem();
        if (SceneManager.GetActiveScene().name == "Scen1")
        {
            SceneManager.LoadScene("Scen2");
            SceneManager.sceneLoaded += InitiateFloor;
        }
        else if (SceneManager.GetActiveScene().name == "Scen2")
        {
            SceneManager.LoadScene("Scen1");
            SceneManager.sceneLoaded += InitiateFloor;
        }
    }
    public void InitiateFloor(Scene scene, LoadSceneMode mode)
    {
        if (!savedFloor.Contains(nowFloor))
        {
            fieldManager = GameObject.Find("FieldManager").GetComponent<FieldManager>();
            spawnManager.monsterList.Clear();
            turnManage.monsterList.Clear();
            spawnManager.fieldManager = fieldManager;
        }
        if (savedFloor.Contains(nowFloor))
        {
            fieldManager = GameObject.FindGameObjectWithTag("FieldManager").transform.GetComponent<FieldManager>(); ;
            spawnManager.monsterList.Clear();
            turnManage.monsterList.Clear();
            spawnManager.fieldManager = fieldManager;
        }
    }
    public void GetTileThem()
    {
        if (!savedFloor.Contains(nowFloor))
        {
            TileThem temp = (TileThem)Random.Range(0, 3);
            tileThems.Add(temp);
            Debug.Log((int)temp);
        }
    }
}
