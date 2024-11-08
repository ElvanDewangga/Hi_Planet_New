using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorDungeonGenerator : RandomWalkGenerator
{
    [SerializeField]
    private int corridorLength = 14; 
    [SerializeField]
    private int corridorCount = 5;
    [SerializeField]
    private int corridorwidth = 2;
    [SerializeField] 
    [Range(0.1f, 1f)]
    private float roomPercent;

    protected override void RunProceduralGeneration()
    {
        CorridorGeneration();
    }

    private void CorridorGeneration()
    {
        HashSet<Vector2Int> floorposition = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentionroomposition = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corridors = CreateCorridors(floorposition, potentionroomposition);

        HashSet<Vector2Int> roomposition = CreateRooms(potentionroomposition);
        List<Vector2Int> deadend = FindAllDeadEnds(floorposition);
        CreateRoomsAtDeadEnds(deadend, roomposition);
        floorposition.UnionWith(roomposition);

        for(int i = 0;i < corridors.Count;i++)
        {
            //corridors[i] = IncreaseCorridorsSizeByOne(corridors[i]);
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorposition.UnionWith(corridors[i]);
        }

        tilemapVisualizer.Paintfloortile(floorposition);
        WallGenerator.CreateWalls(floorposition, tilemapVisualizer);
    }

    public void CreateRoomsAtDeadEnds(List<Vector2Int> deadends, HashSet<Vector2Int> roomfloors)
    {
        foreach(var position in deadends)
        {
            if(roomfloors.Contains(position) == false)
            {
                var room = RunRandomWalk(RandomWalkParameters, position);
                roomfloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorposition)
    {
        List<Vector2Int> deadends = new List<Vector2Int>();
        foreach(var position in floorposition)
        {
            var neighbourCount = 0;
            foreach(var direction in Direction2D.cardinalDirectionList)
            {
                if(floorposition.Contains(position + direction))
                {
                    neighbourCount++;
                }
            }

            if(neighbourCount == 1)
            {
                deadends.Add(position);
            }
        }
        return deadends;
    }

    public List<Vector2Int> IncreaseCorridorsSizeByOne(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        Vector2Int prevDirection = Vector2Int.zero;

        for(int i = 1;i < corridor.Count;i++)
        {
            Vector2Int directionFromCell = corridor[i] - corridor[i - 1];
            if(prevDirection != Vector2Int.zero && directionFromCell != prevDirection)
            {
                for(int x = -1;x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
                prevDirection = directionFromCell;
            }
            else
            {
                Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                newCorridor.Add(corridor[i - 1]);
                newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
            }
        }
        return newCorridor;
    }

    public List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newcorridor = new List<Vector2Int>();
        for(int i = 1;i < corridor.Count;i++)
        {
            for(int x = -1;x < corridorwidth; x++)
            {
                for(int y = -1;y < corridorwidth; y++)
                {
                    newcorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newcorridor;
    }

    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if(direction == Vector2Int.up)
        {
            return Vector2Int.right;
        }
        if(direction == Vector2Int.right)
        {
            return Vector2Int.down;
        }
        if(direction == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        if(direction == Vector2Int.left)
        {
            return Vector2Int.up;
        }
        return Vector2Int.zero;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialroompos)
    {
        HashSet<Vector2Int> roomposition = new HashSet<Vector2Int>();
        int roomtocreatecount = Mathf.RoundToInt(potentialroompos.Count * roomPercent);
        List<Vector2Int> roomtocreate = potentialroompos.OrderBy(x => Guid.NewGuid()).Take(roomtocreatecount).ToList();
        foreach(var roompos in roomtocreate)
        {
            var roomfloor = RunRandomWalk(RandomWalkParameters, roompos);
            roomposition.UnionWith(roomfloor);
        }
        return roomposition;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorposition, HashSet<Vector2Int> potentialroompos)
    {
        var currentposition = startposition;
        potentialroompos.Add(currentposition);
        List<List<Vector2Int>> corridors = new List<List<Vector2Int>>();

        for(int i = 0;i < corridorLength; i++)
        {
            var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentposition, corridorLength);
            corridors.Add(corridor);
            currentposition = corridor[corridor.Count - 1];
            potentialroompos.Add(currentposition);
            floorposition.UnionWith(corridor);
        }
        return corridors;
    }
}
