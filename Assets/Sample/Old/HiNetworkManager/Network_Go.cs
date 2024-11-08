using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FishNet.Managing;
using FishNet.Transporting;
public class Network_Go : MonoBehaviour {
    public static Network_Go Ins;

    void Awake () {
        Ins = this;
    }

    public Network_Player_Spawn _Network_Player_Spawn;

   [SerializeField]
   Image Dungeon_Option;
    [SerializeField]
    Image Dungeon_Create_Play;
    [SerializeField]
    Image Dungeon_Option_Close_Button;
    [SerializeField]
    Image Dungeon_Multiplayer_Display;

    // Button (Test)
    public void On_Dungeon_Option () {
        Dungeon_Option.gameObject.SetActive (true);
        Dungeon_Option_Close_Button.gameObject.SetActive (true);
    }

    void Off_Dungeon_Option () {
         Dungeon_Option.gameObject.SetActive (false);
         Dungeon_Option_Close_Button.gameObject.SetActive (false);
    }

    public void On_Dungeon_Create_Play () {
        Dungeon_Create_Play.gameObject.SetActive (true);
        Off_Dungeon_Option ();
    }

    void Off_Dungeon_Create_Play () {
        Dungeon_Create_Play.gameObject.SetActive (false);
    }


    public void On_Dungeon_Multiplayer_Display () {
        Off_Dungeon_Create_Play ();
        Dungeon_Multiplayer_Display.gameObject.SetActive (true);
    }

    void Off_Dungeon_Multiplayer_Display () {
        Dungeon_Multiplayer_Display.gameObject.SetActive (false);
    }

    // Button (Test)
    private Scene Cur_Dungeon_Scene;
    public Spawn_Clone_Object Main_Spawn_Clone_Object;
    public void On_Start_Dungeon () {
        Off_Dungeon_Multiplayer_Display ();
        StartCoroutine (N_On_Start_Dungeon ());
    }

    IEnumerator N_On_Start_Dungeon () {
        Loading.Ins.On_Loading ("Loading_1");
        AsyncOperation As = SceneManager.LoadSceneAsync("DungeonPCG",LoadSceneMode.Additive);
        yield return new WaitUntil (()=> As.isDone);
        yield return new WaitForSeconds (2);
        Cur_Dungeon_Scene = SceneManager.GetSceneByName("DungeonPCG");
       // SceneManager.SetActiveScene(Cur_Dungeon_Scene);
        Dungeon_Settings_Scene.Ins.On_Start_Dungeon_Sub ("Dungeon_1_1");
        Loading.Ins.Off_Loading ("Loading_1");
    }

    public void On_Finish_Dungeon () {
        Loading.Ins.On_Loading ("Loading_1");
        if (Cur_Dungeon_Scene.isLoaded) {
            StartCoroutine (N_On_Finish_Dungeon ());
           
        }
    }

    IEnumerator N_On_Finish_Dungeon () {
        Dungeon_Settings_Scene.Ins.First_Char_Spawn.On_Spawn ();
        AsyncOperation As = SceneManager.UnloadSceneAsync(Cur_Dungeon_Scene);
        yield return new WaitUntil (()=> As.isDone);
        Main_Spawn_Clone_Object.On_Set_Spawn_Clone_Object ();
        Char_Data.Ins.Your_Char_Utama_Script._Char_Status.On_Restore ();
       // SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        yield return new WaitForSeconds (2);
        Loading.Ins.Off_Loading ("Loading_1");
    }

    #region Network_Control

    private NetworkManager _networkManager;
    private LocalConnectionState _clientState = LocalConnectionState.Stopped;
        /// <summary>
        /// Current state of server socket.
        /// </summary>
    private LocalConnectionState _serverState = LocalConnectionState.Stopped;

    void Start()
    {
        Loading.Ins.On_Loading ("Loading_2");
        // Mencari NetworkManager yang ada di scene
        _networkManager = FindObjectOfType<NetworkManager>();
        On_Network_Connect ();
    }

    void On_Network_Connect () {
        if (_networkManager == null)
                return;
        if (_serverState != LocalConnectionState.Stopped)
                _networkManager.ServerManager.StopConnection(true);
            else
                _networkManager.ServerManager.StartConnection();

            if (_clientState != LocalConnectionState.Stopped)
                _networkManager.ClientManager.StopConnection();
            else
                _networkManager.ClientManager.StartConnection();

    }

    #endregion

    #region ClientConnectionHandler
    bool b_Loading_Awal = true;
    public void Client_Connected () {
        if (b_Loading_Awal) {
            b_Loading_Awal = false;
            Loading.Ins.Off_Loading ("Loading_2");
        }
    }
    #endregion
    
}
