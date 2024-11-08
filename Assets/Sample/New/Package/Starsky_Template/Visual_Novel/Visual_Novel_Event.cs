using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Visual_Novel_Event : MonoBehaviour {

   // Pengubah_String - String_Change_String - Replace_String - Regex_Format_1_Event :
   #region Event
   public string Event {get; set;}
   public string Line_1 {get;set;}
   public string Line_2 {get;set;}

   public void On_Give_Data (string Event_V, string Line_1_V, string Line_2_V) {
    Event = Event_V; Line_1 = Line_1_V; Line_2 = Line_2_V;
   }
   // Regex_Format_1_Event :
    public void On_Refresh_Event (string Code_Refresh) {
        Event = ""; Line_1 = ""; Line_2 ="";
        if (Code_Refresh == "Add_Option") {           
            
          // Visual_Novel_System.Ins._Visual_Novel_Canvas.Off_Show_All_Button ();
        } else if (Code_Refresh == "Add_Char" || Code_Refresh == "Remove_Char") {

        }
    }
    // Regex_Format_1_Event :
    bool b_Load_Dict_Event = false;
    Dictionary <string, UnityAction> Dict_Event = new Dictionary<string, UnityAction> ();
    public void On_Start_Event () {
        // Debug.LogError (Event + Line_1 + Line_2);
        if (!b_Load_Dict_Event) {
            Dict_Event = new Dictionary<string, UnityAction> ();
            Dict_Event.Add ("Add_Option", On_Add_Option);
            Dict_Event.Add ("Add_Char", On_Add_Char);
            Dict_Event.Add ("Remove_Char", On_Remove_Char);
            Dict_Event.Add ("Answer_Option", On_Answer_Option);
            Dict_Event.Add ("Close_Visual_Novel", On_Close_Visual_Novel);
            Dict_Event.Add ("Set_Char", On_Set_Char);
            Dict_Event.Add ("Set_Background", On_Set_Background);
            Dict_Event.Add ("Set_Event", On_Set_Event);
        }

        if (Dict_Event.ContainsKey(Event))
        {
            Dict_Event[Event] ();
        } else {
            Debug.LogError ("Dictionary Tidak ada " + Event);
        }
    }

    // Regex_Format_1_Event :
    public void On_Finish_Event () {
        if (Event == "Add_Option") {           
           Visual_Novel_System.Ins._Visual_Novel_Option.On_Vn_Option (L_Option_Title, L_Option_Event);
            L_Option_Title = new List<string> ();
            L_Option_Event = new List<string> ();
        } else if (Event == "Add_Char" || Event == "Remove_Char"){

        }
    }
   #endregion
   #region Implementation
    [SerializeField]
    List <string> L_Option_Title = new List<string> ();
    [SerializeField]
    List <string> L_Option_Event = new List<string> ();

    void On_Add_Option () {
        // Visual_Novel_System.Ins._Visual_Novel_Canvas.Off_Show_All_Button (); 
        L_Option_Title.Add (Line_1);
        L_Option_Event.Add (Line_2);
       // Debug.Log ("Add " + Line_1 + L_Option_Event.Count);
    }

    void On_Add_Char () {
        Visual_Novel_System.Ins._Visual_Novel_World.On_Add_Char_2d (Line_1,Line_2);
    }

    void On_Remove_Char () {
        Visual_Novel_System.Ins._Visual_Novel_World.On_Remove_Char_2d (Line_1,Line_2);
    }

    void On_Answer_Option () {
        // Sistem sudah dibuat di Visual_Novel_Option
    }

    void On_Close_Visual_Novel () {
        Visual_Novel_System.Ins.On_Close_Visual_Novel ();
    }

    void On_Set_Char () {
        Visual_Novel_System.Ins._Visual_Novel_World.On_Set_Char_2d (Line_1,Line_2);
    }

    void On_Set_Background () {
        Visual_Novel_System.Ins._Visual_Novel_World.On_Set_Background (Line_1, Line_2);
    }

    void On_Set_Event () {
        On_Doing_Event ();
    }
   #endregion 
   #region 
    
    // Visual_Novel_System :
    public void On_Check_Start_Event (string System_Event_1) {
        if (System_Event_1.Contains ("Add_Option")) { 
            Visual_Novel_System.Ins._Visual_Novel_Canvas.Off_Show_All_Button ();
        }
    }
   #endregion
   #region Doing_Event
    bool b_Load_Dict_Doing_Event = false;
   Dictionary <string, UnityAction> Dict_Doing_Event = new Dictionary<string, UnityAction> ();

   void On_Doing_Event () {
       if (!b_Load_Dict_Doing_Event) {
            b_Load_Dict_Doing_Event = true;
            Dict_Doing_Event = new Dictionary<string, UnityAction> ();
            Dict_Doing_Event.Add ("Open_Visual_Novel", Open_Visual_Novel);
        } 
        Dict_Doing_Event[Line_1] ();
   }

   void Open_Visual_Novel () {
        Visual_Novel_System.Ins.On_Start_Visual_Novel_From_Event (Line_2);
   }

   #endregion
}
