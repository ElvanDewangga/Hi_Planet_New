using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Skill_Click_Button : MonoBehaviour
{
    [SerializeField]
    Skill_Click_Panel _Skill_Click_Panel;
    [SerializeField]
    Button Button_Interact;
    public string Code_Button; // "Basic_Attack", "Skill_Attack"
    public int Number_Button; // 0,1,2

    public void On_Click_Button () {
        _Skill_Click_Panel.On_Skill_Click_Button (this);
    }

    #region Countdown_Display
    [SerializeField]
    Image Bg_Countdown;
    [SerializeField]
    TMP_Text Countdown_Tx;
    #endregion
    #region Skill_Click_Source
        #region Countdown
        float Countdown = 0.0f;
        public void On_Countdown_Skill (float Countdown_V) {
            Button_Interact.interactable = false;
            Countdown = Countdown_V;
            C_N_Countdown_Skill = StartCoroutine (N_Countdown_Skill ());
            Bg_Countdown.gameObject.SetActive (true);
            
        }
        
        Coroutine C_N_Countdown_Skill;
        IEnumerator N_Countdown_Skill() {
           // isCooldown = true;
            float Remaining_Time = Countdown;
            while (Remaining_Time > 0) {
                // Update UI or do something every 0.1 seconds
               // Debug.Log($"Time remaining: {Remaining_Time}");
                Countdown_Tx.text = Remaining_Time.ToString ("F1");
                Remaining_Time -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }

            //isCooldown = false;
          //  Debug.Log("Cooldown finished!");
          Off_Countdown_Skill ();
        }

        void Pause_Countdown_Skill () {
            StopCoroutine (C_N_Countdown_Skill);
        }

        void Off_Countdown_Skill () {
            Bg_Countdown.gameObject.SetActive (false);
            Button_Interact.interactable = true;
        }
        #endregion
    #endregion
}
