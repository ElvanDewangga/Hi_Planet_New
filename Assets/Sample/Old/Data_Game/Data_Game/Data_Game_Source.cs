using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Game_Source : MonoBehaviour {
    #region Data_Game
    List <Data_Game> L_Data_Game_Failed = new List<Data_Game> ();
    public void On_Failed_Load (Data_Game Si) {
     L_Data_Game_Failed.Add (Si);
    }
    #endregion
    #region Data_Game_Inventory
    public void On_Send_Data_To_Inventory (string []Result) {
        int.TryParse (Result[2], out int Max_Slot);
        All_Scene_Go.Ins._Inventory.On_Get_Data (Max_Slot, Fungsi_Umum.Ins._String_Umum.On_String_To_A_String (Result[3]),  Fungsi_Umum.Ins._String_Umum.On_String_To_A_String (Result[4]));
    }
    #endregion
    #region Data_Game_Equipment
    public void On_Send_Data_To_Equipment (string [] Result) {
        Char_Data.Ins.Your_Char_Utama.GetComponent<Char_Utama> ().On_Get_Data_Game_Source (Result);
    }
    #endregion
    #region Data_Game_Player
    public void On_Send_Data_To_Player (string [] Result) {
        Data_Game_Utama.Ins._Data_Game_Account.On_Get_Data_Game_Source (Result);
        Char_Data.Ins.Your_Char_Utama.GetComponent<Char_Utama> ().On_Get_Data_Account_Game_Source (Result);
        All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Username (Data_Game_Utama.Ins._Data_Game_Account.Username);
        Char_Data.Ins.Your_Char_Utama_Script._Network_Char_Utama.On_Get_Data_From_Data_Game_Source (Result [5]);
    }
    #endregion
    #region Data_Game_Storage
    public void On_Send_Data_To_Storage (string []Result) {
        int.TryParse (Result[2], out int Max_Slot);
        All_Scene_Go.Ins._Storage.On_Get_Data (Max_Slot, Fungsi_Umum.Ins._String_Umum.On_String_To_A_String (Result[3]),  Fungsi_Umum.Ins._String_Umum.On_String_To_A_String (Result[4]));
    }
    #endregion
    #region Remove_Loading
    public void On_Add_Loading (string Code) {
        Loading.Ins.On_Add_Loading_Invisible (Code);
    }

    public void On_Remove_Loading (string Code) {
        Loading.Ins.On_Remove_Loading_Invisible (Code);
    }
    #endregion
}
