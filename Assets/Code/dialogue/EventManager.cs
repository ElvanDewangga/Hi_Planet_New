using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public float walkspeedDefault;
    #region instance
    public static EventManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion    

    funcScene fs; 
    public void startEvent()
    {
        fs = GameObject.FindObjectOfType<funcScene>();
        UIManager.instance.buttonCutscene.transform.GetChild(0).gameObject.SetActive(true);
        switch(TutorialManager.instance.tutorialId)
        {
            case 1:
            {
                StartCoroutine(E_001());
            }break;

            case 2:
            {
                StartCoroutine(E_002());
            }break;

            case 3:
            {
                StartCoroutine(E_003());
            }break;

            case 4:
            {
                StartCoroutine(E_004());
            }break;

            case 5:
            {
                StartCoroutine(E_005());
            }break;

            case 6:
            {
                StartCoroutine(E_006());
            }break;
            
            case 7:
            {
                StartCoroutine(E_007());
            }break;

            case 8:
            {
                StartCoroutine(E_008());
            }break;

            case 9:
            {
                StartCoroutine(E_009());
            }break;

            case 10:
            {
                StartCoroutine(E_010());
            }break;

            case 11:
            {
                StartCoroutine(E_011());
            }break;
        }
    }
    
    IEnumerator E_001()
    {
        StartCoroutine(Es1_001());
        yield return new WaitForSeconds(0.85f);
        //TutorialManager.instance.tutorialId = 2; // set tutorial
        while(Vector2.Distance(fs.moolu1.transform.position, fs.moolusLab.position) > 0.01f)
        {
            fs.moolu1.transform.position = Vector2.MoveTowards(fs.moolu1.transform.position, fs.moolusLab.position, Time.deltaTime * walkspeedDefault);
            yield return null;
        }
    }
    IEnumerator Es1_001()
    {
        yield return new WaitUntil(() => Vector2.Distance(fs.moolu1.transform.position, fs.moolusLab.position) <= 0.01f);
        fs.moolu1.SetActive(false);
        yield return null;
    }

    int e2index;
    IEnumerator E_002()
    {
        yield return new WaitForSeconds(1f);
        if(e2index == 0)
        {
            StartDialog.instance.typeoption = true; //switch to dialog dialog mode
            Cut_002.instance.StartConverOption();
            e2index = 1;
            yield return null;
        }else if(e2index == 1)
        {
            StartCoroutine(Es1_002());
            while(Vector2.Distance(fs.moolu2.transform.position, fs.labdoor.position) > 0.01f)
            {
                fs.moolu2.transform.position = Vector2.MoveTowards(fs.moolu2.transform.position, fs.labdoor.position, Time.deltaTime * walkspeedDefault);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    IEnumerator Es1_002()
    {
        yield return new WaitUntil(() => Vector2.Distance(fs.moolu2.transform.position, fs.labdoor.position) <= 0.01f);
        fs.moolu2.SetActive(false);
        yield return null;
    }

    IEnumerator E_003()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_003.instance.StartConverOption();
        yield return null;
    }

    IEnumerator E_004()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_004.instance.StartConverOption();
        yield return null;
    }

    IEnumerator E_005()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_005.instance.StartConverOption();
        yield return null;
    }

    IEnumerator E_006()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_006.instance.StartConverOption();
        yield return null;
    }

    public int e7index;
    IEnumerator E_007()
    {
        if(e7index == 0)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("GOES FISHING");
            e7index = 1;
            yield return null;
        }else if(e7index == 1)
        {
            yield return new WaitForSeconds(1f);
            StartDialog.instance.typeoption = true;
            Cut_007.instance.StartConverOption();
            yield return null;
        }
    }

    IEnumerator E_008()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_008.instance.StartConverOption();
        yield return null;
    }

    IEnumerator E_009()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_009.instance.StartConverOption();
        Debug.Log("anjing2");
        yield return null;
    }

    IEnumerator E_010()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_010.instance.StartConverOption();
        Debug.Log("anjing2");
        yield return null;
    }

    IEnumerator E_011()
    {
        yield return new WaitForSeconds(1f);
        StartDialog.instance.typeoption = true; //switch to dialog dialog mode
        Cut_011.instance.StartConverOption();
        Debug.Log("anjing2");
        yield return null;
    }
}
