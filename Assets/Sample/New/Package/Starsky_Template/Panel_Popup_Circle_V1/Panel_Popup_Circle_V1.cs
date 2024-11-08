using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
public class Panel_Popup_Circle_V1 : MonoBehaviour {
    [SerializeField]
    Image Panel_Popup_Circle_V1_Img;
    [SerializeField]
    public Line_Tab_Layout _Line_Tab_Layout;
    [SerializeField]
    Line_Tab_Samp _Line_Tab_Samp;
    [SerializeField]
    public GameObject Object_Samp;
   
    [SerializeField]
    Transform Position_Panel_Back;
    
#region Line_Tab_Layout
    // Line_Tab_Layout :
    public string Event_Line_Tab;
    public string Sub_Event_Line_Tab;
    public string Code_String_1;
    public int Code_Int;
    public string [] A_Code_String_1;
    public int [] A_Code_Int_1;
    [HideInInspector]
    public GameObject Script_Go;
    // GI_V2_Button :
   // public System_Requirements Cur_System_Requirements;
  //  public Item_Input_2 _Item_Input_2;
    [HideInInspector]
    public string Code_Time;
  //  [HideInInspector]
  //  public Item_Go _Item_Go;
   // [HideInInspector]
   // public Card_Information _Card_Information;
    public UnityEngine.Events.UnityAction Code_UnityAction;
    // Business_Location_Canvas :
    public void On_Doing_Event_All_L_Ins_Go (string Event_V) {
        _Line_Tab_Layout.On_Doing_Event_All_L_Ins_Go (Event_V);
    }
#endregion
    bool b_On_Use_Header_Small_Button = false;
    // Header_Small_Button _Header_Small_Button = new Header_Small_Button ();
    string Sub_Layout_Code = "";
    // Daily_Login :
    public string Item_Input_Code = "";
    public void On_Add_Item_Input_Code (string code) {
        Item_Input_Code = code;
    }
    // Business_Location_Canvas, All_Scene_Go_Script - GI_V2_Tab, Bg_Inventory, Bg_Infinity_Duel :
    public virtual void On_Line_Popup_Circle_V1 (string Event_V) {
        Event_Line_Tab = Event_V;
        b_On_Use_Header_Small_Button = false; // _Header_Small_Button = new Header_Small_Button ();
        if (Sub_Layout_Code == "") {
            _Line_Tab_Layout.On_Input_Layout_Samp (_Line_Tab_Samp.gameObject); 
        }
        /*
        if (Event_Line_Tab == "Business_Job_Add") { // Business_Location_Canvas
            C_Rows_To_List Crw_Partna = Business_Scene.Ins._Business_Data.On_Get_C_Rows_To_List ("Business_Job_Partna");
            C_Rows_To_List Crw_Days = Business_Scene.Ins._Business_Data.On_Get_C_Rows_To_List ("Business_Job_Time");
            foreach (string Item in All_Scene_Go_Script.Instance._Character_Data.Get_C_Rows_Character_Inventory().L_Str) {
                if (Item != "") {
                    Code_Time = "";
                    int.TryParse (Item, out Code_Int);
                    int Slot_Partna = Crw_Partna.On_Check_Same_Slot (0, Item);
                    if (Slot_Partna != -1) {
                        DateTime Dt_Waktu_Kerja = All_Scene_Go_Script.Instance.On_Conv_String_To_Date_Time (Crw_Days.L_Str[Slot_Partna]);
                        TimeSpan Sisa_Waktu = All_Scene_Go_Script.Instance.On_Perbandingan_Cur_Date_Time_Dengan_Target_Date_Time_Output_TimeSpan (Dt_Waktu_Kerja);
                        string Sisa_Waktu_Kerja_String = All_Scene_Go_Script.Instance.On_Conv_Minute_To_Day_Hour_Minute ((int)Sisa_Waktu.TotalMinutes);
                        if ((int)Sisa_Waktu.TotalMinutes >0) {
                            Code_Time = Sisa_Waktu_Kerja_String;
                        } else {
                            Code_Time = "Waiting to be take home.";
                        }
                    }
                    _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_Partna_Select_Button", Object_Samp.GetComponent <Partna_Select_Button> ());
                }
            }
            
        } else if (Event_Line_Tab == "GI_V2_Show_Reward" || Event_Line_Tab == "GI_V2_Inventory") { // All_Scene_Go_Script
            Item_Input [] A_Item_Input = new Item_Input [0];
            if (Event_Line_Tab == "GI_V2_Show_Reward") {
                A_Item_Input = All_Scene_Go_Script.Instance.On_Get_A_Item_Input ();
            } else if (Event_Line_Tab == "GI_V2_Inventory") { // Bg_Inventory :
                if (Item_Input_Code == "Daily_Login_Utama") {
                    Event_Line_Tab = "GI_V2_Inventory";
                    A_Item_Input = All_Scene_Go_Script.Instance._Daily_Login.On_Get_A_Item_Input ();
                } else if (Item_Input_Code == "") {
                    // A_Item_Input = All_Scene_Go_Script.Instance.On_Get_A_Item_Input ();  
                    A_Item_Input = All_Scene_Go_Script.Instance._Bg_Inventory.On_Get_A_Item_Input (); 
                }
            } 
            
            foreach (Item_Input Item in A_Item_Input) {
                Code_String_1 = ""; Code_Int = 0;
                A_Code_String_1 = new string [0]; A_Code_Int_1 = new int [0];

                if (Item.Item_Own > 0) {
                    if (Item_Input_Code == "") {
                        Code_Time = "";
                        Code_String_1 = Item.Item_Name;
                        Debug.LogError (Code_String_1);
                        Code_Int = Item.Item_Own;
                        _Item_Go = null; _Card_Information = null;
                        Type _Type_Item = All_Scene_Go_Script.Instance.Get_Item_Type (Item.Item_Name);
                        if (_Type_Item == typeof(Item_Go))
                        {
                            Debug.LogError ("Item_Go");
                            // Lakukan sesuatu jika objek memiliki tipe Weapon_GO
                            _Item_Go = All_Scene_Go_Script.Instance.Get_Item_Go (Item.Item_Name);
                        }
                        else if (_Type_Item == typeof(Card_Information))
                        {
                            Debug.LogError ("Card");
                            _Card_Information = All_Scene_Go_Script.Instance._Kumpulan_Cards.On_Get_Card_Information (Item.Item_Name);
                            // Lakukan sesuatu jika objek bukan tipe Weapon_GO
                        }
                        _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
                    } else if (Item_Input_Code == "Daily_Login_Utama") {
                        // Edit :
                        A_Code_String_1 = new string [2]; A_Code_Int_1 = new int [1];
                        // END
                        Code_Time = "";
                        A_Code_String_1[0] = Item.Item_Name;
                        A_Code_Int_1[0] = Item.Item_Own;
                        _Item_Go = null; _Card_Information = null;
                        Type _Type_Item = All_Scene_Go_Script.Instance.Get_Item_Type (Item.Item_Name);
                        if (_Type_Item == typeof(Item_Go))
                        {
                            // Lakukan sesuatu jika objek memiliki tipe Weapon_GO
                            _Item_Go = All_Scene_Go_Script.Instance.Get_Item_Go (Item.Item_Name);
                        }
                        else if (_Type_Item == typeof(Card_Information))
                        {
                            _Card_Information = All_Scene_Go_Script.Instance._Kumpulan_Cards.On_Get_Card_Information (Item.Item_Name);
                            // Lakukan sesuatu jika objek bukan tipe Weapon_GO
                        }

                        // Edit :
                        A_Code_String_1[1] = Item.A_Code_Str[0];
                        // END

                        _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());

                        
                    }
                }
            }
            
        } else if (Event_Line_Tab == "GI_V2_Infinity_Duel_Title") {
            Item_Input_2 [] A_Item_Input_2 = new Item_Input_2 [0];
            if (Event_Line_Tab == "GI_V2_Infinity_Duel_Title") {
                A_Item_Input_2 = All_Scene_Go_Script.Instance._Bg_Infinity_Duel.Target_Item_Input_2;
            } 
            
            foreach (Item_Input_2 Item in A_Item_Input_2) {
               // if (Item.Item_Own > 0) {
                Code_Time = "";
                Code_String_1 = Item.Item_Name;
                Code_Int = Item.Item_Own;
                _Item_Go = null; _Card_Information = null;
                _Item_Input_2 = Item;
                Type _Type_Item = All_Scene_Go_Script.Instance.Get_Item_Type (Item.Item_Name);
                 if (_Type_Item == typeof(Item_Go))
                {
                    // Lakukan sesuatu jika objek memiliki tipe Weapon_GO
                     _Item_Go = All_Scene_Go_Script.Instance.Get_Item_Go (Item.Item_Name);
                }
                else if (_Type_Item == typeof(Card_Information))
                {
                    _Card_Information = All_Scene_Go_Script.Instance._Kumpulan_Cards.On_Get_Card_Information (Item.Item_Name);
                    // Lakukan sesuatu jika objek bukan tipe Weapon_GO
                }
                _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
                // }
            }
            
        } else if (Event_Line_Tab == "Player_Profile") { // Header_Script :
            b_On_Use_Header_Small_Button = true;
            _Header_Small_Button = All_Scene_Go_Script.Instance._Header_Script._Header_Small_Button;
        } else if (Event_Line_Tab == "Player_Profile_Change_Avatar") {
            if (All_Scene_Go_Script.Instance._Header_Script.On_Get_Event_Selection_Utama () != "Avatar_Button") {
                Code_Time = "";
                int.TryParse ("-1", out Code_Int); // -1 untuk menghapus avatar.
                _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_Partna_Select_Button", Object_Samp.GetComponent <Partna_Select_Button> ());
            }

            foreach (string Item in All_Scene_Go_Script.Instance._Character_Data.Get_C_Rows_Character_Inventory().L_Str) {
                if (Item != "") {
                    Code_Time = "";
                    int.TryParse (Item, out Code_Int);
                    _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_Partna_Select_Button", Object_Samp.GetComponent <Partna_Select_Button> ());
                }
            }
        } else if (Event_Line_Tab == "Master_Box_Button_Requirements_1") { // Master_Box :
            System_Requirements Req = All_Scene_Go_Script.Instance._Master_Box_Script.On_Get_Target_System_Requirements ();
            System_Requirements.Syarat [] A_Syarat =  Req.On_Get_A_Syarat ();
            int br = 0;
            for (br = 0; br <A_Syarat.Length; br++) {
                Code_String_1 = "";
                Code_Int =br;
                _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
            }
        } else if (Event_Line_Tab == "") {
            _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_Partna_Select_Button", Object_Samp.GetComponent <Partna_Select_Button> ());
        }  else if (Event_Line_Tab == "Oiwa_Duel_Cards" || Event_Line_Tab == "Oiwa_Items" || Event_Line_Tab == "Oiwa_Leader_Expansion_Cards" || Event_Line_Tab == "Oiwa_Special_Items") {
            Oiwa_Shop_Management _Oiwa_Shop_Management = All_Scene_Go_Script.Instance._Oiwa_Shop_Scene._Oiwa_Shop_Management;
            SortedDictionary <int, Item_Go> Dict_Item_Go = new SortedDictionary <int, Item_Go> ();
            SortedDictionary <int, System_Requirements> Dict_System_Requirements = new SortedDictionary <int, System_Requirements> ();
            SortedDictionary <int, Card_Information> Dict_Card_Information = new SortedDictionary <int, Card_Information> ();
            Cur_System_Requirements = null;
            // Auto Filter : Coin - Diamond.
            Filter_System Fil = All_Scene_Go_Script.Instance._Filter_System;
            System_Requirements.Syarat Target_Syarat = Fil.Dict_Code_Req_Syarat [Fil.Filter_Perintah];
            // END
            if (Event_Line_Tab == "Oiwa_Duel_Cards") {
                Dict_Card_Information = _Oiwa_Shop_Management.On_Get_Dict_Card_Information (Event_Line_Tab);
            } else if (Event_Line_Tab == "Oiwa_Items") {
                Dict_Item_Go = _Oiwa_Shop_Management.On_Get_Dict_Item_Go (Event_Line_Tab);
            }
            Dict_System_Requirements = _Oiwa_Shop_Management.On_Get_Dict_System_Requirements (Event_Line_Tab);
            int Id = -1;
            foreach (System_Requirements pair in Dict_System_Requirements.Values)
            {
                Id++;
                if (pair.On_Get_A_Syarat()[0] ==  Target_Syarat) {
                    _Item_Go = null; _Card_Information = null;
                    // Accessing the key and value of each pair
                    Code_Int = Id;
                    Cur_System_Requirements = pair;
                    if (Event_Line_Tab == "Oiwa_Duel_Cards") {
                        Debug.LogError ("Card Information");
                        _Card_Information = Dict_Card_Information [Code_Int];
                    } else if (Event_Line_Tab == "Oiwa_Items") {
                        _Item_Go = Dict_Item_Go [Code_Int];
                    }
                    _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
                }
            }

            if (Target_Syarat == System_Requirements.Syarat.Gold) {
                Sub_Layout_Code = "Diamond";
                Fil.On_Set_Filter_Perintah ("Diamond");
                On_Line_Popup_Circle_V1 (Event_Line_Tab);
            } else {
                Sub_Layout_Code = "";
            }

        } else if (Event_Line_Tab == "Altare_Red_Ticket" || Event_Line_Tab == "Altare_Blue_Ticket" || Event_Line_Tab == "Altare_Black_Ticket" 
        || Event_Line_Tab == "Altare_Yellow_Ticket" || Event_Line_Tab == "Altare_Green_Ticket") {
            Debug.LogError ("Ev 1");
            Oiwa_Shop_Management _Oiwa_Shop_Management = All_Scene_Go_Script.Instance._Oiwa_Shop_Scene._Oiwa_Shop_Management;
            SortedDictionary <int, Item_Go> Dict_Item_Go = new SortedDictionary <int, Item_Go> ();
            SortedDictionary <int, System_Requirements> Dict_System_Requirements_Item_Go = new SortedDictionary <int, System_Requirements> ();
            SortedDictionary <int, Card_Information> Dict_Card_Information = new SortedDictionary <int, Card_Information> ();
            SortedDictionary <int, System_Requirements> Dict_System_Requirements_Card_Information = new SortedDictionary <int, System_Requirements> ();
            Cur_System_Requirements = null;
            // Auto Filter : Coin - Diamond. 
            Filter_System Fil = All_Scene_Go_Script.Instance._Filter_System;
            System_Requirements.Syarat Target_Syarat = Fil.Dict_Code_Req_Syarat [Fil.Filter_Perintah];
            // END
                Dict_Item_Go = All_Scene_Go_Script.Instance._Exchange_Ticket_System.Target_Kumpulan_Items.On_Get_Dict_Item_Go ();
                Dict_Card_Information = All_Scene_Go_Script.Instance._Exchange_Ticket_System.Target_Kumpulan_Items.On_Get_Dict_Card_Information ();
            
            if (All_Scene_Go_Script.Instance._Exchange_Ticket_System.Target_Kumpulan_Items.Trans_A_System_Requirement_Item_Go != null) {
                Dict_System_Requirements_Item_Go = All_Scene_Go_Script.Instance._Exchange_Ticket_System.Target_Kumpulan_Items.On_Get_Dict_System_Requirements ("Item_Go");
                int Id = -1;
                foreach (System_Requirements pair in Dict_System_Requirements_Item_Go.Values)
                {
                    Id++;
                    if (pair.On_Get_A_Syarat()[0] ==  Target_Syarat) {
                        _Item_Go = null; _Card_Information = null;
                        // Accessing the key and value of each pair
                        Code_Int = Id;
                        Cur_System_Requirements = pair;
                    
                        _Item_Go = Dict_Item_Go [Code_Int];

                        _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
                    }
                }
            }

            if (All_Scene_Go_Script.Instance._Exchange_Ticket_System.Target_Kumpulan_Items.Trans_A_System_Requirement_Card_Information != null) {
                Debug.LogError ("Ev 2");
                Dict_System_Requirements_Card_Information = All_Scene_Go_Script.Instance._Exchange_Ticket_System.Target_Kumpulan_Items.On_Get_Dict_System_Requirements ("Card_Information");
                int Id = -1;
                foreach (System_Requirements pair in Dict_System_Requirements_Card_Information.Values)
                {
                    Id++;
                    if (pair.On_Get_A_Syarat()[0] ==  Target_Syarat) {
                        _Item_Go = null; _Card_Information = null;
                        // Accessing the key and value of each pair
                        Code_Int = Id;
                        Cur_System_Requirements = pair;
                       
                        _Card_Information = Dict_Card_Information [Code_Int];

                        _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
                    }
                    Debug.LogError ("Ev 3");
                }
            }
        }
        
        if (b_On_Use_Header_Small_Button) {
            int Tar = -1;
            foreach (string Sml in _Header_Small_Button.L_Code) {
                Tar++;
                Code_String_1 = Sml;
                Code_UnityAction = _Header_Small_Button.L_Action[Tar];
                _Line_Tab_Layout.On_Input_Go_Samp (Object_Samp, "_GI_V2_Button", Object_Samp.GetComponent <GI_V2_Button> ());
            }
        }
        */
        Panel_Popup_Circle_V1_Img.gameObject.SetActive (true);
        // Panel_Popup_Circle_V1_Img.GetComponent<VerticalLayoutGroup>().ForceRebuildLayout();
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)Panel_Popup_Circle_V1_Img.transform);
        // Membuat Scroll rect tampil paling atas :
       // Panel_Popup_Circle_V1_Img.gameObject.GetComponent <ScrollRect> ().verticalNormalizedPosition = 1f;
    }

    // Business_Location_Canvas, Oiwa_Shop_Canvas, Bg_Infinity_Duel :
    public void Off_Line_Popup_Circle_V1 () {
        Sub_Layout_Code = "";
        Item_Input_Code = "";
        A_Code_String_1 = new string [0]; A_Code_Int_1 = new int [0];
       // this.transform.position = A_Position_Panel_Transform.GetChild (0).position;
        this.transform.SetParent (Position_Panel_Back);
        Panel_Popup_Circle_V1_Img.gameObject.SetActive (false);
    }
#region Mask & Position
    // All_Scene_Go_Script - GI_V2_Tab, Header_Script, Bg_Inventory, Oiwa_Shop_Canvas  :
    public void On_Input_Mask_And_Position_Panel (GameObject Target_Mask_Parent_V) {
       // this.transform.position = A_Position_Panel_Transform.GetChild (Urutan_Position_Panel).position;
        this.transform.SetParent (Target_Mask_Parent_V.transform);

    }
#endregion
}
