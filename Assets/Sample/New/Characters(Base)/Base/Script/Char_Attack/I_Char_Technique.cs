using UnityEngine;

public interface I_Char_Technique
{ // Char_Attack, Char_Animation
    public void On_Char_Technique_Max (int s);
    public void On_Get_Game_Object_Char_Technique (GameObject Go, string Code_Go);
    public void On_Set_Char_Utama (Char_Utama Cu);
}