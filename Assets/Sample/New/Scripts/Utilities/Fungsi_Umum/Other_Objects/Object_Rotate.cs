using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Rotate : MonoBehaviour
{
   [SerializeField]
   Vector3 V3_Rotate;
    [SerializeField]
    float Speed;
    void Update () {
        transform.Rotate(V3_Rotate * Speed * Time.deltaTime);
    }
}
