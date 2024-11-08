using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using Org.BouncyCastle.Asn1.X509;
public class Tabel_Popup_Source : MonoBehaviour {
    [SerializeField]
    Transform Tabel_Popup_1;
   #region Tabel_Popup
    Transform Target_Back; GameObject Off_Popup;
    public void Off_Code_Tabel_Popup (string Code, GameObject Off_Popup_V, Transform Target_Back_V) {
        Target_Back = Target_Back_V; Off_Popup = Off_Popup_V;
        
        Type t = Type.GetType("Tabel_Popup_Source");
        MethodInfo method 
                = t.GetMethod("Off_" + Code, BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(this, null);
        Off_Popup.transform.SetParent (Target_Back);    
    }

    GameObject Target_On;
    public void On_Code_Tabel_Popup (string Code, GameObject Target) {
        Target_On = Target;
        Type t = Type.GetType("Tabel_Popup_Source");
        MethodInfo method 
                = t.GetMethod("On_" + Code, BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(this, null);

        
    }
   #endregion
   #region Method
    public void On_Inventory () {
        Target_On.transform.SetParent (Tabel_Popup_1);
    }
    public void Off_Inventory () {
        HiGameManager.inventoryManager.HideInventory ();
    }

    public void On_Storage () {
        Target_On.transform.SetParent (Tabel_Popup_1);
    }
    public void Off_Storage () {
       HiGameManager.storageManager.HideInventory ();
    }

    public void On_Equipment_Setup () {
        Target_On.transform.SetParent (Tabel_Popup_1);
    }

    public void Off_Equipment_Setup () {
        EquipmentManager.Instance.Off_Char_Data_Equipment ();
    }
   #endregion
}
