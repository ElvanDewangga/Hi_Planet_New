using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Object_Variant_Config", menuName = "StarSky/Create Object Variant Config", order = 1)]
public class Object_Variant_Config : ScriptableObject
{
    public enum Animation_Part
    {
        Static,
        Dynamic
    }

    public enum Object_Type
    {
        Default,
        Vfx,
        Shader
    }

    public enum Vfx_Spawn_Target
    {
        Default,
        Character,
        Bullet,
        Hand_Right_Back,
        Hand_Right_Front
    }


    [Header("Cd Time")] public float Base_Duration;

    [Header("Atur waktu Vfx ini sebelum dihancurkan")]
    public float Destroy_Duration;

    public Animation_Part _Animation_Part;
    public bool b_Delay_Animation;
    public AnimationClip Delay_Animation;
    public bool b_Vfx_Spam;
    public bool b_Hit_Vfx;
    public Config_Char_Hit _Config_Char_Hit;
    public bool b_Skill_Effect;
    public Skill_Effect_Setup _Skill_Effect_Setup;


    public bool b_Vfx_Dash;

    public Vector3 V3_Vfx_Dash_Target;

    // Char_Data_Variant_Attack :
    public Object_Delay[] objectsWithDelay; // Array untuk menyimpan objek dan waktu delaynya.

    [Serializable]
    public class Object_Delay
    {
        public Object_Type _Object_Type;
        public GameObject gameObject;
        public float delayTime;

        public Vector3 V3_Scale;
        public Vector3 V3_Child_Scale;
        public Vector3 V3_Offset_Vfx;
        public Vfx_Spawn_Target _Vfx_Spawn_Target;
        public Vector3 V3_Fix_Position;
        public int Code_Target_Position_Fix;
        public bool b_Up_Left;
        public bool b_Up_Right;
        public bool b_Down_Left;
        public bool b_Down_Right;
        public Vector3 V3_Rotation;
        public bool b_Fix_Rotation;

        public float Vfx_Time;
        public bool b_Disable_Equals_Effect_Duration;

        public bool b_Down_Target;

        public Vector3 Down_Target_Col_Scale;

        // Offset muncul hanya ketika b_Down_Target dicentang
        public Vector3 Down_Target_Offset;
        public Config_Vfx_Down_Target _Config_Vfx_Down_Target;

        public bool b_Rotate_Around;
        public Vector3 Titik_Tengah_Start;
        public float Rotate_Around_Speed;

        public bool b_Follow_Char;
        public bool b_Hide_Object;
        public bool Hide_Object_Hand_Left;
        public bool Hide_Object_Hand_Right;

        public bool b_Vfx_Damage;
        public int Vfx_Damage_Power;
        public Vector3 V3_Scale_Damage;
        public Vector3 V3_Offset_Damage;

        public bool b_Limit_Hit;

        public int Limit_Hit;

        // public Vector3 [] A_V3_Child_Scale;
        public bool b_Active_When_Damage;

        public bool b_Hide_Vfx;

        public bool b_Light_Setup;
        public Config_Light_Setup _Config_Light_Setup;


        #region Object_Type (Shader)

        public string[] A_Shader_Component_Name;
        public string C_Code_Skin;

        #endregion
    }
}