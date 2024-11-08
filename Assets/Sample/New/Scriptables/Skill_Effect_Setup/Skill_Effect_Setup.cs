using System;
using UnityEngine;

[Serializable]
public class C_Skill_Effect_Setup
{
    [HideInInspector]
    // Char_Status :
    public float Cur_Time; // Waktu Tersisa untuk efek ini.

    public World_Target _World_Target;
    public Skill_Effect[] A_Skill_Effect;
    public int[] A_Int_Value;
}

public enum World_Target
{
    Self,
    Zone_All
}

public enum Skill_Effect
{
    Heal,
    Defense
}

[CreateAssetMenu(fileName = "Skill_Effect_Setup", menuName = "StarSky/Create Skill Effect Setup", order = 1)]
public class Skill_Effect_Setup : ScriptableObject
{
    public string Skill_Effect_Name;
    public float Effect_Time;
    public C_Skill_Effect_Setup[] A_C_Skill_Effect_Setup;
}