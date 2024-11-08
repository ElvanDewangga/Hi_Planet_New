using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Local_Utama : MonoBehaviour {
    public Data_Local _Data_Local;
    public Login_Script _Login_Script;

    void Awake () {
        Ins = this;
        // 218631193 Pass : 1234567890
       // PlayerPrefs.DeleteAll();
    }

    void Start () {
        _Data_Local.On_Check_Last_Id ();
    }

    public static Data_Local_Utama Ins;

}
