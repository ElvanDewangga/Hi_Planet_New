using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class npcDialogInteractive : MonoBehaviour
{
    public List<Sprite> spr;
    [TextArea]
    public List<string> dialogs;
    public UnityEvent OnEndConversation;

    public void StartDialogInteractive(npcDialogInteractive targetinter)
    {
        StartCoroutine(OnTriggerConver(targetinter));
    }
    IEnumerator OnTriggerConver(npcDialogInteractive targetinter)
    {
        yield return new WaitForSeconds(1);
        StartDialog.instance.npcInter = targetinter;
        StartDialog.instance.nextType();
        yield return null;
    }
}
