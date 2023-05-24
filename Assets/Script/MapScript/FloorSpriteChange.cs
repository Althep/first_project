using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloorSpriteChange : MonoBehaviour
{
    Sprite sprite;
    SpriteManager spManager;
    TileThem tileThem;
    public int randomIndex = 0;
    void Start()
    {
        spManager = GameObject.Find("SpriteManager").transform.GetComponent<SpriteManager>();
        if (GameManager.instance.nowFloor >= 1 )
        {
            tileThem = GameManager.instance.tileThems[GameManager.instance.nowFloor - 1];
            ChangeTileSprite();
        }
        
    }

    void ChangeTileSprite()
    {
        string path = "AnimSprite\\WallFloor\\decorative";
        Sprite[] data = null;
        if (!GameManager.instance.savedFloor.Contains(GameManager.instance.nowFloor))
        {
            switch (tileThem)
            {
                case TileThem.Tile_0:
                    path = "AnimSprite\\WallFloor\\Tiles\\Tile_0";
                    data = Resources.LoadAll<Sprite>(path);
                    randomIndex = Random.Range(0, data.Length);
                    sprite = data[randomIndex];
                    break;
                case TileThem.Tile_1:
                    path = "AnimSprite\\WallFloor\\Tiles\\Tile_1";
                    data = Resources.LoadAll<Sprite>(path);
                    randomIndex = Random.Range(0, data.Length);
                    sprite = data[randomIndex];
                    break;
                case TileThem.Tile_2:
                    path = "AnimSprite\\WallFloor\\Tiles\\Tile_2";
                    data = Resources.LoadAll<Sprite>(path);
                    randomIndex = Random.Range(0, data.Length);
                    sprite = data[randomIndex];
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (tileThem)
            {
                case TileThem.Tile_0:
                    path = "AnimSprite\\WallFloor\\Tiles\\Tile_0";
                    data = Resources.LoadAll<Sprite>(path);
                    sprite = data[randomIndex];
                    break;
                case TileThem.Tile_1:
                    path = "AnimSprite\\WallFloor\\Tiles\\Tile_1";
                    data = Resources.LoadAll<Sprite>(path);
                    sprite = data[randomIndex];
                    break;
                case TileThem.Tile_2:
                    path = "AnimSprite\\WallFloor\\Tiles\\Tile_2";
                    data = Resources.LoadAll<Sprite>(path);
                    sprite = data[randomIndex];
                    break;
                default:
                    break;
            }
        }
        
        this.gameObject.transform.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
