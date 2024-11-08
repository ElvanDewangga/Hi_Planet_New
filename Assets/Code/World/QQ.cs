using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QQ : MonoBehaviour
{
    public void OpenQQMenu()
    {
        UIManager.instance.OpenQQMenu();
    }

    public IEnumerator GoToNexusCenter()
    {
        UIManager.instance._transitionPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("NexusCenterExample");
        yield return null;
    }
}
