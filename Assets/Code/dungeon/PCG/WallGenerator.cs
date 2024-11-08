using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorposition, TilemapVisualizer tilemapvisualizer)
    {
        var basicWallPosition = FindWallsInDirections(floorposition, Direction2D.cardinalDirectionList);
        var cornerwallPosition = FindWallsInDirections(floorposition, Direction2D.diagonallDirectionList);
        CreateBasicWall(tilemapvisualizer, basicWallPosition, floorposition);
        CreateCornerWall(tilemapvisualizer, cornerwallPosition, floorposition);
    }

    private static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerwallposition, HashSet<Vector2Int> floorposition)
    {
        foreach(var position in cornerwallposition)
        {
            string neighboursbinary = "";
            foreach(var direction in Direction2D.eightDirectionList)
            {
                var neighbourposition = position + direction;
                if(floorposition.Contains(neighbourposition))
                {
                    neighboursbinary += "1";
                }else{
                    neighboursbinary += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursbinary);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPosition, HashSet<Vector2Int> floorposition)
    {
        foreach(var position in basicWallPosition)
        {
            string neighboursbinary = "";
            foreach(var direction in Direction2D.cardinalDirectionList)
            {
                var neighbourposition = position + direction;
                if(floorposition.Contains(neighbourposition))
                {
                    neighboursbinary += "1";
                }else{
                    neighboursbinary += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighboursbinary);
        }
    }

    public static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorposition, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallpositions = new HashSet<Vector2Int>();
        foreach(var position in floorposition)
        {
            foreach(Vector2Int direction in directionList)
            {
                var neighbourposition = position + direction;
                if(floorposition.Contains(neighbourposition) == false)
                {
                    wallpositions.Add(neighbourposition);
                }
            }
        }
        return wallpositions;
    }
}
