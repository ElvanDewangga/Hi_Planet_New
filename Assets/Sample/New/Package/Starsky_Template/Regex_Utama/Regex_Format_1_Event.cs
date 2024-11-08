using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Regex_Format_1_Event : MonoBehaviour {
    // Class Regex_Format_1 berada di Replace String.
    #region Dict_Event
    Dictionary <string, object> Dict_Script = new Dictionary<string, object> ();
    bool b_Dict_Script = false;
    void On_Load_Dict_Event () {
        b_Dict_Script = true;
        Dict_Script.Add ("Add_Char", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Add_Option", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Answer_Option", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Remove_Char", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Set_Char", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Close_Visual_Novel", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Love_Point", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Set_Background", Visual_Novel_System.Ins._Visual_Novel_Event);
        Dict_Script.Add ("Set_Event", Visual_Novel_System.Ins._Visual_Novel_Event);
    }   
    #endregion
    #region Doing_Event
    // Regex_Utama : (Visual_Novel_System - Regex_Utama)
    public virtual void On_Doing_Event (List <string> L_1, List <string> L_2, List <string> L_3) {
        if (!b_Dict_Script) {On_Load_Dict_Event ();}
        
        if (L_1.Count >0) {
            On_Refresh_Event (L_1[0]);
           // Debug.Log (L_1.Count);
            int x = 0;
            for (x = 0; x < L_1.Count; x++) {
                On_Doing_Event_2 (L_1[x], L_2[x], L_3[x]);
            }
            On_Finish_Event (L_1[L_1.Count-1]);
        }
    }

    void On_Refresh_Event (string L_1) {
        if (Dict_Script[L_1] is Visual_Novel_Event script) {
            script.On_Refresh_Event (L_1);
        }
    }

    void On_Doing_Event_2 (string L_1, string L_2, string L_3) {
        if (Dict_Script[L_1] is Visual_Novel_Event script) {
            Visual_Novel_System.Ins._Visual_Novel_Event.On_Give_Data (L_1,L_2,L_3);
            script.On_Start_Event ();
        }
    }

    void On_Finish_Event (string L_1) {
        if (Dict_Script[L_1] is Visual_Novel_Event script) {
            script.On_Finish_Event ();
        }
    }
    #endregion
}
