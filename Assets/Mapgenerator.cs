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

        createmap(new Vector2(-7, 5), new Vector2(-2, 3));
        createmap(new Vector2(4, -1), new Vector2(7, -5));
        coridor(new Vector2(-3, 3), new Vector2(6, -1));
    }


    public void createmap(Vector2 topLeft, Vector2 bottomRight)
    {
		print("map");
        int leftX = Mathf.RoundToInt(topLeft.x);
        int leftY = Mathf.RoundToInt(topLeft.y);
        int rightX = Mathf.RoundToInt(bottomRight.x);
        int rightY = Mathf.RoundToInt(bottomRight.y);
        for (int i = leftX; i < rightX+1; i++) {
            var TileType = walkableTile;
            var Tile = walkable;
        
            for(int k = leftY; k > rightY-1; k--)
            {
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

    public void coridor(Vector2 pos1, Vector2 pos2)
    {
        int pos1X = Mathf.RoundToInt(pos1.x);
        int pos1Y = Mathf.RoundToInt(pos1.y);
        int pos2X = Mathf.RoundToInt(pos2.x);
        int pos2Y = Mathf.RoundToInt(pos2.y);
        bool horizontal = false;
        int left = 1;
        if (nonwalkable.HasTile(new Vector3Int(pos1X + 1, pos1Y, 0)) == false)
        {
            horizontal = true;
        }
        if (horizontal)
        {
            int temp = pos1X;
            pos1X = pos1Y;
            pos1Y = temp;
            temp = pos1X;
            pos2X = pos2Y;
            pos2Y = temp;

        }
        if (nonwalkable.HasTile(new Vector3Int(pos1X + 2, pos1Y, 0)) == false)
        {
            pos1X--;
        }
        if (nonwalkable.HasTile(new Vector3Int(pos2X + 2, pos2Y, 0)) == false)
        {
            pos2X--;
        }
        if (pos1Y < pos2Y)
        {
            int temp = pos1Y;
            pos1Y = pos2Y;
            pos2Y = temp;
        }
        if (pos1X > pos2X)
        {
            left = -1;

        }
        int diff = Mathf.RoundToInt(Mathf.Abs(pos1Y - pos2Y)/2);
        for(int i = 0; i< 6; i++)
            {
            pos1Y--;
            nonwalkable.SetTile(new Vector3Int(pos1X+2, pos1Y, 0), nonwalkableTile);
            walkable.SetTile(new Vector3Int(pos1X, pos1Y, 0), walkableTile);
            nonwalkable.SetTile(new Vector3Int(pos1X-1, pos1Y, 0), nonwalkableTile);
            walkable.SetTile(new Vector3Int(pos1X+1, pos1Y, 0), walkableTile);
            }

        diff = Mathf.Abs(pos1X - pos2X);
        nonwalkable.SetTile(new Vector3Int(pos1X + (2*left)+(left - 1)/-2, pos1Y+1, 0), null);
        nonwalkable.SetTile(new Vector3Int(pos1X + (2 * left)+(left -1)/(-2), pos1Y, 0), null);
        pos1X = pos1X + 1-(left - 1) / (-2);
        for (int i = 0; i <diff; i++)
            {
            pos1X= pos1X + 1*left;
            nonwalkable.SetTile(new Vector3Int(pos1X, pos1Y+2 , 0), nonwalkableTile);
            walkable.SetTile(new Vector3Int(pos1X, pos1Y , 0), walkableTile);
            nonwalkable.SetTile(new Vector3Int(pos1X-(3*left), pos1Y-1 , 0), nonwalkableTile);
            walkable.SetTile(new Vector3Int(pos1X, pos1Y+1 , 0), walkableTile);
            }
        diff = pos1Y - pos2Y;
        nonwalkable.SetTile(new Vector3Int(pos1X + left, pos1Y + 1, 0), nonwalkableTile);
        nonwalkable.SetTile(new Vector3Int(pos1X + left, pos1Y+2, 0), nonwalkableTile);
        pos1X = pos1X - 1 + (left - 1) / (-2);
        for (int i = 0; i < diff; i++)
        {
            pos1Y--;
            nonwalkable.SetTile(new Vector3Int(pos1X + 2, pos1Y+(1+left)/2, 0), nonwalkableTile);
            walkable.SetTile(new Vector3Int(pos1X, pos1Y, 0), walkableTile);
            nonwalkable.SetTile(new Vector3Int(pos1X - 1, pos1Y+(1 -left) / 2, 0), nonwalkableTile);
            walkable.SetTile(new Vector3Int(pos1X + 1, pos1Y, 0), walkableTile);
        }

    }
}
