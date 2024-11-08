using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
public class DungeonManager : MonoBehaviour {
    public static DungeonManager instance;

    void Awake () {
        instance = this;
    }

    [SerializeField] private Image dungeonOptionPanel;
    [SerializeField] private Image dungeonCreatePlayPanel;
    [SerializeField] private Image dungeonOptionCloseButton;
    [SerializeField] private Image dungeonMultiplayerDisplay;
    [SerializeField] private Button testButton;
    private Scene currentDungeonScene;
    #region Dungeon Scene Management
    void Start () {
        testButton.onClick.AddListener (StartDungeon);
    }
    
    public void StartDungeon()
    {
        HideDungeonMultiplayerDisplay();
        StartCoroutine(LoadDungeonScene());
    }

    private IEnumerator LoadDungeonScene()
    {
        LoadingManager.instance.ShowLoading("Loading_1");
        StartDungeonSub("Dungeon_1_1");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("DungeonPCG", LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncLoad.isDone);
        TeleportManager.instance.TeleportPlayerAtPoint (AccountManager.instance.player.gameObject, currentDungeonSub.characterSpawns[0]);
        yield return new WaitForSeconds(2);

        currentDungeonScene = SceneManager.GetSceneByName("DungeonPCG");
        
        LoadingManager.instance.HideLoading("Loading_1");
    }

    public void FinishDungeon()
    {
        LoadingManager.instance.ShowLoading("Loading_1");

        if (currentDungeonScene.isLoaded)
        {
            StartCoroutine(UnloadDungeonScene());
        }
    }

    private IEnumerator UnloadDungeonScene()
    {
        // Spawn karakter kembali (belum selesai) Dungeon_Settings_Scene.Instance.FirstCharacterSpawn.SpawnCharacter();
        TeleportManager.instance.TeleportPlayer (AccountManager.instance.player.gameObject);
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentDungeonScene);
        yield return new WaitUntil(() => asyncUnload.isDone);

        AccountManager.instance.player.Restore();
        yield return new WaitForSeconds(2);
        
        LoadingManager.instance.HideLoading("Loading_1");
    }

    #endregion

    #region UI Control

    private void ShowDungeonOption()
    {
        dungeonOptionPanel.gameObject.SetActive(true);
        dungeonOptionCloseButton.gameObject.SetActive(true);
    }

    private void HideDungeonOption()
    {
        dungeonOptionPanel.gameObject.SetActive(false);
        dungeonOptionCloseButton.gameObject.SetActive(false);
    }

    private void ShowDungeonCreatePlay()
    {
        dungeonCreatePlayPanel.gameObject.SetActive(true);
        HideDungeonOption();
    }

    private void HideDungeonCreatePlay()
    {
        dungeonCreatePlayPanel.gameObject.SetActive(false);
    }

    private void ShowDungeonMultiplayerDisplay()
    {
        HideDungeonCreatePlay();
        dungeonMultiplayerDisplay.gameObject.SetActive(true);
    }

    private void HideDungeonMultiplayerDisplay()
    {
        dungeonMultiplayerDisplay.gameObject.SetActive(false);
    }

    #endregion

    
   #region Dungeon Sub

    public static Dungeon_Settings_Scene Instance;

    [SerializeField]
    private Dungeon_Sub[] dungeonSubs;
    private Dungeon_Sub currentDungeonSub;

    // Retrieves a Dungeon_Sub by name, or null if not found
    private Dungeon_Sub GetDungeonSubByName(string dungeonName)
    {
        return dungeonSubs.FirstOrDefault(sub => sub.DungeonSubName == dungeonName);
    }

    // Starts the specified Dungeon_Sub by name
    private void StartDungeonSub(string dungeonName)
    {
        currentDungeonSub = GetDungeonSubByName(dungeonName);

        if (currentDungeonSub != null)
        {
            currentDungeonSub.StartDungeon();
        }
        else
        {
            Debug.LogWarning($"Dungeon Sub with name '{dungeonName}' not found.");
        }
    }

    #endregion
}
