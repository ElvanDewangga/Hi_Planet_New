using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale_Fixer : MonoBehaviour {
    [Header ("Untuk Static : Langsung Diisi.")]
    [Header ("Untuk Dinamis : Panggil 'On_Set_V3_Scale'")]
    [SerializeField]
    Vector3 V3_Scale;
    void OnEnable () {
        this.transform.localScale = V3_Scale;
    }

    public void On_Set_V3_Scale (Vector3 Scale) {
        V3_Scale = Scale;
    }
}
