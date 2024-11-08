using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractedHolder : MonoBehaviour
{
    public UnityEvent OnInteracted;
    [Space]
    public bool useTrigger;

    void OnTriggerEnter(Collider other)
    {
        if(useTrigger)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                OnInteracted.Invoke();
            }
        }
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         StartDialog.instance.typeoption = false;
    //         StartDialog.instance.idh = null;
    //     }
    // }
}
