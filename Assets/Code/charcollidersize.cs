using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charcollidersize : MonoBehaviour
{
    public Vector3 collidersize;
    public float chacontrollerradius;

    [Header("Conditioner")]
    public bool useColCharcol = true;
    public bool useCol = true;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setparentcollidersize", 0.1f);   
    }
    void setparentcollidersize()
    {
        if(transform.parent.TryGetComponent(out CharacterController charcon))
        {
            if(chacontrollerradius != 0)
            {
                charcon.radius = chacontrollerradius;
                transform.parent.GetComponent<BoxCollider>().size = collidersize;
            }

            if(useColCharcol == false)
            {
                charcon.enabled = false;
            }

            if(useCol == false)
            {
                if(charcon.TryGetComponent(out BoxCollider bc))
                {
                    bc.isTrigger = true;
                }   
                if(charcon.TryGetComponent(out SphereCollider sc))
                {
                    sc.isTrigger = true;
                }
            }
        }

    }
}
