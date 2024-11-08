using UnityEngine;

public class Char_Data_Hit_Settings : MonoBehaviour
{
    #region Char_Data_Hit

    public void On_Add_Asset()
    {
        Char_Data.Ins._Char_Data_Hit.On_Add_Dict_Char_Data_Hit(gameObject.name, this);
    }

    public Vector3 V3_Scale;
    public Config_Char_Hit _Config_Char_Hit;

    #endregion
}