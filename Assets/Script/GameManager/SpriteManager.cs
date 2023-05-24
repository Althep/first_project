using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public object[] floorData_0;
    public object[] floorData_1;
    public object[] floorData_2;

    private void Start()
    {
        GetFloorSprite();
    }

    void GetFloorSprite()
    {
        string path = "AnimSprite\\WallFloor\\Tiles\\";
        floorData_0 = Resources.LoadAll(path + "Tile_0");
        floorData_1 = Resources.LoadAll(path + "Tile_1");
        floorData_2 = Resources.LoadAll(path + "Tile_2");
    }
}
