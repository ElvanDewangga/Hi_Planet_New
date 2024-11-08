using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Some_Text : MonoBehaviour
{
   public string On_Remove_Some_Text (string v) {
        string Result = v;
        Result = Result.Replace ("Syarat", "");
        Result = Result.Replace ("Effect", "");
        Result = Result.Replace ("((", "(");
        Result = Result.Replace ("))", ")");
        return Result;
    
    }
}
