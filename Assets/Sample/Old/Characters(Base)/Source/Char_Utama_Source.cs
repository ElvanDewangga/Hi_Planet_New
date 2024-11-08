using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item_Ability;
public class Char_Utama_Source : MonoBehaviour {
   [SerializeField]
   Char_Utama _Char_Utama;
    #region Char_Status
    public virtual void On_Refresh_Cur_Hp (int x) {
        // Refresh Cur_Hp di HUD
       // Debug.Log ("Source : Refresh HUD cur Hp disini");
    }

    public virtual void On_Refresh_Max_Hp (int x) {
        // Refresh Max_Hp di HUD
      //  Debug.Log ("Source : Refresh HUD max Hp disini");
    }

    public void On_Hit_Vfx (GameObject Go_From) {
      Char_Data.Ins._Char_Data_Hit.On_Set_Char_Data_Hit (Go_From, _Char_Utama.gameObject);
    }

    
    #endregion
    #region Char_Attack - Char_Technique
    public void On_Variant_Attack_Vfx (GameObject Go_From, GameObject Go_Peluru, string Type_V) {
      GameObject Cloning = GameObject.Instantiate (Char_Data.Ins._Char_Data_Variant_Attack.gameObject);
      Cloning.transform.SetParent (Go_Peluru.transform);
      Char_Data_Variant_Attack Cloning_Variant = Cloning.GetComponent <Char_Data_Variant_Attack> ();
      Cloning_Variant.On_Set_Clone ();
      // Latest Cloning_Variant.On_Set_Char_Data_Variant_Attack (Go_From, Go_Peluru, Type_V);
       
    }
    
    #endregion
    #region Char_Attack - Skill_Click_Source
    public void On_Countdown_Skill (float Countdown_V) {
      Skill_Click_Panel.Ins._Skill_Click_Source.On_Countdown_Skill (Countdown_V);
    }
    #endregion
    #region Char_Level
    public virtual void On_Refresh_Level (int Level_V) {
       // All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Level (Level_V);
    }

    public virtual void On_Refresh_Exp (int Cur_Exp_V, int Max_Exp_V) {

    }

    public void On_Save_Level_And_Exp (int Level_V, int Exp_V) {
      string [] Host_Server_Value = new string [7]; // 1 For Id *3 for (table,title,value) *2 for (Id & Own)
            string [] Host_Server_Field = new string [7];
            Host_Server_Field[0] = "Id";Host_Server_Value[0] = Data_Game_Utama.Ins._Data_Game_Account.Id;
            Host_Server_Field[1] = "table_1";Host_Server_Value[1] = "Db_Equipment";
            Host_Server_Field[2] = "title_1";Host_Server_Value[2] = "Level";
            Host_Server_Field[3] = "value_1";Host_Server_Value[3] = Level_V.ToString ();

            Host_Server_Field[4] = "table_2";Host_Server_Value[4] = "Db_Equipment";
            Host_Server_Field[5] = "title_2";Host_Server_Value[5] = "Cur_Exp";
            Host_Server_Field[6] = "value_2";Host_Server_Value[6] = Exp_V.ToString ();

            Data_Game_Utama.Ins._Data_Game_Equipment.StartSave (Host_Server_Field, Host_Server_Value);
        Debug.Log ("Save Exp and level");
    }
    #endregion
    #region Char_Equipment
    
    public void On_Get_Data_Item_Input_Inventory (int s, Data_Item_Input To) {
      /*
      var original = All_Scene_Go.Ins._Inventory.Inventory_Data_Item_Input[s];
      return new Data_Item_Input(original);
      */
      Data_Item_Input From = All_Scene_Go.Ins._Inventory.Inventory_Data_Item_Input[s];
      On_Transfer_Data_Item_Input (From, To);
    }
  /*
    public void On_Transfer_Data_Item_Input (Data_Item_Input From, Data_Item_Input To) {
      To.On_Transfer_Data_Item_Input (From,To);
    }
*/
    // Char_Equipment :
   public void On_Transfer_Data_Item_Input (Data_Item_Input From, Data_Item_Input To) {
      Convert_Item_Effect Ci = new Convert_Item_Effect ();
      Ci.On_Transfer_Data_Item_Input (From, To);
   }

    public void On_Refresh_Data_Item_Input (Data_Item_Input To) {
        Convert_Item_Effect Ci = new Convert_Item_Effect ();
      Ci.On_Refresh_Data_Item_Input (To); 
    }
    #endregion
    #region Char_AI_Event
      #region Char_AI_Follow
      public void On_Char_Direction_2d (string v) {
        _Char_Utama._Char_Direction_2d.On_Char_Direction_2d (v);
      }
      #endregion
    #endregion
    #region Char_Direction_2d
    public void On_Get_Char_Direction_2d (string v) {
      _Char_Utama._Char_AI._Char_AI_Attack.On_Get_Char_Direction_2d (v);
      _Char_Utama._Char_AI._Char_Utama.On_Get_Char_Direction_2d (v);
      _Char_Utama._Char_AI._Char_Utama.On_Get_Char_Direction_2d (v);
      if (_Char_Utama._Char_Animation._A_Aseprite.A_Object_Target_Rotation_With_Direction.Length >0) {
        /* Latest
        _Char_Utama._Char_Animation._A_Aseprite.A_Object_Target_Rotation_With_Direction [0].On_Get_Char_Direction_2d (v);
        _Char_Utama._Char_Animation._A_Aseprite.A_Object_Target_Rotation_With_Direction [1].On_Get_Char_Direction_2d (v);
        */
      }
    }
    #endregion
}
