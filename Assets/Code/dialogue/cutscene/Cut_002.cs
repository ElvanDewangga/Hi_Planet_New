using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cut_002 : MonoBehaviour
{
    #region instance
    public static Cut_002 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int converid;
    public Sprite[] spr;
    
    [TextArea]
    public string[] option1;
    [TextArea]
    public string[] answer1;
    [TextArea]
    public string answer2;
    [TextArea]
    public string answer3;

    public void StartConverOption()
    {
        if(StartDialog.instance.typeoption)
        {
            switch(converid)
            {
                case 0:
                {
                    CDialogManager.instance.currentSpr = spr[0];
                    CDialogManager.instance.currentOption = option1;
                    CDialogManager.instance.ShowOption();
                    converid++;
                }break;

                case 1:
                {
                    CDialogManager.instance.currentSpr = spr[1];
                    StartDialog.instance.singleConversation = answer1[CDialogManager.instance.optionID];
                    converid++;
                }break;

                case 2:
                {
                    CDialogManager.instance.currentSpr = spr[2];
                    StartDialog.instance.singleConversation = answer2;
                    converid++;
                }break;

                case 3:
                {
                    CDialogManager.instance.currentSpr = spr[3];
                    StartDialog.instance.singleConversation = answer3;
                    converid++;
                }break;

                case 4:
                {
                    //TutorialManager.instance.tutorialId = 3; // set tutorial
                    StartDialog.instance.typeoption = false; //switch to normal dialog mode
                    UIManager.instance.buttonCutscene.transform.GetChild(0).gameObject.SetActive(false);
                    Debug.Log("INFO : meet moolu Finish..");
                    StartDialog.instance.isendconveroption = true;
                    //EventManager.instance.startEvent(); //Modifikasi(hapus/rubah)-triger untuk cutscene 003
                }break;
            }
             StartDialog.instance.nextTypeToOption();
        }
    }
}
