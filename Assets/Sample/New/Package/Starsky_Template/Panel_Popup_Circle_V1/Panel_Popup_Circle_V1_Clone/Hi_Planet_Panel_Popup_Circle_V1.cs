using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item_Ability;
public class Hi_Planet_Panel_Popup_Circle_V1 : Panel_Popup_Circle_V1 {
    
    public override void On_Line_Popup_Circle_V1 (string Event_V) {
        if (Event_V == "Inventory") {
            Sub_Event_Line_Tab = "Inventory";
        } else if (Event_V == "Storage") {
            Sub_Event_Line_Tab = "Inventory";
        }
        base.On_Line_Popup_Circle_V1 (Event_V);
        On_Process_Data ();

    }
    /* Event_Line_Tab :
    "Inventory" = Inventory
    */
    void On_Process_Data () {
        if (Event_Line_Tab == "Inventory" || Event_Line_Tab == "Storage") {
            DataItemInput [] A_Item_Input = new DataItemInput [0];
            if (Event_Line_Tab == "Inventory") {
                
                A_Item_Input = HiGameManager.instance._inventoryManager.dataItemInputs;
            } else if (Event_Line_Tab == "Storage") {
                A_Item_Input = HiGameManager.instance._storageManager.dataItemInputs;
                // Setingan setup sama seperti inventory hanya berbeda lokasi load saja.
            }

            
            
            foreach (DataItemInput Data_Item in A_Item_Input) {
                Code_String_1 = ""; Code_Int = 0;
                A_Code_String_1 = new string [0]; A_Code_Int_1 = new int [0];
                // if (Data_Item.Quantity > 0) {
                   // if (Item_Input_Code == "") {
                        Code_Time = "";
                        Script_Go = null;
                        if (Data_Item !=null) {
                            Script_Go = Data_Item.gameObject;
                        }
                         _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
                   // } 
               // }
            }
        }
    }
}
