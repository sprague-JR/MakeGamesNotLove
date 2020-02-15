using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Room
{
	public Vector2Int gridLoc;
	Vector2Int dimensions;

	Vector2Int offset;
	Color color;
	GameObject rendered;


	public Room(int x, int y, int grid_size = 10, int min_length = 5, int max_length = 8)
	{
		color = Random.ColorHSV();

		gridLoc = new Vector2Int(x, y);

		dimensions = new Vector2Int(Random.Range(min_length, max_length), Random.Range(min_length, max_length));

		offset = new Vector2Int(Random.Range(0, grid_size - dimensions.x),Random.Range(0,grid_size - dimensions.y));


		rendered = GameObject.CreatePrimitive(PrimitiveType.Cube);

		rendered.transform.position = new Vector3((gridLoc.x * grid_size) + offset.x + (dimensions.x / 2), 0, (gridLoc.y * grid_size)+ offset.y + (dimensions.y / 2));
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
		Debug.DrawLine(new Vector3((xa * 10) + 5,0,(ya * 10) + 5), new Vector3((xb * 10) + 5, 0, (yb * 10) + 5), Color.green,Mathf.Infinity);

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

	public void AddConnection(DungeonNode node)
	{
		if (!connected.Contains(node))
		{
			connected.Add(node);
			corridoors.Add(new Corridoor(room.gridLoc.x, room.gridLoc.y, node.room.gridLoc.x, node.room.gridLoc.y));
			node.AddConnection(this);
		}

	}






}
