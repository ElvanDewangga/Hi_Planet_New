using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePortalPoint : MonoBehaviour
{
    SinglePortalPoint[] spps;
    [HideInInspector]public Image img;
    public GameObject portal;
    [HideInInspector]public int teleindex;

    void Start()
    {
        img = GetComponent<Image>();
    }

    public void getportal()
    {
        spps = GameObject.FindObjectsOfType<SinglePortalPoint>();
        if(teleindex == 0)
        {
            for(int i = 0;i < spps.Length;i++)
            {
                spps[i].teleindex = 0;
                spps[i].img.color = PortalManager.instance.SelectCol[0];
                spps[i].img.gameObject.transform.localScale = Vector3.one;
            }
            PortalManager.instance._destinationPortal = portal.transform;
            img.gameObject.transform.localScale = new Vector3(1.3f,1.3f,1f);
            img.color = PortalManager.instance.SelectCol[1];
            teleindex = 1;
        }else{
            img.color = PortalManager.instance.SelectCol[0];
            teleindex = 0;
            PortalManager.instance.singleTeleport();
        }
    }
}
