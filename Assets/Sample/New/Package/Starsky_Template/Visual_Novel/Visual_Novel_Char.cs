using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Visual_Novel_Char : MonoBehaviour {
   [SerializeField]
   SpriteRenderer Char_Png;
   // Visual_Novel_Event 
   public void On_Set_Sprite (Sprite S) {
    Char_Png.sprite = S;
   } 

   #region Animator
   [SerializeField]
   Animator _Animator;
   // Visual_Novel_World :
   public void On_Set_Animation (string Title) {
      _Animator.SetTrigger (Title);
   }
   #endregion
   #region Destroy
   // Visual_Novel_World :
   public void On_Destroy (float Delay_Time) {
      StartCoroutine (N_On_Destroy (Delay_Time));
   }

   IEnumerator N_On_Destroy (float Delay_Time) {
      yield return new WaitForSeconds (Delay_Time);
      Destroy (this.gameObject);
   }
   #endregion
}
