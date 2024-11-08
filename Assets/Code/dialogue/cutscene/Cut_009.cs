using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class convers9
{
    public Sprite spr;
    [TextArea]
    public string dialogs;
}

public class Cut_009 : MonoBehaviour
{
    #region instance
    public static Cut_009 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int converid;
    public convers8[] convers;

    [Header("option")]
    public Sprite[] spr2;
    public string conver1;
    public string[] conver2_option;
    public string[] conver3_answer;
    public string conver4;
    public string conver5;
    public string conver6;
    
    void disableoption()
    {
        StartDialog.instance.typeoption = false;
    }
    
    public void StartConverOption()
    {
        Debug.Log("anjing");
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
                    CDialogManager.instance.currentSpr = convers[1].spr;
                    StartDialog.instance.singleConversation = convers[1].dialogs;
                    converid++;
                }break;

                case 2:
                {
                    StartDialog.instance.typeoption = true;
                    CDialogManager.instance.currentSpr = convers[2].spr;
                    StartDialog.instance.singleConversation = convers[2].dialogs;
                    converid++;
                }break;

                case 3:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    Invoke("disableoption", 0.1f);
                    converid++;
                }break;

                ///////////////////////////////////////////////////////////
                
                case 4:
                {
                    CDialogManager.instance.currentSpr = spr2[0];
                    StartDialog.instance.singleConversation = conver1;
                    converid++;
                }break;
            
                case 5:
                {
                    CDialogManager.instance.currentSpr = spr2[1];
                    CDialogManager.instance.currentOption = conver2_option;
                    CDialogManager.instance.ShowOption();
                    StartDialog.instance.singleConversation = "";
                    converid++;
                }break;

                case 6:
                {
                    CDialogManager.instance.currentSpr = spr2[2];
                    StartDialog.instance.singleConversation = conver3_answer[CDialogManager.instance.optionID];
                    converid++;
                }break;

                case 7:
                {
                    CDialogManager.instance.currentSpr = spr2[3];
                    StartDialog.instance.singleConversation = conver4;
                    converid++;
                }break;

                case 8:
                {
                    CDialogManager.instance.currentSpr = spr2[4];
                    StartDialog.instance.singleConversation = conver5;
                    converid++;
                }break;

                case 9:
                {
                    CDialogManager.instance.currentSpr = spr2[5];
                    StartDialog.instance.singleConversation = conver6;
                    converid++;
                }break;

                case 10:
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
