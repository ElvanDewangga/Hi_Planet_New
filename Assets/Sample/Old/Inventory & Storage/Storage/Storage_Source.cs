using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage_Source : Inventory_Source {
    public override void On_Display () {
        Inventory_Panel_Popup_Circle_V1 = GameObject.Instantiate (_Panel_Popup_Circle_V1.gameObject);
        Inventory_Panel_Popup_Circle_V1.transform.SetParent (Panel_Inventory);
        Inventory_Panel_Popup_Circle_V1.gameObject.SetActive (true);
        Inventory_Panel_Popup_Circle_V1.GetComponent<Panel_Popup_Circle_V1> ().On_Line_Popup_Circle_V1 ("Storage");

        // All_Scene_Go.Ins._Tabel_Popup.On_Set_Tabel_Popup (Tab_Inventory.gameObject, "Storage", _Inventory.Tabel_Inventory.transform);
    }

    public override void Off_Display () {
        base.Off_Display ();
    }
}
