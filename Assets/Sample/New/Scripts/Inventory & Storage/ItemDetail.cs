using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class ItemDetail : MonoBehaviour {
    #region Umum
       
        [SerializeField]
        Image _Item_Detail;
        public Item_Detail_Source _Item_Detail_Source;
        string Code_Detail; 
        [HideInInspector]
        public  GameObject Target_Object;
        public void On_Display () {
            _Item_Detail.gameObject.SetActive (true);
        }
        // Inventory, Button :
        public void Off_Display () {
           
            _Item_Detail.gameObject.SetActive (false);
            _Item_Detail_Source.Off_Display ();
        }
        #region Direct_Code_Detail
        // Turn On ini jika pakai Code Detail Khusus seperti : "Equipment", "Recycle", "Crafting", dll
        // Gunakan ini karena Settings up "Equipment", "Recycle", "Crafting" adalah Sama.
        // Tempat kan ini sebelum membuka panel inventory / storage.
        // Char_Data_Equipment - Char_Data_Source :
        bool b_Direct_Code_Detail = false;
        string Direct_Code_Detail = "";
        // InventoryManager :
        public void On_Direct_Code_Detail (string Code_V) {
            b_Direct_Code_Detail = true;
            Direct_Code_Detail = Code_V;
        }
        // Turn Off setiap panel sudah selesai dipakai.
        // Tempat kan ini saat panel di tutup dan tidak dipakai.
        // InventoryManager
        public void Off_Direct_Code_Detail () {
             b_Direct_Code_Detail = false;
            Direct_Code_Detail = "";
            Off_Display_Setting_Tambahan ();
        }

        
        #endregion
        #region GI_V2_Button (Hi_Planet)
            public void On_Get_Data (string Code_Detail_V, GameObject Si) {
                On_Refresh_Item_Detail_Source ();
                if (!b_Direct_Code_Detail) {
                    Code_Detail = Code_Detail_V;
                } else {
                    Code_Detail = Direct_Code_Detail;
                } 
                
                Target_Object = Si;
                _Item_Detail_Source.On_Convert_Object_To_Item_Detail (Code_Detail, Si);
            }

            // Tidak Dipakai :
            public string Display_Setting_Tambahan = ""; // "Equipment_Area"
            // GI_V2_Button :
            void On_Display_Setting_Tambahan (string val) {
                Display_Setting_Tambahan = val;
            }

            void Off_Display_Setting_Tambahan () {
                Display_Setting_Tambahan = "";
            }
            // End
        #endregion
        #region Item_Detail_Source
            List <string> L_Item_Source_Str = new List<string> ();
            List <int> L_Item_Source_Int = new List<int> ();
            List <Sprite> L_Item_Source_Sprite = new List<Sprite> ();
            public void On_Add_Item_Source_Str (string v) {
                L_Item_Source_Str.Add (v);
            }

            public void On_Add_Item_Source_Int (int v) {
                L_Item_Source_Int.Add (v);
            }

            public void On_Add_Item_Source_Sprite (Sprite v) {
                L_Item_Source_Sprite.Add (v);
            }

            void On_Refresh_Item_Detail_Source () {
                L_Item_Source_Str = new List<string> ();
                L_Item_Source_Int = new List<int> ();
                L_Item_Source_Sprite = new List<Sprite> ();
            }

            public void On_Start_Display () {
                On_Display ();
            }

            public void On_Refresh_Display () {
                Bg_Input_Quantity.gameObject.SetActive (false);
                Confirm_Button.gameObject.SetActive (false);
                int x =0;
                for (x=0; x< A_Button.Length; x++) {
                    A_Button[x].gameObject.SetActive (false);
                }
            }
        #endregion

    #endregion
    #region Parts
        #region Bg_Item
            [SerializeField]
            GameObject Bg_Item;
            [SerializeField]
            public Image Image_Item;
        #endregion
        #region Bg_Name
            [SerializeField]
            GameObject Bg_Name;
            [SerializeField]
            public TMP_Text Name_Tx;
        #endregion
        #region Bg_Description
            [SerializeField]
            GameObject Bg_Description;
            [SerializeField]
            public TMP_Text Description_Tx;
        #endregion
        #region Bg_Input_Quantity
            [SerializeField]
            public GameObject Bg_Input_Quantity;
            [SerializeField]
            public TMP_Text Max_Quantity_Tx;
            [SerializeField]
            public TMP_InputField IF_Quantity;
        #endregion
        #region Bg_Confirm
            [SerializeField]
            GameObject Bg_Confirm;
            [SerializeField]
            public Button Confirm_Button;
            
            public void On_Confirm_Button () {
                _Item_Detail_Source.On_Confirm_Button (Code_Detail);
                Off_Display ();
            }
            [SerializeField]
            Button [] A_Button;
            string [] A_Code_Confirm_Event;
            string [] A_Code_Confirm_Text;
            public void On_Click_Button (int Code_V) {
                _Item_Detail_Source.On_Click_Button (A_Code_Confirm_Event [Code_V]);
            }

            void On_Example_Add_Button () {
                string [] A_Code_Confirm_Event_V = new string [2];
                string [] A_Code_Confirm_Text_V = new string [2];
                A_Code_Confirm_Event_V[0] = "Equip_Event";
                A_Code_Confirm_Event_V[1] = "Drop_Event";

                A_Code_Confirm_Text_V[0] = "Equip";
                A_Code_Confirm_Text_V[1] = "Drop";
                On_Add_Button (A_Code_Confirm_Event_V, A_Code_Confirm_Text_V);
            }

            public void On_Add_Button (string [] Code_Event_V, string [] Code_Text_V) {
                A_Code_Confirm_Event = new string [0]; A_Code_Confirm_Text = new string [0];
                A_Code_Confirm_Event = Code_Event_V; A_Code_Confirm_Text = Code_Text_V;
                
                int x =0;
                for (x=0; x< A_Button.Length; x++) {
                    if (x < A_Code_Confirm_Event.Length) {
                        A_Button[x].gameObject.SetActive (true);
                        A_Button[x].gameObject.GetComponentInChildren <TMP_Text> ().text = Code_Text_V[x];
                    } else {
                        A_Button[x].gameObject.SetActive (false);
                    }
                }
            }
        #endregion
    #endregion
}
