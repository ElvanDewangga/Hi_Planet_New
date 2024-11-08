using UnityEngine;
using FishNet.Managing;
using FishNet.Transporting;
using FishNet.Object;

public class NetworkWorld : NetworkBehaviour
{
    #region Variables
    [Header("Player Spawn Settings")]

    private NetworkManager _networkManager;
    private LocalConnectionState _clientState = LocalConnectionState.Stopped;
    private LocalConnectionState _serverState = LocalConnectionState.Stopped;
    private bool initialLoading = true;
    #endregion

    #region NetworkPlayerSpawn
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        // Any additional server start logic can be added here
    }
    
    #endregion

    #region NetworkControl
    // ClientConnectionHandler
    public void NetworkStart ()
    {
        InitializeNetworkManager();
        HandleNetworkConnection();
        LoadingManager.instance.HideLoading("Loading_2");

    }

    private void InitializeNetworkManager()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
        if (_networkManager == null)
        {
            Debug.LogError("NetworkManager not found in the scene.");
        }
    }

    private void HandleNetworkConnection()
    {
        if (_networkManager == null) return;

        if (_serverState == LocalConnectionState.Stopped)
        {
            _networkManager.ServerManager.StartConnection();
        }
        else
        {
            _networkManager.ServerManager.StopConnection(true);
        }

        if (_clientState == LocalConnectionState.Stopped)
        {
            _networkManager.ClientManager.StartConnection();
        }
        else
        {
            _networkManager.ClientManager.StopConnection();
        }
    }
    #endregion
}
