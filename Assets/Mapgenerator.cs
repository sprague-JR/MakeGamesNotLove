using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapgenerator : MonoBehaviour
{
    // Start is called before the first frame update
    Tilemap walkable;
    Tilemap nonwalkable;
    public TileBase walkableTile;
    public TileBase nonwalkableTile;
 

    void Start()
    {
        walkable = GameObject.Find("Walkable").GetComponent<Tilemap>();
        nonwalkable = GameObject.Find("Nonwalkable").GetComponent<Tilemap>();
        createmap(new Vector2(-10, 4), new Vector2(8, -4));
    }


    public void createmap(Vector2 topLeft, Vector2 bottomRight)
    {
        int leftX = Mathf.RoundToInt(topLeft.x);
        int leftY = Mathf.RoundToInt(topLeft.y);
        int rightX = Mathf.RoundToInt(bottomRight.x);
        int rightY = Mathf.RoundToInt(bottomRight.y);
        for (int i = leftX; i < rightX+1; i++) {
            Debug.Log("for i");
            var TileType = walkableTile;
            var Tile = walkable;
        
            for(int k = leftY; k > rightY-1; k--)
            {
                Debug.Log("for k");
                if (i == leftX || i == rightX || k == leftY || k == rightY)
                {
                    TileType = nonwalkableTile;
                    Tile = nonwalkable;
                }
                else
                {
                    TileType = walkableTile;
                    Tile = walkable;
                }
                Tile.SetTile(new Vector3Int(i, k, 0), TileType);
            }
        }

        
    }
    public void openTile(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        nonwalkable.SetTile(new Vector3Int(x, y, 0), null);
        walkable.SetTile(new Vector3Int(x, y, 0), walkableTile);
    }


}
