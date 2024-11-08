using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Game_Player : Data_Game
{
    
    public override void On_Start_Save () {

    }

    public override void On_Finish_Save () {

    }

    public override void On_Start_Load (string Id) {
        string [] Host_Server_Field = new string [2]; Host_Server_Field[0] = "Id"; Host_Server_Field[1] = "Table";
        string [] Host_Server_Value = new string [2]; Host_Server_Value[0] = Id; Host_Server_Value[1] = "Db_Player";
        System_Settings.Ins.On_Host_Server_GO ("Load_Db_Deck", "Read_All_Table_1", Host_Server_Field, Host_Server_Value, this);
    }

    public override void On_Finish_Load () {
        Data_Game_Utama.Ins._Data_Game_Source.On_Send_Data_To_Player (Load_Status_Result);
    }

    public override void On_LHS_Load_Status (string [] Res) {
            Load_Status_Result = Res;
            if (Res[0] == "Failed") {
                Data_Game_Utama.Ins._Data_Game_Source.On_Failed_Load (this);
            } else if (Res[0] == "Success") {
                On_Finish_Load ();
            }

    }
}
