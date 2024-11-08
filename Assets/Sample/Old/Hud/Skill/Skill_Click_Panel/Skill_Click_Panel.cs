using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Click_Panel : MonoBehaviour {
    public static Skill_Click_Panel Ins;
    void Awake () {
        Ins = this;
    }
    
    [SerializeField]
    Skill_Click_Button Skill_Click_Button_Attack;
    [SerializeField]
    Skill_Click_Button [] A_Skill_Click_Button;
    [SerializeField]
    public Skill_Click_Source _Skill_Click_Source;
    #region Skill_Click_Button
    Skill_Click_Button Cur_Skill_Click_Button;
    public void On_Skill_Click_Button (Skill_Click_Button Sk) {
        Cur_Skill_Click_Button = Sk;
        _Skill_Click_Source.On_Set_Skill (Cur_Skill_Click_Button);
    }
    #endregion  

    
    
}
