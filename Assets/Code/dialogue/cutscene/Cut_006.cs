using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class convers
{
    public Sprite spr;
    [TextArea]
    public string dialogs;
}

public class Cut_006 : MonoBehaviour
{
    #region instance
    public static Cut_006 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int converid;
    public convers[] convers;
    
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
                    CDialogManager.instance.currentSpr = convers[1].spr;
                    StartDialog.instance.singleConversation = convers[1].dialogs;
                    converid++;
                }break;

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
                    converid++;
                }break;

                /////////////////////////////////////////////////////////////
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
                }break;
            }
            StartDialog.instance.nextTypeToOption();
        }
    }
}
