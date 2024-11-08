using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starsky;
using UnityEngine.Events;
public class Load_Csv : MonoBehaviour {
   public static Load_Csv Ins;
    void Awake () {
        Ins = this;
    }
   void Start () {
    // Load_Data ();
   }
    #region Dictionary
    Dictionary <string, UnityAction> Dict_Unity_Action_Type = new Dictionary<string, UnityAction> ();
    Dictionary <string, object> Dict_Unity_Action_Title = new Dictionary<string, object> ();
    #endregion
   #region Implementation
   bool b_Load_Dict = false;
   string Title_Csv, Type_Csv;
   // Visual_Novel_System :
    public void On_Load_Data (string Title, string Type) {
        Title_Csv = Title; Type_Csv = Type;
        if (!b_Load_Dict) {
            b_Load_Dict = true;
           
            Dict_Unity_Action_Title = new Dictionary<string, object> ();
            Dict_Unity_Action_Title.Add ("Visual_Novel_Res/Dialog_Test", Visual_Novel_System.Ins);
            Dict_Unity_Action_Title.Add ("Visual_Novel_Res/Opening_Act", Visual_Novel_System.Ins);
            Dict_Unity_Action_Title.Add ("Visual_Novel_Res/First_Home", Visual_Novel_System.Ins);

             Dict_Unity_Action_Type = new Dictionary<string, UnityAction> ();
            Dict_Unity_Action_Type.Add ("Visual_Novel_Kalimat", On_Visual_Novel_Kalimat);
        }

        Load_Data ();
    }
   #endregion
    
    List <Dictionary<string,object>> data = new List<Dictionary<string, object>>  ();
    List <Dictionary<string,object>> data_2 = new List<Dictionary<string, object>>  ();
   void Load_Data () {
        data = new List<Dictionary<string, object>>  ();
        data_2 = new List<Dictionary<string, object>>  ();

        data = CSVReader.Read (Title_Csv);
        data_2 = CSVReader.Read_2 (Title_Csv);

        Dict_Unity_Action_Type[Type_Csv] ();
   }
 
   void On_Visual_Novel_Kalimat () {
        for (var i = 0; i < data.Count; i++) {
                Visual_Novel_Kalimat Cn_Data = new Visual_Novel_Kalimat ();
               
                Cn_Data.Name_Char = data[i]["Name_Char"].ToString ();
                Cn_Data.Dialog = data[i]["Dialog"].ToString ();
                Cn_Data.Char_Event = data[i]["Char_Event"].ToString ();
                Cn_Data.System_Event_1 = data[i]["System_Event_1"].ToString ();
                Cn_Data.System_Event_2 = data[i]["System_Event_2"].ToString ();

                // int Number = int.Parse (data[i]["Number"].ToString (), System.Globalization.NumberStyles.Integer);
                
                // END
                
                On_Send_Data (Cn_Data);
        }
           
    } 
   
   void On_Send_Data (object s) {
        if (Dict_Unity_Action_Title[Title_Csv] is Visual_Novel_System script){
            script.On_Add_L_Visual_Novel_Kalimat((Visual_Novel_Kalimat)s);
        }
      
   }

   void Add_Data_Curse_Cards (int Number, string Name_Card, string Type, string Card_Effect,string [] A_Card_Effect, string [] A_Code_Effect) {
        /*
        Card_Curse s = new Card_Curse ();
        s.Number = Number;
        s.Name_Card = Name_Card;
        s.Type = Type;
        s.Card_Effect = Card_Effect;
        s.A_Card_Effect = A_Card_Effect;
        s.A_Code_Effect = A_Code_Effect;
        s.On_Convert_A_Code_Effect_To_L_C_Card_Effect ();
        Source_Scene.Ins._Database_Card.On_Add_L_Card_Curse (s); 
        */  
   }

   void Add_Data_Blessing_Cards (int Number, string Name_Card, string Type, string Card_Effect, string [] A_Card_Effect, string [] A_Code_Effect) {
        /*
        Card_Blessing u = new Card_Blessing ();
        u.Number = Number;   
        u.Name_Card = Name_Card; 
        u.Type = Type;
        u.Card_Effect = Card_Effect;
        u.A_Card_Effect = A_Card_Effect;
        u.A_Code_Effect = A_Code_Effect;
        u.On_Convert_A_Code_Effect_To_L_C_Card_Effect ();
        Source_Scene.Ins._Database_Card.On_Add_L_Card_Blessing (u); 
        */
   }

   void Add_Data_Spirit_Cards (int Number, string Name_Card, int Attack, int Defense, string Attribute, int Soul,string Extra_Card, string Card_Effect, string [] A_Card_Effect, string [] A_Code_Effect) {
        /*
        Card_Spirit u = new Card_Spirit ();
        u.Number = Number;   
        u.Name_Card = Name_Card; 
        u.Attack = Attack;
        u.Defense = Defense;
        u.Attribute = Attribute;
        u.Soul = Soul;
        u.Extra_Card = Extra_Card;
        u.Card_Effect = Card_Effect;
        u.A_Card_Effect = A_Card_Effect;
        u.A_Code_Effect = A_Code_Effect;
        u.On_Convert_A_Code_Effect_To_L_C_Card_Effect ();
        Source_Scene.Ins._Database_Card.On_Add_L_Card_Spirit (u);
        */
   } 

   void Add_Data_List_Cards (int Number, string Name_Card, string Type_Card) {
        /*
        Card_List u = new Card_List ();
        u.Number = Number;   
        u.Name_Card = Name_Card; 
        u.Type_Card = Type_Card;
      
        Source_Scene.Ins._Database_Card.On_Add_L_Card_List (u);
        */
   } 

    
}
