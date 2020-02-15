using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{
	// Start is called before the first frame update

	public int x, y;

	public int critical_path_length;

	public int min_rooms, max_rooms;

	List<DungeonNode> rooms;

	List<Vector2Int> empty;






    void Start()
    {

		makeRooms();

		//connect();

		GeneratePath();
    }




	void makeRooms()
	{

		rooms = new List<DungeonNode>();
		empty = new List<Vector2Int>();

		for (int ix = 0; ix < x; ix++)
		{
			for (int iy = 0; iy < y; iy++)
			{
				empty.Add(new Vector2Int(ix, iy));
			}
		}

		for (int i = 0; i < Random.Range(min_rooms, min_rooms + ((max_rooms - min_rooms) * 0.5f)); i++)
		{
			int index = Mathf.RoundToInt(Random.Range(0, empty.Count - 1));
			rooms.Add(new DungeonNode(new Room(empty[index].x, empty[index].y)));
			Debug.Log(index);
			empty.RemoveAt(index);

		}


	}



	void connect()
	{
		//connect pre-existing rooms using a corridoor
		List<Corridoor> corridoors = new List<Corridoor>();

		bool[,] big_grid = new bool[x, y];

		for(int i = 0; i < rooms.Count;i++)
		{
			big_grid[rooms[i].room.gridLoc.x, rooms[i].room.gridLoc.y] = true;

		}


		foreach(DungeonNode first in rooms)
		{
			foreach(DungeonNode second in rooms)
			{

				//add connections between adjacent horizontal and vertical rooms
				if ((first.room.gridLoc.x == second.room.gridLoc.x && first.room.gridLoc.y == second.room.gridLoc.y + 1)
					|| first.room.gridLoc.x == second.room.gridLoc.x + 1 && first.room.gridLoc.y == second.room.gridLoc.y)
				{
					first.AddConnection(second);
				}
			}
		}







	}





	void GeneratePath()
	{

		bool[,] hasRoom = new bool[x, y];
		List<Vector2Int> toVisit = new List<Vector2Int>();
		List<Vector2Int> visited = new List<Vector2Int>();
		List<Vector2Int> required = new List<Vector2Int>();

		foreach (DungeonNode dn in rooms)
		{
			hasRoom[dn.room.gridLoc.x, dn.room.gridLoc.y] = true;
			toVisit.Add(dn.room.gridLoc);
			required.Add(dn.room.gridLoc);
		}

		Dictionary<Vector2Int, int> distance = new Dictionary<Vector2Int, int>();
		Dictionary<Vector2Int, Vector2Int> previous = new Dictionary<Vector2Int, Vector2Int>();

		Vector2Int current = toVisit[0];

		toVisit.RemoveAt(0);

		distance.Add(current, 0);
		visited.Add(current);
		//distance to self is zero;

		previous.Add(current, new Vector2Int(-1,-1));
		
		while( !searchComplete(required, visited))
		{

			if(current.x > 0 && !visited.Contains(new Vector2Int(current.x - 1, current.y)))
			{
				Vector2Int temp = new Vector2Int(current.x - 1, current.y);
				visited.Add(temp);
				toVisit.Remove(temp);

				distance.Add(temp, distance[current] + (hasRoom[temp.x, temp.y] ? 0 : 1));

			}

			if (current.x < x - 1 && !visited.Contains(new Vector2Int(current.x + 1, current.y)))
			{
				Vector2Int temp = new Vector2Int(current.x + 1, current.y);
				visited.Add(temp);
				toVisit.Remove(temp);

				distance.Add(temp, distance[current] + (hasRoom[temp.x, temp.y] ? 0 : 1));

			}


			if (current.y > 0 && !visited.Contains(new Vector2Int(current.x, current.y - 1)))
			{
				Vector2Int temp = new Vector2Int(current.x, current.y - 1);
				visited.Add(temp);
				toVisit.Remove(temp);

				distance.Add(temp, distance[current] + (hasRoom[temp.x, temp.y] ? 0 : 1));

			}

			if (current.y < y - 1 && !visited.Contains(new Vector2Int(current.x, current.y + 1)))
			{
				Vector2Int temp = new Vector2Int(current.x, current.y + 1);
				visited.Add(temp);
				toVisit.Remove(temp);

				distance.Add(temp, distance[current] + (hasRoom[temp.x, temp.y] ? 0 : 1));

			}
			// Add unvisited adjacent rooms


			//update current
		}






		



	}


	bool searchComplete(List<Vector2Int> necessary, List<Vector2Int> current)
	{

		foreach(Vector2Int vn in necessary)
		{
			if(!current.Contains(vn))
				return false;

		}
		return true;

	}





}
