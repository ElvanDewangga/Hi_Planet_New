using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions 
{
    public static int[] ConvertStringArrayToIntArray(string[] array) 
    {
        int[] result = new int[array.Length];
        for (int i = 0; i < array.Length; i++) 
        {
            int.TryParse(array[i], out result[i]);
        }
        return result;
    }

    public static string[] ConvertIntArrayToStringArray(int[] array) 
    {
        string[] result = new string[array.Length];
        for (int i = 0; i < array.Length; i++) 
        {
            result[i] = array[i].ToString();
        }
        return result;
    }
}

