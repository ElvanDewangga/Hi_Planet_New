using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class convers7
{
    public Sprite spr;
    [TextArea]
    public string dialogs;
}

public class Cut_007 : MonoBehaviour
{
    #region instance
    public static Cut_007 instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int converid;
    public convers7[] convers;

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
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                }break;
            }
            StartDialog.instance.nextTypeToOption();
        }
    }
}
