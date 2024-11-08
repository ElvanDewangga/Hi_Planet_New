using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class convers11
{
    public Sprite spr;
    [TextArea]
    public string dialogs;
}

public class Cut_011 : MonoBehaviour
{
    #region instance
    public static Cut_011 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int converid;
    public convers10[] convers;
    
    public void StartConverOption()
    {
        if(StartDialog.instance.typeoption)
        {
            switch(converid)
            {
                case 0:
                {
                    StartDialog.instance.typeoption = true;
                    CDialogManager.instance.currentSpr = convers[0].spr;
                    StartDialog.instance.singleConversation = convers[0].dialogs;
                    converid++;
                }break;

                case 1:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                ////////////////////////////////////////////////////////////////////////////1

                case 2:
                {
                    CDialogManager.instance.currentSpr = convers[2].spr;
                    StartDialog.instance.singleConversation = convers[2].dialogs;
                    converid++;
                }break;

                case 3:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    UIManager.instance.OpenQQMenu();
                    converid++;
                }break;

                ///////////////////////////////////////////////////////////////////////////2

                case 4:
                {
                    CDialogManager.instance.currentSpr = convers[3].spr;
                    StartDialog.instance.singleConversation = convers[3].dialogs;
                    converid++;
                }break;

                case 5:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                ////////////////////////////////////////////////////////////////////////////////3
                case 6:
                {
                    CDialogManager.instance.currentSpr = convers[4].spr;
                    StartDialog.instance.singleConversation = convers[4].dialogs;
                    converid++;
                }break;

                case 7:
                {
                    CDialogManager.instance.currentSpr = convers[5].spr;
                    StartDialog.instance.singleConversation = convers[5].dialogs;
                    converid++;
                }break;

                case 8:
                {
                    CDialogManager.instance.currentSpr = convers[6].spr;
                    StartDialog.instance.singleConversation = convers[6].dialogs;
                    converid++;
                }break;

                case 9:
                {
                    CDialogManager.instance.currentSpr = convers[7].spr;
                    StartDialog.instance.singleConversation = convers[7].dialogs;
                    converid++;
                }break;

                case 10:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                //////////////////////////////////////////////////////////////////////////////////4

                case 11:
                {
                    CDialogManager.instance.currentSpr = convers[8].spr;
                    StartDialog.instance.singleConversation = convers[8].dialogs;
                    converid++;
                }break;

                case 12:
                {
                    CDialogManager.instance.currentSpr = convers[9].spr;
                    StartDialog.instance.singleConversation = convers[9].dialogs;
                    converid++;
                }break;

                case 13:
                {
                    CDialogManager.instance.currentSpr = convers[10].spr;
                    StartDialog.instance.singleConversation = convers[10].dialogs;
                    converid++;
                }break;

                case 14:
                {
                    CDialogManager.instance.currentSpr = convers[11].spr;
                    StartDialog.instance.singleConversation = convers[11].dialogs;
                    converid++;
                }break;

                case 15:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                //////////////////////////////////////////////////////////////////////////////5
                case 16:
                {
                    CDialogManager.instance.currentSpr = convers[12].spr;
                    StartDialog.instance.singleConversation = convers[12].dialogs;
                    converid++;
                }break;

                case 17:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    converid++;
                }break;

                ////////////////////////////////////////////////////////////////////6

                case 18:
                {
                    CDialogManager.instance.currentSpr = convers[13].spr;
                    StartDialog.instance.singleConversation = convers[13].dialogs;
                    converid++;
                }break;

                case 19:
                {
                    CDialogManager.instance.currentSpr = convers[14].spr;
                    StartDialog.instance.singleConversation = convers[14].dialogs;
                    converid++;
                }break;

                case 20:
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
