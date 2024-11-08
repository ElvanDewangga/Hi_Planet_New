using System.Collections.Generic;
using UnityEngine;

public class Char_Data_Level : MonoBehaviour
{
    public int Max_Level = 120;
    private Dictionary<int, int> Dict_Level_Up_Exp = new();

    private void Start()
    {
        Dict_Level_Up_Exp = new Dictionary<int, int>();
        Dict_Level_Up_Exp.Add(1, 20);
        Dict_Level_Up_Exp.Add(2, 30);
        Dict_Level_Up_Exp.Add(3, 50);
        Dict_Level_Up_Exp.Add(4, 80);
        Dict_Level_Up_Exp.Add(5, 120);
        Dict_Level_Up_Exp.Add(6, 170);
        Dict_Level_Up_Exp.Add(7, 230);
        Dict_Level_Up_Exp.Add(8, 300);
        Dict_Level_Up_Exp.Add(9, 380);
        Dict_Level_Up_Exp.Add(10, 470);

        Dict_Level_Up_Exp.Add(11, 470);
        Dict_Level_Up_Exp.Add(12, 470);
        Dict_Level_Up_Exp.Add(13, 470);
        Dict_Level_Up_Exp.Add(14, 470);
        Dict_Level_Up_Exp.Add(15, 470);
        Dict_Level_Up_Exp.Add(16, 470);
        Dict_Level_Up_Exp.Add(17, 470);
        Dict_Level_Up_Exp.Add(18, 470);
    }

    // Char_Level :
    public int On_Get_Next_Target_Exp(int Level_Up)
    {
        return Dict_Level_Up_Exp[Level_Up];
    }
}