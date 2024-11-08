using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Char_Data_Technique : MonoBehaviour {
    [SerializeField]
    Transform A_Attack_Technique;
    #region Char_Utama
    public void On_Load_Char_Technique (Char_Utama Cs, Char_Pack p) {
        // Basic Attack :
        Char_Pack_Technique Cpt = p._Char_Pack_Technique;
        Char_Technique_Settings [] Cts = Cpt.On_Get_A_Char_Technique_Settings ();

        foreach (Char_Technique_Settings s in Cts) {
        Transform childTransform = A_Attack_Technique.transform.Find(s.Technique_Name);
            if (childTransform != null)
            {
                GameObject childObject = childTransform.gameObject;
                // Lakukan sesuatu dengan childObject
                GameObject Ins = GameObject.Instantiate (childObject);
                Cs.On_Get_Game_Object_Char_Technique (Ins, "Basic_Attack");
            }
            else
            {
                Debug.LogError("Child tidak ditemukan. " + s.Technique_Name);
            }    
        }

        // Skill :
        if (p._Char_Pack_Technique_Skill) {
            Char_Pack_Technique Cpt_Skill = p._Char_Pack_Technique_Skill;
            Char_Technique_Settings [] Cts_Skill = Cpt_Skill.On_Get_A_Char_Technique_Settings ();

            foreach (Char_Technique_Settings s in Cts_Skill) {
                Transform childTransform = A_Attack_Technique.transform.Find(s.Technique_Name);
                if (childTransform != null)
                {
                    GameObject childObject = childTransform.gameObject;
                    // Lakukan sesuatu dengan childObject
                    GameObject Ins = GameObject.Instantiate (childObject);
                    Cs.On_Get_Game_Object_Char_Technique (Ins, "Skill_Attack"); // You can see at Char_Attack for Code.
                }
                else
                {
                    Debug.LogError("Child tidak ditemukan. " + s.Technique_Name);
                }    
            }
        }
        
    }

    
    #endregion
    #region Char_Attack_Move
    public Aiming_Direction _Aiming_Direction;
    #endregion
   
}
