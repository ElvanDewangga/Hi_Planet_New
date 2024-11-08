using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialDialogue : MonoBehaviour
{
    #region instance
    public static TutorialDialogue instance;
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

    public List<logProp> T001_log;
    public List<logProp> T002_log;
    public List<logProp> T003_log;
    public List<logProp> T004_log;
    public List<logProp> T005_log;
    public List<logProp> T006_log;
    public List<logProp> T007_log;
    public List<logProp> T008_log;
    public List<logProp> T009_log;
}
