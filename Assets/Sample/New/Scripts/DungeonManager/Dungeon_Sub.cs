using System.Collections.Generic;
using UnityEngine;

public class Dungeon_Sub : MonoBehaviour
{
    [Header("Dungeon Settings")]
    [SerializeField] private string dungeonSubName = "";
    [SerializeField] private string musicTitle = "";
    public Transform [] characterSpawns;

    // Property to access the dungeon sub name
    public string DungeonSubName => dungeonSubName;

    #region Dungeon Initialization
    // Initializes the dungeon sub, spawns characters, and sets the music
    public void StartDungeon()
    {
        SetMusic();
    }
    #endregion

    #region Sound System

    // Sets the music for this dungeon using Sound_System instance
    private void SetMusic()
    {
        if (Sound_System.Ins != null)
        {
            Sound_System.Ins.On_Set_Music(musicTitle);
        }
        else
        {
            Debug.LogWarning("Sound_System instance not found. Music cannot be set.");
        }
    }

    #endregion
}