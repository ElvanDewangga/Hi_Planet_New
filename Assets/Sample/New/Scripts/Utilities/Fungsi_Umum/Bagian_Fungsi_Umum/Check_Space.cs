using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Space : MonoBehaviour
{
    public string On_Check_Space (string V) {
        string Result = "";
        if (V != "") {

            char[] A_Text = V.ToCharArray();

            int i = 0;
            for (i=0; i < A_Text.Length; i++) {
                if (A_Text[i] == ' ') {A_Text[i] = '_';}
            }

            // Tar.text = "";
            int j = 0;
            for (j=0; j < A_Text.Length; j++) {
                Result = Result + A_Text[j];
            }
        }
        return Result;
    }
}
