using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Source : MonoBehaviour {
    [SerializeField]
    public  Inventory _Inventory;
    [SerializeField]
    public Panel_Popup_Circle_V1 _Panel_Popup_Circle_V1;
    [SerializeField]
    public Transform Panel_Inventory;
     [HideInInspector]
    public GameObject Inventory_Panel_Popup_Circle_V1;
     bool b_Display = false;
   // Inventory :
   public virtual void On_Display () {
        Inventory_Panel_Popup_Circle_V1 = GameObject.Instantiate (_Panel_Popup_Circle_V1.gameObject);
        Inventory_Panel_Popup_Circle_V1.transform.SetParent (Panel_Inventory);
        Inventory_Panel_Popup_Circle_V1.gameObject.SetActive (true);
        Inventory_Panel_Popup_Circle_V1.GetComponent<Panel_Popup_Circle_V1> ().On_Line_Popup_Circle_V1 ("Inventory");
   }
     
   public void On_Set_Back_System () {
          All_Scene_Go.Ins._Tabel_Popup.On_Set_Tabel_Popup (Tab_Inventory.gameObject, _Inventory.Code_Inventory, _Inventory.Tabel_Inventory.transform);
   }

   [SerializeField]
   public Transform Tab_Inventory;  
   public virtual void Off_Display () {
        if (Inventory_Panel_Popup_Circle_V1 != null) {
            Destroy (Inventory_Panel_Popup_Circle_V1);
            Inventory_Panel_Popup_Circle_V1 = null;
            _Inventory._Item_Detail.Off_Display ();
        }
   }

   public string On_Get_Id_From_Data_Item_Input_From_Name (string Name_V) {
     return ItemInputManager.instance.On_Get_Data_Item_Input_From_Name (Name_V).Id;
     
   }

   #region Inventory for Data_Game_Inventory
   
   public void On_Save_Inventory (string Code_V) {
     
     if (Code_V == "Storage") {
          Data_Game_Utama.Ins._Data_Game_Inventory.StartSave ();
     } else if (Code_V == "Save_Items") {
          Data_Game_Utama.Ins._Data_Game_Inventory.StartSave ();
          
     }
     
   } 
   #endregion

}
