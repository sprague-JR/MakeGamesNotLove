using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{
    public int critical_path_length;
    List<Vector2Int> empty;
    public int min_rooms, max_rooms;
    List<DungeonNode> rooms;
    public int x, y;

    void Start()
    {
        makeRooms();
        makePath();
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
                        new DungeonNode(new Room(current.room.gridLoc.x - 1, current.room.gridLoc.y, 10, 2, 2, true));
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
                        new DungeonNode(new Room(current.room.gridLoc.x + 1, current.room.gridLoc.y, 10, 2, 2, true));
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
                        new DungeonNode(new Room(current.room.gridLoc.x, current.room.gridLoc.y - 1, 10, 2, 2, true));
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
                        new DungeonNode(new Room(current.room.gridLoc.x, current.room.gridLoc.y + 1, 10, 2, 2, true));
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
                }else if(!rooms.Contains(grid[ix,iy]))
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
            if (dist.ContainsKey(dn) && dist[dn] < minDist)
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

        Debug.Log(minDist);
        return min;
    }
}