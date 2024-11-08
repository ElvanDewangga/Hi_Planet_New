using UnityEngine;

[CreateAssetMenu(fileName = "Object_Variant_Config", menuName = "StarSky/Create Config_Vfx_Down_Target", order = 1)]
public class Config_Vfx_Down_Target : ScriptableObject
{
    public Vector3[] A_Position;
    public Vector3[] Target_Position;
    public Vector3[] A_Rotation;
    public float[] A_Delay_Time;
    public float[] A_Speed_Down;

    [Header("Lihat dibagian Config_Variant_Attack :")]
    public int[] A_Finish_Vfx_Code;

    public int[] A_Hilang_Vfx_Code;
}