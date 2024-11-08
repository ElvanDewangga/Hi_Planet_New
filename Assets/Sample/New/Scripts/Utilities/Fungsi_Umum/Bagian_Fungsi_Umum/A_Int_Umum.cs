using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class A_Int_Umum : MonoBehaviour
{
    public string [] On_A_Int_To_A_String (int [] b) {
        string[] stringArray = b.Select(num => num.ToString()).ToArray(); // Mengonversi array string ke array int menggunakan LINQ
        return stringArray;
    }

    public string On_A_Int_To_String (int [] b) {
        string Result = "";
        int x= 0;
        for (x=0; x < b.Length; x++) {
            if (x == b.Length-1) { // akhir
                Result = Result +b[x].ToString ();
            } else { // awal dan tengah
                Result = Result + b[x].ToString ()+":";
            }
        }
        return Result;
    }
}
