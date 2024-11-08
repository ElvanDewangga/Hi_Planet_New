using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalspawner : MonoBehaviour
{
    #region instance
    public static portalspawner instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject portal;
    public Vector3 offset;
    public List<Transform> dest;
    public bool canspawn;

    int index;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && canspawn == true)
        {
            GameObject ins = Instantiate(portal, transform.position + (transform.GetChild(0).transform.right * offset.x), Quaternion.identity);
            PortalManager.instance._destinationPortal = ins.transform;
            if(!dest.Contains(ins.transform))
            {
                dest.Add(ins.transform);
            }
            
            if(index == 1)
            {
                dest[1].GetComponent<portaltravel>().dest = dest[0];
                dest[0].GetComponent<portaltravel>().dest = dest[1];
            }else
            {
                index++;
            }
        }  
    }
}
