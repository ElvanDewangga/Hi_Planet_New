using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dungeon : MonoBehaviour
{
    public bool isInWorld;
    public string SceneName;

    void Start()
    {
        if(PlayerPrefs.HasKey("StageName"))
        {
            SceneName = PlayerPrefs.GetString("StageName");
        }
    }

    public void GoToDungeon()
    {
        StartCoroutine(GoToSceneName());
    }
    IEnumerator GoToSceneName()
    {
        UIManager.instance._transitionPanel.SetActive(true);
        PlayerPrefs.SetInt("FromScene", 0);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneName);
        if(isInWorld){PlayerPrefs.SetString("StageName", SceneManager.GetActiveScene().name);}
        yield return null;
    }
}
