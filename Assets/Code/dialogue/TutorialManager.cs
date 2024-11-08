using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public float walkspeedDefault;
    #region instance
    public static TutorialManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    public int tutorialId;
}
