using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine;

public class RandomWalkGenerator : AbstractdungeonGenerator
{
    [SerializeField]
    public SRWSO RandomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorposition = RunRandomWalk(RandomWalkParameters, startposition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.Paintfloortile(floorposition);
        WallGenerator.CreateWalls(floorposition, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SRWSO param, Vector2Int position)
    {
        var currentposition = position;
        HashSet<Vector2Int> floorposition = new HashSet<Vector2Int>();
        for(int i = 0; i < param.iteration; i++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithm.SimpleRandowmWalk(currentposition, param.walkLength);
            floorposition.UnionWith(path);
            if(param.StartRandomEachIteration)
            {
                currentposition = floorposition.ElementAt(Random.Range(0, floorposition.Count));
            }
        }
        return floorposition;
    }
}
