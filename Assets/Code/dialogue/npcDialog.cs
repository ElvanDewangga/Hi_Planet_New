using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class npcDialog : MonoBehaviour
{
    [HideInInspector]public List<Sprite> spr;
    public List<string> dialogs;
    public int dialoguId;
    // Start is called before the first frame update
    Sprite sprt;
    string str;
    public UnityEvent OnEndConversation;

    void Start()
    {
        switch(dialoguId)
        {
            case 1:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T001_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T001_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T001_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T001_log[i].spr;
                    str = TutorialDialogue.instance.T001_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 2:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T002_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T002_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T002_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T002_log[i].spr;
                    str = TutorialDialogue.instance.T002_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 3:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T003_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T003_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T003_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T003_log[i].spr;
                    str = TutorialDialogue.instance.T003_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 4:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T004_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T004_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T004_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T004_log[i].spr;
                    str = TutorialDialogue.instance.T004_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 5:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T005_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T005_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T005_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T005_log[i].spr;
                    str = TutorialDialogue.instance.T005_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 6:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T006_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T006_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T006_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T006_log[i].spr;
                    str = TutorialDialogue.instance.T006_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 7:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T007_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T007_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T007_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T007_log[i].spr;
                    str = TutorialDialogue.instance.T007_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 8:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T008_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T008_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T008_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T008_log[i].spr;
                    str = TutorialDialogue.instance.T008_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;

            case 9:
            {
                spr = new List<Sprite>(new Sprite[TutorialDialogue.instance.T009_log.Count]);
                dialogs = new List<string>(new string[TutorialDialogue.instance.T009_log.Count]);
                for(int i = 0;i < TutorialDialogue.instance.T009_log.Count;i++)
                {
                    sprt = TutorialDialogue.instance.T009_log[i].spr;
                    str = TutorialDialogue.instance.T009_log[i].dialogs;
                    
                    spr[i] = sprt;
                    dialogs[i] = str;
                }
            }break;
        }
    }
}
