using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Data_Game_Account : MonoBehaviour {
      // Data_Game_Equipment :
    public string Id = "";
    public string Username = "";
    public string Password = "";

    void On_Start_Load_Player () {
          Loading.Ins.On_b_Loading_Invisible ("Loading_1", On_Start_Load_Player_2,0);
          StartCoroutine (N_On_Start_Load_Player ());
    }

    IEnumerator N_On_Start_Load_Player () {
      yield return new WaitForSeconds (2.0f);
      Data_Game_Utama.Ins._Data_Game_Inventory.StartLoad (Id);
          Data_Game_Utama.Ins._Data_Game_Storage.StartLoad (Id);
    }

    void On_Start_Load_Player_2 () {
        Data_Game_Utama.Ins._Data_Game_Equipment.StartLoad (Id);
    }

    #region Data_Game_Source
    public void On_Get_Data_Game_Source (string [] Rows) {
      Username = Rows[1];
      Password = Rows[2];
        
      if (Username != "" && Password != "" && Password == Login_Script_Password) { 
        On_Start_Load_Player ();
        Login_Script_Success_Action ();
        
      } else {
          if (Username == "") {
            On_Invalid_Account ("Id_Salah_Atau_Network");
          } else if (Password != "" && Password != Login_Script_Password) {
            On_Invalid_Account ("Password_Salah");
          }
      } 
      
    }  
    #endregion
    #region Invalid_Account
      void On_Invalid_Account (string Penyebab_V) {
        Login_Script_Failed_Action (Penyebab_V);
      }
    #endregion
    #region Hi_Planet_Login_Script
      string Login_Script_Password = "";
      UnityAction Login_Script_Success_Action;
      UnityAction <string> Login_Script_Failed_Action;
      public void On_Get_Data_Login_Script (string Id_V, string Password_Input_V, UnityAction Success_Action, UnityAction <string> Failed_Action) {
        Id = Id_V; Login_Script_Success_Action = Success_Action; Login_Script_Password= Password_Input_V; Login_Script_Failed_Action = Failed_Action;
        Data_Game_Utama.Ins._Data_Game_Player.On_Start_Load (Id);
      }
    #endregion
}
