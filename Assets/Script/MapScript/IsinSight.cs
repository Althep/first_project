using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsinSight : MonoBehaviour
{
    //layer3 = insight layer 7 = visited
    public bool visited = false;
    public bool inSight = false;
    public bool isRayCasted = false;
    float distWithPlayer;
    GameObject player;
    Vision playerVision;
    SpriteRenderer objSprite;
    Vector3 oldPos;
    Transform myTr;
    Vector3 oldPlayerPos;
    [SerializeField] RaycastHit2D[] hit;
    Isvisited Isvisited;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerVision = player.GetComponent<Vision>();
        if (this.transform.tag == "Wall" || this.transform.tag == "Tile")
        {
        }
        distWithPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if (distWithPlayer <= playerVision.playerVision)
        {
        }
        objSprite = this.transform.GetComponent<SpriteRenderer>();
        myTr = this.transform;
        oldPos = this.transform.position;
        if (this.tag != "Monster" || this.tag != "Item")
        {
            Isvisited = this.transform.GetComponent<Isvisited>();
        }
    }
    private void Update()
    {

        Debug.Log("2!!!");

        if (oldPlayerPos != player.transform.position || Input.GetKeyDown("[5]"))
        {
            ChangeLayer();
            ChangeColor();
        }
        if (this.transform.tag == "Monster" && oldPos != this.transform.position && distWithPlayer <= playerVision.playerVision)
        {
            CastRay();
            oldPos = this.transform.position;
        }
    }
    public void CastRay()
    {

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (playerVision == null)
        {
            playerVision = player.GetComponent<Vision>();
        }
        if (this.tag != "Monster" || this.tag != "Item")
        {
            Isvisited = this.transform.GetComponent<Isvisited>();
        }
        bool isContainWall = false;
        distWithPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        hit = Physics2D.RaycastAll(this.transform.position, (player.transform.position - this.transform.position), distWithPlayer);
        if (hit.Length != 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.position != this.transform.position)
                {
                    if (hit[i].transform.tag == "Wall")
                    {
                        isContainWall = true;
                    }
                }
            }
        }
        if (!isContainWall)
        {
            inSight = true;
            visited = true;
            Isvisited.isVisit = true;
        }
        else if (isContainWall)
        {
            inSight = false;
        }
        if (!visited && !inSight)
        {
            gameObject.layer = 6;
        }
        if (Vector2.Distance(this.transform.position, player.transform.position) < 1.5)
        {
            visited = true;
            inSight = true;
            Isvisited.isVisit = true;
        }
        oldPos = this.transform.position;
        ChangeLayer();
        ChangeColor();
    }

    public void ChangeLayer()
    {
        if (visited && !inSight)
        {
            gameObject.layer = 7;
        }
        else if (visited && inSight)
        {
            gameObject.layer = 3;
        }
    }
    public void ChangeColor()
    {
        if (objSprite == null)
        {
            objSprite = this.transform.GetComponent<SpriteRenderer>();
        }
        if (gameObject.layer == 7)
        {
            if (this.transform.tag == "Wall" || transform.tag == "Tile" || transform.tag == "Floor")
            {
                objSprite.color = Color.gray;
            }
            if (this.transform.tag == "Monster")
            {
                objSprite.color = new Color(1, 1, 1, 0);
            }
        }
        if (gameObject.layer == 3)
        {
            if (this.transform.tag == "Wall" || transform.tag == "Tile" || transform.tag == "Floor")
            {
                objSprite.color = Color.white;
            }
            if (this.transform.tag == "Monster")
            {
                objSprite.color = new Color(1, 1, 1, 1);
            }
        }
    }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (oldPlayerPos != player.transform.position || (Input.GetKeyDown("[5]")))
            {
                CastRay();
                oldPlayerPos = player.transform.position;
            }
            if (inSight && this.gameObject.layer != 3)
            {
                ChangeColor();
            }

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
}
