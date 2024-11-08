using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBob : MonoBehaviour
{
    public GameObject[] arrows;
    public CanvasGroup alphaobj;
    // Start is called before the first frame update
    void Start()
    {
        StartButtonInStageSelect();
    }

    void StartButtonInStageSelect()
    {
        arrows[0].LeanMoveLocal(new Vector3(270,0,0), 0.3f).setOnComplete(sbis); 
        arrows[1].LeanMoveLocal(new Vector3(-270,0,0), 0.3f); 
        alphaobj.LeanAlpha(0.7f, 0.3f);
    }
    void sbis()
    {
       arrows[0].LeanMoveLocal(new Vector3(240, 0, 0), 0.3f).setOnComplete(StartButtonInStageSelect);
       arrows[1].LeanMoveLocal(new Vector3(-240, 0, 0), 0.3f); 
       alphaobj.LeanAlpha(1, 0.3f); 
    }
}
