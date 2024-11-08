using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.X509;
using UnityEngine;

[CreateAssetMenu(fileName = "Config_Char_Hit", menuName = "StarSky/Create Config Char Hit", order = 1)]
public class Config_Char_Hit : ScriptableObject {
    public GameObject Hit_Vfx;
    [Header ("Atur waktu Vfx ini sebelum dihancurkan")]
    public float Destroy_Duration;

   public Hit_Type _Hit_Type;
   public Vector3 V3_Position;
   public Vector3 V3_Scale;
   public Vector3 V3_Scale_Child;

   public bool b_Light_Setup = false;
   public Config_Light_Setup _Config_Light_Setup;
   
   public enum Hit_Type{
    Default
   }
}
