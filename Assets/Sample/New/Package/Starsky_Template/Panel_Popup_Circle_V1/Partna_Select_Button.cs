using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Partna_Select_Button : MonoBehaviour {
    [Header ("Centang ini jika sekali button maka akan munculkan Select_Img")]
    [SerializeField]
    bool b_Select_Img = false;
    [Header ("Centang ini jika sekali button maka akan mengubah Sprite The_Button")]
    [SerializeField]
    bool b_Select_Sprite = false;

    [SerializeField]
    Image Select_Img;
    [SerializeField]
    Button The_Button;
    
    

    [HideInInspector] // Business_Location_Canvas, Header_Script :
    public int Code_Int;
    // [SerializeField] // Oiwa_Shop_Canvas, Bg_Infinity_Duel :
    public string Code_Str;
    [SerializeField]
    string Event;
    void Start () {
        if (Event == "Infinity_Battle_Title" ) {
            On_Setting_Event ();
        }
    }
    public void On_Input_Data (string Event_V, int Code_Int_V, string Code_Time_V) {
       Event = Event_V ; Code_Int = Code_Int_V; Code_Str = Code_Time_V;
       On_Setting_Event ();
    }

    public void On_Input_Data (string Event_V, int Code_Int_V) {
       Event = Event_V ; Code_Int = Code_Int_V;
       On_Setting_Event ();
    }

    public void On_Input_Data (string Event_V, string Code_Str_V) { // Header_Script - Panel_Selection_Utama_1, Oiwa_Shop_Canvas
       Event = Event_V ; Code_Str = Code_Str_V;
       On_Setting_Event ();
    }
    // Button, Panel_Selection_Utama_1, Heaqder_Script :
    public void Cli_Button () {
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
        if (Event == "Business_Job_Add") { // Business_Location_Canvas - Panel_Popup_Circle_V1 - Line_Tab_Layout
            Business_Scene.Ins._Business_Location_Canvas.On_Add_partna_Select_Button (this);
        } else if (Event == "Player_Profile_Change_Avatar") {
            All_Scene_Go_Script.Instance._Header_Script.On_Select_Partna (this);
        } else if (Event == "Player_Profile_Change_Header") {
            All_Scene_Go_Script.Instance._Panel_Selection_Utama_1.On_Refresh_Hubungan_Button (this);
            All_Scene_Go_Script.Instance._Header_Script.On_Panel_Selection_Utama_1 (Code_Str);
        } else if (Event == "Player_Profile_Change_Footer") {

        }else if (Event == "Oiwa_Shop_Pilihan_Utama" ) { // Oiwa_Shop_Canvas
            All_Scene_Go_Script.Instance._Oiwa_Shop_Scene._Oiwa_Shop_Canvas.On_Partna_Select_Button (this);
        }else if (Event == "Infinity_Battle_Title" ) { // Oiwa_Shop_Canvas
            All_Scene_Go_Script.Instance._Bg_Infinity_Duel.On_Infinity_Duel_Title (this);
        }
        */
    }


    void Off_Doing_Event () {
        if (Event == "Business_Job_Add") { // Business_Location_Canvas - Panel_Popup_Circle_V1 - Line_Tab_Layout
          //  Business_Scene.Ins._Business_Location_Canvas.Off_Add_partna_Select_Button ();
        } else if (Event == "Player_Profile_Change_Avatar") {

        } else if (Event == "Player_Profile_Change_Header") {

        } else if (Event == "Player_Profile_Change_Footer") {
            
        }else if (Event == "Oiwa_Shop_Pilihan_Utama") { // Oiwa_Shop_Canvas
    
        } else if (Event == "Infinity_Battle_Title" ) { // Oiwa_Shop_Canvas
            
        }
    }

    void On_Setting_Event () {
        /*
        if (Event == "Business_Job_Add") { // Business_Location_Canvas - Panel_Popup_Circle_V1 - Line_Tab_Layout
            b_Select_Img = true; b_Select_Sprite = false;
            The_Button.image.sprite = All_Scene_Go_Script.Instance._Character_Data.On_Get_A_Character_Script ()[Code_Int].Char_Duel_Sprite;
            if (Code_Str != "") {
                On_Locked_Img ();
            } else {
                Off_Locked_Img ();
            }
        } else if (Event == "Player_Profile_Change_Avatar") {
             b_Select_Img = true; b_Select_Sprite = false;
             if (Code_Int >= 0) {
             The_Button.image.sprite = All_Scene_Go_Script.Instance._Character_Data.On_Get_A_Character_Script ()[Code_Int].Char_Duel_Sprite;
             }
             else if (Code_Int == -1) {
                The_Button.image.sprite = All_Scene_Go_Script.Instance.Future_Circle_V1_Sp;
             }
        } else if (Event == "Player_Profile_Change_Header" || Event == "Oiwa_Shop_Pilihan_Utama" || Event == "Infinity_Battle_Title") {
            b_Select_Img = false; b_Select_Sprite = true;
            Sp_On_Select = All_Scene_Go_Script.Instance.Future_2_On;
            Sp_Off_Select = All_Scene_Go_Script.Instance.Future_2_Off;
            The_Button.image.sprite = Sp_Off_Select;
        } 
        */
    }

    #region using b_Select_Img
    bool b_On_Select = false;
    // Line_Tab_Layout, Panel_Selection_Utama_1 :
    public void Off_Select_Directly () {
        b_On_Select = false;
        if (b_Select_Img) {Select_Img.gameObject.SetActive (false);}
        if (b_Select_Sprite) {The_Button.image.sprite = Sp_Off_Select;}
        Off_Doing_Event ();
    }
    void On_Select_Directly () {
        On_Doing_Event ();
    }
    // this - Business_Location_Canvas, Panel_Selection_Utama_1, Oiwa_Shop_Canvas :
    public void On_Select_Directly_2 () {
        b_On_Select = true;
        if (b_Select_Img) {Select_Img.gameObject.SetActive (true);}
        if (b_Select_Sprite) {The_Button.image.sprite = Sp_On_Select;}
    }
    #endregion
    #region using b_Select_Sprite
    Sprite Sp_On_Select, Sp_Off_Select;
    #endregion
    #region Locked_Img
    [SerializeField]
    Image Locked_Img;
    [SerializeField]
    TMP_Text [] A_Locked_Tx;
    
    void On_Locked_Img () {
        A_Locked_Tx[0].text = Code_Str;
        Locked_Img.gameObject.SetActive (true);
        
    }

    void Off_Locked_Img () {
        Locked_Img.gameObject.SetActive (false);
    }
    #endregion
    
}
