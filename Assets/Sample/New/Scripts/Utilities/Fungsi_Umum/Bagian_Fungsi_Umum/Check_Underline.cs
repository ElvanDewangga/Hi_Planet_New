using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Underline : MonoBehaviour {
    public string On_Check_Underline(string V) {
        string Result = "";
        char[] A_Text = V.ToCharArray();

        int i = 0;
        for (i=0; i < A_Text.Length; i++) {
            if (A_Text[i] == '_') {A_Text[i] = ' ';}
        }

        // Tar.text = "";
        int j = 0;
        for (j=0; j < A_Text.Length; j++) {
            Result = Result + A_Text[j];
        }
        return Result;
    }
    
}
