using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Ex_optiondialog : MonoBehaviour
{
    int converid;
    [TextArea]
    public string[] option1;
    [TextArea]
    public string[] answer1;
    public Sprite[] spr;

    public void talk()
    {
        if(StartDialog.instance.typeoption)
        {
            switch(converid)
            {
                case 0:
                {
                    CDialogManager.instance.currentSpr = spr[0];
                    StartDialog.instance.singleConversation = "What is 2 + 2?";
                    converid++;
                }break;

                case 1:
                {
                    CDialogManager.instance.currentSpr = spr[1];
                    CDialogManager.instance.currentOption = option1;
                    StartDialog.instance.singleConversation = "";
                    CDialogManager.instance.ShowOption();
                    converid++;
                }break;

                case 2:
                {
                    CDialogManager.instance.currentSpr = spr[2];
                    StartDialog.instance.singleConversation = answer1[CDialogManager.instance.optionID];
                    converid++;
                }break;

                case 3:
                {
                    StartDialog.instance.typeoption = false; 
                    StartDialog.instance.isendconveroption = true;
                    converid = 0;
                }break;
            }
            StartDialog.instance.nextTypeToOption();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartDialog.instance.typeoption = true;
        }
    }
}
