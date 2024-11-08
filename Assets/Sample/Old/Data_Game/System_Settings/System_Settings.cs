using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Settings : MonoBehaviour {
   // Starsky :
    public string Php_Link = "http://liwebgames.com/Php_Hi_Planet/";
    [SerializeField]
    GameObject Load_Host_Server_GO_Samp;
    public static System_Settings Ins;
    void Awake () {
        Ins = this;
    }

    public void On_Test () {
            string [] Host_Server_Value = new string [4]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [4];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = Data_Game_Utama.Ins._Data_Game_Account.Id;
            Host_Server_Field[1] = "table_1";Host_Server_Value[1] = "Db_Player";
            Host_Server_Field[2] = "title_1";Host_Server_Value[2] = "Tutorial";
            Host_Server_Field[3] = "value_1";Host_Server_Value[3] = "1";

        On_Host_Server_GO ("Save_Status", "Write_All_Table_Value_Fix", Host_Server_Field, Host_Server_Value, null); 
    }
    void Example_On_Host_Server_GO () {
        // int Total_Table = 4;
            string [] Host_Server_Value = new string [4]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [4];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = "21379103749";
            Host_Server_Field[1] = "table_1";Host_Server_Value[1] = "Db_Player";
            Host_Server_Field[2] = "title_1";Host_Server_Value[2] = "Gold";
            Host_Server_Field[3] = "value_1";Host_Server_Value[3] = "50000";

        On_Host_Server_GO ("Save_Status", "Write_All_Table_Value_20", Host_Server_Field, Host_Server_Value, null);
    }

    // Bg_First_Login :
    public virtual void On_Host_Server_GO (string Rows_Target_V, string Judul_Php, string [] Form_Field_V, string [] Form_Value_V, Data_Game Data_Game_V) {
        // Mengecek Spasi diganti dengan _ :
        int i = 0;
        for (i=0; i < Form_Value_V.Length; i++) {
         //   Debug.LogError (Form_Value_V[i]);
            Form_Value_V[i] = Fungsi_Umum.Ins._Check_Space.On_Check_Space (Form_Value_V[i]);
        }
       
        GameObject Ins = GameObject.Instantiate (Load_Host_Server_GO_Samp);
        Ins.SetActive (true);
        Ins.GetComponent <Load_Host_Server> ().Read_Data_Rows (Rows_Target_V, Judul_Php, Form_Field_V, Form_Value_V, Data_Game_V);
        
    }
}
