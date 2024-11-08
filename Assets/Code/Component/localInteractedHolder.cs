using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class localInteractedHolder : MonoBehaviour
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

        if(other.gameObject.CompareTag("Player"))
        {
            //FOR DIALOG
            StartDialog.instance.typeoption = true;
            StartDialog.instance.idh = GetComponent<localInteractedHolder>();
        }
    }

    // void OnTriggerExit(Collider2D other)
    // {
    //     if(other.gameObject.CompareTag("Player"))
    //     {
    //         StartDialog.instance.typeoption = false;
    //         StartDialog.instance.idh = null;
    //     }
    // }
}
