using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Number : MonoBehaviour
{
    #region Random_Number
    int On_Random_Number () {
        int v = UnityEngine.Random.Range (0,10);
        return v;
    }
    // BG_First_Login :
    public string On_Random_Password () {
        string x = On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString () + On_Random_Number().ToString ();
        return x;
    } 
    #endregion
}
