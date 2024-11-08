using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cut_004 : MonoBehaviour
{
    #region instance
    public static Cut_004 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int converid;
    public Sprite[] spr;
    public Sprite[] spr2;
    public Sprite[] spr3;

    [TextArea]
    public string[] dialogs;
    [TextArea]
    public string[] dialogs2;
    [TextArea]
    public string[] dialogs3;

    public void StartConverOption()
    {
        if(StartDialog.instance.typeoption)
        {
            switch(converid)
            {
                case 0:
                {
                    StartDialog.instance.typeoption = true;
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
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                ////////////////////////////////////////////////////////////
                case 4:
                {
                    CDialogManager.instance.currentSpr = spr2[0];
                    StartDialog.instance.singleConversation = dialogs2[0];
                    converid++;
                }break;

                case 5:
                {
                    CDialogManager.instance.currentSpr = spr2[1];
                    StartDialog.instance.singleConversation = dialogs2[1];
                    converid++;
                }break;

                case 6:
                {
                    CDialogManager.instance.currentSpr = spr2[2];
                    StartDialog.instance.singleConversation = dialogs2[2];
                    converid++;
                }break;

                case 7:
                {
                    CDialogManager.instance.currentSpr = spr2[3];
                    StartDialog.instance.singleConversation = dialogs2[3];
                    converid++;
                }break;

                case 8:
                {
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                    UIManager.instance.OpenCosmicStore();
                }break;

                ////////////////////////////////////////////////////////////
                case 9:
                {
                    CDialogManager.instance.currentSpr = spr3[0];
                    StartDialog.instance.singleConversation = dialogs3[0];
                    converid++;
                }break;

                case 10:
                {
                    CDialogManager.instance.currentSpr = spr3[1];
                    StartDialog.instance.singleConversation = dialogs3[1];
                    converid++;
                }break;

                case 11:
                {
                    CDialogManager.instance.currentSpr = spr3[2];
                    StartDialog.instance.singleConversation = dialogs3[2];
                    converid++;
                }break;

                case 12:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                }break;
            }
            StartDialog.instance.nextTypeToOption();
        }
    }
}
