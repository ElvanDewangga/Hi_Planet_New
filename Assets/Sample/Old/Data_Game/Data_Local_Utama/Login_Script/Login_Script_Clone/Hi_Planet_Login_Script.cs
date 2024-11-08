using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hi_Planet_Login_Script : Login_Script {
    public override void On_Login () {
       // base.On_Login ();
        Data_Game_Utama.Ins._Data_Game_Account.On_Get_Data_Login_Script (_Data_Local_Utama._Data_Local.Last_Id, _Data_Local_Utama._Data_Local.Last_Password,On_Success_Login,On_Failed_Login);
    }

    #region Data_Local
        public override void On_Have_Last_Id () {
            base.On_Have_Last_Id ();
        }
        
        public override void Off_Have_Last_Id () {
            base.Off_Have_Last_Id ();
        }

    #endregion
    #region Data_Game_Account
    public void On_Failed_Login (string Penyebab_V) {
        if (Penyebab_V == "Id_Salah_Atau_Network") {
            All_Scene_Go.Ins._Tabel_Popup.On_Tabel_Popup_2 ("Error Message", "Id not found or Network Error", new string [] {""}, new string[] {"Confirm"});
            
        } else if (Penyebab_V == "Password_Salah") {
            All_Scene_Go.Ins._Tabel_Popup.On_Tabel_Popup_2 ("Error Message", "Password not correct", new string [] {""}, new string[] {"Confirm"});
        }
        Loading.Ins.Off_Loading ("Loading_2");
    }

    public void On_Success_Login () {
        base.On_Login ();
        Loading.Ins.Off_Loading ("Loading_2");
    }
    #endregion
    public override void On_Login_Confirm_Old_Account () {
        if (IF_Id.text == "") {
            All_Scene_Go.Ins._Tabel_Popup.On_Tabel_Popup_2 ("Error Message", "Id cannot be empty", new string [] {""}, new string[] {"Confirm"});
             
        } else if (IF_Password.text == "") {
            All_Scene_Go.Ins._Tabel_Popup.On_Tabel_Popup_2 ("Error Message", "Password cannot be empty", new string [] {""}, new string[] {"Confirm"});
        } else {
            Loading.Ins.On_Loading ("Loading_2"); 
            base.On_Login_Confirm_Old_Account ();
            On_Login ();
        }
    }
    #region Display
    
    #endregion
}
