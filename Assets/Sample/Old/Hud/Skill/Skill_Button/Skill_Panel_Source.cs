using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Panel_Source : MonoBehaviour {

    #region Skill_Panel
    public void On_Set_Skill (Skill_Button Sk) {
        if (Char_Data.Ins.Your_Char_Utama_Script != null) {
            Char_Data.Ins.Your_Char_Utama_Script._Char_Attack.On_Set_Skill_Panel_Source (Sk.Code_Button, Sk.Number_Button);
        }
    }
    #endregion
}
