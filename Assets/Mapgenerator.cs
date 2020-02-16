using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapgenerator : MonoBehaviour
{
    // Start is called before the first frame update
    Tilemap walkable;
    Tilemap nonwalkable;
    Tilemap water;
    public TileBase walkableTile;
    public TileBase nonwalkableTile;
    public TileBase waterTile;
 

    void Awake()
    {
        walkable = GameObject.Find("Walkable").GetComponent<Tilemap>();
        nonwalkable = GameObject.Find("Nonwalkable").GetComponent<Tilemap>();
        water = GameObject.Find("Water").GetComponent<Tilemap>();

		//createmap(new Vector2(0, 10), new Vector2(8, 0));
		//createmap(new Vector2(15, 20), new Vector2(30, 0));
		//corridoor(new Vector2(15, 7), new Vector2(8, 9));
		//Wallify(20, 25);


		//createmap(new Vector2(0, 20), new Vector2(8, 15));
		//corridoor(new Vector2(3, 10), new Vector2(5, 15));
	}


    public void createmap(Vector2 topLeft, Vector2 bottomRight)
    {
        int leftX = Mathf.RoundToInt(topLeft.x);
        int leftY = Mathf.RoundToInt(topLeft.y);
        int rightX = Mathf.RoundToInt(bottomRight.x);
        int rightY = Mathf.RoundToInt(bottomRight.y);
        for(int x = leftX; x < rightX + 1; x++)
		{
			for(int y = rightY; y < leftY + 1; y++)
			{

				if(x == leftX || y == leftY || x == rightX || y == rightY)
				{
					closeTile(new Vector2(x, y));
				}
				else
				{
					openTile(new Vector2(x, y));
				}
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


	public void closeTile(Vector2 position)
	{
		int x = Mathf.RoundToInt(position.x);
		int y = Mathf.RoundToInt(position.y);

		if (walkable.HasTile(new Vector3Int(x, y, 0)))
			return;

		nonwalkable.SetTile(new Vector3Int(x, y, 0), nonwalkableTile);
		walkable.SetTile(new Vector3Int(x, y, 0), null);

	}

    public void setWaterTile(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        if (nonwalkable.HasTile(new Vector3Int(x, y, 0)))
            return;

        water.SetTile(new Vector3Int(x,y,0), waterTile);
        nonwalkable.SetTile(new Vector3Int(x, y, 0), null);
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


	public void corridoor(Vector2 pos1, Vector2 pos2)
	{
		if(isAdjacent(pos1, pos2))
		{

			return;



		}
		else
		{
			if(isHorizontal(pos1))
			{
				if (pos1.x < pos2.x)
					joinHorizontal(pos1,pos2);
				else
					joinHorizontal(pos2, pos1);

			}
			else
			{
				if (pos1.y < pos2.y)
				{
					joinVertical(pos1, pos2);
				}
				else
				{
					joinVertical(pos2, pos1);
				}

			}

			//do something different

		}





	}



	public bool isAdjacent(Vector2 pos1, Vector2 pos2)
	{
		if (Mathf.Abs(pos1.x - pos2.x) == 1.0f && pos1.y == pos2.y)
		{
			openTile(pos1);
			openTile(pos2);

			openTile(new Vector2(pos1.x, pos1.y + 1));
			openTile(new Vector2(pos2.x, pos2.y + 1));

			return true;

		}


		if (Mathf.Abs(pos1.y - pos2.y) == 1.0f && pos1.x == pos2.x)
		{

			openTile(pos1);
			openTile(pos2);

			openTile(new Vector2(pos1.x +1, pos1.y));
			openTile(new Vector2(pos2.x + 1, pos2.y));
			return true;

		}

		return false;


	}


	public bool isHorizontal(Vector2 pos1)
	{
		return !nonwalkable.HasTile(new Vector3Int(Mathf.RoundToInt(pos1.x + 1),Mathf.RoundToInt(pos1.y),0));

	}


	public void joinHorizontal(Vector2 left, Vector2 right)
	{
		//always left to right


		float total_distance = (right.x - left.x) + 1;


		float delta = right.y - left.y;


		for(float i = left.x; i < left.x + Mathf.Floor(total_distance /2); i++)
		{
			openTile(new Vector2(i, left.y));

		}


		for (float i = left.x + Mathf.Floor(total_distance / 2); i <= right.x; i++)
		{
			openTile(new Vector2(i, right.y));

		}


		if(delta > 0)
		{
			for(float i = left.y; i < left.y + delta; i++)
			{
				openTile(new Vector2(left.x + Mathf.Floor(total_distance / 2), i));
			}


		}
		else if (delta < 0)
		{
			for (float i = left.y; i > left.y + delta; i--)
			{
				openTile(new Vector2(left.x + Mathf.Floor(total_distance / 2), i));
			}


		}


		//walls


		for (float i = left.x; i < left.x + Mathf.Floor(total_distance / 2) - Mathf.Sign(delta); i++)
		{
			closeTile(new Vector2(i, left.y + 1));
		}


		for (float i = left.x + Mathf.Floor(total_distance / 2) - Mathf.Sign(delta); i <= right.x; i++)
		{
			closeTile(new Vector2(i, right.y + 1));

		}

		for (float i = left.x; i < left.x + Mathf.Floor(total_distance / 2) + Mathf.Sign(delta); i++)
		{
			closeTile(new Vector2(i, left.y - 1));
		}


		for (float i = left.x + Mathf.Floor(total_distance / 2) + Mathf.Sign(delta); i <= right.x; i++)
		{
			closeTile(new Vector2(i, right.y - 1));

		}



		if (delta > 0)
		{
			for (float i = left.y + 1; i < left.y + delta + 1; i++)
			{
				closeTile(new Vector2(left.x + Mathf.Floor(total_distance / 2) - 1, i));
			}


		}
		else if (delta < 0)
		{
			for (float i = left.y + 1; i > left.y + delta; i--)
			{
				closeTile(new Vector2(left.x + Mathf.Floor(total_distance / 2) + 1, i));
			}


		}

		if (delta > 0)
		{
			for (float i = left.y - 1; i < left.y + delta + 1; i++)
			{
				closeTile(new Vector2(left.x + Mathf.Floor(total_distance / 2) + 1, i));
			}


		}
		else if (delta < 0)
		{
			for (float i = left.y - 1; i > left.y + delta - 1; i--)
			{
				closeTile(new Vector2(left.x + Mathf.Floor(total_distance / 2) - 1, i));
			}


		}






	}


	public void joinVertical(Vector2 left, Vector2 right)
	{
		//always left to right


		float total_distance = (right.y - left.y) + 1;


		float delta = right.x - left.x;


		for (float i = left.y; i < left.y + Mathf.Floor(total_distance / 2); i++)
		{
			openTile(new Vector2(left.x, i));

		}


		for (float i = left.y + Mathf.Floor(total_distance / 2); i <= right.y; i++)
		{
			openTile(new Vector2(right.x, i));

		}


		if (delta > 0)
		{
			for (float i = left.x; i < left.x + delta; i++)
			{
				openTile(new Vector2(i, left.y + Mathf.Floor(total_distance / 2)));
			}


		}
		else if (delta < 0)
		{
			for (float i = left.x; i > left.x + delta; i--)
			{
				openTile(new Vector2(i, left.y + Mathf.Floor(total_distance / 2)));
			}


		}




		//walls


		for (float i = left.y; i < left.y + Mathf.Floor(total_distance / 2) - Mathf.Sign(delta); i++)
		{
			closeTile(new Vector2(left.x +1,i));
		}


		for (float i = left.y + Mathf.Floor(total_distance / 2) - Mathf.Sign(delta); i <= right.y; i++)
		{
			closeTile(new Vector2( right.x + 1,i));

		}

		for (float i = left.y; i < left.y + Mathf.Floor(total_distance / 2) + Mathf.Sign(delta); i++)
		{
			closeTile(new Vector2(left.x - 1,i));
		}


		for (float i = left.y + Mathf.Floor(total_distance / 2) + Mathf.Sign(delta); i <= right.y; i++)
		{
			closeTile(new Vector2(right.x - 1,i));

		}



		if (delta > 0)
		{
			for (float i = left.x + 1; i < left.x + delta + 1; i++)
			{
				closeTile(new Vector2(i, left.y + Mathf.Floor(total_distance / 2) - 1));
			}


		}
		else if (delta < 0)
		{
			for (float i = left.x + 1; i > left.x + delta; i--)
			{
				closeTile(new Vector2(i, left.y + Mathf.Floor(total_distance / 2) + 1));
			}


		}

		if (delta > 0)
		{
			for (float i = left.x - 1; i < left.x + delta + 1; i++)
			{
				closeTile(new Vector2(i, left.y + Mathf.Floor(total_distance / 2) + 1));
			}


		}
		else if (delta < 0)
		{
			for (float i = left.x - 1; i > left.x + delta - 1; i--)
			{
				closeTile(new Vector2(i, left.y + Mathf.Floor(total_distance / 2) - 1));
			}


		}


	}
	public void Wallify(int endx, int endy)
	{
		for (int i= -20; i <= endx + 20;i++)
		{
			for (int k = -20; k <= endy + 20; k++)
			{
				if(walkable.HasTile(new Vector3Int(i, k, 0))==false)
				{
					closeTile(new Vector2(i, k));
				}

			}

		}
	}
}
