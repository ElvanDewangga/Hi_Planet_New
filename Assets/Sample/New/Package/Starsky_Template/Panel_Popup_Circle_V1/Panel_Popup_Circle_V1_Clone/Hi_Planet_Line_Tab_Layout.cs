using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hi_Planet_Line_Tab_Layout : Line_Tab_Layout {
    public override void On_Set_Layout_Settings (GameObject Ins) {
        Debug.Log ("Set Layout " + _Panel_Popup_Circle_V1.Sub_Event_Line_Tab);
        if (_Panel_Popup_Circle_V1.Sub_Event_Line_Tab == "Inventory") {
            
            Ins.transform.localScale = new Vector3 (0.95f,0.95f,0.95f);
            Ins.GetComponent<Scale_Fixer>().On_Set_V3_Scale (new Vector3 (0.95f,0.95f,0.95f));
            Max_Ins_Per_Layout = 8;
        }
    }

    public override void On_Send_Data_To_Object (GameObject Ins) {
        if (_Partna_Select_Button !=null) {
               
        }
        if (_GI_V2_Button !=null) {
            if (_Panel_Popup_Circle_V1.Sub_Event_Line_Tab == "Inventory") {
                    Ins.GetComponent <GI_V2_Button> ().On_Input_Data (_Panel_Popup_Circle_V1.Event_Line_Tab, _Panel_Popup_Circle_V1.Sub_Event_Line_Tab, _Panel_Popup_Circle_V1.Script_Go); 
            }
        }
    }
    
}
