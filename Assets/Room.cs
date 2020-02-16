using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Room
{
	public Vector2Int gridLoc;
	public Vector2Int dimensions;

	public Vector2Int offset;
	public GameObject rendered;


	public Room(int x, int y, int grid_size = 25, int min_length = 15, int max_length = 21)
	{ 

		gridLoc = new Vector2Int(x, y);

		dimensions = new Vector2Int(Random.Range(min_length, max_length), Random.Range(min_length, max_length));

		offset = new Vector2Int(Random.Range(1, grid_size - dimensions.x - 1),Random.Range(1,grid_size - dimensions.y - 1));


		rendered = GameObject.CreatePrimitive(PrimitiveType.Cube);

		rendered.transform.position = new Vector3((gridLoc.x * grid_size) + offset.x + (dimensions.x / 2), 0, (gridLoc.y * grid_size) + offset.y + (dimensions.y / 2));
		rendered.transform.localScale = new Vector3(dimensions.x, 2, dimensions.y);

	}
}

public class Corridoor
{
	public Vector2Int a;
	public Vector2Int b;
	//which grid squares this corridoor connects

	public Corridoor(int xa, int ya, int xb,int yb)
	{
		a = new Vector2Int(xa, ya);
		b = new Vector2Int(xb, yb);
		Debug.DrawLine(new Vector3((xa * 25) + 5,0,(ya * 25) + 5), new Vector3((xb * 25) + 5, 0, (yb * 25) + 5), Color.green,Mathf.Infinity);

	}



	public Corridoor(Room a, Room b)
	{

		if (a.gridLoc.x > b.gridLoc.x)
			a_right_b(a, b);
		else if (a.gridLoc.x < b.gridLoc.x)
			a_right_b(b, a);
		else if (a.gridLoc.y > b.gridLoc.y)
			a_over_b(a, b);
		else if (a.gridLoc.y < b.gridLoc.y)
			a_over_b(b, a);



	}


	private void a_over_b(Room a, Room b)
	{
		int ay = (a.gridLoc.y * 25) + a.offset.y;
		// bottom y value in room

		int by = (b.gridLoc.y * 25) + b.offset.y + b.dimensions.y;
		//top y value in room

		int ax = 0;
		int bx = 0;

		if (ay - by > 5)
		{
			ax = Random.Range((a.gridLoc.x * 25) + a.offset.x + 2, (a.gridLoc.x * 25) + a.offset.x + a.dimensions.x - 2);

			bx = Random.Range((b.gridLoc.x * 25) + b.offset.x + 2, (b.gridLoc.x * 25) + b.offset.x + b.dimensions.x - 2);

		}
		else
		{
			int min = Mathf.Max((a.gridLoc.x * 25) + a.offset.x + 2, (b.gridLoc.x * 25) + b.offset.x + 2);
			int max = Mathf.Min((a.gridLoc.x * 25) + a.offset.x + a.dimensions.x - 2, (b.gridLoc.x * 25) + b.offset.x + b.dimensions.x - 2);

			ax = Random.Range(min, max);
			bx = ax;
		}

		this.a = new Vector2Int(ax, ay);
		this.b = new Vector2Int(bx, by);


	}

	private void a_right_b(Room a, Room b)
	{

		int ax = (a.gridLoc.x * 25) + a.offset.x;
		// bottom y value in room

		int bx = (b.gridLoc.x * 25) + b.offset.x + b.dimensions.x;
		//top y value in room



		int ay = 0, by = 0;



		if (ay - by > 5)
		{
			ay = Random.Range((a.gridLoc.y * 25) + a.offset.y + 2, (a.gridLoc.y * 25) + a.offset.y + a.dimensions.y - 2);

			by = Random.Range((b.gridLoc.y * 25) + b.offset.y + 2, (b.gridLoc.y * 25) + b.offset.y + b.dimensions.y - 2);

		}
		else
		{
			int min = Mathf.Max((a.gridLoc.y * 25) + a.offset.y + 2, (b.gridLoc.y * 25) + b.offset.y + 2);
			int max = Mathf.Min((a.gridLoc.y * 25) + a.offset.y + a.dimensions.y - 2, (b.gridLoc.y * 25) + b.offset.y + b.dimensions.y - 2);

			ay = Random.Range(min, max);
			by = ay;
		}


		this.a = new Vector2Int(ax, ay);
		this.b = new Vector2Int(bx, by);


	}



}




public class DungeonNode
{
	public Room room;
	public List<DungeonNode> connected;

	public List<Corridoor> corridoors;



	public DungeonNode(Room myRoom)
	{
		room = myRoom;
		connected = new List<DungeonNode>();
		corridoors = new List<Corridoor>();


	}

	public void AddConnection(DungeonNode node, Corridoor corridoor = null)
	{
		if (!connected.Contains(node))
		{
			connected.Add(node);

			if (corridoor is null)
			{
				corridoors.Add(new Corridoor(room, node.room));
				node.AddConnection(this, corridoors[corridoors.Count - 1]);
			}
			else
			{
				corridoors.Add(corridoor);
			}
		}

	}

	public void Destroy()
	{
		GameObject.Destroy(room.rendered);

	}






}
