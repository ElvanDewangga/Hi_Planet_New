using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hi_Planet_Char_Utama_Source : Char_Utama_Source {
   
    public override void On_Refresh_Cur_Hp (int x) {
       All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Cur_Hp (x);
    }

    public override void On_Refresh_Max_Hp (int x) {
        All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Max_Hp (x);
    }

    #region Char_Level
    public override void On_Refresh_Level (int Level_V) {
        All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Level (Level_V);
    }

    public override void On_Refresh_Exp (int Cur_Exp_V, int Max_Exp_V) {
        All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Exp (Cur_Exp_V, Max_Exp_V);
    }
    #endregion
}
