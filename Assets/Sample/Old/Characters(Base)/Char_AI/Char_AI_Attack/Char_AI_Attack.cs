using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_AI_Attack : Char_AI_Event {
    [SerializeField]
    Char_AI _Char_AI;
    [SerializeField]
    public Transform A_Char_AI_Play_Attack;
    
   public override void Play () {
    base.Play ();
    On_Play ();
   }

   public override void Stop () {
    base.Stop ();
    if (Cur_Char_AI_Play_Attack !=null) {
        Cur_Char_AI_Play_Attack.Stop ();
    }
   }
  
   Char_AI_Play_Attack Cur_Char_AI_Play_Attack; 
   void On_Play () {
    // Debug.Log ("Attack 1");
        if (Attack_Code == -1) {
            int Child_Total = A_Char_AI_Play_Attack.childCount;
            if (Child_Total > 0) {
                Cur_Char_AI_Play_Attack = A_Char_AI_Play_Attack.GetChild (Random.Range (0,Child_Total)).GetComponent <Char_AI_Play_Attack> ();
                Char_Technique Technique_Req = On_Get_Char_Technique_Equals_Requirements (_Char_AI._Char_Utama._Char_Animation.On_Get_Attack_Go (), Cur_Char_AI_Play_Attack.Code_Char_AI_Play);
                if (Technique_Req) {
                    Cur_Char_AI_Play_Attack.On_Set_Char_Technique(Technique_Req);
                    Cur_Char_AI_Play_Attack.Play (); 
                }
            } else {
                Cur_Char_AI_Play_Attack = null;
            }
        } else {
            int Child_Total = A_Char_AI_Play_Attack.childCount;
            if (Child_Total > 0) {
                Cur_Char_AI_Play_Attack = A_Char_AI_Play_Attack.GetChild (Attack_Code).GetComponent <Char_AI_Play_Attack> ();
                // Char_Technique Technique_Req = On_Get_Char_Technique_Equals_Requirements (_Char_AI._Char_Utama._Char_Animation.On_Get_Attack_Go (), Cur_Char_AI_Play_Attack.Code_Char_AI_Play);
                Char_Technique [] A_Char_Technique = _Char_AI._Char_Utama._Char_Animation.On_Get_Skill_Go ();
                Char_Technique Technique_Req =A_Char_Technique[Attack_Code];
                if (Technique_Req) {
                    Cur_Char_AI_Play_Attack.On_Set_Char_Technique(Technique_Req);
                    Cur_Char_AI_Play_Attack.Play (); 
                }
            } else {
                Cur_Char_AI_Play_Attack = null;
            }
        }
   }

   Char_Technique On_Get_Char_Technique_Equals_Requirements (Char_Technique [] A_Tec, string Code_Char_AI_Play_V) {
    Char_Technique Res =null;
    foreach (Char_Technique ct in A_Tec) {
        if (ct.Code_Char_AI_Play == Code_Char_AI_Play_V) {
            Res = ct;
        }
    }
    return Res;
   } 


   #region Char_Utama_Source
        #region Char_Direction_2d
        public void On_Get_Char_Direction_2d (string s) {
            foreach (Transform trs in A_Char_AI_Play_Attack.transform) {
                trs.GetComponent <Char_AI_Play_Attack> ().On_Change_Direction_2d (s);
            }
        }
        #endregion
   #endregion
   #region Char_AI
   [SerializeField]
   int Attack_Code = -1;
   // -1 : Tidak ada target
   // 0 : Basic Attack
   // 1 - 3 : Skill
    public void On_Set_Attack_Code (int Attack_Code_V) {
        Attack_Code = Attack_Code_V;
    }
   #endregion
   
  
}
