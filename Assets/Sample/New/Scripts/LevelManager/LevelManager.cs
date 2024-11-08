using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    [SerializeField] private int maxLevel = 120;
    [SerializeField] private Dictionary<int, int> levelUpExpRequirements = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeLevelExpRequirements();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeLevelExpRequirements()
    {
        levelUpExpRequirements.Add(1, 20);
        levelUpExpRequirements.Add(2, 30);
        levelUpExpRequirements.Add(3, 50);
        levelUpExpRequirements.Add(4, 80);
        levelUpExpRequirements.Add(5, 120);
        levelUpExpRequirements.Add(6, 170);
        levelUpExpRequirements.Add(7, 230);
        levelUpExpRequirements.Add(8, 300);
        levelUpExpRequirements.Add(9, 380);
        levelUpExpRequirements.Add(10, 470);

        // Assuming levels 11-18 have a fixed exp requirement of 470
        for (int i = 11; i <= 120; i++)
        {
            levelUpExpRequirements.Add(i, 700);
        }
    }

    public int GetNextLevelExpRequirement(int currentLevel)
    {
        if (levelUpExpRequirements.TryGetValue(currentLevel, out int expRequirement))
        {
            return expRequirement;
        }
        
        Debug.LogWarning($"Exp requirement for level {currentLevel} not found.");
        return -1; // Return -1 or an appropriate default/fallback value.
    }

    public int MaxLevel => maxLevel;
}
