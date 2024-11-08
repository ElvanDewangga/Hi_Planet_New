using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ProceduralGenerationAlgorithm : MonoBehaviour
{
    public static HashSet<Vector2Int> SimpleRandowmWalk(Vector2Int startpos, int walklength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startpos);
        var previousposition = startpos;

        for(int i = 0;i < walklength;i++)
        {
            var  newposition = previousposition + Direction2D.GetRandomCardinalDirection();
            path.Add(newposition);
            previousposition = newposition;
        };
        return path;
    }
    
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startposition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentposition = startposition;
        corridor.Add(currentposition);

        for(int i = 0; i < corridorLength;i++)
        {
            currentposition += direction;
            corridor.Add(currentposition);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceSplit, int minwidth, int minheight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        roomQueue.Enqueue(spaceSplit);

        while(roomQueue.Count > 0)
        {
            var room = roomQueue.Dequeue();
            if(room.size.y >= minheight && room.size.x >= minwidth)
            {
                if(UnityEngine.Random.value < 0.5f)
                {
                    if(room.size.y >= minheight * 2)
                    {
                        SplitHorizontally(minheight, roomQueue, room);
                    }else if(room.size.x >= minwidth * 2)
                    {
                        SplitVertically(minwidth, roomQueue, room);
                    }else if(room.size.x >= minwidth && room.size.y >= minheight)
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    if(room.size.x >= minwidth * 2)
                    {
                        SplitVertically(minwidth, roomQueue, room);
                    }
                    else if(room.size.y >= minheight * 2)
                    {
                        SplitHorizontally(minheight, roomQueue, room);
                    }else if(room.size.x >= minwidth && room.size.y >= minheight)
                    {
                        roomList.Add(room);
                    }
                }
            }
        }
        return roomList;
    }

    private static void SplitVertically(int minwidth, Queue<BoundsInt> roomsqueue, BoundsInt rooms)
    {
        var xsplit = Random.Range(1, rooms.size.x);
        BoundsInt room1 = new BoundsInt(rooms.min, new Vector3Int(xsplit, rooms.size.y, rooms.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(rooms.min.x + xsplit, rooms.min.y, rooms.min.z),
        new Vector3Int(rooms.size.x - xsplit, rooms.size.y, rooms.size.z));
        roomsqueue.Enqueue(room1);
        roomsqueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minheight, Queue<BoundsInt> roomsqueue, BoundsInt rooms)
    {
        var ysplit = Random.Range(1, rooms.size.y);
        BoundsInt room1 = new BoundsInt(rooms.min, new Vector3Int(rooms.size.x, ysplit, rooms.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(rooms.min.x, rooms.min.y + ysplit, rooms.min.z),
        new Vector3Int(rooms.size.x, rooms.size.y - ysplit, rooms.size.z));
        roomsqueue.Enqueue(room1);
        roomsqueue.Enqueue(room2);
    }
}


public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),
        new Vector2Int(1,0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    public static List<Vector2Int>diagonallDirectionList = new List<Vector2Int>
    {
        new Vector2Int(1, 1),
        new Vector2Int(1,-1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1) 
    };

    public static List<Vector2Int> eightDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),
        new Vector2Int(1,1),
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1)
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}
