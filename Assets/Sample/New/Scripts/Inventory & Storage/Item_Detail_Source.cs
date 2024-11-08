using System.Collections;
using System.Collections.Generic;
using Item_Ability;
using Org.BouncyCastle.Asn1.Misc;
using UnityEngine;

public class Item_Detail_Source : MonoBehaviour {
   #region Item_Detail
   [SerializeField]
   ItemDetail itemDetail;
   [SerializeField]
   string Code_Detail = "";
   DataItemInput _Data_Item_Input;
   // ItemDetail :
    public void On_Convert_Object_To_Item_Detail (string Code_Detail_V,GameObject s) {
        itemDetail.On_Refresh_Display (); Code_Detail = Code_Detail_V;
        Hi_Planet_GI_V2_Button V2_Button = s.GetComponent <Hi_Planet_GI_V2_Button> ();
        _Data_Item_Input = s.GetComponent <Hi_Planet_GI_V2_Button> ()._Data_Item_Input;
        /*
        itemDetail.On_Add_Item_Source_Str (_Data_Item_Input._Item_Input.Name);
        itemDetail.On_Add_Item_Source_Str (_Data_Item_Input._Item_Input.Item_Detail);
        itemDetail.On_Add_Item_Source_Int (_Data_Item_Input.Quantity);
        itemDetail.On_Add_Item_Source_Sprite (_Data_Item_Input._Item_Input.Item_Sprite);
        */

        if (Code_Detail_V == "Inventory" || Code_Detail_V == "Storage") {
            itemDetail.Bg_Input_Quantity.gameObject.SetActive (true);
             itemDetail.Confirm_Button.gameObject.SetActive (true);

            itemDetail.Image_Item.sprite = _Data_Item_Input._Item_Input.Item_Sprite;
            itemDetail.Name_Tx.text = _Data_Item_Input._Item_Input.Name;
            itemDetail.Description_Tx.text = _Data_Item_Input._Item_Input.Item_Detail;
            itemDetail.Max_Quantity_Tx.text =  "Items Transfer : (Max. " + _Data_Item_Input.Quantity.ToString () + ")";
           
           itemDetail.IF_Quantity.GetComponent <Input_Field_Default> ().On_Setup (_Data_Item_Input.Quantity.ToString ());
           itemDetail.IF_Quantity.GetComponent <Input_Field_Max_Value> ().On_Setup (0, _Data_Item_Input.Quantity);
        } else if (Code_Detail_V == "Equipment_Setup") {
           itemDetail.Confirm_Button.gameObject.SetActive (false);
            itemDetail.Image_Item.sprite = _Data_Item_Input._Item_Input.Item_Sprite;
            itemDetail.Name_Tx.text = _Data_Item_Input._Item_Input.Name;

            if (_Data_Item_Input._Item_Input.Type == "Drone" || _Data_Item_Input._Item_Input.Type == "Wing" || _Data_Item_Input._Item_Input.Type == "Helmet" || _Data_Item_Input._Item_Input.Type == "Armor" || _Data_Item_Input._Item_Input.Type == "Intelligence_Cube") {
                itemDetail.Description_Tx.text = _Data_Item_Input._Item_Input.Item_Detail;
                int x =0;
                for (x=0; x < _Data_Item_Input._Item_Input.L_Item_Effect.Count; x++) {
                    itemDetail.Description_Tx.text += "\n" + _Data_Item_Input._Item_Input.L_Item_Effect[x].Code_Effect[0] + " " + "+" + _Data_Item_Input._Item_Input.L_Item_Effect[x].Code_Value[0].ToString ();
                }
                
                if (V2_Button.Event == "Inventory") {
                    string [] A_Code_Confirm_Event_V = new string [2];
                    string [] A_Code_Confirm_Text_V = new string [2];
                    A_Code_Confirm_Event_V[0] = "Equip_Event";
                    A_Code_Confirm_Event_V[1] = "Drop_Event";
                    A_Code_Confirm_Text_V[0] = "Equip";
                    A_Code_Confirm_Text_V[1] = "Drop";
                    itemDetail.On_Add_Button (A_Code_Confirm_Event_V, A_Code_Confirm_Text_V);

                    EquipmentManager.Instance.On_Perubahan_Status (_Data_Item_Input);
                } else if (V2_Button.Event == "Equipment_Setup") {
                    string [] A_Code_Confirm_Event_V = new string [1];
                    string [] A_Code_Confirm_Text_V = new string [1];
                    A_Code_Confirm_Event_V[0] = "Unequip_Event";
                    A_Code_Confirm_Text_V[0] = "Unequip";
                    itemDetail.On_Add_Button (A_Code_Confirm_Event_V, A_Code_Confirm_Text_V);

                    EquipmentManager.Instance.On_Perubahan_Status (_Data_Item_Input);
                }
            } else {

                itemDetail.Description_Tx.text = _Data_Item_Input._Item_Input.Item_Detail;

                string [] A_Code_Confirm_Event_V = new string [1];
                string [] A_Code_Confirm_Text_V = new string [1];
                A_Code_Confirm_Event_V[0] = "Drop_Event";
                A_Code_Confirm_Text_V[0] = "Drop";
                itemDetail.On_Add_Button (A_Code_Confirm_Event_V, A_Code_Confirm_Text_V);
            }

        }
        itemDetail.On_Start_Display ();
    }

