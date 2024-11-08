using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GI_V2_Tab : MonoBehaviour {
    [SerializeField]
    TMP_Text GI_V2_Tab_Title_Tx;
    [SerializeField]
    GameObject Mask_Parent;
    // Master_Box_Script (Claim_Reward), Player_Status_Script:
    public void On_GI_V2_Tab () {
        GI_V2_Tab_Title_Tx.text = "Obtain Items";
        On_GI_V2_Tab_Process ();
    }

    // Master_Box_Script (Show_Reward), Player_Status_Script:
    public void On_GI_V2_Tab (string Title_V) {
        GI_V2_Tab_Title_Tx.text = Title_V;
        On_GI_V2_Tab_Process ();
    }

    void On_GI_V2_Tab_Process () {
        /*
        All_Scene_Go_Script.Instance._Panel_Popup_GI_V2.On_Line_Popup_Circle_V1 ("GI_V2_Show_Reward");
        All_Scene_Go_Script.Instance._Panel_Popup_GI_V2.On_Input_Mask_And_Position_Panel (Mask_Parent);
        this.gameObject.SetActive (true);
        All_Scene_Go_Script.Instance._Popup_Canvas.On_Black_Universal ("Black_Universal_1");
        */
    }
    public void On_Confirm () {
        /*
        All_Scene_Go_Script.Instance._Panel_Popup_GI_V2.Off_Line_Popup_Circle_V1 ();
        this.gameObject.SetActive (false);
        All_Scene_Go_Script.Instance._Popup_Canvas.Off_Black_Universal ("Black_Universal_1");
        if (All_Scene_Go_Script.Instance._Bg_Select_Mini_Item.gameObject.activeInHierarchy ) {
            All_Scene_Go_Script.Instance._Bg_Select_Mini_Item.Off_Show ();
        }
        */
    }
}
