using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Char_Attack_Collider_Sample))]
public class DM_ColliderSample : MonoBehaviour
{
   // Char_Technique ct;
    void Start()
    {
      // ct = GetComponent<Char_Attack_Collider_Sample>()._Char_Technique;
    }

    void OnTriggerEnter(Collider other)
    {
       // if(!other.gameObject.CompareTag(ct._Char_Utama.gameObject.tag))
        // {
            Debug.Log("COLNAME" + " " + other.transform.name);
            if(TryGetComponent(out Char_Attack_Move cam))
            {
                StartCoroutine(cam.Moveduration(0));
            }
      //   }
    }
}
