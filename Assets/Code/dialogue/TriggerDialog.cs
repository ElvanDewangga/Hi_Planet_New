using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TriggerDialogue : MonoBehaviour
{
    #region instance
    public static TriggerDialogue instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    [Serializable]
    public class logProp
    {
        public Sprite spr;
        [TextArea]
        public string dialogs;
    }

    public List<logProp> Trigger_log;
}
