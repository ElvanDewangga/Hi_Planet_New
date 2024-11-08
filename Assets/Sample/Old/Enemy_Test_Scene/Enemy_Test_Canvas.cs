using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Test_Canvas : MonoBehaviour {
   [SerializeField]
   Image Enemy_List_Panel;
   [SerializeField]
   Image Option_List_Panel;
   [SerializeField]
   Button Test_Button; 
   public void On_Spawn_Enemy_Test (string Code_Pack_V) {
        Enemy_Test_Scene.Ins._Char_Spawn.On_Set_Code_Char_Pack ("Enemy", Code_Pack_V);
        Enemy_List_Panel.gameObject.SetActive (false);
        
   } 

   public void On_Test_Enemy_Scene () {
    Test_Button.gameObject.SetActive (false);
    Enemy_Test_Scene.Ins.On_Masuk_Scene ();
   }

   public void On_Choose_Option (string Code_V) {
    Char_Utama_Select._Char_AI.On_Set_Attack_Code (-1);
     if (C_N_On_Test_Animation_Damage != null) {StopCoroutine (C_N_On_Test_Animation_Damage);}
    if (Code_V == "Move_Target") {
          Code_V = "Char_AI_Follow";
          Char_Utama_Select._Char_AI.On_Set_AI (Code_V);
    } else if (Code_V == "Automatic") {

    } else if (Code_V == "Idle") {
     Code_V = "Char_AI_Idle";
     Char_Utama_Select._Char_AI.On_Set_AI (Code_V);
    } else if (Code_V == "Damage") {
     Code_V = "Char_AI_Idle";
     Char_Utama_Select._Char_AI.On_Set_AI (Code_V);
     On_Test_Animation_Damage ();
    } else if (Code_V == "Dead") {
     Code_V = "Char_AI_Idle";
     Char_Utama_Select._Char_AI.On_Set_AI (Code_V);
     On_Test_Animation_Dead ();
    } else if (Code_V == "Attack_1" ||Code_V == "Attack_2"|| Code_V == "Attack_3" || Code_V == "Attack_4" ) {
      if (Code_V == "Attack_1") {
        Char_Utama_Select._Char_AI.On_Set_Attack_Code (0);
      } else if (Code_V == "Attack_2") {
        Char_Utama_Select._Char_AI.On_Set_Attack_Code (1);
      } else if (Code_V == "Attack_3") {
        Char_Utama_Select._Char_AI.On_Set_Attack_Code (2);
      } else if (Code_V == "Attack_4") {
        Char_Utama_Select._Char_AI.On_Set_Attack_Code (3);
      }
      Code_V = "Char_AI_Attack";
      

      Char_Utama_Select._Char_AI.On_Set_AI (Code_V);
    }
    Option_List_Panel.gameObject.SetActive (false);
    
   }

   #region Hi_Planet_Object_Click
   [SerializeField]
   Char_Utama Char_Utama_Select;

   public void On_Char_Utama_Select (GameObject s) {
     Char_Utama_Select = s.GetComponent <Char_Utama> ();
     
   } 
    public void On_Open_Option_Panel () {
         Option_List_Panel.gameObject.SetActive (true);
    }
   #endregion

   #region Enemy_Test_Scene
    public void On_Display () {
        Enemy_List_Panel.gameObject.SetActive (true);
    }
    
   #endregion

   #region Test_Animation_Damage
   void On_Test_Animation_Damage () {
     C_N_On_Test_Animation_Damage = StartCoroutine (N_On_Test_Animation_Damage ());
   }
   Coroutine C_N_On_Test_Animation_Damage ;  
  IEnumerator N_On_Test_Animation_Damage () {
     yield return new WaitForSeconds (1.5f);
     Char_Utama_Select._Char_Status.On_Cur_Hp (-10, null);
     C_N_On_Test_Animation_Damage = StartCoroutine (N_On_Test_Animation_Damage ());
  }
   #endregion
   #region Test_Animation_Dead
    void On_Test_Animation_Dead () {
      Char_Utama_Select._Char_Status.On_Instant_Damage (null);
   }
   #endregion
}
