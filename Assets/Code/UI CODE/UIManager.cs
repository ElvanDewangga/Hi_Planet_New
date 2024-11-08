using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    #region Veriables
    DungeonSection ds;
    [HideInInspector]public int openindex;
    #region instance
    public static UIManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    
    [Header("ChatBox")]
    public GameObject dialogBox;
    public GameObject optionPanel;
    public Image charSprite;

    [Header("Panels")]
    public GameObject _cosmicenergystore;
    public GameObject _cosmicstore;
    public GameObject _recycleCenter;
    public GameObject _cosmicForumPanel;
    public GameObject _matchPanel;
    public GameObject _bookPanel;
    [Space]
    public GameObject _craftPanel;
    public GameObject _refinePanel;
    public GameObject _enchancePanel;
    [Space]
    public Transform _rewardPanel;
    public GameObject _victoryPanel;
    public GameObject _stageSelectPanel;
    public GameObject _areaSelectPanel;
    public GameObject _stageGroup;

    [Header("Exchange UI")]
    public Image _itemdDisplay;
    public Slider _exchnageAdjuster;
    [Space]
    public TextMeshProUGUI _maxExchangeText;
    public TextMeshProUGUI _itemExchangeText;
    public TextMeshProUGUI _getCurrencyText;

    [Header("Button")]
    public GameObject _nextChatButton;
    public CanvasGroup _buttonAndLabelAreaSelect;

    [Header("text")]
    public TextMeshProUGUI _areaNameText;

    [Header("LosePanel")]
    public GameObject _gameOverPanel;
    public TextMeshProUGUI _respawnTimeText; 

    [Header("Component")]
    public GameObject _transitionPanel;
    public GameObject _dungeonGoConfirmationPanel;
    public GameObject _BackToNexusConfirmationPanel;
    public GameObject _portalPanel;
    [Space]
    public CanvasGroup bossNamePanel;
    public CanvasGroup _bossWarningPanel;
    public GameObject _bossSprite;
    public GameObject _bossLabel;

    [Header("UITribute")]
    public GameObject _portalTribute;

    [Header("Temporaryui")]
    public GameObject buttonCutscene;

    StartDialog sd;
    SSelectManager ssm;
    #endregion
    
    void Start()
    {
        ssm = GameObject.FindObjectOfType<SSelectManager>();
        sd = GameObject.FindObjectOfType<StartDialog>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(ds == null)
        {
            ds = GameObject.FindAnyObjectByType<DungeonSection>();
        }
    }

    public void OpenQQMenu()
    {
        _stageSelectPanel.transform.GetChild(1).transform.localScale = Vector3.zero;
        _areaSelectPanel.SetActive(false);
        _stageSelectPanel.SetActive(true);
        _stageSelectPanel.transform.GetChild(1).LeanScale(Vector3.one, 0.5f).setEase(LeanTweenType.easeOutExpo);
    }

    #region StoreUI
    public void OpenCosmicEnergyStore()
    {
        dialogBox.SetActive(false);
        if(openindex == 0)
        {
            _cosmicenergystore.SetActive(true);
            openindex++;
        }else{
            _cosmicenergystore.SetActive(false);
            openindex = 0;
            
            if(TutorialManager.instance.tutorialId == 3)
            {
                StartDialog.instance.typeoption = true;
                dialogBox.SetActive(true);
                Cut_003.instance.StartConverOption();
            }
        }
    }

    public void OpenCosmicStore()
    {
        if(openindex == 0)
        {
            _cosmicstore.SetActive(true);
            openindex++;
        }else{
            _cosmicstore.SetActive(false);
            openindex = 0;

            if(TutorialManager.instance.tutorialId == 4)
            {
                StartDialog.instance.typeoption = true;
                dialogBox.SetActive(true);
                Cut_004.instance.StartConverOption();
            }
        }
    }

    public void OpenRecycleCenter()
    {
        _exchnageAdjuster.maxValue = ExchangeCenter.instance.available;
        _maxExchangeText.text = ExchangeCenter.instance.available.ToString();
        if(openindex == 0)
        {
            _recycleCenter.SetActive(true);
            openindex++;
        }else{
            _recycleCenter.SetActive(false);
            openindex = 0;

            if(TutorialManager.instance.tutorialId == 5)
            {
                StartDialog.instance.typeoption = true;
                dialogBox.SetActive(true);
                Cut_005.instance.StartConverOption();
            }
        }
    }

    public void OpenCosmicForum()
    {
        if(openindex == 0)
        {
            _cosmicForumPanel.SetActive(true);
            openindex++;
        }else{
            _cosmicForumPanel.SetActive(false);
            openindex = 0;

            if(TutorialManager.instance.tutorialId == 6)
            {
                StartDialog.instance.typeoption = true;
                dialogBox.SetActive(true);
                Cut_006.instance.StartConverOption();
            }
        }
    }

    public void OpenBookPanel()
    {
        if(openindex == 0)
        {
            _bookPanel.SetActive(true);
            openindex++;
        }else{
            _bookPanel.SetActive(false);
            openindex = 0;
        }
    }

    public void OpenArenaPanel()
    {
        if(openindex == 0)
        {
            _matchPanel.SetActive(true);
            openindex++;
        }else{
            _matchPanel.SetActive(false);
            openindex = 0;
        }
    }

    #endregion
    
    #region GameOverUI
    [HideInInspector]public GameObject player;
    public IEnumerator ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(respawnTime());
        yield return new WaitForSeconds(0.3f);
        _gameOverPanel.SetActive(true);
        _gameOverPanel.transform.GetChild(0).GetComponent<CanvasGroup>().LeanAlpha(1, 0.2f);
        _gameOverPanel.transform.GetChild(1).LeanMoveLocal(Vector3.one, 0.2f).setEase(LeanTweenType.easeOutQuart).setDelay(0.2f);
        _gameOverPanel.transform.GetChild(2).LeanMoveLocal(Vector3.one, 0.2f).setEase(LeanTweenType.easeOutQuart).setDelay(0.2f);
        yield return null;
    }
    IEnumerator respawnTime()
    {
        int tempcounter = 5;
        float respawnpro = 0;

        yield return new WaitForSeconds(1.5f);
        while(tempcounter >= 0)
        {
            if(respawnpro < Time.time)
            {
                respawnpro = Time.time + 1;
                tempcounter--;
                _respawnTimeText.text = tempcounter.ToString();
                if(tempcounter < 0)
                {
                    GameOverButton_Exit();
                }
            }
            yield return null;
        }
        yield return null;
    }
    public void GameOverButton_Exit()
    {
        _gameOverPanel.SetActive(false);
        PlayerPrefs.SetInt("FromScene", 0);
        StartCoroutine(ds.BackToDungeonArea());
    }
    public void GameOverButton_Retry()
    {
        if(ds != null)
        {
            _gameOverPanel.SetActive(false);
            StartCoroutine(ds.BackToDungeonArea());
        }
    }
    #endregion

    #region StageSelectUI
    public void BackToStage()
    {
        _stageSelectPanel.SetActive(true);
        _areaSelectPanel.SetActive(false);
    }

    public void BackToNexus()
    {
        _stageSelectPanel.SetActive(false);
        _areaSelectPanel.SetActive(false);
    }

    public void GoToStage()
    {
        StartCoroutine(ssm.GoToStage());
    }
    #endregion

    #region BackToNexusUI
    public void BackToNexusConfirmation_Popup()
    {
        _BackToNexusConfirmationPanel.SetActive(true);
    }
    public void BTNConfirm()
    {
        QQ q = GameObject.FindObjectOfType<QQ>();
        StartCoroutine(q.GoToNexusCenter());
        _BackToNexusConfirmationPanel.SetActive(false);
    }
    public void BTNLater()
    {
        _BackToNexusConfirmationPanel.SetActive(false);
    }
    #endregion

    #region inworldUI
    public void OpenCraftPanel()
    {
        if(openindex == 0)
        {
            _craftPanel.SetActive(true);
            openindex++;
        }else{
            _craftPanel.SetActive(false);
            openindex = 0;
        }
    }

    public void OpenRefinePanel()
    {
        if(openindex == 0)
        {
            _refinePanel.SetActive(true);
            openindex++;
        }else{
            _refinePanel.SetActive(false);
            openindex = 0;
        }
    }

    public void OpenEnchancePanel()
    {
        if(openindex == 0)
        {
            _enchancePanel.SetActive(true);
            openindex++;
        }else{
            _enchancePanel.SetActive(false);
            openindex = 0;
        }
    }

    public void OpenPortalPanel()
    {
        if(openindex == 0)
        {
            _portalPanel.SetActive(true);
            openindex = 1;
        }else{
           _portalPanel.SetActive(false);
            PortalManager.instance.ClosePortal();
            openindex = 0;
        }
        
    }
    #endregion

    void OnDestroy()
    {
        if(!Application.isPlaying)
        {
            if(PlayerPrefs.HasKey("FromScene"))
            {
                PlayerPrefs.DeleteKey("FromScene");
            }
        }
    }
}
