using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class BossDungeonManager : MonoBehaviour
{
    #region instance
    public static BossDungeonManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public int itemIndex;
    public GameObject[] _getItems;
    public GameObject _storyItem;
    public List<GameObject> availableItems;
    public Vector2 minMaxCubeGet; GameObject ins;
    int minmaxCube;
    public UnityEvent OnBossDefeated;
    public UnityEvent OnBossDefeatedAfterReward;

    [Space]
    public bool isAlreadyCleared;
    public bool isFirstTime;

    [Header("BossProp")]
    public string bossName;
    public MonoBehaviour _BossScript;

    public void StartBoss()
    {
        _BossScript.Invoke("activeboss", 0);
    }

    void Start()
    {
        if(PlayerPrefs.HasKey("cleared"))
        {
            isAlreadyCleared = true;
        }

        if(PlayerPrefs.HasKey("first"))
        {
            isFirstTime = false;
        }
    }

    public void BossDefeat()
    {   
        OnBossDefeated.Invoke();
    }

    void freecam()
    {
        Camera.main.GetComponent<CameraMove>().fromclamp = Vector2.zero;
        Camera.main.GetComponent<CameraMove>().toclamp = Vector2.zero;
    }

    public void clampcam(Vector2 min, Vector2 max)
    {
        Camera.main.GetComponent<CameraMove>().fromclamp = min;
        Camera.main.GetComponent<CameraMove>().toclamp = max;
    }

    public void TrigerReward()
    {
        StartCoroutine(randomizeReward());
        StartCoroutine(freecamcor());
    }

    IEnumerator freecamcor()
    {
        yield return new WaitForSeconds(1);
        freecam();
        yield return null;
    }
    IEnumerator randomizeReward()
    {
        yield return new WaitForSeconds(1);
        UIManager.instance._victoryPanel.SetActive(true);
        UIManager.instance._victoryPanel.GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        UIManager.instance._victoryPanel.transform.GetChild(1).LeanScale(new Vector3(3.8f, 3.8f, 3.8f), 1f);
        UIManager.instance._victoryPanel.transform.GetChild(0).LeanScaleY(1, 0.6f);
        UIManager.instance._victoryPanel.GetComponent<CanvasGroup>().LeanAlpha(0, 0.6f).setEase(LeanTweenType.easeOutCubic).setDelay(1.5f);
        
        yield return new WaitForSeconds(2);
        UIManager.instance._victoryPanel.SetActive(false);
        UIManager.instance._rewardPanel.LeanScaleY(1, 0.3f).setEase(LeanTweenType.easeOutBack);
        UIManager.instance._rewardPanel.GetComponent<CanvasGroup>().LeanAlpha(0, 0.3f).setDelay(3);
        UIManager.instance._rewardPanel.transform.gameObject.SetActive(true);
        
        if(isFirstTime) //when in quest story
        {
            ins = Instantiate(_storyItem, UIManager.instance._rewardPanel.position, Quaternion.identity);
            ins.transform.parent = UIManager.instance._rewardPanel.GetChild(0).transform;
            isFirstTime = false;
            PlayerPrefs.SetString("first", "null");
            ins.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "X1";
            availableItems.Add(ins);
        }else{
            //when quest story is done
            if(!isAlreadyCleared)
            {
                for(int i = 0;i < 3;i++)
                {
                    itemIndex = Random.Range(0, _getItems.Length);
                    ins = Instantiate(_getItems[itemIndex], UIManager.instance._rewardPanel.position, Quaternion.identity);
                    ins.transform.parent = UIManager.instance._rewardPanel.GetChild(0).transform;
                    availableItems.Add(ins);
                }
                isAlreadyCleared = true;
                PlayerPrefs.SetString("cleared", "null");
            }else{
                ins = Instantiate(_getItems[0], UIManager.instance._rewardPanel.position, Quaternion.identity);
                ins.transform.parent = UIManager.instance._rewardPanel.GetChild(0).transform;
                availableItems.Add(ins);
            }
        }
        StartCoroutine(ActiveItemPop());
        yield return new WaitForSeconds(4f);
        UIManager.instance._rewardPanel.transform.gameObject.SetActive(false);
        OnBossDefeatedAfterReward.Invoke();
        availableItems.Clear();
        yield return null;
    }

    float spawnrate = 0;
    int childindex;
    IEnumerator ActiveItemPop()
    {
        yield return new WaitForSeconds(0.4f);
        UIManager.instance._rewardPanel.transform.GetChild(2).LeanMoveLocal(Vector3.zero, 0.2f).setEase(LeanTweenType.easeOutExpo);
        yield return new WaitForSeconds(0.6f);
        int itemcountget = UIManager.instance._rewardPanel.GetChild(0).childCount;
        while(childindex < itemcountget)
        {
            if(spawnrate < Time.time)
            {
                spawnrate = Time.time + 0.2f;
                UIManager.instance._rewardPanel.GetChild(0).GetChild(childindex).GetComponent<CanvasGroup>().LeanAlpha(1, 0.15f);
                if(ins.transform.name == "Cubes(Clone)")
                {
                    minmaxCube = Random.Range((int)minMaxCubeGet.x, (int)minMaxCubeGet.y);
                    UIManager.instance._rewardPanel.GetChild(0).GetChild(childindex).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "X" + minmaxCube.ToString();
                }else{
                    UIManager.instance._rewardPanel.GetChild(0).GetChild(childindex).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "X1";
                }
                UIManager.instance._rewardPanel.GetChild(0).GetChild(childindex).transform.localScale = Vector3.one;
                childindex++;
            }
            yield return null;
        }
        yield return new WaitUntil(() => childindex >= itemcountget);
        childindex = 0;
        yield return null;
    }
}
