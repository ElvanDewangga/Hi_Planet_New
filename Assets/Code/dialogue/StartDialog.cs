using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Events;

public class StartDialog : MonoBehaviour
{
    #region instance
    public static StartDialog instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    float ratepro;
    int charindex;
    private Char[] textchar;
    private string currenttype;

    [HideInInspector]public int chatIndex;
    string texttype;
    public TextMeshProUGUI dialogText;
    public float rate;
    private int index;
    //////////////////////////////
    public string singleConversation;
    public localInteractedHolder idh;

    public UnityEvent OnNextChatOption;

    public void NextChatOption()
    {
        OnNextChatOption.Invoke();
    }

    void Start()
    {
        ratepro = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && target != null || Input.GetKeyDown(KeyCode.B) && npcInter != null)
        {
            nextType();
        }
    }

    public void BeginChat()
    {
        if(target != null || npcInter != null)
        {
            nextType();
        }
    }

    public void BeginChatOption()
    {
        if(target != null || npcInter != null)
        {
            nextTypeToOption();
        }
    }

    public void BeginChatInteractive()
    {
        if(target != null || npcInter != null)
        {
            nextTypeInteractive();
        }
    }

    public bool canskip = true;
    public npcDialog target;
    public bool typeoption;

    public void nextType()
    {
        UIManager.instance._nextChatButton.SetActive(true);
        if(idh == null)
        {
            if(typeoption == false)
            {
                if(npcInter == null)
                {
                    if(chatIndex >= target.dialogs.Count)
                    {
                        UIManager.instance.dialogBox.SetActive(false);
                        target.OnEndConversation.Invoke();
                        chatIndex = 0;
                    }else if(chatIndex < target.dialogs.Count)
                    {
                        if(canskip == true)
                        {
                            canskip = false;
                            UIManager.instance.dialogBox.SetActive(true);
                            UIManager.instance.charSprite.sprite = target.spr[chatIndex];
                            texttype = target.dialogs[chatIndex];
                            charindex = 0;
                            currenttype = "";
                            textchar = texttype.ToCharArray();
                            StartCoroutine(starttype());
                            chatIndex++;
                        }
                    }
                }else{
                    nextTypeInteractive();
                }
            }
        }else{
            idh.OnInteracted.Invoke();
        }
    }

    [HideInInspector]public npcDialogInteractive npcInter;
    void nextTypeInteractive()
    {
        UIManager.instance._nextChatButton.SetActive(true);
        if(!typeoption)
        {
            if(chatIndex >= npcInter.dialogs.Count)
            {
                UIManager.instance.dialogBox.SetActive(false);
                npcInter.OnEndConversation.Invoke();
                chatIndex = 0;
            }else if(chatIndex < npcInter.dialogs.Count)
            {
                if(canskip == true)
                {
                    canskip = false;
                    UIManager.instance.dialogBox.SetActive(true);
                    UIManager.instance.charSprite.sprite = npcInter.spr[chatIndex];
                    texttype = npcInter.dialogs[chatIndex];
                    charindex = 0;
                    currenttype = "";
                    textchar = texttype.ToCharArray();
                    StartCoroutine(starttype());
                    chatIndex++;
                }
            }
        }
    }

    public bool isendconveroption;
    public void nextTypeToOption()
    {
        UIManager.instance._nextChatButton.SetActive(true);
        if(isendconveroption)
        {
            UIManager.instance.dialogBox.SetActive(false);
            isendconveroption = false;
        }
        else
        {
            UIManager.instance.dialogBox.SetActive(true);
            UIManager.instance.charSprite.sprite = CDialogManager.instance.currentSpr;
            texttype = singleConversation;
            charindex = 0;
            currenttype = "";
            textchar = texttype.ToCharArray();
            StartCoroutine(starttype());
        }
    }

    IEnumerator starttype()
    {
        while(currenttype != texttype)
        {
            if(ratepro < Time.time)
            {
                ratepro = Time.time + rate;
                currenttype += textchar[charindex].ToString();
                dialogText.text = currenttype;
                if(charindex < texttype.Length)
                {
                    charindex++;
                }
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        canskip = true;
    }
}
