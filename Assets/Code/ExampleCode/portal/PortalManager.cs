using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PortalManager : MonoBehaviour
{
    #region instance
    public static PortalManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [HideInInspector]public portaltravel pt;
    public string SceneName;
    public Transform _destinationPortal;
    GameObject player;
    Camera cam;
    [Space]
    public Color[] SelectCol;

    [Header("Multiportal")]
    public bool isallowmulti;

    [Header("Singleportal")]
    public bool isallowsingle;

    [Header("Sceneportal")]
    public bool isallowscene;

    void Start()
    {
        //Invoke("StartFind", 0.5f);
    }

    void StartFind()
    {
        cam = Camera.main;
        player = GameObject.Find("Clone_Player").gameObject;
    }

    #region TravelGroup
    public void RemovePortal()
    {
        pt.tribute = null;
        player.SetActive(true);
        portalspawner.instance.dest.Remove(pt.transform);
        Destroy(pt.gameObject);
        UIManager.instance.OpenPortalPanel();
    }
    public void Teleport()
    {
        //GameObject.FindObjectOfType<camerafollow>().on = false;
        pt.tribute = null;
        if(isallowmulti == true)
        {
            UIManager.instance._portalTribute.SetActive(false);
            UIManager.instance.openindex = 0;
            
            player.SetActive(false);
            cam.transform.LeanMove(GameObject.Find("CenterMap").transform.position, 0.3f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.value(20f, 9f, 0.2f).setOnUpdate((float val) => 
            {
                GameObject.FindAnyObjectByType<PixelPerfectCamera>().assetsPPU = (int)val;
            });
            isZoomOut = true;
            FindPortal();
        }else if(isallowsingle == true)
        {
            UIManager.instance._portalPanel.SetActive(false);
            player.SetActive(false);
            StartCoroutine(delaytravel());
        }else if(isallowscene == true)
        {
            player.SetActive(false);
        }
    }
    public void singleTeleport()
    {
        ClosePortal();
        StartCoroutine(delaytravel());
        player.SetActive(false);
    }

    IEnumerator delaytravel()
    {
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        //GameObject.FindObjectOfType<camerafollow>().on = true;
        Debug.Log("delaytravel");
        player.transform.position = _destinationPortal.position;
    }
    void FindPortal()
    {
        portaltravel[] pts = GameObject.FindObjectsOfType<portaltravel>();
        for(int i = 0;i < pts.Length;i++)
        {
            pts[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    #endregion

    bool isZoomOut;
    public void ClosePortal()
    {
        pt.tribute = null;
        UIManager.instance.player.SetActive(true);
        GameObject.FindObjectOfType<camerafollow>().on = true;
        if(isZoomOut == true)
        {
            LeanTween.value(9f, 20, 0.2f).setOnUpdate((float val) => 
            {
                GameObject.FindAnyObjectByType<PixelPerfectCamera>().assetsPPU = (int)val;
            });
            isZoomOut = false;
        }

        portaltravel[] pts = GameObject.FindObjectsOfType<portaltravel>();
        for(int i = 0;i < pts.Length;i++)
        {
            pts[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
