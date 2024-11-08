using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaltravel : MonoBehaviour
{
    public Transform dest;
    [HideInInspector]public GameObject tribute;
    public Vector3 tributeoffset;
    [Space]
    public GameObject point;
    public Vector3 pointoffset;

    public void startteleport()
    {
        UIManager.instance.openindex = 0;
        UIManager.instance.OpenPortalPanel();

        tribute = UIManager.instance._portalTribute;
        tribute.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);

        PortalManager.instance._destinationPortal = dest;
        PortalManager.instance.pt = GetComponent<portaltravel>();
        UIManager.instance.player.SetActive(false);
    }

    void LateUpdate()
    {
        if(tribute != null)
        {
            tribute.transform.position = Camera.main.WorldToScreenPoint(transform.position + tributeoffset);
        }

        if(point != null)
        {
            point.transform.position = Camera.main.WorldToScreenPoint(transform.position + pointoffset);
        }
    }
}
