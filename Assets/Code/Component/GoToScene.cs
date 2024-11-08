using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public string SceneName;

    public void OnGoScene()
    {
        StartCoroutine(GoToSceneName());
    }
    IEnumerator GoToSceneName()
    {
        UIManager.instance._transitionPanel.SetActive(true);
        PlayerPrefs.SetInt("FromScene", 0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneName);
        yield return null;
    }
}
