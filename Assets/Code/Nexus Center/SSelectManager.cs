using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSelectManager : MonoBehaviour
{
    public GameObject[] _Stage;
    [HideInInspector]public int stageindex;
    public string stageSceneName;

    public void SelectStage()
    {
        for(int i = 0;i < 5;i++)
        {
            _Stage[i].SetActive(false);
        }
        _Stage[stageindex].SetActive(true);
    }

    public IEnumerator GoToStage()
    {
        UIManager.instance._transitionPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(stageSceneName);
        yield return null;
    }
}
