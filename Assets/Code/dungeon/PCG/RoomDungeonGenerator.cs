using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomDungeonGenerator : RandomWalkGenerator
{
    [SerializeField]private int minRoomWidth = 4; [SerializeField]private int minRoomHeight = 4;
    [SerializeField]private int dungeonWidth = 20; [SerializeField]private int dungeonHeight = 20;
    [SerializeField]
    public int corridorwidth;
    [SerializeField]
    [Range(0, 10)]
    private int offset;
    [SerializeField]private bool RandomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        var roomlist = ProceduralGenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startposition, new Vector3Int
        (dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomlist);

        List<Vector2Int> roomscenter = new List<Vector2Int>();
        foreach(var room in roomlist)
        {
            roomscenter.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomscenter);
        floor.UnionWith(corridors);

        tilemapVisualizer.Paintfloortile(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomscenter)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomscenter[Random.Range(0, roomscenter.Count)];
        roomscenter.Remove(currentRoomCenter);

        while(roomscenter.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomscenter);
            roomscenter.Remove(closest);
            HashSet<Vector2Int> newcorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newcorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentroomcenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentroomcenter;
        corridor.Add(position);
        
        while (position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }


            // Original line
            // corridor.Add(position);

            // Added to set corridor width
            for (int k = 0; k < corridorwidth; k++)
            {
                for (int j = 0; j < corridorwidth; j++)
                {
                    var offset = new Vector2Int(k, j);
                    corridor.Add(position + offset);
                }
            }
        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if(destination.x < position.x)
            {
                position += Vector2Int.left;
            }

            // Original line
            // corridor.Add(position);
            // Added to set corridor width

            for (int k = 0; k < corridorwidth; k++)
            {
                for (int j = 0; j < corridorwidth; j++)
                {
                    var offset = new Vector2Int(k, j);
                    corridor.Add(position + offset);
                }
            }
            
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentroomcenter, List<Vector2Int> roomcenter)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomcenter)
        {
            float currentDistance = Vector2.Distance(position, currentroomcenter);
            if(currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomslist)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach(var room in roomslist)
        {
            for(int col = offset; col < room.size.x - offset; col++)
            {
                for(int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
