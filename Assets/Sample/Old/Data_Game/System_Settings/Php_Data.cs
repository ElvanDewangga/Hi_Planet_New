using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using System;
using UnityEditor;
using Starsky;

public class Php_Data : MonoBehaviour { 
    /*
   #region Save_Status
   string Event_Start_Save_Status, Event_Finish_Save_Status;
        void On_Refresh_Save_Status () {
            Card_Pedia_Edit_Deck = null;
            b_Write_All_Table_Value_20_Tidak_Cukup = false;
        }
    // this, this - Popup (When failed connect) :
        public void On_Start_Save_Status () {
            Universal_Scene.Ins._Loading.On_b_Loading_Invisible ("Start_Save_Status", 0);
            Universal_Scene.Ins._Loading.On_Add_Loading_Invisible ("Php_Data_Save_Status");
        
            Type t = Type.GetType("Php_Data");
            // BindingFlags.Instance | BindingFlags.NonPublic.
            MethodInfo method 
                = t.GetMethod(Event_Start_Save_Status, BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(this, null);
        }

        public void On_Finish_Save_Status () {
            Type t = Type.GetType("Php_Data");
            // BindingFlags.Instance | BindingFlags.NonPublic.
            MethodInfo method 
                = t.GetMethod(Event_Finish_Save_Status, BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(this, null);
            
        }

        // Load_Host_Server
        public void On_LHS_Save_Status (string Res) {
             
            if (Res == "Failed") {
                Universal_Scene.Ins._Loading.On_Remove_Loading_Invisible ("Php_Data_Save_Status");
                 string [] A_Text = new string [1]; string [] A_Event = new string [1];
                A_Text [0] = "Refresh"; A_Event[0] = "Start_Save_Status"; 
                Universal_Scene.Ins._Popup.On_Popup_Box_Choice ("Connection Error", "Please check your connection and try again.", A_Text, A_Event);
            } else if (Res == "Success") {
                if (!b_Write_All_Table_Value_20_Tidak_Cukup) {
                    Universal_Scene.Ins._Loading.On_Remove_Loading_Invisible ("Php_Data_Save_Status");
                    On_Finish_Save_Status ();
                } else {
                    On_Write_All_Table_Value_20 ();
                }
            }
        }
   #endregion 
   #region New_Cards
        #region Save_New_Cards
            #region Testing
            // Button (Sementara) :
            public void On_Add_Save_New_Cards_A_Card () {
                List <String> Got_Cards = new List<string> ();
                List <String> Got_Owns = new List<string> ();
                Got_Cards.Add ("1"); Got_Owns.Add ("1");
                Got_Cards.Add ("2"); Got_Owns.Add ("1");
                Got_Cards.Add ("3"); Got_Owns.Add ("1");
                Got_Cards.Add ("4"); Got_Owns.Add ("1");
                Got_Cards.Add ("5"); Got_Owns.Add ("1");

                On_Save_New_Cards (Got_Cards, Got_Owns);
            }

            public void On_Add_Save_New_Cards_A_Card_2 () {
                List <String> Got_Cards = new List<string> ();
                List <String> Got_Owns = new List<string> ();
                Got_Cards.Add ("1"); Got_Owns.Add ("1");
                Got_Cards.Add ("6"); Got_Owns.Add ("1");
                Got_Cards.Add ("7"); Got_Owns.Add ("1");
                Got_Cards.Add ("8"); Got_Owns.Add ("1");
                Got_Cards.Add ("9"); Got_Owns.Add ("1");
                On_Save_New_Cards (Got_Cards, Got_Owns);
            }
            #endregion
        [SerializeField]
        List <string> Save_New_Cards_A_Cards = new List<string> ();
        [SerializeField]
        List <string> Save_New_Cards_A_Owns = new List<string> ();
        
        void On_Save_New_Cards (List <string> A_Cards_V, List <string> A_Owns_V) {
            On_Refresh_Save_Status ();
            Save_New_Cards_A_Cards = A_Cards_V; Save_New_Cards_A_Owns = A_Owns_V;
            Event_Start_Save_Status = "On_Start_Save_New_Cards";
            Event_Finish_Save_Status = "On_Finish_Save_New_Cards";
            On_Start_Save_Status ();
        }

        
        // this with invoke
        public void On_Start_Save_New_Cards () {
            int Val = -1;
            foreach (string s in Save_New_Cards_A_Cards) {
                Val++;
                C_Int Code_Urt = Universal_Scene.Ins.On_Check_Value_On_List_Object_Script (s, L_Card_Pedia);
                if (Code_Urt.Int_1 == -1) {
                    On_Add_New_Card_To_Card_Pedia (Save_New_Cards_A_Cards[Val], Save_New_Cards_A_Owns[Val]);
                } else {
                    int.TryParse (Save_New_Cards_A_Owns[Val], out int New_Own);
                    L_Card_Pedia[Code_Urt.Int_1].On_Add_Own (Code_Urt.Int_2, New_Own); 
                }
            }

            foreach (Card_Pedia Cp in L_Card_Pedia) {
                if (Cp.b_Value_Change) {
                    Cp.On_Save_Status (); 
                }
            }

            On_Write_All_Table_Value_20 ();
          
        }
        // this with invoke
        public void On_Finish_Save_New_Cards () {
            Debug.LogError ("Already Saved");
        }

        #endregion
        #region Save_Edit_Deck
        // Edit_Deck :
        int Id_Edit_Deck;
        Card_Pedia Card_Pedia_Edit_Deck;
        string Name_Edit_Deck;
        public void On_Save_Edit_Deck (int Id, Card_Pedia V, string Name_Deck_V) {
            
            On_Refresh_Save_Status ();
            Id_Edit_Deck = Id;
            Card_Pedia_Edit_Deck = V;
            Name_Edit_Deck = Name_Deck_V;
            Event_Start_Save_Status = "On_Start_Save_Edit_Deck";
            Event_Finish_Save_Status = "On_Finish_Save_Edit_Deck";
            On_Start_Save_Status ();
        }

        

        
        // this with invoke
        public void On_Start_Save_Edit_Deck () {
            Card_Pedia_Edit_Deck.On_Convert_Only_For_Save ();
            On_Write_All_Table_Value_20 ();
        }
        // this with invoke
        public void On_Finish_Save_Edit_Deck () {
            Debug.LogError ("Deck Already Saved");
            string [] A_Text = new string [1]; string [] A_Event = new string [1];
            A_Text [0] = "Okay"; A_Event[0] = ""; 
            Universal_Scene.Ins._Popup.On_Popup_Box_Choice ("Save Success", "The deck has been saved.", A_Text, A_Event);
        }
        #endregion
        #region Save_Delete_Deck
        // My_Deck :
        List <int> L_Id_Save_Delete_Deck = new List<int> ();
        public void On_Save_Delete_Deck (List <int> L_Id) {
            
            On_Refresh_Save_Status ();
            L_Id_Save_Delete_Deck = L_Id;
            Event_Start_Save_Status = "On_Start_Save_Delete_Deck";
            Event_Finish_Save_Status = "On_Finish_Save_Delete_Deck";
            On_Start_Save_Status ();
        }

        

        
        // this with invoke
        public void On_Start_Save_Delete_Deck () {
            On_Write_All_Table_Value_20 ();
        }
        // this with invoke
        public void On_Finish_Save_Delete_Deck () {
            Debug.LogError ("Deck Already Delete"); 
            Home_Scene.Ins._My_Deck.On_Finish_Confirm_Delete_Deck ();
        }
        #endregion
        
   #endregion
   #region Write_All_Table_Value_20
    [SerializeField]
   List <Card_Pedia> L_Save_Card_Pedia = new List<Card_Pedia> ();
   public void On_Add_L_Save_Card_Pedia (Card_Pedia Cp) {
        L_Save_Card_Pedia.Add (Cp);
   }

    bool b_Write_All_Table_Value_20_Tidak_Cukup = false; // blm dipakai, ini dipakai jika table_value > 20
    void On_Write_All_Table_Value_20 () {
        b_Write_All_Table_Value_20_Tidak_Cukup = false;
        if (Event_Start_Save_Status == "On_Start_Save_New_Cards") {
            int Total_Table = 2;
            string [] Host_Server_Value = new string [1 + (L_Save_Card_Pedia.Count * 3 * Total_Table)]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [1 + (L_Save_Card_Pedia.Count * 3 * Total_Table)];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = Universal_Scene.Ins._Data_Player.Id;
            int x = 0;
            int xy = 0; int Num = -1;
            for (x= 0; x < L_Save_Card_Pedia.Count; x++) {
                xy ++; Num++;
                if (Num >=Total_Table *9) { // hasil tidak boleh lebih dari 20.
                    // 18. Kalau misal, dalam 1 [For] berisi 3 table, usahakan kasih batas dengan jumlah Listnya, Contoh (3 table = [0,1,2 - 3,4,5 - 6,7,8, dst 15-16-17, maka jika ke-18 pending proses listnya dulu])
                    // kalau 4 table maka : boleh pending di 20/16 (karena angka terakhir dari 4 table : 12,13,14,15 adalah 16)
                    // Sudah DI TEST. Jika ingin test cukup kecilin batas maksimal Numnya >= Total_Table
                    goto Slot_Tidak_Cukup;
                }
                Host_Server_Field[xy] = "table_" +Num.ToString ();
                Host_Server_Value[xy] = "Db_Card_Pedia"; 
                xy ++;
                Host_Server_Field[xy] = "title_" +Num.ToString ();
                Host_Server_Value[xy] = "Card_Pedia_Id_" + L_Save_Card_Pedia[x].Title_Code.ToString ();
                xy ++;
                Host_Server_Field[xy] = "value_" +Num.ToString ();
                Host_Server_Value[xy] = L_Save_Card_Pedia[x].Get_String ("Card_Pedia_Id");
                xy ++; Num ++;
                if (Num >=Total_Table *9) { // 18
                    goto Slot_Tidak_Cukup;
                }
                Host_Server_Field[xy] = "table_" +Num.ToString ();
                Host_Server_Value[xy] = "Db_Card_Pedia"; 
                xy ++;
                Host_Server_Field[xy] = "title_" +Num.ToString ();
                Host_Server_Value[xy] = "Card_Pedia_Own_" + L_Save_Card_Pedia[x].Title_Code.ToString ();
                xy ++;
                Host_Server_Field[xy] = "value_" +Num.ToString ();
                Host_Server_Value[xy] = L_Save_Card_Pedia[x].Get_String ("Card_Pedia_Own");

                
                
                Slot_Tidak_Cukup :
                if (Num >=Total_Table *9) {
                    int i = Num;
                        while (i > -1) 
                        {
                        i -=Total_Table;
                        L_Save_Card_Pedia.RemoveAt (0);
                        }
                    b_Write_All_Table_Value_20_Tidak_Cukup = true;
                    Debug.LogError ("Tidak Cukup Php_Data"); 
                }
            }
            
            System_Settings.Ins.On_Host_Server_GO ("Save_New_Cards", "Write_All_Table_Value_20", Host_Server_Field, Host_Server_Value);

            

            
        } else if (Event_Start_Save_Status == "On_Start_Save_Edit_Deck") {
            int Total_Table = 2;
            string [] Host_Server_Value = new string [1 + (1 * 3 * Total_Table)]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [1 + (1 * 3 * Total_Table)];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = Universal_Scene.Ins._Data_Player.Id;

                Host_Server_Field[1] = "table_0";
                Host_Server_Value[1] = "Db_Deck"; 
                Host_Server_Field[2] = "title_0";
                Host_Server_Value[2] = "Deck_" + Id_Edit_Deck.ToString ();
                Host_Server_Field[3] = "value_0";
                Host_Server_Value[3] = Card_Pedia_Edit_Deck.Get_String("Card_Pedia_Id");

                Host_Server_Field[4] = "table_1";
                Host_Server_Value[4] = "Db_Deck"; 
                Host_Server_Field[5] = "title_1";
                Host_Server_Value[5] = "Deck_Keterangan_" + Id_Edit_Deck.ToString ();
                Host_Server_Field[6] = "value_1";
                Host_Server_Value[6] =  Name_Edit_Deck;

                System_Settings.Ins.On_Host_Server_GO ("Save_New_Cards", "Write_All_Table_Value_20", Host_Server_Field, Host_Server_Value);
        } else if (Event_Start_Save_Status == "On_Start_Save_Delete_Deck") {
               int Total_Table = 2;
            string [] Host_Server_Value = new string [1 + (L_Id_Save_Delete_Deck.Count * 3 * Total_Table)]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [1 + (L_Id_Save_Delete_Deck.Count * 3 * Total_Table)];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = Universal_Scene.Ins._Data_Player.Id;
            int x = 0;
            int xy = 0; int Num = -1;
            for (x= 0; x < L_Id_Save_Delete_Deck.Count; x++) {
                xy ++; Num++;
                if (Num >=Total_Table *9) { // hasil tidak boleh lebih dari 20.
                    // 18. Kalau misal, dalam 1 [For] berisi 3 table, usahakan kasih batas dengan jumlah Listnya, Contoh (3 table = [0,1,2 - 3,4,5 - 6,7,8, dst 15-16-17, maka jika ke-18 pending proses listnya dulu])
                    // kalau 4 table maka : boleh pending di 20/16 (karena angka terakhir dari 4 table : 12,13,14,15 adalah 16)
                    // Sudah DI TEST. Jika ingin test cukup kecilin batas maksimal Numnya >= Total_Table
                    goto Slot_Tidak_Cukup;
                }
                Host_Server_Field[xy] = "table_" +Num.ToString ();
                Host_Server_Value[xy] = "Db_Deck"; 
                xy ++;
                Host_Server_Field[xy] = "title_" +Num.ToString ();
                Host_Server_Value[xy] = "Deck_" + L_Id_Save_Delete_Deck[x].ToString ();
                xy ++;
                Host_Server_Field[xy] = "value_" +Num.ToString ();
                Host_Server_Value[xy] = "";
                xy ++; Num ++;
                if (Num >=Total_Table *9) { // 18
                    goto Slot_Tidak_Cukup;
                }
                Host_Server_Field[xy] = "table_" +Num.ToString ();
                Host_Server_Value[xy] = "Db_Deck"; 
                xy ++;
                Host_Server_Field[xy] = "title_" +Num.ToString ();
                Host_Server_Value[xy] = "Deck_Keterangan_" + L_Id_Save_Delete_Deck[x].ToString ();
                xy ++;
                Host_Server_Field[xy] = "value_" +Num.ToString ();
                Host_Server_Value[xy] = "";
                
                Slot_Tidak_Cukup :
                if (Num >=Total_Table *9) {
                    int i = Num;
                        while (i > -1) 
                        {
                        i -=Total_Table;
                        L_Id_Save_Delete_Deck.RemoveAt (0);
                        }
                    b_Write_All_Table_Value_20_Tidak_Cukup = true;
                }
            }
            
            System_Settings.Ins.On_Host_Server_GO ("Save_New_Cards", "Write_All_Table_Value_20", Host_Server_Field, Host_Server_Value);
        }
        L_Save_Card_Pedia = new List<Card_Pedia> ();
         
    }
   #endregion
   #region Load_Status
   [SerializeField]
   string [] Load_Status_Result = new string[0];

   string Event_Start_Load_Status, Event_Finish_Load_Status = "";
   // this, Popup :
        public void On_Start_Load_Status () {
             Universal_Scene.Ins._Loading.On_b_Loading_Invisible ("Start_Load_Status", 0);
            Universal_Scene.Ins._Loading.On_Add_Loading_Invisible ("Php_Data_Load_Status");

            Type t = Type.GetType("Php_Data");
            // BindingFlags.Instance | BindingFlags.NonPublic.
            MethodInfo method 
                = t.GetMethod(Event_Start_Load_Status, BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(this, null);
            
        }
        
        public void On_Finish_Load_Status () {
            Type t = Type.GetType("Php_Data");
            // BindingFlags.Instance | BindingFlags.NonPublic.
            MethodInfo method 
                = t.GetMethod(Event_Finish_Load_Status, BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(this, null);
            
        }

        // Load_Host_Server
        public void On_LHS_Load_Status (string [] Res) {
            Load_Status_Result = Res;
             Universal_Scene.Ins._Loading.On_Remove_Loading_Invisible ("Php_Data_Load_Status");
            if (Res[0] == "Failed") {
                string [] A_Text = new string [1]; string [] A_Event = new string [1];
                A_Text [0] = "Refresh"; A_Event[0] = "Start_Load_Status"; 
                Universal_Scene.Ins._Popup.On_Popup_Box_Choice ("Connection Error", "Please check your connection and try again.", A_Text, A_Event);
            } else if (Res[0] == "Success") {
                On_Finish_Load_Status ();
            }

           
        }
   #endregion
   #region Card_Pedia
        [SerializeField]
        Transform A_Card_Pedia;
        // Edit_Deck :
        public Transform On_Get_A_Card_Pedia () {
            return A_Card_Pedia;
        }

        [SerializeField]
        Card_Pedia Card_Pedia_Samp;
        [SerializeField]
        List <Card_Pedia> L_Card_Pedia;
        [Header ("Value maksimum dalam satu rows adalah ....")]
        public int Max_Value_Card_Pedia = 50;   
        // Card_Pedia : 
        public int Max_Own_Card_Pedia = 1;
        #region Load_Card_Pedia
        // Home_Scene :
        public void On_Load_Card_Pedia () {
            Event_Start_Load_Status = "On_Start_Load_Card_Pedia";
            Event_Finish_Load_Status = "On_Finish_Load_Card_Pedia";
            On_Start_Load_Status ();
        }

        // this with invoke
        public void On_Start_Load_Card_Pedia () {
             string [] Host_Server_Field = new string [2]; Host_Server_Field[0] = "Id"; Host_Server_Field[1] = "Table";
            string [] Host_Server_Value = new string [2]; Host_Server_Value[0] = Universal_Scene.Ins._Data_Player.Id; Host_Server_Value[1] = "Db_Card_Pedia";
            System_Settings.Ins.On_Host_Server_GO ("Load_Card_Pedia", "Read_All_Table_1", Host_Server_Field, Host_Server_Value);
            
        }
        // this with invoke
        public void On_Finish_Load_Card_Pedia () {
            int x = 2;
            for (x = 2; x <Load_Status_Result.Length; x+=2) {
               if (Load_Status_Result[x] != "") {
                GameObject Ins = GameObject.Instantiate (Card_Pedia_Samp.gameObject);
                Ins.transform.SetParent (A_Card_Pedia);
                Ins.GetComponent <Card_Pedia> ().On_Input_Data (Load_Status_Result[x], Load_Status_Result[x+1]);
                Ins.SetActive (true);
                L_Card_Pedia.Add (Ins.GetComponent <Card_Pedia> ());
                Ins.GetComponent <Card_Pedia> ().On_Input_Title_Code (L_Card_Pedia.Count);
               }
            }

            On_Load_Db_Deck ();
        }
        #endregion

        #region Create_Card_Pedia (Saving & Load Card_Pedia)
        List <string> L_New_Card_Code_Id, L_New_Card_Code_Own = new List<string> ();
        void On_Add_New_Card_To_Card_Pedia (string Id, string Own) {
            bool b_Create_New_Card_Pedia = true;
            if (L_Card_Pedia.Count >0) {
                b_Create_New_Card_Pedia = L_Card_Pedia [L_Card_Pedia.Count-1].On_Add_Data (Id,Own);
            } else {
                b_Create_New_Card_Pedia = true;
            }

           if (b_Create_New_Card_Pedia) {
                // L_New_Card_Code_Id.Add (Id); L_New_Card_Code_Own.Add (Own);
               On_Create_Card_Pedia (Id,Own);
           } 
        }


        void On_Create_Card_Pedia (string Id, string Own) {
            GameObject Ins = GameObject.Instantiate (Card_Pedia_Samp.gameObject);
                Ins.transform.SetParent (A_Card_Pedia);
                Ins.GetComponent <Card_Pedia> ().On_Add_Data (Id,Own);
                Ins.SetActive (true);
                L_Card_Pedia.Add (Ins.GetComponent <Card_Pedia> ());
                Ins.GetComponent <Card_Pedia> ().On_Input_Title_Code (L_Card_Pedia.Count);
            
        }

        
        #endregion
        #region Load_Db_Deck
        int Total_Maximum_Db_Deck = 10;
        // My_Deck :
        public void On_Load_Db_Deck () {
            Event_Start_Load_Status = "On_Start_Load_Db_Deck";
            Event_Finish_Load_Status = "On_Finish_Load_Db_Deck";
            On_Start_Load_Status ();
        }

        // this with invoke
        public void On_Start_Load_Db_Deck () {
             string [] Host_Server_Field = new string [2]; Host_Server_Field[0] = "Id"; Host_Server_Field[1] = "Table";
            string [] Host_Server_Value = new string [2]; Host_Server_Value[0] = Universal_Scene.Ins._Data_Player.Id; Host_Server_Value[1] = "Db_Deck";
            System_Settings.Ins.On_Host_Server_GO ("Load_Db_Deck", "Read_All_Table_1", Host_Server_Field, Host_Server_Value);
            
        }
        // this with invoke
        public void On_Finish_Load_Db_Deck () {
            string [] A_Deck = new string [Total_Maximum_Db_Deck];
            string [] A_Deck_Keterangan = new string [Total_Maximum_Db_Deck];

            int x = 2; int Val = -1; // x = 2 karena ada Id dan username
            for (x = 2; x <2 + Total_Maximum_Db_Deck; x++) {
                Val ++;
                Debug.Log (Val);
               A_Deck [Val] = Load_Status_Result [x];
               A_Deck_Keterangan [Val] = Load_Status_Result [x + Total_Maximum_Db_Deck];
            }
            Home_Scene.Ins._My_Deck.On_My_Deck_Utama_2 (A_Deck, A_Deck_Keterangan);
        }
        #endregion
   #endregion
    */
}
