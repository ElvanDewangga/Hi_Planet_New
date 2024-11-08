using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Pack_Technique : MonoBehaviour {
    [SerializeField]
    Char_Technique_Settings [] A_Char_Technique_Settings;

    #region Char_Data_Technique
    public Char_Technique_Settings [] On_Get_A_Char_Technique_Settings () {
        return A_Char_Technique_Settings;
    }
    #endregion
}
