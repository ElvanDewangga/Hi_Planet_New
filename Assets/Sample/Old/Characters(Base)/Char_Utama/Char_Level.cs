using UnityEngine;

public class Char_Level : MonoBehaviour
{
    [SerializeField] private Char_Utama _Char_Utama;

    public int Level;
    public int Exp;
    private int Max_Exp;

    private void On_Get_Max_Exp()
    {
        Max_Exp = Char_Data.Ins._Char_Data_Level.On_Get_Next_Target_Exp(Level);
        On_Check_Level_Up();
    }

    private void Example_Inc_Exp()
    {
        // Menambah Exp 15 :
        On_Inc_Exp(15);
    }

    // Test_Button :
    public void On_Inc_Exp(int b)
    {
        if (Level < Char_Data.Ins._Char_Data_Level.Max_Level)
        {
            Exp += b;
            On_Check_Level_Up();
        }

        On_Save_Level_And_Exp();
    }

    private void On_Check_Level_Up()
    {
        if (Exp >= Max_Exp)
        {
            Exp = Exp - Max_Exp;
            Level++;
            On_Inc_Bonus_Point(4);
            Debug.Log("Level Up");
            On_Get_Max_Exp();
        }

        if (_Char_Utama.Owner == "Player")
        {
            _Char_Utama._Char_Utama_Source.On_Refresh_Level(Level);
            _Char_Utama._Char_Utama_Source.On_Refresh_Exp(Exp, Max_Exp);
            // All_Scene_Go.Ins._Hud_Canvas.On_Refresh_Level (Level);
        }
    }

    #region Char_Utama

    #region Get_Data

    public void On_Get_Data_From_Char_Utama(int Level_V, int Bonus_Point_V, int Cur_Exp_V)
    {
        Level = Level_V;
        Bonus_Point = Bonus_Point_V;
        Exp = Cur_Exp_V;
        On_Get_Max_Exp();
    }

    #endregion

    #endregion

    #region Save

    private void On_Save_Level_And_Exp()
    {
        Char_Data.Ins.Your_Char_Utama_Script._Char_Utama_Source.On_Save_Level_And_Exp(Level, Exp);
    }

    #endregion

    #region Bonus_Point

    public int Bonus_Point;

    // this, Char_Data_Bonus_Point :
    public void On_Inc_Bonus_Point(int b)
    {
        Bonus_Point += b;
    }

    #endregion
}