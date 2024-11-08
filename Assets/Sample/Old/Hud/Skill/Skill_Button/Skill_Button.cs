using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Button : MonoBehaviour
{
    [SerializeField]
    Skill_Panel _Skill_Panel;
    [SerializeField]
    Image Select_Skill_Img;

    public string Code_Button; // "Basic_Attack", "Skill_Attack"
    public int Number_Button; // 0,1,2
    public void On_Skill_Button () {
        Select_Skill_Img.color = _Skill_Panel.Select_On_Color;
        _Skill_Panel.On_Skill_Button (this);
    }

    public void Off_Skill_Button () {
        Select_Skill_Img.color = _Skill_Panel.Select_Off_Color;
    }
}
