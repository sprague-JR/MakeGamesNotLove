using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{

    List<Vector2Int> empty;
    public int min_rooms, max_rooms;
	List<DungeonNode> rooms;
    public int x, y;

	public GameObject[] totems;
	public GameObject[] enemies;

	DungeonNode start;
	DungeonNode end;

	public GameObject player;

	public GameObject exit;


	public Mapgenerator mg;

    void Start()
    {
		mg = GetComponent<Mapgenerator>();
        makeRooms();
        makePath();
		GetDiameterVertices();

		foreach(DungeonNode dn in rooms)
		{
			


			Vector2 tl = new Vector2(
				(dn.room.gridLoc.x * 25) + dn.room.offset.x,
				(dn.room.gridLoc.y * 25) + dn.room.offset.y + dn.room.dimensions.y
				);


			Vector2 br = new Vector2(
				(dn.room.gridLoc.x * 25) + dn.room.offset.x + dn.room.dimensions.x,
				(dn.room.gridLoc.y * 25) + dn.room.offset.y
				);

			mg.createmap(tl, br);

		}


		List<Corridoor> drawnCorridoors = new List<Corridoor>();

		foreach(DungeonNode dn in rooms)
		{
			foreach(Corridoor c in dn.corridoors)
			{
				if(!drawnCorridoors.Contains(c))
				{

					mg.corridoor(c.a,c.b);
					drawnCorridoors.Add(c);
					
				}

			}


		}

		mg.Wallify(x * 25,y * 25);

		populate_start();
		//populate_end();

		populate_leaf_room();


		populate_non_leaf_room();

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
            empty.RemoveAt(index);
        }
    }

    void makePath()
    {
        DungeonNode[,] grid = new DungeonNode[x, y];
        Dictionary<DungeonNode, DungeonNode> prev = new Dictionary<DungeonNode, DungeonNode>();
        Dictionary<DungeonNode, int> dist = new Dictionary<DungeonNode, int>();
        List<DungeonNode> toVisit = new List<DungeonNode>();

        foreach (DungeonNode dn in rooms)
        {
            grid[dn.room.gridLoc.x, dn.room.gridLoc.y] = dn;
        }

        toVisit.Add(rooms[0]);
        dist.Add(toVisit[0], 0);
        prev.Add(toVisit[0], null);

        while (toVisit.Count > 0 && !isFinished(rooms, dist))
        {
            DungeonNode current = getMinDist(toVisit, dist);
            toVisit.Remove(current);

            //decrease along x axis
            if (current.room.gridLoc.x > 0)
            {
                if (grid[current.room.gridLoc.x - 1, current.room.gridLoc.y] != null &&
                    !dist.ContainsKey(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y]))
                {
                    //found a room, that has not yet been visited
                    dist.Add(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y], dist[current]);
                    toVisit.Add(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y]);
                    prev.Add(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y], current);
                }
                else if (grid[current.room.gridLoc.x - 1, current.room.gridLoc.y] == null)
                {
                    grid[current.room.gridLoc.x - 1, current.room.gridLoc.y] =
                        new DungeonNode(new Room(current.room.gridLoc.x - 1, current.room.gridLoc.y));
                    dist.Add(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y], dist[current] + 1);
                    toVisit.Add(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y]);
                    prev.Add(grid[current.room.gridLoc.x - 1, current.room.gridLoc.y], current);
                }
            }

            //increase along x axis
            if (current.room.gridLoc.x < x - 1)
            {
                if (grid[current.room.gridLoc.x + 1, current.room.gridLoc.y] != null &&
                    !dist.ContainsKey(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y]))
                {
                    //found a room, that has not yet been visited
                    dist.Add(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y], dist[current]);
                    toVisit.Add(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y]);
                    prev.Add(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y], current);
                }
                else if (grid[current.room.gridLoc.x + 1, current.room.gridLoc.y] == null)
                {
                    grid[current.room.gridLoc.x + 1, current.room.gridLoc.y] =
                        new DungeonNode(new Room(current.room.gridLoc.x + 1, current.room.gridLoc.y));
                    dist.Add(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y], dist[current] + 1);
                    toVisit.Add(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y]);
                    prev.Add(grid[current.room.gridLoc.x + 1, current.room.gridLoc.y], current);
                }
            }

            //decrease along y axis
            if (current.room.gridLoc.y > 0)
            {
                if (grid[current.room.gridLoc.x, current.room.gridLoc.y - 1] != null &&
                    !dist.ContainsKey(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1]))
                {
                    //found a room, that has not yet been visited
                    dist.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1], dist[current]);
                    toVisit.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1]);
                    prev.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1], current);
                }
                else if (grid[current.room.gridLoc.x, current.room.gridLoc.y - 1] == null)
                {
                    grid[current.room.gridLoc.x, current.room.gridLoc.y - 1] =
                        new DungeonNode(new Room(current.room.gridLoc.x, current.room.gridLoc.y - 1));
                    dist.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1], dist[current] + 1);
                    toVisit.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1]);
                    prev.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y - 1], current);
                }
            }

            //increase along y axis
            if (current.room.gridLoc.y < y - 1)
            {
                if (grid[current.room.gridLoc.x, current.room.gridLoc.y + 1] != null &&
                    !dist.ContainsKey(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1]))
                {
                    //found a room, that has not yet been visited
                    dist.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1], dist[current]);
                    toVisit.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1]);

                    prev.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1], current);
                }
                else if (grid[current.room.gridLoc.x, current.room.gridLoc.y + 1] == null)
                {
                    grid[current.room.gridLoc.x, current.room.gridLoc.y + 1] =
                        new DungeonNode(new Room(current.room.gridLoc.x, current.room.gridLoc.y + 1));
                    dist.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1], dist[current] + 1);
                    toVisit.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1]);
                    prev.Add(grid[current.room.gridLoc.x, current.room.gridLoc.y + 1], current);
                }
            }
        }

        List<DungeonNode> keep = new List<DungeonNode>();

        foreach (DungeonNode dn in rooms)
        {
            DungeonNode current = dn;

            while (prev[current] != null)
            {
                keep.Add(current);
				current.AddConnection(prev[current]);
                current = prev[current];
            }
        }

        for (int ix = 0; ix < x; ix++)
        {
            for (int iy = 0; iy < y; iy++)
            {
                if (grid[ix, iy] != null && !keep.Contains(grid[ix, iy]) && !rooms.Contains(grid[ix,iy]))
                {
                    grid[ix, iy].Destroy();
                    grid[ix, iy] = null;
                }else if(!rooms.Contains(grid[ix,iy]) && grid[ix,iy] != null)
				{
					rooms.Add(grid[ix, iy]);
				}

            }
        }
    }

    bool isFinished(List<DungeonNode> required, Dictionary<DungeonNode, int> dist)
    {
        foreach (DungeonNode r in required)
        {
            if (!dist.ContainsKey(r))
            {
                return false;
            }
        }

        return true;
    }


    DungeonNode getMinDist(List<DungeonNode> visited, Dictionary<DungeonNode, int> dist)
    {
        DungeonNode min = visited[0];
        int minDist = dist[min];

        foreach (DungeonNode dn in visited)
        {
            if (dist.ContainsKey(dn) && dist[dn] <= minDist)
            {
                min = dn;
                minDist = dist[min];
            }

            if (dist.ContainsKey(dn) && dist[dn] == minDist)
            {
                if (!rooms.Contains(min) && rooms.Contains(dn))
                {
                    //show preference for old rooms not making new ones
                    min = dn;
                }
            }
        }
        return min;
    }



	void GetDiameterVertices()
	{
		List<DungeonNode> leaves = new List<DungeonNode>();



		foreach(DungeonNode n in rooms)
		{

			if(n.connected.Count == 1)
			{
				leaves.Add(n);
			}
		}



		int max_steps = 0;

		foreach(DungeonNode start in leaves)
		{
			foreach(DungeonNode end in leaves)
			{

				int steps = path_distance(start, end);
				if (steps > max_steps)
				{
					this.start = start;
					this.end = end;
					max_steps = steps;

				}
			}
		}
	}



	private int path_distance(DungeonNode start, DungeonNode end)
	{

		Dictionary<DungeonNode, int> dist = new Dictionary<DungeonNode, int>();
		List<DungeonNode> toVisit = new List<DungeonNode>();

		dist.Add(start, 0);
		toVisit.Add(start);



		while(toVisit.Count > 0 && !dist.ContainsKey(end))
		{
			DungeonNode current = toVisit[0];
			toVisit.RemoveAt(0);
			//since all rooms have equal weight we do not need to sort by distance

			foreach(DungeonNode connection in current.connected)
			{
				if(!dist.ContainsKey(connection))
				{
					dist.Add(connection, dist[current] + 1);
					toVisit.Add(connection);
					
				}
			}
		}

		if (!dist.ContainsKey(end))
			return -1;
		else
			return dist[end];
	}











	public void populate_start()
	{

		GameObject player_instance = Instantiate(player);

		player_instance.transform.position = new Vector3(
			start.room.gridLoc.x * 25 + start.room.offset.x + (start.room.dimensions.x / 2),
			start.room.gridLoc.y * 25 + start.room.offset.y + (start.room.dimensions.y / 2),
			0);

		transform.parent = player_instance.transform;
		transform.localPosition = new Vector3(0, 0, -10);



		GameObject start_boon = Instantiate(totems[Mathf.RoundToInt(Random.Range(0, totems.Length))]);

		//find suitable location for boon

		Corridoor c = start.corridoors[0];
		//start is a leaf node so there is only one corridoor

		Vector2Int cor_loc = c.a.y == (start.room.gridLoc.y * 25) + start.room.offset.y + start.room.dimensions.y ? c.a: c.b;



		int boon_x = 0;
		if (cor_loc.y == (start.room.gridLoc.y * 25) + start.room.offset.y + start.room.dimensions.y)
		{
			boon_x = Mathf.Abs(cor_loc.x - (start.room.gridLoc.x * 25) + start.room.offset.x) >
			Mathf.Abs(cor_loc.x - (start.room.gridLoc.x * 25) + start.room.offset.x + start.room.dimensions.x) ?
			Random.Range((start.room.gridLoc.x * 25) + start.room.offset.x + 2, cor_loc.x - 2) :
			Random.Range(cor_loc.x + 2, (start.room.gridLoc.x * 25) + start.room.offset.x + start.room.dimensions.x - 2);

		}
		else
		{
			boon_x = Random.Range((start.room.gridLoc.x * 25) + start.room.offset.x + 2, (start.room.gridLoc.x * 25) + start.room.offset.x + start.room.dimensions.x - 2);
		}

		start_boon.transform.position = new Vector3(boon_x, (start.room.gridLoc.y * 25) + start.room.offset.y + start.room.dimensions.y);





	}


	public void populate_end()
	{

		GameObject exit_instance = Instantiate(exit);

		exit_instance.transform.position = new Vector3(
			end.room.gridLoc.x * 25 + end.room.offset.x + (end.room.dimensions.x / 2),
			end.room.gridLoc.y * 25 + end.room.offset.y + (end.room.dimensions.y / 2),
			0);
	}





	public void populate_non_critical_corridoor()
	{





	}



	public void populate_leaf_room()
	{


		foreach(DungeonNode room in rooms)
		{

			if(room.connected.Count == 1 && room != start && room != end)
			{

				//leaf room, time to go crazy

				if(Random.value < 0.3f)
				{
					//spawn boon

					Debug.Log(room.corridoors.Count);
					Corridoor c = room.corridoors[0];
					//start is a leaf node so there is only one corridoor

					Vector2Int cor_loc = c.a.y == (room.room.gridLoc.y * 25) + room.room.offset.y + room.room.dimensions.y ? c.a : c.b;



					int boon_x = 0;
					if (cor_loc.y == (room.room.gridLoc.y * 25) + room.room.offset.y + room.room.dimensions.y)
					{
						boon_x = Mathf.Abs(cor_loc.x - (room.room.gridLoc.x * 25) + room.room.offset.x) >
						Mathf.Abs(cor_loc.x - (room.room.gridLoc.x * 25) + room.room.offset.x + room.room.dimensions.x) ?
						Random.Range((room.room.gridLoc.x * 25) + room.room.offset.x + 2, cor_loc.x - 2) :
						Random.Range(cor_loc.x + 2, (room.room.gridLoc.x * 25) + room.room.offset.x + room.room.dimensions.x - 2);

					}
					else
					{
						boon_x = Random.Range((room.room.gridLoc.x * 25) + room.room.offset.x + 2, (room.room.gridLoc.x * 25) + room.room.offset.x + room.room.dimensions.x - 2);
					}
					GameObject start_boon = Instantiate(totems[Mathf.RoundToInt(Random.Range(0, totems.Length))]);

					start_boon.transform.position = new Vector3(boon_x, (start.room.gridLoc.y * 25) + start.room.offset.y + start.room.dimensions.y);
				}
				else
				{

					for (int i = 0; i < Random.Range(15, 20); i++)
					{
						GameObject temp = GameObject.Instantiate(enemies[Mathf.FloorToInt(Random.Range(0, enemies.Length - 0.1f))]);

						temp.transform.position = new Vector3(
								Random.Range((room.room.gridLoc.x * 25) + room.room.offset.x + 1, (room.room.gridLoc.x * 25) + room.room.offset.x + room.room.dimensions.x - 1),
								Random.Range((room.room.gridLoc.y * 25) + room.room.offset.y + 1, (room.room.gridLoc.y * 25) + room.room.offset.y + room.room.dimensions.y - 1),
						0);
					}
					//spawn tonnes of enemies
				}


			}
		}
	}



	public void populate_non_leaf_room()
	{

		foreach(DungeonNode room in rooms)
		{
			if(room.connected.Count > 1)
			{
				//not a leaf node so don't go crazy;

				for (int i = 0; i < Random.Range(3, 10); i++)
				{
					GameObject temp = GameObject.Instantiate(enemies[Mathf.RoundToInt(Random.Range(0, enemies.Length - 1))]);

					temp.transform.position = new Vector3(
							Random.Range((room.room.gridLoc.x * 25) + room.room.offset.x + 1, (room.room.gridLoc.x * 25) + room.room.offset.x + room.room.dimensions.x - 1),
							Random.Range((room.room.gridLoc.y * 25) + room.room.offset.y + 1, (room.room.gridLoc.y * 25) + room.room.offset.y + room.room.dimensions.y - 1),
					0);
				}
				
				
			}
		}
	}
}




