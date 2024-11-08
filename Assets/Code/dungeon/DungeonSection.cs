using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DungeonSection : MonoBehaviour
{
    public int targetDefeated;
    public int countDefeated;
    public UnityEvent OnAllEnemyDefeated;
    
    [Header("Respawn")]
    public string backToSceneName;
    [Space]
    public Vector3 toPosition;
    [SerializeField]
    Ev_DungeonSection _Ev_DungeonSection;
    public IEnumerator BackToDungeonArea()
    {
        UIManager.instance._transitionPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        _Ev_DungeonSection.On_Back_First_Spawn ();
        yield return null;
        /*
        SceneManager.LoadScene(backToSceneName);
        PlayerPrefs.SetInt("FromScene", 0);
        yield return null;
        */
    }

    public void CheckTargetDefeatedReach()
    {
        if(countDefeated >= targetDefeated)
        {
            OnAllEnemyDefeated.Invoke();
        }
    }
}
