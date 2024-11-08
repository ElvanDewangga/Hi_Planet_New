using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Load_Host_Server : MonoBehaviour {

        string Target_Read = "";
        [SerializeField]
        string [] Rows_Target;
        string [] Form_Field, Form_Value;

        string Code_Read_Data_Rows = "";
        // --- Setelah selesai Read :
        public virtual void Search_Row_Target (string V) {
            Rows_Target[0] = "Success";
            if (_Data_Game != null) {
            _Data_Game.On_LHS_Load_Status (Rows_Target);
            }
            /*
            if (V == "Login_Canvas_Ke_Result_Player_Umum" ) {
                Login_Canvas.Ins.Result_Player_Umum = Rows_Target;
                Login_Canvas.Ins.On_Continue_Sign_In_Guest ();
            }
           
           if (V == "Create_Account") {
                Pembuka_Scene.Ins._Bg_First_Login.On_LHS_Create_Account (Rows_Target[1]);
           }
           if (V == "Read_Last_Id") {
                Pembuka_Scene.Ins._Bg_First_Login.LHS_Last_Id (Rows_Target);
           }
           if (V == "Get_Db_Player") {
                Pembuka_Scene.Ins._Bg_Login.LHS_Login (Rows_Target);
           } 
           if (V == "Save_New_Cards") { // Php_Data
                Rows_Target[0] = "Success";
                Universal_Scene.Ins._Php_Data.On_LHS_Save_Status (Rows_Target[0]);
           } 
           if (V == "Load_Card_Pedia") { // Php_Data
                Rows_Target[0] = "Success";
                Universal_Scene.Ins._Php_Data.On_LHS_Load_Status (Rows_Target);
           } 
           if (V == "Load_Db_Deck") {
                Rows_Target[0] = "Success";
                Universal_Scene.Ins._Php_Data.On_LHS_Load_Status (Rows_Target);
            }
            */
        }

        public virtual void Failed_Confirmation (string V) {
            Rows_Target[0] = "Failed";
            if (!_Data_Game !=null) {
            _Data_Game.On_LHS_Load_Status (Rows_Target);
            }
            /*
            if (V == "Create_Account") {
                Pembuka_Scene.Ins._Bg_First_Login.On_LHS_Create_Account (Rows_Target[0]);
            }
            if (V == "Read_Last_Id") {
                Pembuka_Scene.Ins._Bg_First_Login.On_LHS_Create_Account (Rows_Target[0]);
            }
            if (V == "Get_Db_Player") {
                Pembuka_Scene.Ins._Bg_Login.LHS_Login (Rows_Target);
            } 
            if (V == "Save_New_Cards") {
                Rows_Target[0] = "Failed";
                Universal_Scene.Ins._Php_Data.On_LHS_Save_Status (Rows_Target[0]);
            }
            if (V == "Load_Card_Pedia") {
                Rows_Target[0] = "Failed";
                Universal_Scene.Ins._Php_Data.On_LHS_Load_Status (Rows_Target);
            }
            if (V == "Load_Db_Deck") {
                Rows_Target[0] = "Failed";
                Universal_Scene.Ins._Php_Data.On_LHS_Load_Status (Rows_Target);
            }
            */
        }

        Data_Game _Data_Game;
        //  System_Settings : Sedang memuat . . .
        public void Read_Data_Rows (string Code_Php, string Judul_Php, string [] Form_Field_V, string [] Form_Value_V, Data_Game s) {
            Code_Read_Data_Rows = Code_Php; _Data_Game = s;
            Form_Field = Form_Field_V; Form_Value = Form_Value_V;
            // Debug.Log (Judul_Php + Form_Value_V[0]);
            switch(Judul_Php)  {
             
            case "Register_Soul_Card":
                Target_Read = "Register_Soul_Card.php";
                break;
            case "Read_Umum":
                Target_Read = "Read_Umum.php";
                break;
            case "Read_All_Table_1":
                Target_Read = "Read_All_Table_1.php";
                break;
            case "Write_All_Table_Value_20":
                Target_Read = "Write_All_Table_Value_20.php";
                break;
            case "Write_All_Table_Value_Fix":
                Target_Read = "Write_All_Table_Value_Fix.php";
                break;
            default:
                Target_Read = "";
                
                break;
            }
            if (Target_Read != "") {
                 StartCoroutine (N_Read_Data_Rows ());
            } else {
                Debug.LogError ("Tidak ada yang perlu diakses.");
            }
        }

         WWWForm php_form;
        UnityWebRequest Uwr_login;
        
        IEnumerator N_Read_Data_Rows () {
            if (Target_Read != "") {
                yield return new WaitForSeconds (0.05f);
                php_form = new WWWForm ();
                int frm = 0;
                for (frm=0; frm < Form_Field.Length; frm ++) {
                    php_form.AddField (Form_Field[frm], Form_Value[frm]);
                }
           
                
                Uwr_login = UnityWebRequest.Post ((System_Settings.Ins.Php_Link + Target_Read), php_form); //Pastikan URL BEDAKAN (UNTUK SETIAP FUNGSI)
			    yield return Uwr_login.SendWebRequest ();
			    string fulldata = Uwr_login.downloadHandler.text;
                yield return new WaitForSeconds (0.1f);
                Rows_Target = fulldata.Split (';');
                yield return new WaitForSeconds (0.1f);
              
                    if (Uwr_login.result == UnityWebRequest.Result.ConnectionError) {
                        Debug.Log ("ReadError_1");
                        Failed_Confirmation (Code_Read_Data_Rows);
                      //  StartCoroutine (N_Read_Data_Rows ());
                        yield break;
                    }
                    if (Rows_Target.Length >1) { 
                          
                            Search_Row_Target (Code_Read_Data_Rows);
                            Destroy (this.gameObject);
                    } else {
                        Debug.Log ("ReadError_1");
                         Failed_Confirmation (Code_Read_Data_Rows);
                       // StartCoroutine (N_Read_Data_Rows ());
                        yield break;
                    }
                //}  catch (System.IndexOutOfRangeException) {
                   // StartCoroutine (N_Read_Data_Rows ());
                // }
            } else {
                Debug.LogError ("Tidak ada yang perlu diakses");
            }
        }
}
