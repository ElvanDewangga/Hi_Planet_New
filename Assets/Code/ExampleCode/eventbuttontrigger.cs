using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class eventbuttontrigger : MonoBehaviour
{
    public string[] infos;
    public TextMeshProUGUI textinfo;
    public int index;

    public void getInfo()
    {
        //textinfo.text = infos[index];
        index++;
    }
}
