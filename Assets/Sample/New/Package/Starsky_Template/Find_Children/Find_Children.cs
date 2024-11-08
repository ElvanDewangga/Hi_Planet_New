using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Children : MonoBehaviour {
    // Popup_Type :
    public GameObject On_Find_Children_GO (string Name) {
        GameObject Res = null;
        Transform childTransform = this.gameObject.transform.Find(Name);
        if (childTransform != null)
        {
            Res = childTransform.gameObject;
        } else {
            Debug.LogError ("Object tidak ditemukan " + Name);
        }
        return Res;
    }
}
