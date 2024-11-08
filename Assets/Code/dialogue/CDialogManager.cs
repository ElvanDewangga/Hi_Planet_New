using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CDialogManager : MonoBehaviour
{
    #region instance
    public static CDialogManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject optionHolder;
    [HideInInspector]public Sprite currentSpr;
    public string[] currentOption;
    [HideInInspector]public string[] currentAnswer;

    GameObject ins;
    public List<GameObject> insgroup;
    [HideInInspector]public int optionID;

    public void ShowOption()
    {
        StartDialog.instance.dialogText.text = "";
        UIManager.instance._nextChatButton.SetActive(false);
        for(int i = 0;i < 2;i++)
        {
            ins = Instantiate(optionHolder, UIManager.instance.dialogBox.transform.position, Quaternion.identity);
            ins.transform.parent = UIManager.instance.optionPanel.transform;
            if(!insgroup.Contains(ins))
            {
                insgroup.Add(ins);
            }
            ins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentOption[i];
            ins.GetComponent<OptionHolderID>().id = i;
        }
    }

    public void CutsceneTrigger()
    {
        for(int i = 0; i < 2;i++)
        {
            if(insgroup.Count > 0)
            {
                Destroy(insgroup[0].gameObject);
                insgroup.RemoveAt(0);
            }
        }
        
        switch(TutorialManager.instance.tutorialId)
        {
            case 2:
            {
                Cut_002.instance.StartConverOption();
            }break;

            case 3:
            {
                Cut_003.instance.StartConverOption();
            }break;

            case 4:
            {
                Cut_004.instance.StartConverOption();
            }break;

            case 5:
            {
                Cut_005.instance.StartConverOption();
            }break;

            case 6:
            {
                Cut_006.instance.StartConverOption();
            }break;
            
            case 7:
            {
                Cut_007.instance.StartConverOption();
            }break;

            case 8:
            {
                Cut_008.instance.StartConverOption();
            }break;

            case 9:
            {
                Cut_009.instance.StartConverOption();
            }break;

            case 10:
            {
                Cut_010.instance.StartConverOption();
            }break;

            case 11:
            {
                Cut_011.instance.StartConverOption();
            }break;
        }
    }
}
