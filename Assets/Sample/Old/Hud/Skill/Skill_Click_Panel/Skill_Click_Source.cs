using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Click_Source : MonoBehaviour{

    #region Skill_Click_Panel
    Skill_Click_Button Cur_Skill_Click_Button;
    public void On_Set_Skill (Skill_Click_Button Sk) {
        Cur_Skill_Click_Button = Sk;
        if (Char_Data.Ins.Your_Char_Utama_Script != null) {
            Char_Data.Ins.Your_Char_Utama_Script._Char_Attack.On_Set_Skill_Panel_Source (Sk.Code_Button, Sk.Number_Button);
        }
    }
    #endregion
    #region Skill_Countdown (Char_Attack - Char_Utama_Source)
    // Char_Utama_Source :
    public void On_Countdown_Skill (float Countdown_V) {
      Cur_Skill_Click_Button.On_Countdown_Skill (Countdown_V);
    }
    #endregion
}
