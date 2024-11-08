using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cut_003 : MonoBehaviour
{
    #region instance
    public static Cut_003 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    StartDialog sd;
    public int converid;
    public Sprite[] spr;
    public Sprite[] spr2;

    [TextArea]
    public string[] dialogs;
    [TextArea]
    public string[] dialogs2;

    public void StartConverOption()
    {
        if(StartDialog.instance.typeoption)
        {
            switch(converid)
            {
                case 0:
                {
                    CDialogManager.instance.currentSpr = spr[0];
                    StartDialog.instance.singleConversation = dialogs[0];
                    converid++;
                }break;

                case 1:
                {
                    CDialogManager.instance.currentSpr = spr[1];
                     StartDialog.instance.singleConversation = dialogs[1];
                    converid++;
                }break;

                case 2:
                {
                    CDialogManager.instance.currentSpr = spr[2];
                     StartDialog.instance.singleConversation = dialogs[2];
                    converid++;
                }break;

                case 3:
                {
                    CDialogManager.instance.currentSpr = spr[3];
                    StartDialog.instance.singleConversation = dialogs[3];
                    converid++;
                }break;

                case 4:
                {
                    CDialogManager.instance.currentSpr = spr[4];
                     StartDialog.instance.singleConversation = dialogs[4];
                    converid++;
                }break;

                case 5:
                {
                    CDialogManager.instance.currentSpr = spr[5];
                    StartDialog.instance.singleConversation = dialogs[5];
                    converid++;
                }break;

                case 6:
                {
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                ////////////////////////////////////////////////////////////////////////////

                case 7:
                {
                    CDialogManager.instance.currentSpr = spr2[0];
                    StartDialog.instance.singleConversation = dialogs2[0];
                    converid++;
                }break;

                case 8:
                {
                    CDialogManager.instance.currentSpr = spr2[1];
                    StartDialog.instance.singleConversation = dialogs2[1];
                    converid++;
                }break;

                case 9:
                {
                    CDialogManager.instance.currentSpr = spr2[2];
                     StartDialog.instance.singleConversation = dialogs2[2];
                    converid++;
                }break;

                case 10:
                {
                    StartDialog.instance.isendconveroption = true;
                    //TutorialManager.instance.tutorialId = 4;
                    StartDialog.instance.typeoption = false;
                }break;
            }
             StartDialog.instance.nextTypeToOption();
        }
    }
}
