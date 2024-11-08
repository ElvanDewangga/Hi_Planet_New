using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class BossDungeonArea : MonoBehaviour
{
    public UnityEvent OnBeginBoss;
    [Space]
    public Vector2 clamp_min;
    public Vector2 clamp_max;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            //StartCoroutine(BeginBattle());
            StartCoroutine(BeginWarning());
            BossDungeonManager.instance.clampcam(clamp_min, clamp_max);
        }
    }

    IEnumerator BeginBattle()
    {
        yield return new WaitForSeconds(1f);
        UIManager.instance.bossNamePanel.gameObject.SetActive(true);
        UIManager.instance.bossNamePanel.LeanAlpha(1, 0.3f);
        UIManager.instance._bossSprite.LeanMoveLocalX(-693f, 0.4f).setEase(LeanTweenType.easeOutBack);
        UIManager.instance._bossLabel.LeanMoveLocalX(0, 0.1f).setDelay(0.25f);
        UIManager.instance._bossLabel.LeanMoveLocalX(100, 1.7f).setDelay(0.35f);

        UIManager.instance.bossNamePanel.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = BossDungeonManager.instance.bossName;
        yield return new WaitForSeconds(2f);

        UIManager.instance._bossSprite.LeanMoveLocalX(-1850f, 0.3f).setDelay(0.1f);
        UIManager.instance._bossLabel.LeanMoveLocalX(2147f, 0.3f);
        UIManager.instance._bossLabel.LeanScaleY(0, 0.2f);
        UIManager.instance.bossNamePanel.LeanAlpha(0, 0.3f).setDelay(0.3f);

        yield return new WaitForSeconds(0.6f);
        UIManager.instance.bossNamePanel.gameObject.SetActive(false);
        OnBeginBoss.Invoke();
        yield return null;
    }

    IEnumerator BeginWarning()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.instance._bossWarningPanel.transform.GetChild(0).transform.GetChild(0).LeanScaleY(0, 0.1f).setDelay(2.8f);
        UIManager.instance._bossWarningPanel.gameObject.SetActive(true);
        UIManager.instance._bossWarningPanel.LeanAlpha(1, 0.5f).setLoopPingPong();
        yield return new WaitForSeconds(3);
        UIManager.instance._bossWarningPanel.gameObject.SetActive(false);
        OnBeginBoss.Invoke();
        yield return null;
    }
}
