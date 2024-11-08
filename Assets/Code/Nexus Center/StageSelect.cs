using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    SSelectManager ssm;
    public int stageindex;
    public string stageName;
    public string stageSceneName;
    void Start()
    {
        ssm = GameObject.FindObjectOfType<SSelectManager>();
        GetComponent<Button>().onClick.AddListener(SelectStage);
    }
    void SelectStage()
    {
        UIManager.instance._buttonAndLabelAreaSelect.LeanAlpha(0, 0);
        UIManager.instance._stageGroup.transform.localScale = Vector3.zero;
        UIManager.instance._areaNameText.text = stageName;
        UIManager.instance._stageSelectPanel.SetActive(false);
        UIManager.instance._areaSelectPanel.SetActive(true);
        UIManager.instance._stageGroup.LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeOutCirc);
        UIManager.instance._buttonAndLabelAreaSelect.LeanAlpha(1, 0.3f).setDelay(0.5f);
        ssm.stageindex = stageindex;
        ssm.stageSceneName = stageSceneName;
        ssm.SelectStage();
    }
}
