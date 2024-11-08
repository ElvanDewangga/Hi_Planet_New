using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Data_Game : MonoBehaviour {
    [HideInInspector]
    public string Code_Option = ""; // "Save". "Load"
    public string Code_Data_Game = "";
    public virtual void On_Start_Save () {
        Code_Option = "Save";
    }

    public virtual void On_Start_Save (string [] Host_Server_Field, string [] Host_Server_Value) {
        Code_Option = "Save";
    }

    public virtual void On_Finish_Save () {
       // Debug.Log ("Save Game");
       
    }

    public virtual void On_Start_Load (string Id) {
        Code_Option = "Load";
        Data_Game_Utama.Ins._Data_Game_Source.On_Add_Loading (Code_Data_Game);
    }

    public virtual void On_Finish_Load () {
        Data_Game_Utama.Ins._Data_Game_Source.On_Remove_Loading (Code_Data_Game);
    }

    public string [] Load_Status_Result; 
    public virtual void On_LHS_Load_Status (string [] Res) {
    
            Load_Status_Result = Res;
            if (Res[0] == "Failed") {
                Data_Game_Utama.Ins._Data_Game_Source.On_Failed_Load (this);
            } else if (Res[0] == "Success") {
                if (Code_Option == "Load") {
                    On_Finish_Load ();
                } else if (Code_Option == "Save") {
                    On_Finish_Save ();
                }
            }
    }
}
