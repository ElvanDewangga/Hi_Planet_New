using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dungeonsition : MonoBehaviour
{
    [Serializable]
    public class GoMethod
    {
        public bool isToScene;
        public int sceneIndex;
        [Space(2)]
        public bool isToPosition;
        public Vector3 pos;
    }

    public GoMethod goMethod;

    public IEnumerator GoToArea(GameObject player)
    {
        UIManager.instance._transitionPanel.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        if(goMethod.isToScene)
        {
            SceneManager.LoadScene(goMethod.sceneIndex);
        }else if(goMethod.isToPosition)
        {
            player.transform.position = new Vector3(goMethod.pos.x, goMethod.pos.y, goMethod.pos.z);
        }
        yield return new WaitForSeconds(0.82f);
        UIManager.instance._transitionPanel.SetActive(false);
        yield return null;
    }
}
