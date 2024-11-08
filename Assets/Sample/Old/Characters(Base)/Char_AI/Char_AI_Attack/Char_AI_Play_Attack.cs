using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_AI_Play_Attack : MonoBehaviour {
    public string Code_Char_AI_Play; // "Char_AI_Play_Attack_Shoot"
   public Char_AI _Char_AI;
   public Char_AI_Attack _Char_AI_Attack;
   public Char_Technique _Char_Technique;
    public bool b_Play = false;
    public string Animation_Attack_Param = "Attack_1";
// Char_AI_Attack :
   public virtual void On_Set_Char_Technique (Char_Technique Ct) {
        _Char_Technique = Ct;
   }

   public virtual void Play () {
    b_Play = true;
   }

   public virtual void Stop () {
    b_Attack = false;
    b_Play = false;
   }

   public virtual void On_Get_Position () {
        
   }

   public virtual void On_Attack () {
    if (!b_Attack) {
            b_Attack = true;
            Debug.Log("Enemy attacking!");
            //_Char_Technique.gameObject.SetActive (true);
            playVFX ();
            
           
            // b_Attack = false;
            StartCoroutine (N_CD_Attack ());
        }
   }

   public void playVFX()
   {
        _Char_Technique.On_Slash(_Char_AI._Char_Utama);
       // Debug.Log("CharTech_Onslash");
   }

    // Hanya SEMENTARA :
   IEnumerator N_CD_Attack () {
    Object_Variant_Config Obj = _Char_Technique.On_Get_Object_Variant_Config ();
    if (Obj.b_Delay_Animation) {
        _Char_AI._Char_Utama._Char_Animation.On_Play_Animation ("Delay_" +Animation_Attack_Param);
        Debug.Log ("Delay" +Obj.Delay_Animation.length);
        yield return new WaitForSeconds (Obj.Delay_Animation.length);
        
    } else {
        Debug.Log ("Animation");
        _Char_AI._Char_Utama._Char_Animation.On_Play_Animation (Animation_Attack_Param);
    }
    yield return new WaitForSeconds (_Char_Technique.Cd_Time);
    
    _Char_AI._Char_Utama._Char_Animation.On_Play_Animation ("Idle");
    b_Attack = false;
   }

   public Vector3 attackCenterOffset = Vector3.zero;
   public Vector3 Real_Attack_Center_Offset;
   public virtual void On_Change_Direction_2d (string v) {
      if (v == "Left") {
            attackCenterOffset =Real_Attack_Center_Offset * -1;
        } else if (v == "Right") {
            attackCenterOffset = Real_Attack_Center_Offset;
        }
   }

   public bool b_Attack = false;

}
