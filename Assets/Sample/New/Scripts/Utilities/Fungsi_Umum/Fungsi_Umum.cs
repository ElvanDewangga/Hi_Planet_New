using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungsi_Umum : MonoBehaviour {
    void Awake () {
        Ins = this;
    }
    public static Fungsi_Umum Ins;

    public Random_Number _Random_Number;
    public Check_Space _Check_Space;
    public String_Umum _String_Umum;
    public A_String_Umum _A_String_Umum;
    public A_Int_Umum _A_Int_Umum;
}
