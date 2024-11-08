using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Script : MonoBehaviour
{
    #region Animator
   [SerializeField]
   Animator _Animator;
   // Visual_Novel_Canvas :
   public void On_Set_Animation (string Title) {
      _Animator.SetTrigger (Title);
   }
   #endregion
   #region Destroy
   void On_Destroy (float Delay_Time) {
      StartCoroutine (N_On_Destroy (Delay_Time));
   }

   IEnumerator N_On_Destroy (float Delay_Time) {
      yield return new WaitForSeconds (Delay_Time);
      Destroy (this.gameObject);
   }
   #endregion
   #region Set_Active
   GameObject Target_Set_Active;
    // Visual_Novel_Canvas :
   void On_Set_Active (float Delay_Time, bool b) {
    Target_Set_Active = this.gameObject;
      StartCoroutine (N_On_Set_Active (Delay_Time, b));
   }

   public void On_Set_Active (float Delay_Time, GameObject Si, bool b) {
    Target_Set_Active = Si;
      StartCoroutine (N_On_Set_Active (Delay_Time, b));
   }

   IEnumerator N_On_Set_Active (float Delay_Time, bool bv) {
      yield return new WaitForSeconds (Delay_Time);
      Target_Set_Active.gameObject.SetActive (bv);
   }
   #endregion
}
