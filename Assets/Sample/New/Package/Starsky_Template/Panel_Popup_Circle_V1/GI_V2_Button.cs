using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class GI_V2_Button : MonoBehaviour {
    [Header ("Centang ini jika sekali button maka akan munculkan Select_Img")]
    public bool b_Select_Img = false;

    [SerializeField]
    Image Select_Img;
    [SerializeField]
    Button The_Button;
    [SerializeField]
    public Image [] A_Image;
    [SerializeField]
    public TMP_Text [] A_TMP_Text;

    [HideInInspector] 
    public int [] A_Code_Int;
    [SerializeField]
    public string [] A_Code_Str;
    [SerializeField]
    UnityAction [] A_UnityAction;
    [SerializeField]

    Panel_Popup_Circle_V1 _Panel_Popup_Circle_V1;
    // [HideInInspector]
    public string Event;
    public string Sub_Event;

   

    void On_Refresh_Data () {

    }
    // Line_Tab_Layout :

    public void On_Input_Data (string Event_V, string Code_But, UnityAction _Action) {
       Event = Event_V;
       A_Code_Str = new string [1]; A_UnityAction = new UnityAction [1];
       A_Code_Str[0] = Code_But; A_UnityAction[0] = _Action;
       On_Setting_Event ();
    }

    public void On_Input_Data (string Event_V, string Str_1, int Int_1) {
        Event = Event_V;
        A_Code_Str = new string [1]; A_Code_Int = new int[1];
        A_Code_Str [0] = Str_1; A_Code_Int [0] = Int_1;
        On_Setting_Event ();
    }

    public virtual void On_Input_Data (string Event_V, string Sub_Event_V, GameObject Script_Go) {
        Event = Event_V; Sub_Event = Sub_Event_V;
        On_Setting_Event ();
    }
    /*
    Data_Item_Input _Data_Item_Input;
    public virtual void On_Input_Data (string Event_V, Data_Item_Input Di_V) {
        Event = Event_V; _Data_Item_Input = Di_V;
        On_Setting_Event ();
    }
    */
    public void On_Input_Panel_Popup_Circle_V1 (Panel_Popup_Circle_V1 Ps) {
        _Panel_Popup_Circle_V1 = Ps;
    }


    public virtual void Cli_Button () {
      //  Debug.LogError ("Cli_Button");
        if (!b_Select_Img) {
            On_Select_Directly ();
            On_Select_Directly_2 ();
        } else {
            if (!b_On_Select) {
                On_Select_Directly ();
            } else {
                Off_Select_Directly ();
                
            }
        }

    }

    void On_Doing_Event () {
        /*
        if (Event == "GI_V2_Show_Reward") { // All_Scene_Go_Script - Panel_Popup_Circle_V1 - Line_Tab_Layout
           // Business_Scene.Ins._Business_Location_Canvas.On_Add_partna_Select_Button (this);
        } else if (Event == "GI_V2_Inventory") {
            if (Sub_Event == "") {
                A_Image[0].gameObject.SetActive (true); A_Image[1].gameObject.SetActive (false);
                All_Scene_Go_Script.Instance._Bg_Inventory.On_Item_Detail (this);
            } else if (Sub_Event == "Daily_Login_Utama") {

            }
        }else if (Event == "GI_V2_Infinity_Duel_Title") {
            
        }else if (Event == "Player_Profile") {
            A_UnityAction[0].Invoke();
        } else if (Event == "Master_Box_Button_Requirements_1") {
           // All_Scene_Go_Script.Instance._Master_Box_Script.On_Get_Target_System_Requirements ().On_Load_And_Check_System_Requirements (A_Code_Int [0]);
        } else if (Event == "Oiwa_Duel_Cards" || Event == "Oiwa_Items" 
        || Event == "Oiwa_Leader_Expansion_Cards" || Event == "Oiwa_Special_Items") {
            All_Scene_Go_Script.Instance._Bg_Select_Mini_Item.On_Show (this);
        }  else if (Event== "Altare_Red_Ticket" || Event == "Altare_Blue_Ticket" || Event == "Altare_Black_Ticket" 
        || Event == "Altare_Yellow_Ticket" || Event == "Altare_Green_Ticket") {
            All_Scene_Go_Script.Instance._Bg_Select_Mini_Item.On_Show (this);
        } 
        */
    }


    void Off_Doing_Event () {
        /*
        if (Event == "GI_V2_Show_Reward") { // All_Scene_Go_Script - Panel_Popup_Circle_V1 - Line_Tab_Layout
          //  Business_Scene.Ins._Business_Location_Canvas.Off_Add_partna_Select_Button ();
        }  else if (Event == "GI_V2_Inventory") {
            
        }else if (Event == "GI_V2_Infinity_Duel_Title") {
            
        }
        */
    }

    bool b_Use_Select_Img = false;
    void On_Setting_Event () {
        if (Event == "GI_V2_Show_Reward" || Event == "GI_V2_Inventory") { // All_Scene_Go_Script - Panel_Popup_Circle_V1 - Line_Tab_Layout
            b_Use_Select_Img = true;
            
            On_Set_Item_Sprite ();
            
            
            A_Image[2].gameObject.SetActive (true);

            if (Sub_Event == "Daily_Login_Utama") {
                if (A_Code_Str [1] == "Claimed") {
                    A_Image[3].gameObject.SetActive (true);
                } else {
                    A_Image[3].gameObject.SetActive (false);
                }
            }
        }
        
    }

    void On_Set_Item_Sprite () {
        /*
        if (_Item_Go != null) {
                if (_Item_Go.On_Get_Class_Item ().Item_Category == "Item" || _Item_Go.On_Get_Class_Item().Item_Category == "Avatar") {
                    A_Image[0].gameObject.SetActive (true); A_Image[1].gameObject.SetActive (false);
                    A_Image [0].sprite = _Item_Go.On_Get_Class_Item ().Item_Sprite;
                    The_Button.image.sprite = All_Scene_Go_Script.Instance.GI_Box_Sprite[_Item_Go.On_Get_Class_Item().Item_Star];
                } 
        } else if (_Card_Information != null) {
                A_Image[0].gameObject.SetActive (false); A_Image[1].gameObject.SetActive (true);
                A_Image [1].sprite = _Card_Information.On_Get_Transfer_Sprite ();
        }

        if (Event == "GI_V2_Show_Reward") {
                A_TMP_Text[0].text = "+ " + A_Code_Int[0].ToString ();
        }  else if (Event == "GI_V2_Inventory") {
                A_TMP_Text[0].text = "x " + A_Code_Int[0].ToString ();
        } else if (Event == "GI_V2_Infinity_Duel_Title") {
                if (_Item_Input_2.Min_Own != _Item_Input_2.Max_Own) {
                    A_TMP_Text[0].text = _Item_Input_2.Min_Own.ToString () + " - " + _Item_Input_2.Max_Own.ToString ();
                } else {
                    A_TMP_Text[0].text = "+ " + _Item_Input_2.Max_Own.ToString ();
                }
        } else if (Event == "Oiwa_Duel_Cards" || Event == "Oiwa_Items" 
        || Event == "Oiwa_Leader_Expansion_Cards" || Event == "Oiwa_Special_Items") {
                int Syarat_Cd = All_Scene_Go_Script.Instance._Filter_System.On_Get_Urutan_Syarat(A_System_Requirements[0], System_Requirements.Syarat.Item_Own);
                string Code_Got = A_System_Requirements[0].A_Int[Syarat_Cd].ToString ();

                A_TMP_Text[0].text = "+ " + Code_Got.ToString ();
        } else if (Event== "Altare_Red_Ticket" || Event == "Altare_Blue_Ticket" || Event == "Altare_Black_Ticket" 
        || Event == "Altare_Yellow_Ticket" || Event == "Altare_Green_Ticket") {
                int Syarat_Cd = All_Scene_Go_Script.Instance._Filter_System.On_Get_Urutan_Syarat(A_System_Requirements[0], System_Requirements.Syarat.Item_Own);
                string Code_Got = A_System_Requirements[0].A_Int[Syarat_Cd].ToString ();

                A_TMP_Text[0].text = "+ " + Code_Got.ToString ();
        }else if (Event == "Show_Mini_Items_Buy" || Event == "Show_Mini_Items_Buy_2") {
                int Syarat_Cd = All_Scene_Go_Script.Instance._Filter_System.On_Get_Urutan_Syarat(A_System_Requirements[0], System_Requirements.Syarat.Item_Own);
                string Code_Got = A_System_Requirements[0].A_Int[Syarat_Cd].ToString ();

                A_TMP_Text[0].text = "+ " + Code_Got.ToString ();
        }
        */
    }
    #region using b_Select_Img
    public bool b_On_Select = false;
    // Line_Tab_Layout :
    public virtual void Off_Select_Directly () {
        b_On_Select = false;
        if (b_Use_Select_Img){ Select_Img.gameObject.SetActive (false);}
        Off_Doing_Event ();
    }
    void On_Select_Directly () {
        On_Doing_Event ();
    }
    // this - Business_Location_Canvas :
    public virtual void On_Select_Directly_2 () {
        b_On_Select = true;
        if (b_Use_Select_Img) {Select_Img.gameObject.SetActive (true);}
    }
    #endregion
    #region Locked_Img
    [SerializeField]
    public Image Locked_Img;
    
    
    /*
    void On_Locked_Img () {
       // A_Locked_Tx[0].text = Code_Str;
        Locked_Img.gameObject.SetActive (true);
        
    }

    void Off_Locked_Img () {
        Locked_Img.gameObject.SetActive (false);
    }
    */
    #endregion
    #region GI_V2_Button
    public virtual void On_Send_Same_Data_To_GI_V2_Button (GI_V2_Button Target) {
        /*
        if (Event == "GI_V2_Inventory") {
            Target.On_Input_Data (Event, A_Code_Str[0], A_Code_Int[0],_Item_Go);
        }
        
        else if (Event == "Oiwa_Duel_Cards" || Event == "Oiwa_Items" 
        || Event == "Oiwa_Leader_Expansion_Cards" || Event == "Oiwa_Special_Items") {
                Target.On_Input_A_System_Requirements (A_System_Requirements);
            if (_Item_Go != null) {
                
                Target.On_Input_Data ("Show_Mini_Items_Buy",A_Code_Str[0], A_Code_Int[0], _Item_Go);
            } else {
                Target.On_Input_Data ("Show_Mini_Items_Buy",A_Code_Str[0], A_Code_Int[0], _Card_Information); 
            }
        } else if (Event== "Altare_Red_Ticket" || Event == "Altare_Blue_Ticket" || Event == "Altare_Black_Ticket" 
        || Event == "Altare_Yellow_Ticket" || Event == "Altare_Green_Ticket") {
                Target.On_Input_A_System_Requirements (A_System_Requirements);
            if (_Item_Go != null) {
                
                Target.On_Input_Data ("Show_Mini_Items_Buy_2",A_Code_Str[0], A_Code_Int[0], _Item_Go);
            } else {
                Target.On_Input_Data ("Show_Mini_Items_Buy_2",A_Code_Str[0], A_Code_Int[0], _Card_Information); 
            }
        }
        */
    }
    #endregion
}
