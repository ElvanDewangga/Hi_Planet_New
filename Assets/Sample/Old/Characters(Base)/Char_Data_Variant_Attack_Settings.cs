using UnityEngine;

public class Char_Data_Variant_Attack_Settings : MonoBehaviour
{
    #region Object_Varian_Config

    public Object_Variant_Config config; // Reference ke ScriptableObject

    #endregion

    #region Char_Data_Variant_Attack

    public void On_Add_Asset()
    {
        Char_Data.Ins._Char_Data_Variant_Attack.On_Add_Dict_Char_Data_Variant_Attack(gameObject.name, this);
    }

    /*
    public Vector3 V3_Scale;
    public Vector3 V3_Offset_Vfx;
    public Vector3 V3_Fix_Position;
    */

    #endregion

    #region Char_Data_Variant_Attack

    // Ini akan digunakan untuk mempermudah Test :
    private Char_Data_Variant_Attack_Settings Real_Char_Data_Variant_Attack_Settings;

    public void On_Set_Char_Data_Variant_Attack_Settings(Char_Data_Variant_Attack_Settings Cs)
    {
        Real_Char_Data_Variant_Attack_Settings = Cs;
        // V3_Scale = Cs.V3_Scale;
        //  V3_Offset_Vfx = Cs.V3_Offset_Vfx;
        //  V3_Fix_Position = Cs.V3_Fix_Position;
    }

    #endregion
}