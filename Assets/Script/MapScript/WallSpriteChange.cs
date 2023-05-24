using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro.Examples;
using UnityEngine;

public class WallSpriteChange : MonoBehaviour
{
    // Start is called before the first frame update
    FieldManager fieldManager;
    List<GameObject> gameObjects;
    object data;
    Sprite sprite;
    Sprite[] spriteData;
    string number;
    public int spriteNumber;
    bool isUp;
    bool isDown;
    bool isLeft;
    bool isRight;
    int x;
    int y;
    bool isWall = false;
    List<Vector3> wallPos = new List<Vector3>();
    private void Start()
    {
        fieldManager = GameObject.FindGameObjectWithTag("FieldManager").GetComponent<FieldManager>();
        sprite = this.gameObject.transform.GetComponent<SpriteRenderer>().sprite;
        MakeCollider();
        x = (int)this.transform.position.x;
        y = (int)this.transform.position.y;
    }
    void MakeCollider()
    {
        if (spriteNumber == 0)
        {
            
            for (int i = -1; i < 2; i++)
            {
                foreach (Collider2D col in Physics2D.OverlapCircleAll(this.transform.position, 0.7f))
                {
                    if (col.transform.tag == "Wall" && col.transform.position != this.transform.position)
                    {
                        isWall = true;
                        if(col.transform.position == (new Vector3(x, y + 1)))
                        {
                            isUp = true;
                        }
                        else if (col.transform.position == (new Vector3(x, y - 1)))
                        {
                            isDown = true;
                        }
                        else if (col.transform.position == (new Vector3(x+1, y)))
                        {
                            isRight = true;
                        }
                        else if (col.transform.position == (new Vector3(x-1, y)))
                        {
                            isLeft = true;
                        }
                    }
                }

            }
            if (!isWall)
            {
                //±âµÕ
                int temp = Random.Range(0,5);
                number = temp.ToString();
                string path = "AnimSprite\\WallFloor\\"+ number;
                sprite = Resources.Load<Sprite>(path);
                this.gameObject.transform.GetComponent<SpriteRenderer>().sprite = sprite;
                this.gameObject.transform.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.5f);

            }

        }
        
    }
    void PillarSprite()
    {
        if (isLeft && isRight && isUp && isDown)
        {



        }
        else if (!isLeft)
        {

        }


        
        else
        {

        }
    }
}
