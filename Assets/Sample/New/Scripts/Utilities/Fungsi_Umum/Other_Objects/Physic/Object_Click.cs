using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Click : MonoBehaviour
{
    
    public string Code_Event = "";
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Deteksi klik kiri
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
               // Debug.Log (hit.transform.name);
                if (hit.transform == transform) {
                    On_Click_Object ();
                }
            }
        }
    }

    public virtual void On_Click_Object () {

    }
}
