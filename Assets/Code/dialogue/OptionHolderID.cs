using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionHolderID : MonoBehaviour
{
    public int id;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(click);
    }

    void click()
    {
        CDialogManager.instance.optionID = id;
        StartDialog.instance.NextChatOption();
    }
}
