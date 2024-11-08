using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Position : MonoBehaviour
{
    [SerializeField]
    float Pos_Z = 0.0f;
   

    void Update () {
        if (Pos_Z != 0.0f) {
            this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, -0.5f);
        }
    }
}
