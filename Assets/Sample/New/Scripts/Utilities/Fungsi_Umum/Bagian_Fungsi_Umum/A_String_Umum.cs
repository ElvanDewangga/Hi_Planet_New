using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class A_String_Umum : MonoBehaviour {
    public int [] On_A_String_To_A_Int (string [] b) {
        int[] intArray = b.Select(s => {
        if (int.TryParse(s, out int result)) {
            return result;
        }
        return 1; // Ganti elemen yang tidak bisa dikonversi dengan 1
        }).ToArray();
        return intArray;
    }

    public string On_A_String_To_String (string [] b) {
        string Result = "";
        int x= 0;
        for (x=0; x < b.Length; x++) {
            if (x == b.Length-1) { // akhir
                Result = Result +b[x];
            } else { // awal dan tengah
                Result = Result + b[x]+":";
            }
        }
        return Result;
    }
}
