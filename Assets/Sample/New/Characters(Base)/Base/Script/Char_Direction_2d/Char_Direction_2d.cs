using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Direction_2d : MonoBehaviour {
    [SerializeField]
    Char_Utama _Char_Utama;
    #region Char_AI_Follow - Char_Source
    // Char_AI_Follow, Char_AI_Play_Attack (Char_AI_Play_Attack_Shoot),
    public string Code_Direction_2d = "";
    /*
    Left, Right
    */
    // Char_AI_Follow (Enemy), Dual_Joystick :
    public void On_Char_Direction_2d (string v) {
        Code_Direction_2d = v;
        if (_Char_Utama._Char_Animation._A_Aseprite != null) {
            if (_Char_Utama.Owner == "Enemy") {
                if (Code_Direction_2d == "Left") {
                //  _Char_Utama.transform.localRotation = Quaternion.Euler (270,180,0);
                    _Char_Utama._Char_Animation._A_Aseprite.transform.localRotation = Quaternion.Euler (270,180,0);
                    
                } else if (Code_Direction_2d == "Right") {
                    _Char_Utama._Char_Animation._A_Aseprite.transform.localRotation = Quaternion.Euler (90,0,0);
                }
            }
        }

         _Char_Utama._Char_Utama_Source.On_Get_Char_Direction_2d (Code_Direction_2d);
    }
    
    #endregion
}
