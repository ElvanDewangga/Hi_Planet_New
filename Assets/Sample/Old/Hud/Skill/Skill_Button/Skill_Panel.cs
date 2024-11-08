using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Skill_Panel : MonoBehaviour {
    [SerializeField]
    Skill_Panel_Source _Skill_Panel_Source;
   [SerializeField]
   Skill_Button [] A_Skill_Button;

   Skill_Button Cur_Skill_Button;

    public Color Select_On_Color;
    public Color Select_Off_Color;
    
    void Start () {
        A_Skill_Button[0].On_Skill_Button ();
    }
    #region Skill_Panel
    public void On_Skill_Button (Skill_Button Sb) {
        Cur_Skill_Button = Sb;
        _Skill_Panel_Source.On_Set_Skill (Cur_Skill_Button);
        
        foreach (Skill_Button St in A_Skill_Button) {
            if (St == Sb) {
                
            } else {
                St.Off_Skill_Button ();
            }
        }
    }
    #endregion
}
