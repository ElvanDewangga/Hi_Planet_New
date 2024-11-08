using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Mode : MonoBehaviour {
    public bool b_Test_Mode = false;
    public static Game_Mode Ins;
    // Char_Technique
    void Awake () {
        Ins = this;
    }
}
