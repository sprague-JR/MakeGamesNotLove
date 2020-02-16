using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Credits : MonoBehaviour

{
    Tilemap wall;
    Tilemap fire;
    public TileBase wallTile;
    public TileBase waterTile;
    public TileBase fireTile;
    public int l = 0;
    public int z = 0;

    void Start()

    {


        wall = GameObject.Find("Wall").GetComponent<Tilemap>();
        Background(7, 7,-20,-20,wall,wallTile);
    }
    public void Background(int endx, int endy,int startx, int starty,Tilemap tile, TileBase Base)
    {

        for (int i =startx; i <= endx + 20; i++)
        {
            for (int k = starty; k <= endy + 20; k++)
            {

                
                    tile.SetTile(new Vector3Int(i, k, 0), Base);

                
                
                



            }

        }
    }



    // Update is called once per frame





        
    
}
