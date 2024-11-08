using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification_Script : MonoBehaviour {
    // Test_Button :
    public World_Place_Name _World_Place_Name;
    [SerializeField]
    string Type_Tab, Type_Button = "";
    [SerializeField]
    string Isi = "";
    [SerializeField]
    TMP_Text Notification_Isi_Tx;
   // [SerializeField]
   // Panel_Destroyer _Panel_Destroyer;
    GameObject Ins_Go;
    void On_Notification_2 (string Type_V) {
        // All_Scene_Go_Script.Ins._Popup_Type.Set_Popup_Tab (2, true, Type_V);
    }
    #region Implementation
    void On_Contoh_Pemanggilan () {
        string [] A_Title = new string[1];
        A_Title[0] = "Confirm";
        string [] A_Event = new string[1];
        A_Event[0] = "Replace";
        On_Notification ("Notification", "Notification_1_1", "Inventory sudah Penuh.", A_Title, A_Event);
    }

    // Sample_Scene :
    public void On_Notification (string Type_V, string Type_Button_V, string Isi_V, string [] A_Button_Title, string [] A_Button_Event) {
        Type_Tab = Type_V;  Type_Button = Type_Button_V; Isi = Isi_V;
        /*
        if (Type_Tab == "Notification") {
            _Panel_Destroyer.On_Destroy_All_Clone ();
            Notification_Isi_Tx.text = Isi;
            On_Notification_2 (Type_V); 
            int x = 0;
            for (x=0; x <A_Button_Title.Length; x++) {
                Ins_Go = null;
                if (Type_Button == "Notification") { // Sample_Scene
                    Ins_Go = GameObject.Instantiate (All_Scene_Go_Script.Ins._Button_Event_Server.Button_2);
                } else if (Type_Button == "Notification_1_1") { // Sample_Scene
                    Ins_Go = GameObject.Instantiate (All_Scene_Go_Script.Ins._Button_Event_Server.Button_2_1);
                }
                Clone_Sample Cs = Ins_Go.GetComponent <Clone_Sample> ();
                Cs.On_Set_b_Clone (true);
                Button_Event Be = Ins_Go.GetComponent <Button_Event> ();
                Ins_Go.transform.SetParent (_Panel_Destroyer.Layout);
                Be.On_Set_Event (A_Button_Event[x]); Be.On_Set_Title (A_Button_Title[x]);
            }
        } 
        */
    }

    // Button - Button_Event_Server
    public void On_Set_Event (string v) {
        /*
        if (v == "Close_Notification") {
            All_Scene_Go_Script.Ins._Popup_Type.Set_Popup_Tab (2, false, Type_Tab);
        } else if (v == "Back_Menu") {
             All_Scene_Go_Script.Ins._Popup_Type.Set_Popup_Tab (2, false, Type_Tab);
             All_Scene_Go_Script.Ins._Back_Menu.On_Remove_Menu_Latest ();
        }
        */
        Isi = "";
        Type_Tab = "";
    }
    
    
    #endregion
}