        public void Off_Display () {
            if (Code_Detail == "Equipment_Setup") {
                EquipmentManager.Instance.Off_Perubahan_Status ();
            }
        }
        #region Confirm_Button
        public void On_Confirm_Button (string Code_Detail_V) {
            if (Code_Detail_V == "Inventory") {
                Debug.Log ("Inventory");
                // Transfer to Storage
                DataItemInput Di = itemDetail.Target_Object.GetComponent <Hi_Planet_GI_V2_Button> ()._Data_Item_Input;
                int.TryParse (itemDetail.IF_Quantity.text, out int Transfer_Quantity);

                List <string> L_Name = new List<string> ();
                List <int> L_Quantity = new List<int> ();
                L_Name.Add (_Data_Item_Input._Item_Input.Name); L_Quantity.Add (Transfer_Quantity*-1);
                HiGameManager.instance._inventoryManager.RemoveItem (L_Name, L_Quantity);

                List <string> L_Name2 = new List<string> ();
                List <int> L_Quantity2 = new List<int> ();
                L_Name2.Add (_Data_Item_Input._Item_Input.Name); L_Quantity2.Add (Transfer_Quantity);
                HiGameManager.instance._storageManager.AddItem (L_Name2, L_Quantity2);

                HiGameManager.instance._inventoryManager.RefreshPage ();
                HiGameManager.instance._storageManager.RefreshPage ();
            } else if (Code_Detail_V == "Storage") {
                // Transfer to Inventory
                Debug.Log ("Storage");
                DataItemInput Di = itemDetail.Target_Object.GetComponent <Hi_Planet_GI_V2_Button> ()._Data_Item_Input;
                int.TryParse (itemDetail.IF_Quantity.text, out int Transfer_Quantity);

                List <string> L_Name = new List<string> ();
                List <int> L_Quantity = new List<int> ();
                L_Name.Add (_Data_Item_Input._Item_Input.Name); L_Quantity.Add (Transfer_Quantity*-1);
                HiGameManager.instance._storageManager.RemoveItem (L_Name, L_Quantity);

                List <string> L_Name2 = new List<string> ();
                List <int> L_Quantity2 = new List<int> ();
                L_Name2.Add (_Data_Item_Input._Item_Input.Name); L_Quantity2.Add (Transfer_Quantity);
                HiGameManager.instance._inventoryManager.AddItem (L_Name2, L_Quantity2);

                HiGameManager.instance._inventoryManager.RefreshPage ();
                HiGameManager.instance._storageManager.RefreshPage ();
            } 

            
        }

        public void On_Click_Button (string Code) {
            if (Code == "Equip_Event") {
                Debug.Log ("Doing Equip");
                AccountManager.instance.player.EquipItem (_Data_Item_Input.Slot_Panel, _Data_Item_Input._Item_Input.Type);
                itemDetail.Off_Display();
            } else if (Code == "Drop_Event") {
                 Debug.Log ("Doing Drop");
            } else if (Code == "Unequip_Event") {
                 AccountManager.instance.player.UnequipItem (_Data_Item_Input._Item_Input.Type);
                itemDetail.Off_Display();
            }
        }
        #endregion
   #endregion
}
