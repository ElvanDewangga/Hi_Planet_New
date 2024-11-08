using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaNPC : MonoBehaviour
{
    public int converid;
    public Sprite[] spr;
    
    public string conver1;
    [TextArea]
    public string[] conver2_option;
    [TextArea]
    public string[] conver3_answer;
    [TextArea]
    public string conver4;

    public void StartConverOption()
    {
        if(StartDialog.instance.typeoption)
        {
            switch(converid)
            {
                case 0:
                {
                    CDialogManager.instance.currentSpr = spr[0];
                    StartDialog.instance.singleConversation = conver1;
                    converid++;
                }break;

                case 1:
                {
                    CDialogManager.instance.currentSpr = spr[1];
                    CDialogManager.instance.currentOption = conver2_option;
                    CDialogManager.instance.ShowOption();
                    StartDialog.instance.singleConversation = "";
                    converid++;
                }break;

                case 2:
                {
                    CDialogManager.instance.currentSpr = spr[2];
                    StartDialog.instance.singleConversation = conver3_answer[CDialogManager.instance.optionID];
                    converid++;
                }break;

                case 3:
                {
                    if(CDialogManager.instance.optionID == 0)
                    {
                        CDialogManager.instance.currentSpr = spr[3];
                        StartDialog.instance.singleConversation = conver4;
                        converid++;
                    }else{
                        StartDialog.instance.singleConversation = "";
                        StartDialog.instance.isendconveroption = true;
                        StartDialog.instance.typeoption = false;
                        converid = 0;
                    }
                }break;

                case 4:
                {
                    StartDialog.instance.singleConversation = "";
                    StartDialog.instance.isendconveroption = true;
                    StartDialog.instance.typeoption = false;
                    UIManager.instance.OpenArenaPanel();
                    converid = 0;
                }break;
            }
            StartDialog.instance.nextTypeToOption();
        }
    }
}
