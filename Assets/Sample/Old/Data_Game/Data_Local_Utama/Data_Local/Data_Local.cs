using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Local : MonoBehaviour {
    [SerializeField]
    Data_Local_Utama _Data_Local_Utama;
    public string Last_Id = "";
    public string Last_Password = "";
    public virtual void On_Login () {
         _Data_Local_Utama._Data_Local.On_Save_Last_Id ();
         
    }
    public void On_Check_Last_Id () {
        Last_Id = PlayerPrefs.GetString ("Last_Id");
        Last_Password = PlayerPrefs.GetString ("Last_Password");
        if (Last_Id != "") {
            _Data_Local_Utama._Login_Script.On_Have_Last_Id ();
        } else {
            _Data_Local_Utama._Login_Script.Off_Have_Last_Id ();
        }
    }
    #region Login_Script
    public void On_Set_Last_Id (string Id_V, string Pass_V) {
        Last_Id = Id_V; Last_Password = Pass_V;
    }

    public void On_Save_Last_Id () {
        PlayerPrefs.SetString ("Last_Id", Last_Id);
        PlayerPrefs.SetString ("Last_Password", Last_Password);
    }
    #endregion
}
