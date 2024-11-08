using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Target Follow")]
    public Transform target;
    // the target position can be adjusted by an offset in order to foucs on a
    // target's head for example
    public Vector3 offset = Vector3.zero;
    // Hi_Plancet_Char_Data_Source
    public void On_Set_Target (Transform v) {
        // objek yang akan diikuti sama camera ini.
        target = v;
    }
    // smooth the camera movement
    [Header("Dampening")]
    public float damp = 2;

    [Header("Clamping")]
    public Vector2 fromclamp;
    public Vector2 toclamp;
    Vector3 goal;
    Vector3 position;
    void LateUpdate()
    {
        if (!target) return;

        // calculate goal position
        goal = target.position + offset;

        // interpolate
        if(fromclamp != Vector2.zero)
        {
            goal.x = Mathf.Clamp(goal.x, fromclamp.x, toclamp.x);
            goal.y = Mathf.Clamp(goal.y, fromclamp.y, toclamp.y);
        }

        position = Vector3.Lerp(transform.position, goal, Time.deltaTime * damp);
        // convert to 3D but keep Z to stay in front of 2D plane
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
