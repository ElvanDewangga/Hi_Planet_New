using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Inventory
{
   [SerializeField]
   Inventory _Inventory;
    public override void On_Inventory () {
      base.On_Inventory ();
      _Inventory.On_Inventory ();
    }

    // Tabel_Popup_Source :
    public override void Off_Inventory () {
      base.Off_Inventory ();
      // _Inventory.Off_Inventory ();
    }
    #region Pengaturan
   // int Max_Slot = 40;
    #endregion
    #region Data_Game_Source
    public override void On_Get_Data(int Max_Slot_V, string[] Item_Id, string[] Item_Own) {
        base.On_Get_Data (Max_Slot_V, Item_Id, Item_Own);  
    }
    #endregion
    #region Panel_Popup_Circle_V1
    
    #endregion
}
