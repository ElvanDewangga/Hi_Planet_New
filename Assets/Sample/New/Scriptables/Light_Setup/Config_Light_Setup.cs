using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Untuk Light2D

[CreateAssetMenu(fileName = "New Light Setup", menuName = "StarSky/Create Light Setup", order = 1)]
public class Config_Light_Setup : ScriptableObject
{
    // Transform properties
    public enum Position_Target {Character,Vfx}
    public Position_Target _Position_Target;
    public Vector3 Start_Position;
    public Vector3 Start_Rotation;
    public Vector3 Start_Scale;
    public Vector3 End_Scale;
    public float Speed_Scale;

    // Light2D properties
    public float Start_Intensity = 1f;
    public float End_Intensity = 1f; // Target Intensity
    public float Speed_Intensity = 1f; // Speed of intensity change
    public bool b_Ping_Pong = false; // Ping-pong effect
    public bool b_Loop = false; // Loop effect (infinite ping-pong)

    public Light2D.LightType Start_Light_Type = Light2D.LightType.Point;
    [Header ("Light Type : Freeform")]
    public float Start_Fall_Off = 0.5f;
    public float Start_Fall_Off_Strength = 1f;
    public Color Start_Color = Color.white; // Default to white

    [Header ("Light Type : Spot")]
    // Spot Light specific properties
    public float Start_Spot_OuterRadius = 5f; // Outer radius of spot light
    public float Start_Spot_InnerRadius = 2f; // Inner radius of spot light
    public float Start_Spot_Angle_Inner = 30f; // Spot light angle (outer cone)
     public float Start_Spot_Angle_Outer = 30f; // Spot light angle (outer cone)
}
