using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Untuk Light2D

public class Logic_Light_Setup : MonoBehaviour
{
    [SerializeField]
    GameObject Light_Ins;

    #region Char_Data_Varriant_Attack
    public void ApplySetup(Transform Char_Position, Transform Vfx_Go, Config_Light_Setup _Config_Light_Setup)
    {
        GameObject Ins = Instantiate(Light_Ins);
        Ins.transform.rotation = Vfx_Go.rotation * Quaternion.Euler(_Config_Light_Setup.Start_Rotation);
        
        // Apply Transform properties
        if (_Config_Light_Setup._Position_Target == Config_Light_Setup.Position_Target.Character)
        {
            Ins.transform.position = Char_Position.position + _Config_Light_Setup.Start_Position;
        }
        else if (_Config_Light_Setup._Position_Target == Config_Light_Setup.Position_Target.Vfx)
        {
            Ins.transform.position = Vfx_Go.position + _Config_Light_Setup.Start_Position;
        }

        Ins.transform.localScale = _Config_Light_Setup.Start_Scale;

        Light2D targetLight = Ins.GetComponentInChildren<Light2D>();
        
        // Apply common Light2D properties
        targetLight.intensity = _Config_Light_Setup.Start_Intensity;
        targetLight.color = _Config_Light_Setup.Start_Color;
        targetLight.lightType = _Config_Light_Setup.Start_Light_Type;

        // Handle light type specific settings
        switch (targetLight.lightType)
        {
            case Light2D.LightType.Freeform:
                targetLight.falloffIntensity = _Config_Light_Setup.Start_Fall_Off_Strength;
                targetLight.shapeLightFalloffSize = _Config_Light_Setup.Start_Fall_Off;
                break;

            case Light2D.LightType.Point:
                targetLight.pointLightInnerRadius = _Config_Light_Setup.Start_Spot_InnerRadius;
                targetLight.pointLightOuterRadius = _Config_Light_Setup.Start_Spot_OuterRadius;
                targetLight.pointLightInnerAngle = _Config_Light_Setup.Start_Spot_Angle_Inner;
                targetLight.pointLightOuterAngle = _Config_Light_Setup.Start_Spot_Angle_Outer;
                break;
        }

        Ins.transform.SetParent (Vfx_Go);
        Ins.gameObject.SetActive(true);

        // Start intensity adjustment coroutine
        if (_Config_Light_Setup.End_Intensity > 0) {
            StartCoroutine(AdjustIntensity(targetLight, _Config_Light_Setup));
        }

        // Start scale adjustment coroutine
        if (_Config_Light_Setup.End_Scale != Vector3.zero)
        {
            StartCoroutine(AdjustScale(Ins.transform, _Config_Light_Setup));
        }
    }
    #endregion

    private IEnumerator AdjustIntensity(Light2D targetLight, Config_Light_Setup setup)
    {
        float elapsedTime = 0f;
        float duration = Mathf.Abs(setup.End_Intensity - setup.Start_Intensity) / setup.Speed_Intensity;

        while (true)
        {
            if (setup.b_Ping_Pong || setup.b_Loop)
            {
                // Ping-pong effect between Start_Intensity and End_Intensity
                targetLight.intensity = Mathf.PingPong(elapsedTime * setup.Speed_Intensity, Mathf.Abs(setup.End_Intensity - setup.Start_Intensity)) + Mathf.Min(setup.Start_Intensity, setup.End_Intensity);

                if (!setup.b_Loop && elapsedTime >= duration * 2)  // If not looping, stop after one ping-pong cycle
                {
                    yield break;
                }
            }
            else
            {
                // Linear transition from Start_Intensity to End_Intensity
                targetLight.intensity = Mathf.Lerp(setup.Start_Intensity, setup.End_Intensity, elapsedTime / duration);

                if (elapsedTime >= duration)
                {
                    yield break;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


        private IEnumerator AdjustScale(Transform lightTransform, Config_Light_Setup setup)
        {
            float elapsedTime = 0f;
            float duration = Vector3.Distance(setup.Start_Scale, setup.End_Scale) / Mathf.Max(setup.Speed_Scale, 0.01f);  // Prevent too small Speed_Scale
            bool returning = false;

            while (true)
            {
                if (setup.b_Ping_Pong)
                {
                    float progress = elapsedTime / duration;

                    if (!returning)
                    {
                        lightTransform.localScale = Vector3.Lerp(setup.Start_Scale, setup.End_Scale, progress);
                    }
                    else
                    {
                        lightTransform.localScale = Vector3.Lerp(setup.End_Scale, setup.Start_Scale, progress);
                    }

                    if (elapsedTime >= duration)
                    {
                        if (!returning)
                        {
                            returning = true;
                            elapsedTime = 0f;
                        }
                        else
                        {
                            yield break;
                        }
                    }
                }
                else
                {
                    lightTransform.localScale = Vector3.Lerp(setup.Start_Scale, setup.End_Scale, elapsedTime / duration);

                    if (elapsedTime >= duration)
                    {
                        yield break;
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }




}
