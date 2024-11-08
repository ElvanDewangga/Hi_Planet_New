using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractdungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractdungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractdungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }

        if(GUILayout.Button("Clear Dungeon"))
        {
            generator.ClearDungeon();
        }
    }
}
